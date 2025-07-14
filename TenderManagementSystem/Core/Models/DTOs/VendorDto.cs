using TenderManagementSystem.Application.DTOs;

namespace TenderManagementSystem.Core.Models.DTOs;

public class VendorDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int BidCount { get; set; }
    public ICollection<VendorBidsDto> Bids { get; set; }
}