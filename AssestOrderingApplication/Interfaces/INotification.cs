namespace AssestOrderingApplication.Interfaces
{
    public interface INotification
    {
        public string _message { get; set; }


        bool sendNotiifcation(string sender);
    }
}
