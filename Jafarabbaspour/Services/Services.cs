using Jafarabbaspour.Context;
using Jafarabbaspour.DTOs;
using Jafarabbaspour.Models;
using KajeAbi.Application.Tools;
using Microsoft.EntityFrameworkCore;

namespace Jafarabbaspour.Services
{
    public class Services
    {
        #region Constructor

        private readonly ResumeContext _context;
        public Services(ResumeContext context)
        {
            _context = context;
        }

        #endregion


        #region About me



        public async Task<AboutMe> GetAboutMe()
        {
            return await _context.AboutMe.FirstOrDefaultAsync();
        }

        public async Task<AddOrEditResult> AddOrEditAboutMe(AboutMe model)
        {
            if (model.Id != 0)
            {
                var aboutMe = await _context.AboutMe.FirstOrDefaultAsync(a => a.Id == model.Id);

                aboutMe.About = model.About;
                aboutMe.Image = model.Image;

                await _context.SaveChangesAsync();

                return AddOrEditResult.Success;
            }

            var newAbout = new AboutMe
            {
                About = model.About,
                Image = model.Image
            };
            await _context.AddAsync(newAbout);
            await _context.SaveChangesAsync();

            return AddOrEditResult.Success;

        }

        #endregion

        #region Contact info

        public async Task<Contact> GetContactInfo()
        {
            return await _context.Contact.FirstOrDefaultAsync();

        }

        public async Task<AddOrEditResult> AddOrEditContactInfo(Contact model)
        {
            if (model.Id != 0)
            {
                var exist = await _context.Contact.FirstOrDefaultAsync(a => a.Id == model.Id);

                exist.Mobile = model.Mobile;
                exist.Email = model.Email;
                exist.Address = model.Address;

                await _context.SaveChangesAsync();

                return AddOrEditResult.Success;
            }

            var addNew = new Contact
            {
                Mobile = model.Mobile,
                Email = model.Email,
                Address = model.Address,
            };
            await _context.AddAsync(addNew);
            await _context.SaveChangesAsync();

            return AddOrEditResult.Success;
        }


        #endregion

        #region Contact form

        public async Task<FilterContactForms> GetAllContactForms(FilterContactForms filter)
        {
            var query = await _context.ContactForms.Where(s => !s.IsDelete).ToListAsync();

            switch (filter.ReadState)
            {
                case ReadState.IsRead:
                    query.Where(c => c.IsRead);
                    break;
                case ReadState.IsNotRead:
                    query.Where(c => !c.IsRead);
                    break;
            }

            var filterResult = new FilterContactForms
            {
                Entities = query,
                ReadState = filter.ReadState
            };

            return filter;

        }

        public async Task<AddOrEditResult> AddContactForm(ContactForm model)
        {
            if (model is null)
            {
                return AddOrEditResult.Failed;
            }

            var newModel = new ContactForm
            {
                Email = model.Email,
                Mobile = model.Mobile,
                FullName = model.FullName,
                Subject = model.Subject,
                Text = model.Text

            };

            await _context.AddAsync(newModel);
            await _context.SaveChangesAsync();
            return AddOrEditResult.Success;

        }

        public async Task<bool> ContactFormRead(int id)
        {
            var contact = await _context.ContactForms.FirstOrDefaultAsync(c => c.Id == id);
            if (contact is not null)
            {
                contact.IsRead = true;
                await _context.SaveChangesAsync();
                return true;
            }

            return false;

        }

        #endregion

        #region Portfolio

        public async Task<List<Portfolio>> GetAllPortfolios()
        {
            return await _context.Portfolios.Where(s => !s.IsDelete).ToListAsync();

        }

        public async Task<AddOrEditResult> CreatePortfolio(Portfolio model, IFormFile imageFile)
        {
            if (model is null)
            {
                return AddOrEditResult.Failed;
            }



            if (imageFile is not null)
            {
                model.Image = ChangeImage(imageFile);
            }


            var newModel = new Portfolio
            {
                Image = model.Image,
                Description = model.Description,
                Title = model.Title,
                Url = model.Url,
                RoleInTeam = model.RoleInTeam
            };

            await _context.AddAsync(newModel);
            await _context.SaveChangesAsync();
            return AddOrEditResult.Success;

        }

        public async Task<AddOrEditResult> UpdatePortfolio(Portfolio model, IFormFile? imageFile)
        {
            var exist = await _context.Portfolios.FirstOrDefaultAsync(p => p.Id == model.Id);

            if (exist is not null)
            {
                exist.Description = model.Description;
                exist.Title = model.Title;
                exist.Url = model.Url;
                exist.RoleInTeam = model.RoleInTeam;

                if (imageFile is not null)
                {
                    model.Image = ChangeImage(imageFile);
                    exist.Image = model.Image;
                }
                await _context.SaveChangesAsync();

                return AddOrEditResult.Success;
            }

            return AddOrEditResult.Failed;

        }
        public async Task<Portfolio> GetPortfolioById(int id)
        {
            return await _context.Portfolios.FirstOrDefaultAsync(p => p.Id == id);

        }

        public async Task<bool> DeletePortfolio(int id)
        {
            var exist = await _context.Portfolios.FirstOrDefaultAsync(c => c.Id == id);
            if (exist is not null)
            {
                exist.IsDelete = true;
                await _context.SaveChangesAsync();
                return true;
            }

            return false;

        }

        #endregion

        #region Skill

        public async Task<List<Skill>> GetAllSkills()
        {
            return await _context.Skills.Where(s => !s.IsDelete).ToListAsync();

        }

        public async Task<AddOrEditResult> CreateSkill(Skill model)
        {
            if (model is null)
            {
                return AddOrEditResult.Failed;
            }

            var newModel = new Skill
            {
                SkillLevel = model.SkillLevel,
                Title = model.Title,
                Percent = model.Percent
            };

            await _context.AddAsync(newModel);
            await _context.SaveChangesAsync();
            return AddOrEditResult.Success;

        }

        public async Task<AddOrEditResult> UpdateSkill(Skill model)
        {
            var exist = await _context.Skills.FirstOrDefaultAsync(p => p.Id == model.Id);

            if (exist is not null)
            {
                exist.SkillLevel = model.SkillLevel;
                exist.Title = model.Title;
                exist.Percent = model.Percent;

                _context.Update(exist);
                await _context.SaveChangesAsync();

                return AddOrEditResult.Success;
            }

            return AddOrEditResult.Failed;

        }
        public async Task<Skill> GetSkillById(int id)
        {
            return await _context.Skills.FirstOrDefaultAsync(p => p.Id == id);


        }

        public async Task<bool> DeleteSkill(int id)
        {
            var exist = await _context.Skills.FirstOrDefaultAsync(c => c.Id == id);
            if (exist is not null)
            {
                exist.IsDelete = true;
                await _context.SaveChangesAsync();
                return true;
            }

            return false;

        }

        #endregion

        #region Social media

        public async Task<List<SocialMedia>> GetAllSocialMedias()
        {
            return await _context.SocialMedia.Where(s => !s.IsDelete).ToListAsync();

        }

        public async Task<AddOrEditResult> CreateSocialMedia(SocialMedia model)
        {
            if (model is null)
            {
                return AddOrEditResult.Failed;
            }

            var newModel = new SocialMedia
            {
                Title = model.Title,
                Url = model.Url
            };

            await _context.AddAsync(newModel);
            await _context.SaveChangesAsync();
            return AddOrEditResult.Success;

        }

        public async Task<AddOrEditResult> UpdateSocialMedia(SocialMedia model)
        {
            var exist = await _context.SocialMedia.FirstOrDefaultAsync(p => p.Id == model.Id);

            if (exist is not null)
            {
                exist.Title = model.Title;
                exist.Url = model.Url;

                await _context.SaveChangesAsync();

                return AddOrEditResult.Success;
            }

            return AddOrEditResult.Failed;

        }
        public async Task<SocialMedia> GetSocialMediaById(int id)
        {
            return await _context.SocialMedia.FirstOrDefaultAsync(p => p.Id == id);


        }

        public async Task<bool> DeleteSocialMedia(int id)
        {
            var exist = await _context.SocialMedia.FirstOrDefaultAsync(c => c.Id == id);
            if (exist is not null)
            {
                exist.IsDelete = true;
                await _context.SaveChangesAsync();
                return true;
            }

            return false;

        }


        #endregion

        #region Work experience

        public async Task<List<WorkExperience>> GetAllWorkExperiences()
        {
            return await _context.WorkExperiences.Where(s => !s.IsDelete).ToListAsync();

        }

        public async Task<AddOrEditResult> CreateWorkExperience(WorkExperience model)
        {
            if (model is null)
            {
                return AddOrEditResult.Failed;
            }

            var newModel = new WorkExperience
            {
                JobTitle = model.JobTitle,
                Location = model.Location
            };

            await _context.AddAsync(newModel);
            await _context.SaveChangesAsync();
            return AddOrEditResult.Success;

        }

        public async Task<AddOrEditResult> UpdateWorkExperience(WorkExperience model)
        {
            var exist = await _context.WorkExperiences.FirstOrDefaultAsync(p => p.Id == model.Id);

            if (exist is not null)
            {
                exist.Location = model.Location;
                exist.JobTitle = model.JobTitle;

                await _context.SaveChangesAsync();

                return AddOrEditResult.Success;
            }

            return AddOrEditResult.Failed;

        }
        public async Task<WorkExperience> GetWorkExperienceById(int id)
        {
            return await _context.WorkExperiences.FirstOrDefaultAsync(p => p.Id == id);


        }

        public async Task<bool> DeleteWorkExperience(int id)
        {
            var exist = await _context.WorkExperiences.FirstOrDefaultAsync(c => c.Id == id);
            if (exist is not null)
            {
                exist.IsDelete = true;
                await _context.SaveChangesAsync();
                return true;
            }

            return false;

        }

        #endregion

        #region User

        public async Task<LoginResult> Login(User user)
        {
            var result = await _context.Users.AnyAsync(u =>
                u.UserName == user.UserName && u.Password == user.Password);

            if (result)
            {
                return LoginResult.LoggedIn;
            }

            return LoginResult.Failed;

        }

        #endregion


        public async Task<string> GetMessageById(int id)
        {
            await ContactFormRead(id);

            var contact = await _context.ContactForms.FirstOrDefaultAsync(c => c.Id == id);
            return contact?.Text;
        }

        private static string ChangeImage(IFormFile icon, string? lastImageName = "")
        {
            string newImageName = Guid.NewGuid() + Path.GetExtension(icon.FileName);
            if (!string.IsNullOrEmpty(lastImageName))
            {
                icon.AddImageToServer(newImageName, "/images/", 400, 300,
                    "/images/thumb/",
                    deletefileName: lastImageName.Equals("/images/upload.svg") ? null : lastImageName);
            }
            else
            {
                icon.AddImageToServer(newImageName, "/images/", 400, 300,
                    "/images/thumb/");
            }

            newImageName = Path.ChangeExtension(newImageName, "webp");

            return newImageName;
        }
    }
}
