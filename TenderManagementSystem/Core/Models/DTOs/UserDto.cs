namespace TenderManagementSystem.Application.DTOs;

public class UserDto
{
    public string Id { get; set; }
    public string Username { get; set; }
    public ICollection<string> Roles { get; set; }
}