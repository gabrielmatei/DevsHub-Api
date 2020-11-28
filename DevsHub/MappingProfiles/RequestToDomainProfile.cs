using AutoMapper;
using DevsHub.Contracts.V1.Requests;
using DevsHub.Domain;

namespace DevsHub.MappingProfiles
{
    public class RequestToDomainProfile : Profile
    {
        public RequestToDomainProfile()
        {
            #region Account
            CreateMap<RegisterRequest, User>();
            CreateMap<RegisterRequest, UserProfile>();
            #endregion
            #region Users
            CreateMap<UpdateUserRequest, User>();
            #endregion
            #region Contests
            CreateMap<CreateOrUpdateContestRequest, Contest>();
            #endregion
            #region Tutorials
            #endregion
            #region Tutorial Categories
            CreateMap<CreateOrUpdateTutorialCategoryRequest, TutorialCategory>();
            #endregion
        }
    }
}
