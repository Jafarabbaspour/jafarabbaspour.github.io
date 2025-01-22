namespace Jafarabbaspour.Models
{
    public class AboutMe:BaseEntities
    {
        public string About { get; set; }
        public string? Image { get; set; }
    }

    public enum AddOrEditResult
    {
        Success,
        Failed
    }
}
