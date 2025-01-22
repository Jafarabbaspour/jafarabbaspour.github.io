namespace Jafarabbaspour.Models
{
    public class ContactForm:BaseEntities
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Subject { get; set; }
        public string Text { get; set; }
        public bool IsRead { get; set; }
    }
}
