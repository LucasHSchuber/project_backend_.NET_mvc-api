using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace project_webbservice.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string? Name { get; set; }

        // Navigation property
        public ICollection<Audio>? Auidos { get; set; }
    }


}