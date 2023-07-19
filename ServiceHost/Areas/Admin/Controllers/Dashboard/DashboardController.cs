using _0_Framework.Application;
using BookShop.Areas.Admin.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsManagement.Application.Contrasts.News;
using NewsManagement.Application.Contrasts.NumberOfVisitChart;
using NewsWebsite.Common;
using System.ComponentModel;

namespace ServiceHost.Areas.Admin.Controllers.Dashboard
{
    [DisplayName("داشبورد")]
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly INewsApplication _newsApplication;

        public DashboardController(INewsApplication newsApplication)
        {
            _newsApplication = newsApplication;
        }

        [Area("Admin")]
        [Route("admin/dashboard/index")]
        [HttpGet, DisplayName("مشاهده")]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public IActionResult Index()
        {
            ViewBag.News = _newsApplication.CountNews();
            ViewBag.NewsPublished = _newsApplication.GetPublished();
            ViewBag.FuturePublishedNews = _newsApplication.CountFuturePublish();
            ViewBag.DraftNews = _newsApplication.CountNewsUnPublished();


            var month = StringExtensions.GetMonth();
            long numberOfVisit;
            var year = DateTimeExtensions.ConvertMiladiToShamsi(DateTime.Now, "yyyy");
            DateTime StartDateTimeMiladi;
            DateTime EndDateTimeMiladi;
            var numberOfVisitList = new List<NumberOfVisitChartViewModel>();

            for (int i = 0; i < month.Length; i++)
            {
                StartDateTimeMiladi = DateTimeExtensions.ConvertShamsiToMiladi($"{year}/{i + 1}/01");
                if (i < 11)
                    EndDateTimeMiladi = DateTimeExtensions.ConvertShamsiToMiladi($"{year}/{i + 2}/01");
                else
                    EndDateTimeMiladi = DateTimeExtensions.ConvertShamsiToMiladi($"{year}/01/01");

                numberOfVisit = _newsApplication.NumberOfVisit(StartDateTimeMiladi, EndDateTimeMiladi);
                numberOfVisitList.Add(new NumberOfVisitChartViewModel { Name = month[i], Value = numberOfVisit });
            }

            ViewBag.NumberOfVisitChart = numberOfVisitList;
            return View();
        }
    }
}
