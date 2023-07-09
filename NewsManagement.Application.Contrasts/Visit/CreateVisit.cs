using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsManagement.Application.Contrasts.Visit
{
	public class CreateVisit
	{
		public long NewsId { get; set; }
		public string IpAddress { get; set; }
		public DateTime LastVisitDateTime { get; set; }
		public int NumberOfVisit { get; set; }


		public CreateVisit(long newsId, string ipAddress, DateTime lastVisitDateTime, int numberOfVisit)
		{
			NewsId = newsId;
			IpAddress = ipAddress;
			LastVisitDateTime = lastVisitDateTime;
			NumberOfVisit = numberOfVisit;
		}
	}
}
