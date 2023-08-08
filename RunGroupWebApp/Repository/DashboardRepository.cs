using Microsoft.EntityFrameworkCore;
using RunGroupWebApp.Data;
using RunGroupWebApp.Interfaces;
using RunGroupWebApp.Models;

namespace RunGroupWebApp.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DashboardRepository(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<List<Club>> GetAllUserClubs()
        {
            //var currUser = _httpContextAccessor.HttpContext?.User;
            //var userClubs = _context.Clubs.Where(c => c.AppUser.Id == currUser.ToString());
            //return userClubs.ToList();

            var currUserId = _httpContextAccessor.HttpContext?.User.GetUserId();
            var userClubs = _context.Clubs.Where(c => c.AppUser.Id == currUserId);
            return userClubs.ToList();
        }

        public async Task<List<Race>> GetAllUserRaces()
        {
            var currUserId = _httpContextAccessor.HttpContext?.User.GetUserId();
            var userRaces = _context.Races.Where(c => c.AppUser.Id == currUserId);
            return userRaces.ToList();
        }

        public async Task<AppUser> GetByIdNoTracking(string id)
        {
           return await _context.Users.Where(u => u.Id ==id).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<AppUser> GetUserById(string id)
        {
           return await _context.Users.FindAsync(id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        } 

        public bool Update(AppUser user)
        {
            _context.Update(user);
            return Save();
        }
    }
}
