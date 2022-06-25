namespace Chat.Db.Models
{
    public class ChatMessage
    {
        public Guid ChatId { get; set; }

        public Guid UserId { get; set; }

        public Guid GroupId { get; set; }

        public string Message { get; set; }

        public DateTime RecCreated { get; set; }
    }
}
