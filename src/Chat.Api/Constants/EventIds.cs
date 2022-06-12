namespace Chat.Api.Constants
{
    public static class EventIds
    {
        #region Error

        /// <summary>
        /// The create user unexpected error.
        /// </summary>
        public static EventId CreateUserUnexpectedError = new EventId(2401, "CREATE_USER_UNEXPECTED_ERROR");

        /// <summary>
        /// The create group unexpected error.
        /// </summary>
        public static EventId CreateGroupUnexpectedError = new EventId(2402, "CREATE_GROUP_UNEXPECTED_ERROR");

        #endregion
    }
}
