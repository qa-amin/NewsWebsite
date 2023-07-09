using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NewsManagement.Application.Contrasts.Visit;
using NewsManagement.Domain.VisitAgg;

namespace NewsManagement.Application
{
	public class VisitApplication : IVisitApplication
	{
		private readonly IVisitRepository _repository;
		private readonly IHttpContextAccessor _accessor;

		public VisitApplication(IVisitRepository repository, IHttpContextAccessor accessor)
		{
			_repository = repository;
			_accessor = accessor;
		}

		public void VisitLog(long newsId)
		{
			var ip = _accessor.HttpContext?.Connection?.RemoteIpAddress.ToString();

			var visit = _repository.GetVisit(newsId, ip);

			if (visit != null && visit.LastVisitDateTime.Date != DateTime.Now.Date)
			{
				visit.InCrease();
				
				
			}
			else if (visit == null)
			{
				var newVisit = new Visit(newsId, ip, DateTime.Now, 1); 
				_repository.Create(newVisit);
				_repository.SaveChanges();
				
			}
		}
	}
}
