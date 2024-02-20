using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace project_webbservice.Models
{

    public class User
    {

        //properties
        [Key]
        public int UserId { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public string? UserInfo { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;

        public string? ImageName { get; set; }

        [NotMapped]
        // [Display(Name = "Bild")]
        public IFormFile? ImageFile { get; set; }
        // public int avatar { get; set; }

        // Navigation property
        public ICollection<UserList>? Lists { get; set; }
        public ICollection<Like>? Likes { get; set; }


    }
}