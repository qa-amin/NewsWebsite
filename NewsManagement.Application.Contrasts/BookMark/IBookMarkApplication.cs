using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsManagement.Application.Contrasts.BookMark
{
	public interface IBookMarkApplication
	{
		void BookMarkNews(long newsId, long userId);
	}
}
