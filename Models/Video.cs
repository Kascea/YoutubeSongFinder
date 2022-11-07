namespace MontageWebsite.Models
{
    public class Video
    {
        public string VideoID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ChannelID { get; set; }
        public string ChannelTitle { get; set; }
        public bool Approved { get; set; }
        public bool Submitted { get; set; }

        public Video()
        {
            VideoID = String.Empty;
            Title = String.Empty;
            Description = String.Empty;
            ChannelID = String.Empty;
            ChannelTitle = String.Empty;
            Approved = false;
            Submitted = false;
        }
    }
}
