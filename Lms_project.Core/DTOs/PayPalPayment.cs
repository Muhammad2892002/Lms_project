using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Lms_project.Core.DTOs
{
    public record PayPalPayment(string PayPalOrderId, string approvalLinks);
    public record PayPalOrderResponse(
  [property: JsonPropertyName("id")]
    string Id,

  [property: JsonPropertyName("links")]
    List<PayPalLink> Links,
  [property:JsonPropertyName("status")]
 string? Status
);

    public record PayPalLink(
        [property: JsonPropertyName("href")]
    string Href,

        [property: JsonPropertyName("rel")]
    string Rel
    );
    public record AccessTokenResponse([property: JsonPropertyName("access_token")] string AccessToken);

    public record PayResponseDto(int PaymentId, string OrderId, decimal Amount, string Currency, string ApprovalUrl);

}
