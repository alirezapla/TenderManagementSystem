namespace TenderManagementSystem.Application.DTOs;

public class UpdateTenderDTO
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Deadline { get; set; }
    public string CategoryId { get; set; }
    public string StatusId { get; set; }
}