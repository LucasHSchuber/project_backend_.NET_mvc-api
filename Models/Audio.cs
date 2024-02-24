using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace project_webbservice.Models
{

    public class Audio
    {

        public int AudioID { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public TimeSpan Duration { get; set; }
        public string? ImageName { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;


        [NotMapped]
        // [Display(Name = "Bild")]
        public IFormFile? ImageFile { get; set; } // audio image
        public string? FilePath { get; set; }


        [NotMapped]
        // [Display(Name = "Ljud")]
        public IFormFile? AudioFile { get; set; } // Property to hold the uploaded audio file
        public byte[]? AudioData { get; set; } // Property to store the binary data of the audio file


        public int CategoryID { get; set; }

        // [ForeignKey("CategoryID")]
        public Category? Category { get; set; }

        public int UserId { get; set; }

        // [ForeignKey("UserId")]
        public ICollection<User>? Users { get; set; }
        public ICollection<UserAudio>? UserAudios { get; set; }
        public ICollection<Like>? Likes { get; set; }

    }


}