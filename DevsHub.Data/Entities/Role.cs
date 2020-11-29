namespace DevsHub.Data
{
    public static class Role
    {
        public const string Admin = "admin";
        public const string Organizer = "organizer";
        public const string User = "user";

        public static string[] Roles = new string[] { Admin, Organizer, User };
    }
}
