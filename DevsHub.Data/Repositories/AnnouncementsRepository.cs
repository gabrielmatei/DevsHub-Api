namespace DevsHub.Data.Repositories
{
    public interface IAnnouncementsRepository : IRepository<Announcement>
    {
    }

    public class AnnouncementsRepository : Repository<Announcement>, IAnnouncementsRepository
    {
        private DataContext _dataContext => this.Context as DataContext;

        public AnnouncementsRepository(DataContext dataContext) : base(dataContext)
        {
        }
    }
}
