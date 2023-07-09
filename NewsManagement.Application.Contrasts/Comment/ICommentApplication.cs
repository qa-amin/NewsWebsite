using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsManagement.Application.Contrasts.Comment
{
	public interface ICommentApplication
	{
		(List<CommentViewModel>, int) Search(CommentSearchModel searchModel);
	}
}
