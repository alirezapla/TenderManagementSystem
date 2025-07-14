namespace TenderManagementSystem.Application.DTOs;

public class TenderDetailsDto : TenderDto
{
    public ICollection<BidDto> Bids { get; set; } = new List<BidDto>();
}