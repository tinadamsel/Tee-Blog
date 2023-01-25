using Core.DbContext;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace TEEBLOG.Controllers
{
    public class VideoController : Controller
    {
        private readonly AppDBContext _context;

        public VideoController(AppDBContext context)
        {
            _context = context;
        }

        public IActionResult Video()
        {
            return View();
        }
        public IActionResult AdminVideoPage()
        {
            IEnumerable<Video>videos = _context.Videos.ToList();
            return View(videos);
        }
        [HttpGet]
        public IActionResult AddVideo()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddNewVideo(Video video)
        {
            if (video != null)
            {
                _context.Videos.Add(video);
                _context.SaveChanges();
                return RedirectToAction("AdminVideoPage");
            }
            return View(video);
        }
    }
}
