namespace DevsHub.Data
{
    public partial class TutorialCategory
    {
        public void Update(TutorialCategory entity)
        {
            this.Name = entity.Name;
        }
    }
}
