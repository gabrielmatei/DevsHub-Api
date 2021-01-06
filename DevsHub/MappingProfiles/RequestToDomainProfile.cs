using AutoMapper;
using DevsHub.Contracts.V1.Requests;
using DevsHub.Data;
using System;
using System.Collections.Generic;

namespace DevsHub.MappingProfiles
{
    public class RequestToDomainProfile : Profile
    {
        public RequestToDomainProfile()
        {
            #region Account
            CreateMap<RegisterRequest, User>();
            CreateMap<RegisterRequest, UserProfile>();
            CreateMap<UpdateAccountRequest, UserProfile>();
            #endregion

            #region Users
            CreateMap<UpdateUserRequest, User>();
            #endregion

            #region Contests
            CreateMap<CreateOrUpdateContestRequest, Contest>();
            #endregion

            #region Tutorials
            CreateMap<CreateOrUpdateTutorialRequest, Tutorial>()
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => new List<TutorialCategory>()));
            #endregion

            #region Tutorial Categories
            CreateMap<CreateOrUpdateTutorialCategoryRequest, TutorialCategory>();
            #endregion

            #region Announcement
            CreateMap<CreateOrUpdateAnnouncementRequest, Announcement>();
            #endregion
        }
    }
}
