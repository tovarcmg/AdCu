namespace MyMauiApp;

public class PushNotification
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public string Token { get; set; }
    public DateTime SendAt { get; set; }
    public bool Sent { get; set; }
}