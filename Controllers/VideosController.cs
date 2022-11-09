using Microsoft.AspNetCore.Mvc;
using Google.Apis.YouTube.v3;
using Google.Apis.Services;
using MontageWebsite.Models;

namespace MontageWebsite.Controllers
{
    public class VideosController : Controller
    {
        private static VideoService YTService;
        private static List<Video> Videos;

        public VideosController()
        {
            if(YTService == null)
            {
                YTService = new VideoService();
            }
            if(Videos == null)
            {
                Videos = new List<Video>();
            }
        }

        public async Task<IActionResult> Index(string searchTerm)
        {
            if (!string.IsNullOrEmpty(searchTerm))
            {
                Videos = await YTService.GetVideosAsync(searchTerm);
            }
            return View(Videos);
        }

        [HttpPost]
        public IActionResult SubmitVideoRequest(IFormCollection formval)
        {
            foreach(Video video in Videos)
            {
                if(formval.ContainsKey(video.VideoID))
                {
                    //Add video to requested database

                    video.Submitted = true;
                }
            }
            return View("Index", Videos);
        }

    }
}
