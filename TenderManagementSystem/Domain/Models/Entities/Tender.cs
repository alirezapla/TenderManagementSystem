namespace TenderManagementSystem.Domain.Models.Entities;

public class Tender: BaseEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Deadline { get; set; }
    public string CategoryId { get; set; }
    public Category Category { get; set; }
    public string StatusId { get; set; }
    public Status Status { get; set; }
    public IEnumerable<Bid> Bids { get; set; }

}