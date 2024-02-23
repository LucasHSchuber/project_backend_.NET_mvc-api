using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace project_webbservice.Models
{
    public class UserAudio
    {
        [Key]
        public int UserAudioId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User? User { get; set; }

        [ForeignKey("Audio")]
        public int AudioId { get; set; }
        public Audio? Audio { get; set; }
    }
}
