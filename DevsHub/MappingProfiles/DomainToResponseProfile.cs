using AutoMapper;
using DevsHub.Contracts.V1.Responses;
using DevsHub.Domain;
using System.Collections.Generic;

namespace DevsHub.MappingProfiles
{
    public class DomainToResponseProfile : Profile
    {
        public DomainToResponseProfile()
        {
            #region Account
            CreateMap<User, AccountResponse>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));
            #endregion
            #region Users
            CreateMap<User, UserResponse>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));
            CreateMap<List<User>, UserListResponse>()
                .ForMember(dest => dest.Users, opt => opt.MapFrom(src => src));
            #endregion
        }
    }
}
