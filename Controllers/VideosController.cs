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

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Search()
        {
            return View();
        }

        public async Task<IActionResult> GetVideos(string searchTerm)
        {
            var searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.Q = searchTerm;
            searchListRequest.MaxResults = 5;

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
                        Thumbnail =  new Thumbnail()
                        {
                            VideoID = searchResult.Id.VideoId,
                            Url = searchResult.Snippet.Thumbnails.Default__.Url,
                            Width = searchResult.Snippet.Thumbnails.Default__.Width,
                            Height = searchResult.Snippet.Thumbnails.Default__.Height
                        }
                    };
                    videos.Add(video);
                }
            }

            return View("Index", videos.ToList());
        }
    }
}
