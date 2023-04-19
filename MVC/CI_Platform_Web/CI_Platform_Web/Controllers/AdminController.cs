﻿using Microsoft.AspNetCore.Mvc;
using CI_Platform.Entities.CIPlatformDbContext;
using CI_Platform.Entities.Data;
using CI_Platform.Entities.ViewModels;
using CI_Platform.Repository.Interface;
using System.Net.Mail;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform_Web.Controllers
{
    public class AdminController : Controller
    {

        private readonly CiPlatformDbContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IAdminUserRepository _adminUserRepository;
        private readonly IAdminCMSRepository _adminCMSRepository;
        private  readonly IAdminMissionRepository _adminMissionRepository;
        private readonly IAdminMissionThemeRepository _adminMissionThemeRepository;
        private readonly IAdminMissionSkillsRepository _adminMissionSkillsRepository;
        private readonly IAdminMissionApplicationRepository _adminMissionApplicationRepository;

        public AdminController(CiPlatformDbContext db, IAdminUserRepository adminUserRepository, IAdminCMSRepository adminCMSRepository, IAdminMissionRepository adminMissionRepository, IAdminMissionThemeRepository adminMissionThemeRepository, IAdminMissionSkillsRepository adminMissionSkillsRepository, IAdminMissionApplicationRepository adminMissionApplicationRepository, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _hostEnvironment = webHostEnvironment;
            _adminUserRepository = adminUserRepository;
            _adminCMSRepository = adminCMSRepository;
            _adminMissionRepository = adminMissionRepository;
            _adminMissionThemeRepository = adminMissionThemeRepository;
            _adminMissionSkillsRepository = adminMissionSkillsRepository;
            _adminMissionApplicationRepository = adminMissionApplicationRepository;
        }




        //<--------------------------------------------------------------------User----------------------------------------------------------------------------->

        public IActionResult AdminUserHome()
        {
            List<AdminUserDisplayVm> userdetails = _adminUserRepository.GetUsers();
            return View(userdetails);
        }





        public IActionResult AdminUserAdd()
        {
            AdminUserCreateVm vm = _adminUserRepository.Getcountry();
            
            return View(vm);
        }





        [HttpPost]
        public IActionResult AdminUserAdd(AdminUserCreateVm obj, IFormFile? image)
        {
            if(image != null)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(wwwRootPath, @"assets\UserAvatar");
                var extension = Path.GetExtension(image.FileName);

                using (var filestream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    image.CopyTo(filestream);
                }

                obj.Avatar = fileName + extension;

                //_adminUserRepository.AddUser(obj);
                //return RedirectToAction("AdminUserHome");
            }
            
            

            _adminUserRepository.AddUser(obj);
            //ModelState.AddModelError("EmployeeId", "Enter unique emp id");
            //return View(obj);
            return RedirectToAction("AdminUserHome");

        }




        public JsonResult GetCities(long countryid)
        {
            List<City> cities = _adminUserRepository.GetCities(countryid);
            return new JsonResult(cities);
        }



        public IActionResult AdminUserEdit(long userid)
        {
            AdminUserCreateVm userdetails = _adminUserRepository.GetUser(userid);
            return View(userdetails);
        }

        [HttpPost]
        public IActionResult AdminUserEdit(AdminUserCreateVm obj, long userid, IFormFile image)
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;
            string fileName = Guid.NewGuid().ToString();
            var uploads = Path.Combine(wwwRootPath, @"assets\UserAvatar");
            var extension = Path.GetExtension(image.FileName);

            using (var filestream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
            {
                image.CopyTo(filestream);
            }

            //delete existing user avatar if present
            User? edituser = _db.Users.FirstOrDefault(user => user.UserId == userid);
            if(edituser != null && edituser.Avatar!=null)
            {
                string imagepath = edituser.Avatar;
                var filepath = Path.GetFullPath(Path.Combine(wwwRootPath, @"assets\UserAvatar\" + imagepath));
                if (System.IO.File.Exists(filepath))
                {
                    System.IO.File.Delete(filepath);
                }
            }
            //delete existing user avatar if present
            obj.Avatar = fileName + extension;
            obj.UserId = userid;
            _adminUserRepository.EditUser(obj);
            return RedirectToAction("AdminUserHome");
        }

        
        public string AdminUserDelete(long userid)
        {
            string result = "";

            string? response = _adminUserRepository.DeleteUser(userid);
            if(response != "")
            {
                result = response;
                return result;
            }
            return result;
            
        }
        //<---------------------------------------------------------------------------CMS------------------------------------------------------------------------>


        #region CMS
        public IActionResult AdminCMSHome()
        {
            List<AdminCMSDisplayVm> cmspages = _adminCMSRepository.GetCms();
            return View(cmspages);
        }






        #region CMS ADD
        public IActionResult AdminCMSAdd()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AdminCMSAdd(AdminCMSCreateVm obj)
        {
            _adminCMSRepository.AddCMS(obj);
            return RedirectToAction("AdminCMSHome");
        }
        #endregion





        #region CMS EDIT
        public IActionResult AdminCMSEdit(long cmspageid)
        {
            AdminCMSCreateVm cms = _adminCMSRepository.GetCMSDetails(cmspageid);
            if (cms != null)
            {
                return View(cms);
            }
            return View();
        }

        
        [HttpPost]
        public IActionResult AdminCMSEdit(AdminCMSCreateVm obj)
        {
            //obj.CmsPageId = cmspageid;
            string? response = _adminCMSRepository.SaveEditData(obj);
            if (response != null)
            {
                return RedirectToAction("AdminCMSHome");
            }
            return View(obj);
        }
        #endregion

        public string AdminCMSDelete(long cmspageid)
        {
            string result = "";
            string? response = _adminCMSRepository.DeleteCMS(cmspageid);
            if(response != null)
            {
                result = response;
                return result;
            }
            return result;
        }
        #endregion





        //<---------------------------------------------------------------------------Mission------------------------------------------------------------------------>
        public IActionResult AdminMissionHome()
        {
            List<AdminMissionDisplayVm> missions = _adminMissionRepository.GetMissions();
            return View(missions);
        }



        //<------------------------------------------------------------------------Mission Theme---------------------------------------------------------------------->

        #region Mission Theme
        public IActionResult AdminMissionThemeHome()
        {
            List<AdminMissionThemeDisplayVm> themes = _adminMissionThemeRepository.GetMissionThemes();
            return View(themes);
        }

        #region Theme Add
        public IActionResult AdminMissionThemeAdd()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AdminMissionThemeAdd(AdminMissionThemeCreateVm obj)
        {
           string response =  _adminMissionThemeRepository.AddTheme(obj);
            if (string.IsNullOrEmpty(response))
            {
                return View(obj);
            }
            return RedirectToAction("AdminMissionThemeHome");
        }
        #endregion

        #region Theme Edit
        public IActionResult AdminMissionThemeEdit(long themeid)
        {
            AdminMissionThemeCreateVm vm =_adminMissionThemeRepository.GetTheme(themeid);
            if (vm != null)
            {
                return View(vm);
            }
            return View();
        }

        [HttpPost]
        public IActionResult AdminMissionThemeEdit(AdminMissionThemeCreateVm obj)
        {
            string? response = _adminMissionThemeRepository.EditTheme(obj);
            if (string.IsNullOrEmpty(response))
            {
                return View(obj);
            }
            return RedirectToAction("AdminMissionThemeHome");
        }
        #endregion

        public string AdminMissionThemeDelete(long themeid)
        {
            string? reply = string.Empty;
            string? response = _adminMissionThemeRepository.DeleteTheme(themeid);
            if (string.IsNullOrEmpty(response))
            {
                //reply = string.Empty;
                return reply;
            }
            reply = response;
            return reply;
        }

        #endregion

        //<------------------------------------------------------------------------Mission Skills---------------------------------------------------------------------->

        #region Mission Skill

        public IActionResult AdminMissionSkillsHome()
        {
            List<AdminMissionSkillsDisplayVm> skills = _adminMissionSkillsRepository.GetMissionSkills();
            return View(skills);
        }

        #region Mission Skills Add
        public IActionResult AdminMissionSkillsAdd()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AdminMissionSkillsAdd(AdminMissionSkillsCreateVm obj)
        {
            string? response = _adminMissionSkillsRepository.AddSkill(obj);
            if (string.IsNullOrEmpty(response))
            {
                return View(obj);
            }
            return RedirectToAction("AdminMissionSkillsHome");
        }
        #endregion


        #region Mission Skills Edit
        public IActionResult AdminMissionSkillsEdit(long skillid)
        {
            AdminMissionSkillsCreateVm getskill = _adminMissionSkillsRepository.GetSkills(skillid);
            if (getskill != null)
            {
                return View(getskill);
            }
            return View();
        }

        [HttpPost]
        public IActionResult AdminMissionSkillsEdit(AdminMissionSkillsCreateVm obj)
        {
            string? response = _adminMissionSkillsRepository.EditSkill(obj);
            if (string.IsNullOrEmpty(response))
            {
                return View(obj);
            }
            return RedirectToAction("AdminMissionSkillsHome");
        }

        #endregion

        public string AdminMissionSkillsDelete(long skillid)
        {
            string? reply = string.Empty;
            string response = _adminMissionSkillsRepository.DeleteSkill(skillid);
            if (string.IsNullOrEmpty(response))
            {
                return reply;
            }
            reply = response;
            return reply;
        }


        #endregion  

        //<------------------------------------------------------------------------Mission Application---------------------------------------------------------------------->
        public IActionResult AdminMissionApplicationHome()
        {
            List<AdminMissionApplicationDisplayVm> applications = _adminMissionApplicationRepository.GetMissionApplications();
            return View(applications);
        }
        
        public IActionResult AdminApplicationApprove(long applicationid)
        {
            string response = _adminMissionApplicationRepository.ApproveApplication(applicationid);
            if (string.IsNullOrEmpty(response))
            {
                ViewBag.Message = "An Error occured";
                return RedirectToAction("AdminMissionApplicationHome");

            }
            ViewBag.Message = response;
            return RedirectToAction("AdminMissionApplicationHome");
        }

        public IActionResult AdminApplicationDecline(long applicationid)
        {
            string response = _adminMissionApplicationRepository.DeclineApplication(applicationid);
            if (string.IsNullOrEmpty(response))
            {
                return RedirectToAction("AdminMissionApplicationHome");
            }
            return RedirectToAction("AdminMissionApplicationHome");
        }
        
    }
}
