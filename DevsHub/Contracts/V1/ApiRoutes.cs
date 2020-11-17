namespace DevsHub.Contracts.V1
{
    public static class ApiRoutes
    {
        private const string Root = "api";
        private const string Version = "v1";

        private const string Base = Root + "/" + Version;

        public static class Account
        {
            public const string Register = Base + "/account/register";
            public const string Login = Base + "/account/login";
            public const string Get = Base + "/account";
        }

        public static class Users
        {
            public const string GetAll = Base + "/users";
            public const string Get = Base + "/users/{id}";
            public const string Create = Base + "/users";
            public const string Update = Base + "/users/{id}";
            public const string Delete = Base + "/users/{id}";
        }

        public static class Tutorial
        {
            public static class Category
            {
                public const string GetAll = Base + "/tutorials/categories";
                public const string Create = Base + "/tutorials/categories";
                public const string Update = Base + "/tutorials/categories/{id}";
                public const string Delete = Base + "/tutorials/categories/{id}";
            }
        }
    }
}
