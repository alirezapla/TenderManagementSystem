namespace TenderManagementSystem.Domain.Models.Entities;

public class Bid: BaseEntity
{
    public string TenderId { get; set; }
    public Tender Tender { get; set; }
    public string VendorId { get; set; }
    public Vendor Vendor { get; set; }
    public decimal Amount { get; set; }
    public string Comments { get; set; }
    public string StatusId { get; set; }
    public Status Status { get; set; }
    public DateTime SubmissionDate { get; set; }
}