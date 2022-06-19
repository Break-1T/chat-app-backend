namespace Chat.Api.Constants
{
    public static class EventIds
    {
        #region Error

        /// <summary>
        /// The sign up unexpected error.
        /// </summary>
        public static EventId SignUpUnexpectedError = new EventId(2401, "SIGN_UP_UNEXPECTED_ERROR");

        /// <summary>
        /// The create group unexpected error.
        /// </summary>
        public static EventId CreateGroupUnexpectedError = new EventId(2402, "CREATE_GROUP_UNEXPECTED_ERROR");

        /// <summary>
        /// The identity server invalid status code error.
        /// </summary>
        public static EventId IdentityServerInvalidStatusCodeError = new EventId(2403, "IDENTITY_SERVER_INVALID_STATUS_CODE_ERROR");

        /// <summary>
        /// The login unexpected error.
        /// </summary>
        public static EventId LoginUnexpectedError = new EventId(2404, "LOGIN_UNEXPECTED_ERROR");

        #endregion
    }
}
