﻿namespace RunGroupWebApp.ViewModels
{
    public class EditProfileVM
    {
        public int? Pace { get; set; }
        public int? Mileage { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public IFormFile? Image { get; set; }
    }
}
