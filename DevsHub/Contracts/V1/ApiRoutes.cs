namespace DevsHub.Contracts.V1
{
    public static class ApiRoutes
    {
        private const string Root = "api";
        private const string Version = "v1";

        private const string Base = Root + "/" + Version;

        public static class Values
        {
            public const string GetAll = Base + "/values";
            public const string Get = Base + "/values/{id}";
            public const string Create = Base + "/values";
            public const string Update = Base + "/values/{id}";
            public const string Delete = Base + "/values/{id}";
        }
    }
}
