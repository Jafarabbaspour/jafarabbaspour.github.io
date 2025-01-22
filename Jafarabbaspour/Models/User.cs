namespace Jafarabbaspour.Models
{
    public class User:BaseEntities
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public enum LoginResult
    {
        LoggedIn,
        Failed
    }
}
