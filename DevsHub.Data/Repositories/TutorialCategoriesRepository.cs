namespace DevsHub.Data.Repositories
{
    public interface ITutorialCategoriesRepository : IRepository<TutorialCategory>
    {
    }

    public class TutorialCategoriesRepository : Repository<TutorialCategory>, ITutorialCategoriesRepository
    {
        private DataContext _dataContext => this.Context as DataContext;

        public TutorialCategoriesRepository(DataContext dataContext) : base(dataContext)
        {
        }
    }
}
