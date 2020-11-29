namespace DevsHub.Data
{
    public partial class UserProfile
    {
        public void Update(UserProfile entity)
        {
            this.FirstName = entity.FirstName;
            this.LastName = entity.LastName;
        }
    }
}
