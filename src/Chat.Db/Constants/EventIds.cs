﻿using Microsoft.Extensions.Logging;

namespace Chat.Db.Constants
{
    public static class EventIds
    {
        #region Error

        /// <summary>
        /// The add group unexpected error.
        /// </summary>
        public static EventId AddGroupUnexpectedError = new EventId(1401, "ADD_GROUP_UNEXPECTED_ERROR");

        /// <summary>
        /// The update group unexpected error.
        /// </summary>
        public static EventId UpdateGroupUnexpectedError = new EventId(1402, "UPDATE_GROUP_UNEXPECTED_ERROR");

        /// <summary>
        /// The get group unexpected error.
        /// </summary>
        public static EventId GetGroupUnexpectedError = new EventId(1403, "GET_GROUP_UNEXPECTED_ERROR");

        /// <summary>
        /// The get groups unexpected error.
        /// </summary>
        public static EventId GetGroupsUnexpectedError = new EventId(1405, "GET_GROUPS_UNEXPECTED_ERROR");

        public static EventId GetUserGroupUnexpectedError = new EventId(1406, "GET_USER_GROUP_UNEXPECTED_ERROR");

        #endregion
    }
}
