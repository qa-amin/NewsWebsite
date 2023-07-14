using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsManagement.Application.Contrasts.Like
{
	public interface ILikeApplication
	{
		void LikeOrDisLike(long newsId,bool isLike);
		
	}
}
