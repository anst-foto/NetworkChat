using System.Text.Json;

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

        public ChatMessageType(string json)
        {
            var temp = JsonSerializer.Deserialize<ChatMessageType>(json);
            Type = temp.Type;
            Message = temp.Message;
        }

        private static string Create(TypeMessage type, string message)
        {
            var temp = new ChatMessageType(type, message);
            var res = JsonSerializer.Serialize(temp);
            return res;
        }

        public static string CreateWelcome(string name)
        {
            return Create(TypeMessage.Welcome, name);
        }

        public static string CreateStop()
        {
            return Create(TypeMessage.Stop, "");
        }

        public static string CreateMessage(string message)
        {
            return Create(TypeMessage.Message, message);
        }
    }
}