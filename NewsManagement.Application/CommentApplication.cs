using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewsManagement.Application.Contrasts.Comment;
using NewsManagement.Domain.CommentAgg;

namespace NewsManagement.Application
{
	public class CommentApplication : ICommentApplication
	{
		private readonly ICommentRepository _commentRepository;

		public CommentApplication(ICommentRepository commentRepository)
		{
			_commentRepository = commentRepository;
		}

		public (List<CommentViewModel>, int) Search(CommentSearchModel searchModel)
		{
			return _commentRepository.Search(searchModel);
		}
	}
}
