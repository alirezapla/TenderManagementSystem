using TenderManagementSystem.Domain.Models.Entities.RBAC;

namespace TenderManagementSystem.Domain.Models.Entities;

public class Vendor : BaseEntity
{
    public string Name { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
    public IEnumerable<Bid> Bids { get; set; }
}