using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;


namespace project_webbservice.Models
{
    public class Avatar
    {
        [Key]
        public int AvatarId { get; set; }

        public string? AvatarImageName { get; set; }

        [NotMapped]
        public IFormFile? AvatarImageFile { get; set; }


    }
}
