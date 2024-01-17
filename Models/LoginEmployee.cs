using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
public class LoginEmployee
{
    [Required(ErrorMessage = "Please enter your EmployeeID.")]
    [RegularExpression(@"^[a-zA-Z][a-zA-Z0-9]*$", ErrorMessage = "Username must start with an alphabet and can only contain letters and numbers.")]
    public string? EmployeeID { get; set; }

    [Required(ErrorMessage = "Please enter your password.")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$", ErrorMessage = "Password must be at least 8 characters and contain at least one uppercase letter, one lowercase letter, and one number.")]
    public string? Password { get; set; }
}