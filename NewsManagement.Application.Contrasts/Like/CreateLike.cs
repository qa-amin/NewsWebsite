using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsManagement.Application.Contrasts.Like
{
	public class CreateLike
	{
		public long NewsId { get; set; }
		public string IpAddress { get; set; }
		public bool IsLiked { get; set; }

		public DateTime CreationDate { get; set; }
		public DateTime? UpdateDate { get; set; }
		public DateTime? RemoveDate { get; set; }
		public bool IsRemove { get; set; }
	}
}
