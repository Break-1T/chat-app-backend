namespace Chat.Api.Constants
{
    public static class ApiConstant
    {
        public const string SocketPath = "hub";

        public static class ApiClaims
        {
            public const string UserIdClaimType = "UserId";
            
            public const string GroupIdClaimType = "GroupId";

            public const string UserId = "sub";
        }
    }
}
