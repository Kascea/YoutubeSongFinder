namespace MontageWebsite.Models
{
    public class Video
    {
        public string ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ChannelID { get; set; }
        public string ChannelTitle { get; set; }
        public string ThumbnailUrl { get; set; }
        public long? ThumbnailHeight { get; set; }
        public long? ThumbnailWidth { get; set; } 
        public string Status { get; set; }

        public Video()
        {
            ID = String.Empty;
            Title = String.Empty;
            Description = String.Empty;
            ChannelID = String.Empty;
            ChannelTitle = String.Empty;
            ThumbnailUrl = String.Empty;
            ThumbnailHeight = 0;
            ThumbnailWidth = 0;
            Status = String.Empty;
        }
    }
}
