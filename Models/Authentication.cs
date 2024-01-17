// using System;
// using Microsoft.IdentityModel.Tokens;
// using System.Collections.Generic;
// using System.Configuration;
// using System.IdentityModel.Tokens.Jwt;
// using System.Security.Claims;
// using System.Text;

// namespace VEHCILE.Models
// {
//     public class Authentication{
//         public static string GenerteJwtToken(string username,List<string> roles){
//             var claims=new List<Claim>
//             {
//                 new Claim(JwtRegisteredClaimNames.Sub,username),
//                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
//                 new Claim(ClaimTypes.NameIdentifier, username)
//             };
//             roles.foreach (role=>{
//                 claims.Add(new Claim(ClaimTypes.Role, role));
//             });

//             var key= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Convert.ToString(ConfigurationManager.AppSettings["config:JwtKey"])));
//             var creds=new SigningCredientals(key, SecurityAlgorithms.HmacSha256);
//             var expires=DateTime.Now.AddDays(Convert.ToDouble(Convert.ToString(ConfigurationManager.AppSettings["config: JwtExpireDays"])));

//             var token= new JwtSecurityToken(Convert.ToString(ConfigurationManager.AppSettings["config:JwtIssuer"]),
//             Convert.ToString(ConfigurationManager.AppSettings["config:JwtAudience"]),
//             claims,expires:expires,signingCredientals:creds);
            
//             return  new JwtSecurityTokenHandler().WriteToken(token);
//         }
//     }
// }