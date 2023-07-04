using Microsoft.AspNetCore.Mvc;
using NewsManagement.Application.Contrasts.Tag;
using NewsManagement.Application.Contrasts.Video;
using Newtonsoft.Json;

namespace ServiceHost.Areas.Admin.Controllers.Video
{
    public class VideoController : Controller
    {
        private readonly IVideoApplication _videoApplication;

        public VideoController(IVideoApplication videoApplication)
        {
            _videoApplication = videoApplication;
        }

        [Area("Admin")]
        [Route("admin/video/index")]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [Area("Admin")]
        [Route("admin/video/GetVideos")]
        [HttpGet]
        public JsonResult GetVideos(string search, string order, int offset, int limit, string sort)
        {
            var searchModel = new VideoSearchModel()
            {
                Limit = limit,
                Sort = sort,
                Offset = offset,
                Order = order,
                Search = search
            };
            var (videos, total) = _videoApplication.Search(searchModel);
            return Json(new {total= total , rows = videos});
        }


        [Area("Admin")]
        [Route("admin/video/Create")]
        [HttpGet]
        public IActionResult Create()
        {
            return PartialView("_Create", new CreateVideo());
        }
        [Area("Admin")]
        [Route("admin/video/Create")]
        [HttpPost]
        public IActionResult Create(CreateVideo command)
        {
            if (ModelState.IsValid)
            {
                var result = _videoApplication.Create(command);
                if (result.IsSucceeded)
                {
                    TempData["ShowMassage"] = JsonConvert.SerializeObject(result);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, result.Message);
                }
            }
            return PartialView("_Create", command);
        }




        [Area("Admin")]
        [Route("admin/video/Edit")]
        [HttpGet]
        public IActionResult Edit(long id)
        {
            var video = _videoApplication.GetDetails(id);
            return PartialView("_Edit", video);
        }
        [Area("Admin")]
        [Route("admin/video/Edit")]
        [HttpPost]
        public IActionResult Edit(EditVideo command)
        {
            ModelState.Remove("PosterFile");
            if (ModelState.IsValid)
            {
                var result = _videoApplication.Edit(command);
                if (result.IsSucceeded)
                {
                    TempData["ShowMassage"] = JsonConvert.SerializeObject(result);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, result.Message);
                }
            }
            return PartialView("_Edit", command);
        }


        [Area("Admin")]
        [Route("admin/video/Delete")]
        [HttpGet]
        public IActionResult Delete(long id)
        {
            var video = _videoApplication.GetDetails(id);
            return PartialView("_Delete", video);
        }
        [Area("Admin")]
        [Route("admin/video/Delete")]
        [HttpPost]
        public void Delete(EditVideo command)
        {
           
            var result = _videoApplication.Delete(command.Id);
            TempData["ShowMassage"] = JsonConvert.SerializeObject(result);
                    
            
            
        }
    }
}
