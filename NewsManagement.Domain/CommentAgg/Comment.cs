using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Domain;
using NewsManagement.Domain.NewsAgg;

namespace NewsManagement.Domain.CommentAgg
{
	public class Comment : EntityBase<long>
	{
		public string Name { get; set; }
		public string Email { get; set; }
		public string Description { get; set; }
		public long NewsId { get; set; }
		public bool IsConfirm { get; set; }
		public DateTime? PostageDateTime { get; set; }

		public long? ParentCommentId { get; set; }

		public virtual Comment comment { get; set; }
		public virtual ICollection<Comment> comments { get; set; }
		public virtual News News { get; set; }


		public Comment(string name, string email, string description, long newsId, long? parentCommentId)
		{
			Name = name;
			Email = email;
			Description = description;
			NewsId = newsId;
			ParentCommentId = parentCommentId;

			PostageDateTime = DateTime.Now;
		}
	}
}
