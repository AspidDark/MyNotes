namespace MyNotes.Contracts.V1
{
    public static class ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v1";

        public const string Base = Root + "/" + Version;

        public static class Topics
        {

            public const string Get = Base + "/topic";

            public const string GetAll = Base + "/topics";

            public const string Create = Base + "/topic";

            public const string Update = Base + "/topic/{topicId}:Guid";

            public const string Delete = Base + "/topic/{topicId}:Guid";
        }
    }
}
