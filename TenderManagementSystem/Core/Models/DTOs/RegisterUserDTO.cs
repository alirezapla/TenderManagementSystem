using System.ComponentModel.DataAnnotations;

namespace TenderManagementSystem.Application.DTOs
{
    public class RegisterUserDTO
    {
        [Required] public string Username { get; set; }

        [Required] public string Password { get; set; }

        [Required] public string Role { get; set; }
    }
}