using RunGroupWebApp.Models;

namespace RunGroupWebApp.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Club>? Clubs { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public HomeUserCreateVM Register { get; set; } = new HomeUserCreateVM();
    }
}
