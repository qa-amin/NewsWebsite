using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NewsManagement.Application;
using NewsManagement.Application.Contrasts.NewsCategory;
using NewsManagement.Application.Contrasts.Tag;
using NewsManagement.Application.Contrasts.Video;
using NewsManagement.Domain.NewsCategoryAgg;
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


        }
	}
}