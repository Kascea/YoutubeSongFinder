using Microsoft.AspNetCore.Mvc;
using Google.Apis.YouTube.v3;
using Google.Apis.Services;
using MontageWebsite.Models;
using System.Linq;

namespace MontageWebsite.Controllers
{
    public class VideosController : Controller
    {
        private static VideoService YTService;
        private static DBService dbService;
        private static List<Video> Videos;

        public VideosController()
        {
            if(YTService == null)
            {
                YTService = new VideoService();
            }
            if (dbService == null)
            {
                dbService = new DBService();
            }
            if (Videos == null)
            {
                Videos = new List<Video>();
            }
        }

        public async Task<IActionResult> Index(string searchTerm)
        {
            if (!string.IsNullOrEmpty(searchTerm))
            {
                Videos = await YTService.GetVideosAsync(searchTerm);
                Dictionary<string, string> statuses = dbService.GetStatuses(Videos);
                foreach(Video video in Videos)
                {
                    if(statuses.ContainsKey(video.ID))
                    {
                        video.Status = statuses[video.ID];
                    }
                }
            }
            return View(Videos);
        }

        [HttpPost]
        public IActionResult SubmitVideoRequest(IFormCollection formval)
        {
            foreach(Video video in Videos)
            {
                if(formval.ContainsKey(video.ID))
                {
                    bool isSubmitted = dbService.InsertSubmittedVideo(video);
                    if(isSubmitted)
                    {
                        video.Status = "Submitted";
                    }
                }
            }
            return RedirectToAction("Index");
        }

    }
}
