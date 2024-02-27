using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace project_webbservice.Models
{

    public class Like
    {

        [Key]
        public int LikeID { get; set; }
        public int UserID { get; set; }
        public int AudioID { get; set; }
        public DateTime LikedAt { get; set; } = DateTime.Now;

        public User? User { get; set; }
        public Audio? Audio { get; set; }

    }


}