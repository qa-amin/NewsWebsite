using NewsManagement.Domain.NewsAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsManagement.Domain.VisitAgg
{
	public class Visit
	{
		public long NewsId { get; set; }
		public string IpAddress { get; set; }
		public DateTime LastVisitDateTime { get; set; }
		public int NumberOfVisit { get; set; }
		public virtual News News { get; set; }

		public DateTime CreationDate { get; set; }
		public DateTime? UpdateDate { get; set; }
		public DateTime? RemoveDate { get; set; }
		public bool IsRemove { get; set; }

		public Visit(long newsId, string ipAddress, DateTime lastVisitDateTime, int numberOfVisit)
		{
			NewsId = newsId;
			IpAddress = ipAddress;
			LastVisitDateTime = lastVisitDateTime;
			NumberOfVisit = numberOfVisit;
			CreationDate = DateTime.Now;
		}

		public void InCrease()
		{
			this.NumberOfVisit += 1;
			this.LastVisitDateTime = DateTime.Now;
			UpdateDate = DateTime.Now;
		}
	}
}
