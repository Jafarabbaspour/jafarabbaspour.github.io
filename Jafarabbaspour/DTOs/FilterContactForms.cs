using Jafarabbaspour.Models;

namespace Jafarabbaspour.DTOs
{
    public class FilterContactForms
    {
        public ReadState ReadState { get; set; }

        public ICollection<ContactForm> Entities { get; set; }
    }

    public enum ReadState
    {
        IsRead,
        IsNotRead,
    }
}
