namespace TenderManagementSystem.Application.DTOs;

public class TenderDto
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Deadline { get; set; }
    public CategoryDto Category { get; set; }
    public StatusDto Status { get; set; }
}