using TenderManagementSystem.Application.DTOs;
using TenderManagementSystem.Core.Models.DTOs;


public class VendorBidsDto
{
    public string Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime SubmissionDate { get; set; }
    public string TenderTitle { get; set; }
    public StatusDto Status { get; set; }

}