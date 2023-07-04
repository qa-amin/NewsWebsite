using AccountManagement.Application;
using AccountManagement.Application.Contrast.User;
using Microsoft.AspNetCore.Mvc;
using NewsManagement.Application.Contrasts.Tag;
using Newtonsoft.Json;

namespace ServiceHost.Areas.Admin.Controllers.Tag
{
    public class TagController : Controller
    {
        private readonly ITagApplication _tagApplication;

        public TagController(ITagApplication tagApplication)
        {
            _tagApplication = tagApplication;
        }

        [Area("Admin")]
        [Route("admin/tag/index")]
        public IActionResult Index()
        {
            return View();
        }
        [Area("Admin")]
        [Route("admin/tag/GetTags")]
        public JsonResult GetTags(string search, string order, int offset, int limit, string sort)
        {
            var searchModel = new TagSearchModel()
            {
                Limit = limit,
                Sort = sort,
                Offset = offset,
                Order = order,
                Search = search
            };
            var (tags, total) = _tagApplication.Search(searchModel);

            return Json(new { total = total, rows = tags });
        }



        [Area("admin")]
        [Route("admin/tag/Create")]
        [HttpGet]
        public IActionResult Create()
        {
            return PartialView("_Create");
        }


        [Area("admin")]
        [Route("admin/tag/Create")]
        [HttpPost]
        public IActionResult Create(CreateTag command)
        {

            if (ModelState.IsValid)
            {
                var result = _tagApplication.Create(command);
                TempData["ShowMassage"] = JsonConvert.SerializeObject(result);
            }

            

            return PartialView("_Create", command);


        }



        [Area("admin")]
        [Route("admin/tag/Edit")]
        [HttpGet]
        public IActionResult Edit(long id)
        {
            var tags = _tagApplication.GetDetails(id);

            return PartialView("_Edit", tags);
        }


        [Area("admin")]
        [Route("admin/tag/Edit")]
        [HttpPost]
        public IActionResult Edit(EditTag command)
        {
           

            if (ModelState.IsValid)
            {
                var result = _tagApplication.Edit(command);

                TempData["ShowMassage"] = JsonConvert.SerializeObject(result);
            }

            return PartialView("_Edit", command);



        }



        [Area("admin")]
        [Route("admin/tag/Delete")]
        [HttpGet]
        public IActionResult Delete(long id)
        {

            var user = _tagApplication.GetDetails(id);

            return PartialView("_Delete", user);
        }


        [Area("admin")]
        [Route("admin/tag/Delete")]
        [HttpPost]
        public void Delete(EditTag command)
        {

            var result = _tagApplication.Delete(command.Id);



            TempData["ShowMassage"] = JsonConvert.SerializeObject(result);



        }
    }
}
