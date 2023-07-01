using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NewsManagement.Application;
using NewsManagement.Application.Contrasts.NewsCategory;
using NewsManagement.Domain.NewsCategoryAgg;
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


		}
	}
}