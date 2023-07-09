using _1_NewsManagementQuery.Contracts.HomePage;
using _1_NewsManagementQuery.Query;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NewsManagement.Application;
using NewsManagement.Application.Contrasts.Comment;
using NewsManagement.Application.Contrasts.News;
using NewsManagement.Application.Contrasts.NewsCategory;
using NewsManagement.Application.Contrasts.NewsLetter;
using NewsManagement.Application.Contrasts.Tag;
using NewsManagement.Application.Contrasts.Video;
using NewsManagement.Domain.CommentAgg;
using NewsManagement.Domain.LikeAgg;
using NewsManagement.Domain.NewsAgg;
using NewsManagement.Domain.NewsCategoryAgg;
using NewsManagement.Domain.NewsLetterAgg;
using NewsManagement.Domain.NewsNewsCategoryAgg;
using NewsManagement.Domain.NewsTagAgg;
using NewsManagement.Domain.TagAgg;
using NewsManagement.Domain.VideoAgg;
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

			services.AddTransient<IHomePageQuery, HomePageQuery>();

        }
	}
}