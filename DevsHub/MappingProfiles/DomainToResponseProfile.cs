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
            CreateMap<User, AccountResponse>();
            CreateMap<UserProfile, UserProfileResponse>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));
            #endregion
            #region Users
            CreateMap<User, UserResponse>();
            CreateMap<UserProfile, UserProfileResponse>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));
            CreateMap<List<User>, UserListResponse>()
                .ForMember(dest => dest.Users, opt => opt.MapFrom(src => src));
            #endregion
            #region Tutorials
            #endregion
            #region Tutorial Categories
            CreateMap<TutorialCategory, TutorialCategoryResponse>();
            CreateMap<List<TutorialCategory>, TutorialCategoryListResponse>()
                .ForMember(dest => dest.TutorialCategories, opt => opt.MapFrom(src => src));
            #endregion
        }
    }
}
