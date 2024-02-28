
namespace projekt_webbservice.DTOs
{
    public class AudioDto
    {
        public int AudioID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime Created { get; set; }
        public string ImageName { get; set; }
        public string ImageNameOriginal { get; set; }
        public string FilePath { get; set; }
        public string CategoryName { get; set; }
    }
}
