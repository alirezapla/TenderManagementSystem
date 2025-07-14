using AutoMapper;
using TenderManagementSystem.Application.Commands.Bids;
using TenderManagementSystem.Application.Commands.Vendors;
using TenderManagementSystem.Application.DTOs;
using TenderManagementSystem.Core.Models.DTOs;
using TenderManagementSystem.Domain.Models.Entities;
using TenderManagementSystem.Domain.Models.Entities.RBAC;

namespace TenderManagementSystem.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Tender, TenderDto>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Deadline, opt => opt.MapFrom(src => src.Deadline))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));
            
            CreateMap<CreateTenderDto, Tender>();

            CreateMap<Category, CategoryDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
            CreateMap<CategoryDto, Category>();

            CreateMap<Status, StatusDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
            CreateMap<StatusDto, Status>();

            CreateMap<Bid, BidDto>()
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
                .ForMember(dest => dest.SubmissionDate, opt => opt.MapFrom(src => src.SubmissionDate))
                .ForMember(dest => dest.Vendor, opt => opt.MapFrom(src => src.Vendor))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));

            CreateMap<BidDto, Bid>();
            CreateMap<CreateBidDto, Bid>();

            CreateMap<Vendor, VendorDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.BidCount, opt => opt.MapFrom(src => src.Bids != null ? src.Bids.Count() : 0))
                .ForMember(dest => dest.Bids, opt => opt.MapFrom(src => src.Bids));

            CreateMap<CreateVendorDto, Vendor>();

            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.UserName));
        }
    }
}