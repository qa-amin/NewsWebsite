using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Domain;
using NewsManagement.Application.Contrasts.Comment;

namespace NewsManagement.Domain.CommentAgg
{
	public interface ICommentRepository : IRepository<long,Comment>
	{
		(List<CommentViewModel>, int) Search(CommentSearchModel searchModel);

		 List<CommentViewModel> GetPaginateComments(int offset, int limit, string orderBy, string searchText,
			string newsId, bool? isConfirm);

		List<Comment> findChildOfComments(long id);

        List<Comment> getNewsComments(long newsId);
    }
}
