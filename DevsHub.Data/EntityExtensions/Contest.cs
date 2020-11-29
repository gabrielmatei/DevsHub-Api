namespace DevsHub.Data
{
    public partial class Contest
    {
        public void Update(Contest entity)
        {
            this.Name = entity.Name;
            this.Description = entity.Description;
            this.Start = entity.Start;
            this.End = entity.End;
        }
    }
}
