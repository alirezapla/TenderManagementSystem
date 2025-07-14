namespace TenderManagementSystem.Application.DTOs;

public class CreateBidDto
{
    public decimal Amount { get; set; }
    public string Comments { get; set; }
    public string VendorId { get; set; }
    public string TenderId { get; set; }
}