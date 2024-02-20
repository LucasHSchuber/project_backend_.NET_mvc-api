using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace project_webbservice.Models
{

    public class UserListAudio
    {

        //properties
        public int UserListAudioID { get; set; }
        public int ListID { get; set; }
        public int AudioID { get; set; }

        public UserList? UserList { get; set; }
        public Audio? Audio { get; set; }

    }
}