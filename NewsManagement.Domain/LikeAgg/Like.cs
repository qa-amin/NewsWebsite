using NewsManagement.Domain.NewsAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace NewsManagement.Domain.LikeAgg
{
	public class Like
	{
		public long NewsId { get; private set; }
		public string IpAddress { get; private set; }
		public bool IsLiked { get; private set; }

		public virtual News News { get; private set; }

		public DateTime CreationDate { get; set; }
		public DateTime? UpdateDate { get; set; }
		public DateTime? RemoveDate { get; set; }
		public bool IsRemove { get; set; }

		public Like(long newsId, string ipAddress, bool isLiked)
		{
			NewsId = newsId;
			IpAddress = ipAddress;
			IsLiked = isLiked;
			CreationDate = DateTime.Now;
			

		}

		public void IsLike(bool isLiked)
		{
			IsLiked = isLiked;
			UpdateDate = DateTime.Now;
			
		}
	}
}
