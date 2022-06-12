using Microsoft.AspNetCore.Mvc;

namespace Chat.Api.ResultModels
{
    public class ApiOperationResult<TEntity>
    {
        private ApiOperationResult(ResultType operationStatus, TEntity entity = default, SerializableError error = default)
        {
            this.Status = operationStatus;
            this.Entity = entity;
            this.Error = error;
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
            /// The error.
            /// </summary>
            Error,
        }

        /// <summary>
        /// Gets the status of store operation.
        /// </summary>
        public ResultType Status { get; }

        public TEntity Entity { get; }

        /// <summary>
        /// Gets error Message.
        /// </summary>
        public SerializableError Error{ get; }

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
        /// ApiOperationResult.
        /// </returns>
        public static ApiOperationResult<TEntity> FromSuccess(TEntity entity)
        {
            return new ApiOperationResult<TEntity>(ResultType.Success, entity);
        }

        /// <summary>
        /// Froms the unexpected error.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        /// <returns>ApiOperationResult.</returns>
        /// <exception cref="System.ArgumentNullException">errorMessage</exception>
        public static ApiOperationResult<TEntity> FromError(SerializableError error)
        {
            if (error == null)
            {
                throw new ArgumentNullException(nameof(error));
            }

            return new ApiOperationResult<TEntity>(ResultType.Error, default, error);
        }
    }
}
