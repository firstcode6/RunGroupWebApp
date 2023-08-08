using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using RunGroupWebApp.Data;
using RunGroupWebApp.Interfaces;
using RunGroupWebApp.Models;
using RunGroupWebApp.Repository;
using RunGroupWebApp.ViewModels;

namespace RunGroupWebApp.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardRepository _dashBoardRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPhotoService _photoService;

        public DashboardController(IDashboardRepository dashBoardRepository, IHttpContextAccessor httpContextAccessor, IPhotoService photoService)
        {
            _dashBoardRepository = dashBoardRepository;
            _httpContextAccessor = httpContextAccessor;
            _photoService = photoService;
        }

        private void MapUserEdit(AppUser user, EditUserDashboardVM editVM, ImageUploadResult photoResult)
        {
            user.Id = editVM.Id;
            user.Pace = editVM.Pace;
            user.Mileage = editVM.Mileage;
            user.ProfileImageUrl = photoResult.Url.ToString();
            user.City = editVM.City;
            user.State = editVM.State;
        }

        public async Task<IActionResult> Index()
        {
            var userRaces = await _dashBoardRepository.GetAllUserRaces();
            var userClubs = await _dashBoardRepository.GetAllUserClubs();
            var dashboardVM = new DashboardVM()
            {
                Races = userRaces,
                Clubs = userClubs
            };

            return View(dashboardVM);
        }

        public async Task<IActionResult> EditUserProfile()
        {
            var currUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            var user = await _dashBoardRepository.GetUserById(currUserId); //AppUser
            if (user == null) return View("Error");
            var editUserVM = new EditUserDashboardVM()
            {
                Id = currUserId,
                Pace = user.Pace,
                Mileage = user.Mileage,
                ProfileImageUrl = user.ProfileImageUrl,
                City = user.City,
                State = user.State,
            };
            return View(editUserVM);

        }

        [HttpPost]
        public async Task<IActionResult> EditUserProfile(EditUserDashboardVM editVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit profile");
                return View("EditUserProfile", editVM);
            }
            var user = await _dashBoardRepository.GetByIdNoTracking(editVM.Id); // AppUser

            // if user doesn't have
            if (user.ProfileImageUrl == "" || user.ProfileImageUrl == null)
            {
                var photoResult = await _photoService.AddPhotoAsync(editVM.Image);
                // Optimistic Concurrency - "Tracking error"
                // use no Tracking
                MapUserEdit(user, editVM, photoResult);
                _dashBoardRepository.Update(user);
                return RedirectToAction("Index");
            }
            else
            {
                try
                {
                    await _photoService.DeletePhotoAsync(user.ProfileImageUrl);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Could not delete photo");
                    return View(editVM);
                }
                var photoResult = await _photoService.AddPhotoAsync(editVM.Image);
                MapUserEdit(user, editVM, photoResult);
                _dashBoardRepository.Update(user);
                return RedirectToAction("Index");

            }
        }
    }
}
