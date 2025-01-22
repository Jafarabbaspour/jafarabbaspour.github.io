namespace Jafarabbaspour.Models
{
    public class Portfolio:BaseEntities
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string? Image { get; set; }
        public string Url { get; set; }
        public string RoleInTeam { get; set; }
    }
}
