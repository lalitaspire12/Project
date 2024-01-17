
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics; 
using System.ComponentModel.DataAnnotations;

namespace VEHCILE.Models
{

public class LoginAdmin
{
    [Required(ErrorMessage = "Username is required")]
    [MaxLength(40)]
    public string? EmployeeID { get; set; }

    // [RegularExpression("^[a-zA-z]+$",ErrorMessage="The Customer Name should contain only alphabets")]
    [RegularExpression(
        "^.*(?=.{8,})(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=]).*$",
        ErrorMessage = "Provide valid password"
    )]
    [Required(ErrorMessage = "Password is required is Required.")]
    public string? Password { get; set; }
}

    
}
