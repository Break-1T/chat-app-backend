using Chat.Db.Interfaces;

namespace Chat.Db.ResultModels
{
    public class DbOperationResult<TEntity> : IResult
    {
        private DbOperationResult(ResultType operationStatus, TEntity entity = default, string errorMessage = default)
        {
            this.Status = operationStatus;
            this.Entity = entity;
            this.ErrorMessage = errorMessage;
        }

        /// <summary>
        /// ResultType.
        /// </summary>
        public enum ResultType
        {
            /// <summary>
            /// Entity successfully added.
            /// </summary>
            Success,

            /// <summary>
            /// The unexpected error.
            /// </summary>
            UnexpectedError,
        }

        /// <summary>
        /// Gets the status of store operation.
        /// </summary>
        public ResultType Status { get; }

        public TEntity Entity { get; }

        /// <summary>
        /// Gets error Message.
        /// </summary>
        public string ErrorMessage { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is success.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is success; otherwise, <c>false</c>.
        /// </value>
        public bool IsSuccess => this.Status == ResultType.Success;

        /// <summary>
        /// Froms the added.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>
        /// DbOperationResult.
        /// </returns>
        public static DbOperationResult<TEntity> FromSuccess(TEntity entity)
        {
            return new DbOperationResult<TEntity>(ResultType.Success, entity);
        }

        /// <summary>
        /// Froms the unexpected error.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        /// <returns>DbOperationResult.</returns>
        /// <exception cref="System.ArgumentNullException">errorMessage</exception>
        public static DbOperationResult<TEntity> FromError(string errorMessage)
        {
            if (string.IsNullOrEmpty(errorMessage))
            {
                throw new ArgumentNullException(nameof(errorMessage));
            }

            return new DbOperationResult<TEntity>(ResultType.UnexpectedError, default, errorMessage);
        }
    }
}
