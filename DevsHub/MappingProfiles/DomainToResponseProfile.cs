using AutoMapper;
using DevsHub.Contracts.V1.Responses;
using DevsHub.Data;
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
            CreateMap<User, UserShortResponse>();
            CreateMap<UserProfile, UserProfileResponse>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));
            CreateMap<List<User>, UserListResponse>()
                .ForMember(dest => dest.Users, opt => opt.MapFrom(src => src));
            #endregion
            #region Contests
            CreateMap<Contest, ContestShortResponse>()
                .ForMember(dest => dest.OrganizerId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Organizer, opt => opt.MapFrom(src => src.User.Profile));
            CreateMap<List<Contest>, ContestShortListResponse>()
                .ForMember(dest => dest.Contests, opt => opt.MapFrom(src => src));
            CreateMap<Contest, ContestResponse>()
                .ForMember(dest => dest.OrganizerId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Organizer, opt => opt.MapFrom(src => src.User.Profile));
            CreateMap<List<Contest>, ContestListResponse>()
                .ForMember(dest => dest.Contests, opt => opt.MapFrom(src => src));
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
