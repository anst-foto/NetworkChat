namespace Commands
{
    public class ChatMessageType
    {
        public TypeMessage Type { get; set; }
        public string Message { get; set; }
        
        public ChatMessageType() {}

        public ChatMessageType(TypeMessage type, string message)
        {
            Type = type;
            Message = message;
        }
    }
}