namespace Chat.Db.Interfaces
{
    /// <summary>
    /// IResult.
    /// </summary>
    public interface IResult
    {
        /// <summary>
        /// Gets a value indicating whether this instance is success.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is success; otherwise, <c>false</c>.
        /// </value>
        bool IsSuccess { get; }
    }
}
