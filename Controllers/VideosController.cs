using Microsoft.AspNetCore.Mvc;
using Google.Apis.YouTube.v3;
using Google.Apis.Services;
using MontageWebsite.Models;

namespace MontageWebsite.Controllers
{
    public class VideosController : Controller
    {
        private YouTubeService youtubeService;

        public VideosController()
        {
            youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = Environment.GetEnvironmentVariable("YOUTUBE_API_KEY", EnvironmentVariableTarget.Machine),
                ApplicationName = this.GetType().ToString()
            });
        }

        public async Task<IActionResult> Index(string searchTerm)
        {
            List<Video> videos = new List<Video>();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                videos = await GetVideosAsync(searchTerm);
            }
            return View(videos);
        }

        private async Task<List<Video>> GetVideosAsync(string searchTerm)
        {
            var searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.Q = searchTerm;
            searchListRequest.MaxResults = 10;

            var searchListResponse = await searchListRequest.ExecuteAsync();

            List<Video> videos = new List<Video>();
            foreach (var searchResult in searchListResponse.Items)
            {
                if (searchResult.Id.Kind == "youtube#video")
                {
                    Video video = new Video()
                    {
                        VideoID = searchResult.Id.VideoId,
                        Title = searchResult.Snippet.Title,
                        Description = searchResult.Snippet.Description,
                        ChannelID = searchResult.Id.ChannelId,
                        ChannelTitle = searchResult.Snippet.ChannelTitle,
                        Submitted = true
                    };
                    videos.Add(video);
                }
            }

            return videos;
        }
    }
}
