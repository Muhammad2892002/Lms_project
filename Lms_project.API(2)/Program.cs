using FluentValidation;
using Lms_project.API_2_.Midlleware;
using Lms_project.Core.Repository;
using Lms_project.Core.Security;
using Lms_project.Core.Service;
using Lms_project.Core.Validators.CourseValidator;
using Lms_project.Core.Validators.EnrollmentValidator;
using Lms_project.Core.Validators.StudentValidator;
using Lms_project.Infra.Data;
using Lms_project.Infra.Repository;
using Lms_project.Infra.Security;
using Lms_project.Infra.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.SwaggerDoc("v1", new()
    {
        Title = "Lms Project",
        Version = "v1",
        Description = "Build a Learning Managment System using Asp.Net Core API "

    });
});
/*DI Here */



builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IStudentRepository,StudentRepository>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IEnrollmentService, EnrollmentService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IPasswordHash, BcryptPaswordHasher>();
builder.Services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
builder.Services.AddSingleton<ITokenService, JwtTokenService>();
builder.Services.AddScoped<IAuthService, AuthService>();





//--------------------------------------------------------------------------------------
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddValidatorsFromAssemblyContaining<CreateCourseDtoValidators>();
builder.Services.AddValidatorsFromAssemblyContaining<EditCourseDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateStudentDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<EditStudentDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateEnrollmentValidator>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
var jwt=builder.Configuration.GetSection("jwt").Get<JwtSetting>();
builder.Services.Configure<JwtSetting>(builder.Configuration.GetSection("jwt"));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
    options=>options.TokenValidationParameters=new TokenValidationParameters { 
      ValidateIssuer = true,
      ValidateAudience=true,
      ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwt.Issuer,
        ValidAudience = jwt.Audiance,
        IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key)),
        ClockSkew=TimeSpan.FromMinutes(1)


    }
    
    );
builder.Services.AddAuthorization();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "LMS API v1");
        options.RoutePrefix = string.Empty;

    });
    //app.UseSwagger();

    //app.UseSwaggerUI();
}
app.UseExceptionHandler();
app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
