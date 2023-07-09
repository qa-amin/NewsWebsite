using NewsManagement.Domain.NewsAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsManagement.Domain.BookMarkAgg
{
	public class BookMark
	{
		public long NewsId { get; set; }
		public long UserId { get; set; }

		public virtual News News { get; set; }


		public DateTime CreationDate { get; set; }
		public DateTime? UpdateDate { get; set; }
		

		public BookMark(long newsId, long userId)
		{
			NewsId = newsId;
			UserId = userId;
			CreationDate = DateTime.Now;
		}
	}
}
