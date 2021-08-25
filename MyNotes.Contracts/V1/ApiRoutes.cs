namespace MyNotes.Contracts.V1
{
    public static class ApiRoutes
    {
        private const string Root = "api";
        private const string Version = "v1";

        private const string Base = Root + "/" + Version;

        public static class TopicsRoute
        {
            private const string baseRoute = "/topic";

            public const string Get = Base + baseRoute;

            public const string GetList = Base + baseRoute + "s";

            public const string Create = Base + baseRoute;

            public const string Update = Base + baseRoute + "/{topicId}:Guid";

            public const string Delete = Base + baseRoute + "/{topicId}:Guid";
        }

        public static class ParagraphRoute
        {
            private const string baseRoute = "/paragraph";

            public const string Get = Base + baseRoute;

            public const string GetList = Base + baseRoute + "s";

            public const string Create = Base + baseRoute;

            public const string Update = Base + baseRoute + "/{paragraphId}:Guid";

            public const string Delete = Base + baseRoute + "/{paragraphId}:Guid";
        }
    }
}
