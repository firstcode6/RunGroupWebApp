using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RunGroupWebApp.Interfaces;
using RunGroupWebApp.Models;
using RunGroupWebApp.ViewModels;

namespace RunGroupWebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
       // private readonly UserManager<AppUser> _userManager;
      // private readonly IHttpContextAccessor _httpContextAccessor;
        //private readonly IPhotoService _photoService;
        // UserManager<AppUser> userManager
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            // _userManager = userManager;
           // _httpContextAccessor = httpContextAccessor;
            //_photoService = photoService;
        }

        [HttpGet("users")]
        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.GetAllUsers();
            List<UserVM> result = new List<UserVM>();
            foreach (var user in users)
            {
                var userVM = new UserVM()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Pace = user.Pace,
                    Mileage = user.Mileage,
                    ProfileImageUrl = user.ProfileImageUrl,
                };
                result.Add(userVM);
            }
            return View(result);
        }

        public async Task<IActionResult> Detail(string id)
        {
            var user = await _userRepository.GetUserById(id);
            var userDetailVM = new UserDetailVM()
            {
                Id = user.Id,
                UserName = user.UserName,
                Pace = user.Pace,
                Mileage = user.Mileage,
                ProfileImageUrl = user.ProfileImageUrl,

            };
            return View(userDetailVM);
        }


        //public async Task<IActionResult> EditProfile()
        //{
        //    var currUserId = _httpContextAccessor.HttpContext.User.GetUserId();
        //    var user = await _userRepository.GetUserById
        //    return View();

        //}

    }
}
