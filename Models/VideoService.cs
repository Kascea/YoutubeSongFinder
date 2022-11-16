using Google.Apis.Services;
using Google.Apis.YouTube.v3;

namespace MontageWebsite.Models
{
    public class VideoService
    {
        private YouTubeService youtubeService;
        public VideoService()
        {
            youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = Environment.GetEnvironmentVariable("YOUTUBE_API_KEY", EnvironmentVariableTarget.Machine),
                ApplicationName = this.GetType().ToString()
            });
        }

        public async Task<List<Video>> GetVideosAsync(string searchTerm)
        {
            var searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.Q = searchTerm;
            searchListRequest.Type = "video";
            searchListRequest.MaxResults = 10;

            var searchListResponse = await searchListRequest.ExecuteAsync();

            List<Video> videos = new List<Video>();
            foreach (var searchResult in searchListResponse.Items)
            {
                if (searchResult.Id.Kind == "youtube#video")
                {
                    Video video = new Video()
                    {
                        ID = searchResult.Id.VideoId,
                        Title = searchResult.Snippet.Title,
                        Description = searchResult.Snippet.Description,
                        ChannelID = searchResult.Id.ChannelId,
                        ChannelTitle = searchResult.Snippet.ChannelTitle,
                        ThumbnailUrl = searchResult.Snippet.Thumbnails.Medium.Url,
                        ThumbnailHeight = searchResult.Snippet.Thumbnails.Medium.Height,
                        ThumbnailWidth = searchResult.Snippet.Thumbnails.Medium.Width
                    };
                    videos.Add(video);
                }
            }
            return videos;
        }
    }
}
