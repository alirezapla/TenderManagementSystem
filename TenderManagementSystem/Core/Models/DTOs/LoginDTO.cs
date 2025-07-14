using System.ComponentModel.DataAnnotations;

namespace TenderManagementSystem.Application.DTOs;

public class LoginDTO
{
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Username is required")]
    public string Username { get; set; }
}