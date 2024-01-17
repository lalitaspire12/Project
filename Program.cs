// Project Title        : Vehicle Management System
// author               : Lalit Kumar Yadav
// created at           : Aspire System Office
// last Modified Date   : 06.03.2023
// reviewed Date        : 23.02.2023
// reviewed By          : Anitha Manogaran
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using VEHCILE.Data;
using VEHCILE.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VEHCILE.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();

builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddSession(options =>
{
    //options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddScoped<IData, inner>();

// builder.Services.AddAuthentication(options =>
// {
//     options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//     options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//     options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
// }).AddJwtBearer(o =>
// {
//     o.TokenValidationParameters = new TokenValidationParameters
//     {
//         ValidIssuer = builder.Configuration["Jwt:Issuer"],
//         ValidAudience = builder.Configuration["Jwt:Audience"],
//         IssuerSigningKey = new SymmetricSecurityKey
//             (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
//         ValidateIssuer = true,
//         ValidateAudience = true,
//         ValidateLifetime = false,
//         ValidateIssuerSigningKey = true
//     };
// });
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
            )
        };
    });

builder.Services.AddAuthorization();

// builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
// builder.Services.Configure<CookiePolicyOptions>(options =>
// {
//     options.CheckConsentNeeded = context = true;
//     options.MinimumSameSitePolicy = SameSiteMode.None;
// });
// builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
// builder.Services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_0);

// builder.Services.AddScoped(typeof(<IRead>), typeof(Repository<>));
// builder.Services.AddScoped<ICustomerAppService, CustomerAppService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
// app.MapGet("/security/getMessage", () => "Hello World!").RequireAuthorization();
// app.MapPost("/security/createToken",
// [AllowAnonymous] (LoginEmployee employee) =>
// {
//     if (employee.EmployeeID == "lalitkumar.ndit@gmail.com" && employee.Password == "Aspire@12")
//     {
//         var issuer = builder.Configuration["Jwt:Issuer"];
//         var audience = builder.Configuration["Jwt:Audience"];
//         var key = Encoding.ASCII.GetBytes
//         (builder.Configuration["Jwt:Key"]);
//         var tokenDescriptor = new SecurityTokenDescriptor
//         {
//             Subject = new ClaimsIdentity(new[]
//             {
//         new Claim("Id", Guid.NewGuid().ToString()),
//         new Claim(JwtRegisteredClaimNames.Sub, employee.EmployeeID),
//         new Claim(JwtRegisteredClaimNames.Email, employee.EmployeeID),
//         new Claim(JwtRegisteredClaimNames.Jti,
//         Guid.NewGuid().ToString())
//              }),
//             Expires = DateTime.UtcNow.AddMinutes(5),
//             Issuer = issuer,
//             Audience = audience,
//             SigningCredentials = new SigningCredentials
//             (new SymmetricSecurityKey(key),
//             SecurityAlgorithms.HmacSha512Signature)
//         };
//         var tokenHandler = new JwtSecurityTokenHandler();
//         var token = tokenHandler.CreateToken(tokenDescriptor);
//         var jwtToken = tokenHandler.WriteToken(token);
//         var stringToken = tokenHandler.WriteToken(token);
//         return Results.Ok(stringToken);
//     }
//     return Results.Unauthorized();
// });

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Log}/{id?}");

app.Run();

// we have to make the custom mode ="ON"

/// this was the file which i made all changes to