namespace DevsHub.Data.Repositories
{
    public interface IUserProfilesRepository : IRepository<UserProfile>
    {
    }

    public class UserProfilesRepository : Repository<UserProfile>, IUserProfilesRepository
    {
        private DataContext _dataContext => this.Context as DataContext;

        public UserProfilesRepository(DataContext dataContext) : base(dataContext)
        {
        }
    }
}
