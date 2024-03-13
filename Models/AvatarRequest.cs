using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace project_webbservice.Models
{
    public class AvatarRequest
    {
        public int AvatarId { get; set; }
        public string? UserId { get; set; }
    }
}