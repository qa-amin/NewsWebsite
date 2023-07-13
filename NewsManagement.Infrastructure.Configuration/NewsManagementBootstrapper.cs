using _1_NewsManagementQuery.Contracts.CategoryOrTagInfo;
using _1_NewsManagementQuery.Contracts.HomePage;
using _1_NewsManagementQuery.Contracts.NewsDetail;
using _1_NewsManagementQuery.Contracts.NewsInCategoriesAndTags;
using _1_NewsManagementQuery.Contracts.NewsPaginate;
using _1_NewsManagementQuery.Contracts.UserBookMark;
using _1_NewsManagementQuery.Query;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NewsManagement.Application;
using NewsManagement.Application.Contrasts.BookMark;
using NewsManagement.Application.Contrasts.Comment;
using NewsManagement.Application.Contrasts.Like;
using NewsManagement.Application.Contrasts.News;
using NewsManagement.Application.Contrasts.NewsCategory;
using NewsManagement.Application.Contrasts.NewsLetter;
using NewsManagement.Application.Contrasts.Tag;
using NewsManagement.Application.Contrasts.Video;
using NewsManagement.Application.Contrasts.Visit;
using NewsManagement.Domain.BookMarkAgg;
using NewsManagement.Domain.CommentAgg;
using NewsManagement.Domain.LikeAgg;
using NewsManagement.Domain.NewsAgg;
using NewsManagement.Domain.NewsCategoryAgg;
using NewsManagement.Domain.NewsLetterAgg;
using NewsManagement.Domain.NewsNewsCategoryAgg;
using NewsManagement.Domain.NewsTagAgg;
using NewsManagement.Domain.TagAgg;
using NewsManagement.Domain.VideoAgg;
using NewsManagement.Domain.VisitAgg;
using NewsManagement.Infrastructure.EFCore;
using NewsManagement.Infrastructure.EFCore.Repository;

namespace NewsManagement.Infrastructure.Configuration
{
	public  class NewsManagementBootstrapper
	{
		public static void Config( IServiceCollection services, string cs)
		{
			services.AddDbContext<NewsManagementDbContext>(option => option.UseSqlServer(cs));


			services.AddTransient<INewsCategoryRepository, NewsCategoryRepository>();
			services.AddTransient<INewsCategoryApplication, NewsCategoryApplication>();

            services.AddTransient<ITagRepository, TagRepository>();
            services.AddTransient<ITagApplication,TagApplication>();

            services.AddTransient<IVideoRepository, VideoRepository>();
            services.AddTransient<IVideoApplication, VideoApplication>();

            services.AddTransient<INewsRepository, NewsRepository>();
            services.AddTransient<INewsApplication, NewsApplication>();


            services.AddTransient<INewsNewsCategoryRepository, NewsNewsCategoryRepository>();
            services.AddTransient<INewsTagRepository, NewsTagRepository>();

            services.AddTransient<INewsLetterApplication, NewsLetterApplication>();
            services.AddTransient<INewsLetterRepository, NewsLetterRepository>();

            services.AddTransient<ICommentRepository,CommentRepository>();
            services.AddTransient<ICommentApplication, CommentApplication>();

            services.AddTransient<ILikeRepository, LikeRepository>();
            services.AddTransient<ILikeApplication, LikeApplication>();


            services.AddTransient<IVisitRepository, VisitRepository>();
            services.AddTransient<IVisitApplication, VisitApplication>();

            services.AddTransient<IBookMarkRepository, BookMarkRepository>();
            services.AddTransient<IBookMarkApplication, BookMarkApplication>();

			services.AddTransient<IHomePageQuery, HomePageQuery>();
			services.AddTransient<INewsDetailQuery, NewsDetailQuery>();
			services.AddTransient<INewsPaginateQuery, NewsPaginateQuery>();
			services.AddTransient<ICategoryOrTagInfoQuery, CategoryOrTagInfoQuery>();
			services.AddTransient<INewsInCategoriesAndTagsQuery, NewsInCategoriesAndTagsQuery>();
			services.AddTransient<IUserBookMarkQuery, UserBookMarkQuery>();
            

        }
	}
}