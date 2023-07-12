using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using _0_Framework.Domain;
using _0_Framework.Infrastructure;
using AccountManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using NewsManagement.Application.Contrasts.News;
using NewsManagement.Application.Contrasts.News;
using NewsManagement.Domain.NewsAgg;
using NewsManagement.Domain.NewsAgg;
using NewsManagement.Domain.NewsNewsCategoryAgg;
using NewsManagement.Domain.NewsTagAgg;
using NewsManagement.Domain.TagAgg;
using NewsManagement.Infrastructure.EFCore.Migrations;
using NewsWebsite.Common;
using NewsWebsite.Entities;
using AccountManagement.Domain.UserAgg;
using AccountManagement.Domain.UserRoleAgg;
using NewsManagement.Domain.CommentAgg;
using NewsManagement.Domain.LikeAgg;
using Microsoft.EntityFrameworkCore.Metadata;
using NewsManagement.Domain.BookMarkAgg;
using NewsManagement.Domain.VisitAgg;
using System.Linq.Dynamic.Core;
using System.Security.Cryptography;

namespace NewsManagement.Infrastructure.EFCore.Repository
{
    public class NewsRepository : RepositoryBase<long,News>, INewsRepository
    {
        private readonly NewsManagementDbContext _context;
        private readonly AccountManagementDbContext _managementDbContext;
        private INewsRepository _newsRepositoryImplementation;

        public NewsRepository( NewsManagementDbContext context, AccountManagementDbContext managementDbContext) : base(context)
        {
            _context = context;
            _managementDbContext = managementDbContext;
        }

        public (List<NewsViewModel>, int) Search(NewsSearchModel searchModel)
        {
            
            List<NewsViewModel> news;
            int total = _context.News.Count();
            if (!searchModel.Search.HasValue())
                searchModel.Search = "";

            if (searchModel.Limit == 0)
                searchModel.Limit = total;

            if (searchModel.Sort == "ShortTitle")
            {
                if (searchModel.Order == "asc")
                    news = GetPaginateNews(searchModel.Offset, searchModel.Limit, "ShortTitle", searchModel.Search, null, null);
                else
                    news = GetPaginateNews(searchModel.Offset, searchModel.Limit, "ShortTitle desc", searchModel.Search, null, null);
            }

            else if (searchModel.Sort == "بازدید")
            {
                if (searchModel.Order == "asc")
                    news = GetPaginateNews(searchModel.Offset, searchModel.Limit, "NumberOfVisit", searchModel.Search, null, null);
                else
                    news = GetPaginateNews(searchModel.Offset, searchModel.Limit, "NumberOfVisit desc", searchModel.Search, null, null);
            }

            else if (searchModel.Sort == "لایک")
            {
                if (searchModel.Order == "asc")
                    news = GetPaginateNews(searchModel.Offset, searchModel.Limit, "NumberOfLike", searchModel.Search, null, null);
                else
                    news = GetPaginateNews(searchModel.Offset, searchModel.Limit, "NumberOfLike desc", searchModel.Search, null, null);
            }

            else if (searchModel.Sort == "دیس لایک")
            {
                if (searchModel.Order == "asc")
                    news = GetPaginateNews(searchModel.Offset, searchModel.Limit, "NumberOfDisLike", searchModel.Search, null, null);
                else
                    news = GetPaginateNews(searchModel.Offset, searchModel.Limit, "NumberOfDisLike desc", searchModel.Search, null, null);
            }

            else if (searchModel.Sort == "تاریخ انتشار")
            {
                if (searchModel.Order == "asc")
                    news = GetPaginateNews(searchModel.Offset, searchModel.Limit, "PublishDateTime", searchModel.Search, null, null);
                else
                    news = GetPaginateNews(searchModel.Offset, searchModel.Limit, "PublishDateTime desc", searchModel.Search, null, null);
            }

            else if (searchModel.Sort == "نظرات")
            {
                if (searchModel.Order == "asc")
                    news = GetPaginateNews(searchModel.Offset, searchModel.Limit, "NumberOfComments", searchModel.Search, null, null);
                else
                    news = GetPaginateNews(searchModel.Offset, searchModel.Limit, "NumberOfComments desc", searchModel.Search, null, null);
            }

            else
                news = GetPaginateNews(searchModel.Offset, searchModel.Limit, "PublishDateTime desc", searchModel.Search, null, null);



            if (searchModel.Search != "")
                total = news.Count();


            return (news,total);

        }

        public void Update(News Enitiy)
        {
	        _context.Update(Enitiy);
        }

        public List<NewsViewModel> GetPaginateNews(int offset, int limit, string orderBy, string searchText,
            bool? isPublish, bool? isInternal)
        {
            var listTags = _context.Tags.ToList();
            var newsCategories = _context.NewsCategories.ToList();
            var users = _managementDbContext.Users.ToList();
            var comments = _context.Comments.ToList();
            var likes = _context.Likes.ToList();
            var visits = _context.Visits.ToList();



            var getDateTimesForSearch = searchText.GetDateTimeForSearch();

            var convertPublish = Convert.ToBoolean(isPublish);
            var convertInternal = Convert.ToBoolean(isInternal);
            var news = _context.News.Include(p => p.NewsNewsCategories).Include(p => p.NewsTags)
                .Where(p =>
                (p.Title.Contains(searchText) 
                || (p.PublishDateTime >= getDateTimesForSearch.First() &&
                                                 p.PublishDateTime <= getDateTimesForSearch.Last()))
                && (isInternal == null || (convertInternal ? p.IsInternal : !p.IsInternal))
                
                && (isPublish == null || (convertPublish ? p.IsPublish && p.PublishDateTime <= DateTime.Now : !p.IsPublish))
                )
                .Select(p => new NewsViewModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    Abstract = p.Abstract,
                    ShortTitle = p.Title.Length > 50 ? p.Title.Substring(0, 50) + "..." : p.Title,
                    Url = p.Url,
                    ImageName = p.ImageName,
                    Description = p.Description,
                    IsPublish = p.IsPublish,
                    AuthorName =GetUserName(p.UserId,users),
                    PublishDateTime = p.PublishDateTime,
                    NameOfCategories = GetCategoriesName(p.NewsNewsCategories, p.Id, newsCategories),
                    NameOfTags = GetTagsName(p.NewsTags, p.Id, listTags),
                    NewsType = p.IsInternal ? "داخلی" : "خارجی",
                    Status = p.IsPublish == false ? "پیش نویس" : ( p.PublishDateTime <= DateTime.Now ? "منتشر" : "انتشار در آینده") ,
                    PersianPublishDate = p.PublishDateTime.ConvertMiladiToShamsi("yyyy/MM/dd ساعت HH:mm"),
                    NumberOfVisit = GetNumberOfVisit(p.Id, visits),
                    NumberOfLike = GetNumberOfLike(p.Id,likes),
                    NumberOfDisLike = GetNumberOfDisLike(p.Id, likes),
                    NumberOfComments = GetNumberOfComments(p.Id, comments),



                }).ToList();

            switch (orderBy)
            {
                case "ShortTitle":
	                news = news.OrderBy(p => p.ShortTitle).ToList();
                    break;
                case "ShortTitle desc":
	                news = news.OrderByDescending(p => p.ShortTitle).ToList();
	                break;
				case "NumberOfVisit":
	                news = news.OrderBy(p => p.NumberOfVisit).ToList();
                    break;
                case "NumberOfVisit desc":
	                news = news.OrderByDescending(p => p.NumberOfVisit).ToList();
	                break;

				case "NumberOfLike":
	                news = news.OrderBy(p => p.NumberOfLike).ToList();
                    break;
                case "NumberOfLike desc":
	                news = news.OrderByDescending(p => p.NumberOfLike).ToList();
	                break;
				case "NumberOfDisLike":
                    news = news.OrderBy(p => p.NumberOfDisLike).ToList();
                    break;
                case "NumberOfDisLike desc":
	                news = news.OrderByDescending(p => p.NumberOfDisLike).ToList();
	                break;
				case "NumberOfComments":
	                news = news.OrderBy(p => p.NumberOfComments).ToList();
                    break;
                case "NumberOfComments desc":
	                news = news.OrderByDescending(p => p.NumberOfComments).ToList();
	                break;
				case "PublishDateTime":
	                news = news.OrderBy(p => p.PublishDateTime).ToList();
                    break;
                case "PublishDateTime desc":
	                news = news.OrderByDescending(p => p.PublishDateTime).ToList();
                    break;

			}

            news = news.Skip(offset).Take(limit).ToList();


			foreach (var item in news)
                item.Row = ++offset;

            return news;
        }

        public  List<NewsViewModel> MostViewedNews(int offset, int limit, string duration)
        {
			
			DateTime StartMiladiDate;
			DateTime EndMiladiDate = DateTime.Now;


			if (duration == "week")
			{
				int NumOfWeek = DateTimeExtensions.ConvertMiladiToShamsi(DateTime.Now, "dddd").GetNumOfWeek();
				StartMiladiDate = DateTime.Now.AddDays((-1) * NumOfWeek).Date + new TimeSpan(0, 0, 0);
			}

			else if (duration == "day")
				StartMiladiDate = DateTime.Now.Date + new TimeSpan(0, 0, 0);

			else
			{
				string DayOfMonth = DateTimeExtensions.ConvertMiladiToShamsi(DateTime.Now, "dd").Fa2En();
				StartMiladiDate = DateTime.Now.AddDays((-1) * (int.Parse(DayOfMonth) - 1)).Date + new TimeSpan(0, 0, 0);
			}

			var listTags =  _context.Tags.ToList();
			var newsCategories =  _context.NewsCategories.ToList();
			var users =  _managementDbContext.Users.ToList();
			var comments =  _context.Comments.ToList();
			var likes =  _context.Likes.ToList();
			var visits =  _context.Visits.ToList();




            var news =  _context.News.Include(p => p.NewsNewsCategories).Include(p => p.NewsTags)
                .Where(n => n.PublishDateTime <= EndMiladiDate && StartMiladiDate <= n.PublishDateTime)
                .Select(p => new NewsViewModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    Abstract = p.Abstract,
                    ShortTitle = p.Title.Length > 50 ? p.Title.Substring(0, 50) + "..." : p.Title,
                    Url = p.Url,
                    ImageName = p.ImageName,
                    Description = p.Description,
                    IsPublish = p.IsPublish,
                    AuthorName = GetUserName(p.UserId, users),
                    PublishDateTime = p.PublishDateTime,
                    NameOfCategories = GetCategoriesName(p.NewsNewsCategories, p.Id, newsCategories),
                    NameOfTags = GetTagsName(p.NewsTags, p.Id, listTags),
                    NewsType = p.IsInternal ? "داخلی" : "خارجی",
                    Status = p.IsPublish == false
                        ? "پیش نویس"
                        : (p.PublishDateTime <= DateTime.Now ? "منتشر" : "انتشار در آینده"),
                    PersianPublishDate = p.PublishDateTime.ConvertMiladiToShamsi("yyyy/MM/dd ساعت HH:mm"),
                    NumberOfVisit =  GetNumberOfVisit(p.Id, visits),
                    NumberOfLike = GetNumberOfLike(p.Id, likes),
                    NumberOfDisLike = GetNumberOfDisLike(p.Id, likes),
                    NumberOfComments = GetNumberOfComments(p.Id, comments),



                }).ToList();

            news = news.OrderByDescending(p => p.NumberOfVisit).ToList();
            news = news.Skip(offset).Take(limit).ToList();
            foreach (var item in news)
				item.Row = ++offset;


            return news;
        }

        public List<NewsViewModel> MostTalkNews(int offset, int limit, string duration)
        {
            DateTime StartMiladiDate;
            DateTime EndMiladiDate = DateTime.Now;


            if (duration == "week")
            {
                int NumOfWeek = DateTimeExtensions.ConvertMiladiToShamsi(DateTime.Now, "dddd").GetNumOfWeek();
                StartMiladiDate = DateTime.Now.AddDays((-1) * NumOfWeek).Date + new TimeSpan(0, 0, 0);
            }

            else if (duration == "day")
                StartMiladiDate = DateTime.Now.Date + new TimeSpan(0, 0, 0);

            else
            {
                string DayOfMonth = DateTimeExtensions.ConvertMiladiToShamsi(DateTime.Now, "dd").Fa2En();
                StartMiladiDate = DateTime.Now.AddDays((-1) * (int.Parse(DayOfMonth) - 1)).Date + new TimeSpan(0, 0, 0);
            }

            var listTags = _context.Tags.ToList();
            var newsCategories = _context.NewsCategories.ToList();
            var users = _managementDbContext.Users.ToList();
            var comments = _context.Comments.ToList();
            var likes = _context.Likes.ToList();
            var visits = _context.Visits.ToList();




            var news = _context.News.Include(p => p.NewsNewsCategories).Include(p => p.NewsTags)
                .Where(n => n.PublishDateTime <= EndMiladiDate && StartMiladiDate <= n.PublishDateTime)
                .Select(p => new NewsViewModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    Abstract = p.Abstract,
                    ShortTitle = p.Title.Length > 50 ? p.Title.Substring(0, 50) + "..." : p.Title,
                    Url = p.Url,
                    ImageName = p.ImageName,
                    Description = p.Description,
                    IsPublish = p.IsPublish,
                    AuthorName = GetUserName(p.UserId, users),
                    PublishDateTime = p.PublishDateTime,
                    NameOfCategories = GetCategoriesName(p.NewsNewsCategories, p.Id, newsCategories),
                    NameOfTags = GetTagsName(p.NewsTags, p.Id, listTags),
                    NewsType = p.IsInternal ? "داخلی" : "خارجی",
                    Status = p.IsPublish == false ? "پیش نویس" : (p.PublishDateTime <= DateTime.Now ? "منتشر" : "انتشار در آینده"),
                    PersianPublishDate = p.PublishDateTime.ConvertMiladiToShamsi("yyyy/MM/dd ساعت HH:mm"),
                    NumberOfVisit = GetNumberOfVisit(p.Id, visits),
                    NumberOfLike = GetNumberOfLike(p.Id, likes),
                    NumberOfDisLike = GetNumberOfDisLike(p.Id, likes),
                    NumberOfComments = GetNumberOfComments(p.Id, comments),



                }).ToList();


            news = news.OrderByDescending(p => p.NumberOfComments).ToList();
            news = news.Skip(offset).Take(limit).ToList();

            foreach (var item in news)
                item.Row = ++offset;

            return news;
        }

        public List<NewsViewModel> MostPopularNews(int offset, int limit)
        {
            var listTags = _context.Tags.ToList();
            var newsCategories = _context.NewsCategories.ToList();
            var users = _managementDbContext.Users.ToList();
            var comments = _context.Comments.ToList();
            var likes = _context.Likes.ToList();
            var visits = _context.Visits.ToList();




            var news = _context.News.Include(p => p.NewsNewsCategories).Include(p => p.NewsTags)
                .Where(p => p.IsPublish && p.PublishDateTime <= DateTime.Now)
                .Select(p => new NewsViewModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    Abstract = p.Abstract,
                    ShortTitle = p.Title.Length > 50 ? p.Title.Substring(0, 50) + "..." : p.Title,
                    Url = p.Url,
                    ImageName = p.ImageName,
                    Description = p.Description,
                    IsPublish = p.IsPublish,
                    AuthorName = GetUserName(p.UserId, users),
                    PublishDateTime = p.PublishDateTime,
                    NameOfCategories = GetCategoriesName(p.NewsNewsCategories, p.Id, newsCategories),
                    NameOfTags = GetTagsName(p.NewsTags, p.Id, listTags),
                    NewsType = p.IsInternal ? "داخلی" : "خارجی",
                    Status = p.IsPublish == false ? "پیش نویس" : (p.PublishDateTime <= DateTime.Now ? "منتشر" : "انتشار در آینده"),
                    PersianPublishDate = p.PublishDateTime.ConvertMiladiToShamsi("yyyy/MM/dd ساعت HH:mm"),
                    NumberOfVisit = GetNumberOfVisit(p.Id, visits),
                    NumberOfLike = GetNumberOfLike(p.Id, likes),
                    NumberOfDisLike = GetNumberOfDisLike(p.Id, likes),
                    NumberOfComments = GetNumberOfComments(p.Id, comments),



                }).ToList();

            news = news.OrderByDescending(p => p.NumberOfLike).ToList();
            news = news.Skip(offset).Take(limit).ToList();

            foreach (var item in news)
                item.Row = ++offset;

            return news;
        }

        public NewsViewModel GetNewsById(long newsId, long? userId)
        {
            var listTags = _context.Tags.ToList();
            var newsCategories = _context.NewsCategories.ToList();
            var users = _managementDbContext.Users.ToList();
            var comments = _context.Comments.ToList();
            var likes = _context.Likes.ToList();
            var visits = _context.Visits.ToList();
            var bookMarks = _context.BookMarks.ToList();




            var news = _context.News.Include(p => p.NewsNewsCategories).Include(p => p.NewsTags)
                .Select(p => new NewsViewModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    Abstract = p.Abstract,
                    ShortTitle = p.Title.Length > 50 ? p.Title.Substring(0, 50) + "..." : p.Title,
                    Url = p.Url,
                    ImageName = p.ImageName,
                    Description = p.Description,
                    IsPublish = p.IsPublish,
                    AuthorName = GetUserName(p.UserId, users),
                    PublishDateTime = p.PublishDateTime,
                    NameOfCategories = GetCategoriesName(p.NewsNewsCategories, p.Id, newsCategories),
                    NameOfTags = GetTagsName(p.NewsTags, p.Id, listTags),
                    NewsType = p.IsInternal ? "داخلی" : "خارجی",
                    Status = p.IsPublish == false ? "پیش نویس" : (p.PublishDateTime <= DateTime.Now ? "منتشر" : "انتشار در آینده"),
                    PersianPublishDate = p.PublishDateTime.ConvertMiladiToShamsi("yyyy/MM/dd ساعت HH:mm"),
                    NumberOfVisit = GetNumberOfVisit(p.Id, visits),
                    NumberOfLike = GetNumberOfLike(p.Id, likes),
                    NumberOfDisLike = GetNumberOfDisLike(p.Id, likes),
                    NumberOfComments = GetNumberOfComments(p.Id, comments),
                    IsBookmarked = IsNewsBookMarked(p.Id, userId, bookMarks),
                    Authorimg =GetUserImage(p.UserId,users) ,
                    TagIdsList = GetTagId(p.NewsTags,p.Id, listTags),


                }).FirstOrDefault(p => p.Id == newsId);

           var listNameTag = news.NameOfTags.Split().Skip(1).ToList();

           news.TagNamesList = listNameTag;



            return news;
        }

        private static List<long> GetTagId(ICollection<NewsTag> newsTag, long pId, List<Tag> tags)
        {
            var newsWithTags = tags.Join(newsTag,
                t => t.Id,
                nt => nt.TagId,
                (t, nt) => new
                {
                    newsId = nt.NewsId,
                    tagId = t.Id,
                   
                }).Where(p => p.newsId == pId).ToList();

            return newsWithTags.Select(p => p.tagId).ToList();
        }

        private static string GetUserImage(long UserId, List<User> users)
        {
            return users.FirstOrDefault(p => p.Id == UserId).Image;
        }

        public List<NewsViewModel> GetNextAndPreviousNews(DateTime? publishDateTime, long? userId)
        {
            var listNews = new List<NewsViewModel>();

            var listTags = _context.Tags.ToList();
            var newsCategories = _context.NewsCategories.ToList();
            var users = _managementDbContext.Users.ToList();
            var comments = _context.Comments.ToList();
            var likes = _context.Likes.ToList();
            var visits = _context.Visits.ToList();
            var bookMarks = _context.BookMarks.ToList();




            var previos = _context.News.Include(p => p.NewsNewsCategories).Include(p => p.NewsTags)
                .Select(p => new NewsViewModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    Abstract = p.Abstract,
                    ShortTitle = p.Title.Length > 50 ? p.Title.Substring(0, 50) + "..." : p.Title,
                    Url = p.Url,
                    ImageName = p.ImageName,
                    Description = p.Description,
                    IsPublish = p.IsPublish,
                    AuthorName = GetUserName(p.UserId, users),
                    PublishDateTime = p.PublishDateTime,
                    NameOfCategories = GetCategoriesName(p.NewsNewsCategories, p.Id, newsCategories),
                    NameOfTags = GetTagsName(p.NewsTags, p.Id, listTags),
                    NewsType = p.IsInternal ? "داخلی" : "خارجی",
                    Status = p.IsPublish == false ? "پیش نویس" : (p.PublishDateTime <= DateTime.Now ? "منتشر" : "انتشار در آینده"),
                    PersianPublishDate = p.PublishDateTime.ConvertMiladiToShamsi("yyyy/MM/dd ساعت HH:mm"),
                    NumberOfVisit = GetNumberOfVisit(p.Id, visits),
                    NumberOfLike = GetNumberOfLike(p.Id, likes),
                    NumberOfDisLike = GetNumberOfDisLike(p.Id, likes),
                    NumberOfComments = GetNumberOfComments(p.Id, comments),
                    IsBookmarked = IsNewsBookMarked(p.Id, userId, bookMarks)



                }).OrderByDescending(p => p.PublishDateTime).FirstOrDefault(p => p.IsPublish && p.PublishDateTime <= DateTime.Now && p.PublishDateTime < publishDateTime);

            listNews.Add(previos);


            var next = _context.News.Include(p => p.NewsNewsCategories).Include(p => p.NewsTags)
                .Select(p => new NewsViewModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    Abstract = p.Abstract,
                    ShortTitle = p.Title.Length > 50 ? p.Title.Substring(0, 50) + "..." : p.Title,
                    Url = p.Url,
                    ImageName = p.ImageName,
                    Description = p.Description,
                    IsPublish = p.IsPublish,
                    AuthorName = GetUserName(p.UserId, users),
                    PublishDateTime = p.PublishDateTime,
                    NameOfCategories = GetCategoriesName(p.NewsNewsCategories, p.Id, newsCategories),
                    NameOfTags = GetTagsName(p.NewsTags, p.Id, listTags),
                    NewsType = p.IsInternal ? "داخلی" : "خارجی",
                    Status = p.IsPublish == false ? "پیش نویس" : (p.PublishDateTime <= DateTime.Now ? "منتشر" : "انتشار در آینده"),
                    PersianPublishDate = p.PublishDateTime.ConvertMiladiToShamsi("yyyy/MM/dd ساعت HH:mm"),
                    NumberOfVisit = GetNumberOfVisit(p.Id, visits),
                    NumberOfLike = GetNumberOfLike(p.Id, likes),
                    NumberOfDisLike = GetNumberOfDisLike(p.Id, likes),
                    NumberOfComments = GetNumberOfComments(p.Id, comments),
                    IsBookmarked = IsNewsBookMarked(p.Id, userId, bookMarks)



                }).OrderBy(p => p.PublishDateTime).FirstOrDefault(p => p.IsPublish && p.PublishDateTime <= DateTime.Now && p.PublishDateTime > publishDateTime);

                listNews.Add(next);

                return listNews;
        }

        public List<NewsViewModel> GetNewsinCategory(int categoryId, int pageIndex, int pageSize)
        {
            var listTags = _context.Tags.ToList();
            var newsCategories = _context.NewsCategories.ToList();
            var users = _managementDbContext.Users.ToList();
            var comments = _context.Comments.ToList();
            var likes = _context.Likes.ToList();
            var visits = _context.Visits.ToList();




            var news = _context.News.Include(p => p.NewsNewsCategories).Include(p => p.NewsTags)
                .Where(n => n.IsPublish && n.PublishDateTime <= DateTime.Now && n.NewsNewsCategories.Select(p => p.NewsCategoryId).Contains(categoryId))
                .Select(p => new NewsViewModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    Abstract = p.Abstract,
                    ShortTitle = p.Title.Length > 50 ? p.Title.Substring(0, 50) + "..." : p.Title,
                    Url = p.Url,
                    ImageName = p.ImageName,
                    Description = p.Description,
                    IsPublish = p.IsPublish,
                    AuthorName = GetUserName(p.UserId, users),
                    PublishDateTime = p.PublishDateTime,
                    NameOfCategories = GetCategoriesName(p.NewsNewsCategories, p.Id, newsCategories),
                    NameOfTags = GetTagsName(p.NewsTags, p.Id, listTags),
                    NewsType = p.IsInternal ? "داخلی" : "خارجی",
                    Status = p.IsPublish == false
                        ? "پیش نویس"
                        : (p.PublishDateTime <= DateTime.Now ? "منتشر" : "انتشار در آینده"),
                    PersianPublishDate = p.PublishDateTime.ConvertMiladiToShamsi("yyyy/MM/dd ساعت HH:mm"),
                    NumberOfVisit = GetNumberOfVisit(p.Id, visits),
                    NumberOfLike = GetNumberOfLike(p.Id, likes),
                    NumberOfDisLike = GetNumberOfDisLike(p.Id, likes),
                    NumberOfComments = GetNumberOfComments(p.Id, comments),



                }).ToList();

            
            news = news.Skip(pageIndex * pageSize).Take(pageSize).ToList();
           
            return news;
        }

        public List<NewsViewModel> GetNewsinTag(long tagId, int pageIndex, int pageSize)
        {
            var listTags = _context.Tags.ToList();
            var newsCategories = _context.NewsCategories.ToList();
            var users = _managementDbContext.Users.ToList();
            var comments = _context.Comments.ToList();
            var likes = _context.Likes.ToList();
            var visits = _context.Visits.ToList();




            var news = _context.News.Include(p => p.NewsNewsCategories).Include(p => p.NewsTags).Include(p => p.NewsTags)
                .Where(n => n.IsPublish && n.PublishDateTime <= DateTime.Now && n.NewsTags.Select(p => p.TagId).Contains(tagId))
                .Select(p => new NewsViewModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    Abstract = p.Abstract,
                    ShortTitle = p.Title.Length > 50 ? p.Title.Substring(0, 50) + "..." : p.Title,
                    Url = p.Url,
                    ImageName = p.ImageName,
                    Description = p.Description,
                    IsPublish = p.IsPublish,
                    AuthorName = GetUserName(p.UserId, users),
                    PublishDateTime = p.PublishDateTime,
                    NameOfCategories = GetCategoriesName(p.NewsNewsCategories, p.Id, newsCategories),
                    NameOfTags = GetTagsName(p.NewsTags, p.Id, listTags),
                    NewsType = p.IsInternal ? "داخلی" : "خارجی",
                    Status = p.IsPublish == false
                        ? "پیش نویس"
                        : (p.PublishDateTime <= DateTime.Now ? "منتشر" : "انتشار در آینده"),
                    PersianPublishDate = p.PublishDateTime.ConvertMiladiToShamsi("yyyy/MM/dd ساعت HH:mm"),
                    NumberOfVisit = GetNumberOfVisit(p.Id, visits),
                    NumberOfLike = GetNumberOfLike(p.Id, likes),
                    NumberOfDisLike = GetNumberOfDisLike(p.Id, likes),
                    NumberOfComments = GetNumberOfComments(p.Id, comments),



                }).ToList();


            news = news.Skip(pageIndex * pageSize).Take(pageSize).ToList();

            return news;
        }

        private static bool IsNewsBookMarked(long newsId, long? userId, List<BookMark> bookMarks)
        {
           return bookMarks.Any(p => p.UserId == userId && p.NewsId == newsId);
        }

        private static int GetNumberOfVisit(long newsId, List<Visit> visits)
        {
	        var numberOfVisit = visits.Where(p => p.NewsId == newsId).Sum(p => p.NumberOfVisit);

	        return numberOfVisit;

        }

        private static int GetNumberOfDisLike(long newsId, List<Like> likes)
        {
			var numberOfDisLike = likes.Where(p => p.NewsId == newsId && !p.IsLiked).Count();
			return numberOfDisLike;
		}

        private static int GetNumberOfLike(long newsId, List<Like> likes)
        {
	        var numberOfLike = likes.Where(p => p.NewsId == newsId && p.IsLiked).Count();
            return numberOfLike;

        }

        private static int GetNumberOfComments(long NewsId, List<Comment> comments)
        {
	        var numberOfComments = comments.Where(p => p.NewsId == NewsId).Count();
            return numberOfComments;
        }

        public long CountNewsPublished()
        {
            var numOfPublishedNews = _context.News.Where(p => p.IsPublish).LongCount();
            return numOfPublishedNews;
        }

        private static string GetUserName(long userId, List<User> users)
        {
            var user = users.FirstOrDefault(p => p.Id == userId);
            return user.FirstName + " " + user.LastName;
        }

        private static string GetTagsName(ICollection<NewsTag> newsTag, long nId, List<Tag> tags)
        {
            
            var newsWithTags = tags.Join(newsTag,
                t => t.Id,
                nt => nt.TagId,
                (t, nt) => new
                {
                    newsId = nt.NewsId,
                    tagId = t.Id,
                    tagName = t.TagName,
                }).Where(p => p.newsId == nId).ToList();

            var tagNames = "";
            foreach (var item in newsWithTags)
            {
                tagNames = tagNames + " " + item.tagName;
            }

            return tagNames;
        }

        private static string GetCategoriesName(ICollection<NewsNewsCategory> newsNewsCategories, long newsId, List<NewsCategory> newsCategories)
        {
            

            var newsWithCategory = newsNewsCategories.Join(newsCategories,
                nnc => nnc.NewsCategoryId,
                nc => nc.Id,
                (nnc, nc) => new
                {
                    newsId = nnc.NewsId,
                    categoryId = nnc.NewsCategoryId,
                    categoryName = nc.CategoryName,
                }).Where(p => p.newsId == newsId).ToList();

            var categoryName = "";
            foreach (var item in newsWithCategory)
            {
                categoryName = categoryName + " " + item.categoryName;
            }

            return categoryName;
        }

       
    }
}
