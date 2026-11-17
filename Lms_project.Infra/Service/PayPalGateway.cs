using Lms_project.Core.DTOs;
using Lms_project.Core.Enums;
using Lms_project.Core.Exceptions;
using Lms_project.Core.Repository;
using Lms_project.Core.Service;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;

namespace Lms_project.Infra.Service
{
    public class PayPalGateway(HttpClient http,IConfiguration _configuration, IEnrollmentRepository _enrollmentRepo) : IPaymentGateway
    {
        string clientID = _configuration["PayPal:ClientId"];
        string secret = _configuration["PayPal:Secret"];
        public async Task<PaymentStatus> CaptureOrder(string orderId)
        {
            var token = await GetAccessToken();

            using var request = new HttpRequestMessage(HttpMethod.Post, $"/v2/checkout/orders/{orderId}/capture");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            request.Content = new StringContent("", Encoding.UTF8, "application/json");

            using var response = await http.SendAsync(request);
          


          
            if ((int)response.StatusCode >= 500)
                response.EnsureSuccessStatusCode();

            if (!response.IsSuccessStatusCode)
                return PaymentStatus.Failed;

            var result = await response.Content.ReadFromJsonAsync<PayPalOrderResponse>();

            return result?.Status == "COMPLETED"
                ? PaymentStatus.Completed
                : PaymentStatus.Failed;
        }

        public async Task<PayPalPayment> CreateOrder(decimal amountInUSD)
        {
         
            var token = await GetAccessToken();
           
            var returnUrl = _configuration["PayPal:ReturnUrl"];

            var orderRequest = new
            {
                intent = "CAPTURE",
                purchase_units = new[]
                {
        new
        {
            amount = new
            {
                currency_code = "USD",
                value = amountInUSD.ToString("0.00", CultureInfo.InvariantCulture)
            }
        }
    },
                application_context = new
                {
                    return_url = returnUrl,
                    cancel_url = returnUrl,
                    user_action = "PAY_NOW"
                }
            };
            using var request = new HttpRequestMessage(
                HttpMethod.Post,
                "/v2/checkout/orders"
                );
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            request.Content = JsonContent.Create(orderRequest);
            using var response = await http.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<PayPalOrderResponse>();

            var approvalLink = result?.Links?
                .FirstOrDefault(x => x.Rel == "approve")?.Href;

            if (string.IsNullOrEmpty(approvalLink))
                throw new InvalidOperationException("PayPal approval link was not returned.");



            return new PayPalPayment(result.Id, approvalLink);

         
        }
        private async Task<string> GetAccessToken() {
            string credintials = $"{clientID}:{secret}";
            string encodedCredinitals = Convert.ToBase64String(
                Encoding.UTF8.GetBytes(credintials)
                );
            var content = new FormUrlEncodedContent(
                new Dictionary<string, string> {
                    {"grant_type","client_credentials" }
                 
                }
                );
            using var httpRequest = new HttpRequestMessage(
                HttpMethod.Post,
                "/v1/oauth2/token"
                );
            httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Basic", encodedCredinitals);
            httpRequest.Content = content;
             using var response =await http.SendAsync(httpRequest);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<AccessTokenResponse>();
         
            return result!.AccessToken;
            
           
         
        
        }
    }
}
