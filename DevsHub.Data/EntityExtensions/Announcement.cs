namespace DevsHub.Data
{
    public partial class Announcement
    {
        public void Update(Announcement entity)
        {
            this.Title = entity.Title;
            this.Body = entity.Body;
            this.Type = entity.Type;
            this.Start = entity.Start;
            this.End = entity.End;
        }
    }
}
