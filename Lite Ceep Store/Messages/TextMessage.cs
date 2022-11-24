namespace Lite_Ceep_Store.Messages
{
    internal class TextMessage : IMessage
    {
        public string Text { get; set; }
        public TextMessage(string text) => Text = text;
    }
}
