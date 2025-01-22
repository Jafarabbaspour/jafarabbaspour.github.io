using Jafarabbaspour.DTOs;
using Jafarabbaspour.Models;
using Microsoft.AspNetCore.Mvc;

namespace Jafarabbaspour.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly Services.Services _services;

        public HomeController(Services.Services services)
        {
            _services = services;
        }


        public async Task<IActionResult> Index()
        {
            return View();
        }

        #region About me

        public async Task<IActionResult> GetAboutMe()
        {
            return View(await _services.GetAboutMe());
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdateAboutMe(AboutMe model)
        {
            
            var result = await _services.AddOrEditAboutMe(model);

            switch (result)
            {
                case AddOrEditResult.Success:
                    break;
                case AddOrEditResult.Failed:
                    break;
            }

            return RedirectToAction("GetAboutMe");
        }

        #endregion

        #region Contact 

        public async Task<IActionResult> GetContactInfo()
        {
            return View(await _services.GetContactInfo());
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdateContactInfo(Contact model)
        {
            var result = await _services.AddOrEditContactInfo(model);

            switch (result)
            {
                case AddOrEditResult.Success:
                    break;
                case AddOrEditResult.Failed:
                    break;
            }

            return RedirectToAction("GetContactInfo");
        }


        #endregion

        #region Contact form

        public async Task<IActionResult> GetAllContactForms(FilterContactForms filter)
        {
            return View(await _services.GetAllContactForms(filter));
        }

        public async Task<IActionResult> ShowMessage(int id)
        {
            var message = await _services.GetMessageById(id);
            return View(message);
        }

        #endregion


        #region Portfolio

        public async Task<IActionResult> GetAllPortfolios()
        {
            return View(await _services.GetAllPortfolios());
        }


        public async Task<IActionResult> CreatePortfolio()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePortfolio(Portfolio model , IFormFile imageFile)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _services.CreatePortfolio(model, imageFile);

            switch (result)
            {
                case AddOrEditResult.Success:
                    break;
                case AddOrEditResult.Failed:
                    break;
                
            }

            return RedirectToAction("GetAllPortfolios");
        }

        public async Task<IActionResult> UpdatePortfolio(int id)
        {
            return View(await _services.GetPortfolioById(id));
        }
        [HttpPost]

        public async Task<IActionResult> UpdatePortfolio(Portfolio model, IFormFile? imageFile)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _services.UpdatePortfolio(model, imageFile);

            switch (result)
            {
                case AddOrEditResult.Success:
                    break;
                case AddOrEditResult.Failed:
                    break;
            }

            return RedirectToAction("GetAllPortfolios");

        }


        public async Task<IActionResult> DeletePortfolio(int id)
        {
            var result = await _services.DeletePortfolio(id);

            return RedirectToAction("GetAllPortfolios");
        }

        #endregion


        #region Skill

        public async Task<IActionResult> GetAllSkills()
        {
            return View(await _services.GetAllSkills());
        }

        public async Task<IActionResult> CreateSkill()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateSkill(Skill model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = await _services.CreateSkill(model);

            switch (result)
            {
                case AddOrEditResult.Success:
                    return RedirectToAction("GetAllSkills");
                   
                case AddOrEditResult.Failed:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return View();

        }

        public async Task<IActionResult> UpdateSkill(int id)
        {
            return View(await _services.GetSkillById(id));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSkill(Skill model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = await _services.UpdateSkill(model);

            switch (result)
            {
                case AddOrEditResult.Success:
                    return RedirectToAction("GetAllSkills");
                case AddOrEditResult.Failed:
                    break;
            }

            return View();

        }

        public async Task<IActionResult> DeleteSkill(int id)
        {
            var result = await _services.DeleteSkill(id);

            return RedirectToAction("GetAllSkills");
        }


        #endregion

        #region Social media

        public async Task<IActionResult> GetAllSocialMedia()
        {
            return View(await _services.GetAllSocialMedias());
        }

        public async Task<IActionResult> CreateSocialMedia()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> CreateSocialMedia(SocialMedia model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = await _services.CreateSocialMedia(model);

            switch (result)
            {
                case AddOrEditResult.Success:
                    return RedirectToAction("GetAllSocialMedia");

                case AddOrEditResult.Failed:
                    break;

            }
            return View();

        }

        public async Task<IActionResult> UpdateSocialMedia(int id)
        {
            return View(await _services.GetSocialMediaById(id));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSocialMedia(SocialMedia model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = await _services.UpdateSocialMedia(model);

            switch (result)
            {
                case AddOrEditResult.Success:
                    return RedirectToAction("GetAllSocialMedia");
                case AddOrEditResult.Failed:
                    break;
            }

            return View();

        }

        public async Task<IActionResult> DeleteSocialMedia(int id)
        {
            var result = await _services.DeleteSocialMedia(id);

            return RedirectToAction("GetAllSocialMedia");
        }


        #endregion

        //#region User

        //public async Task<IActionResult> Login(User user)
        //{
        //    var result =await _services.Login(user);
        //    switch (result)
        //    {
        //        case LoginResult.LoggedIn:
        //            break;
        //        case LoginResult.Failed:
        //            break;
        //        default:
        //    }

        //    return RedirectToAction("index");
        //}

        //#endregion

        #region Work experience

        public async Task<IActionResult> GetAllWorkExperiences()
        {
            return View(await _services.GetAllWorkExperiences());
        }

        public async Task<IActionResult> CreateWorkExperience()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> CreateWorkExperience(WorkExperience model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = await _services.CreateWorkExperience(model);

            switch (result)
            {
                case AddOrEditResult.Success:
                    return RedirectToAction("GetAllWorkExperiences");

                case AddOrEditResult.Failed:
                    break;

            }
            return View();

        }

        public async Task<IActionResult> UpdateWorkExperience(int id)
        {
            return View(await _services.GetWorkExperienceById(id));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateWorkExperience(WorkExperience model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = await _services.UpdateWorkExperience(model);

            switch (result)
            {
                case AddOrEditResult.Success:
                    return RedirectToAction("GetAllWorkExperiences");
                case AddOrEditResult.Failed:
                    break;
            }

            return View();

        }

        public async Task<IActionResult> DeleteWorkExperience(int id)
        {
            var result = await _services.DeleteWorkExperience(id);

            return RedirectToAction("GetAllWorkExperiences");
        }

        #endregion

    }
}
