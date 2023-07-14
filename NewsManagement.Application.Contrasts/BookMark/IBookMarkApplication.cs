using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;

namespace NewsManagement.Application.Contrasts.BookMark
{
	public interface IBookMarkApplication
	{
		bool BookMarkNews(long newsId, long userId);
		OperationResult Delete(DeleteBookMark command);

		DeleteBookMark GetBookMark(long newsId, long userId);
	}
}
