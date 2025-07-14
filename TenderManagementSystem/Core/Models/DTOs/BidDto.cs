using TenderManagementSystem.Application.DTOs;
using TenderManagementSystem.Core.Models.DTOs;


public class BidDto
{
    public string Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime SubmissionDate { get; set; }
    public string TenderTitle { get; set; }
    public VendorDto Vendor { get; set; }
    public StatusDto Status { get; set; }

}