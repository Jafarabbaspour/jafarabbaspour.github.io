using Jafarabbaspour.Models;
using Microsoft.EntityFrameworkCore;

namespace Jafarabbaspour.Context
{
    public class ResumeContext : DbContext
    {
        public ResumeContext(DbContextOptions<ResumeContext> options) : base(options)
        {

        }

        public DbSet<AboutMe> AboutMe { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<ContactForm> ContactForms { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<SocialMedia> SocialMedia { get; set; }
        public DbSet<WorkExperience> WorkExperiences { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
