using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;

namespace NewsManagement.Application.Contrasts.Comment
{
	public interface ICommentApplication
	{
		(List<CommentViewModel>, int) Search(CommentSearchModel searchModel);

		EditComment GetDetails(long id);

		OperationResult Delete(EditComment command);

		OperationResult ConfirmOrInconfirm(long id);

	}
}
