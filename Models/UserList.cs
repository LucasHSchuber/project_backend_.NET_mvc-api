using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace project_webbservice.Models
{

    public class UserList
    {

        [Key]
        public int ListID { get; set; }
        public int UserID { get; set; }
        public string? ListName { get; set; }

        public User? User { get; set; }
        public ICollection<UserListAudio>? UserListAudios { get; set; }

    }


}