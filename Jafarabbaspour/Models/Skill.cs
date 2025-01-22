using System.ComponentModel.DataAnnotations;

namespace Jafarabbaspour.Models
{
    public class Skill:BaseEntities
    {
        public string Title { get; set; }
        public SkillLevel SkillLevel { get; set; }
        [Range(0,100)]
        public int Percent { get; set; }
    }

    public enum SkillLevel
    {
        Professional,
        Advanced,
        Middle
    }
}
