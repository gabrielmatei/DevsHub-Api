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
            #endregion
            #region Users
            CreateMap<CreateUserRequest, User>();
            CreateMap<UpdateUserRequest, User>();
            #endregion
            #region Tutorials
            #endregion
            #region Tutorial Categories
            CreateMap<CreateTutorialCategoryRequest, TutorialCategory>();
            CreateMap<UpdateTutorialCategoryRequest, TutorialCategory>();
            #endregion
        }
    }
}
