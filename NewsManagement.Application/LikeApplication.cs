using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NewsManagement.Application.Contrasts.Like;
using NewsManagement.Domain.LikeAgg;

namespace NewsManagement.Application
{
	public class LikeApplication : ILikeApplication
	{
		private readonly ILikeRepository _likeRepository;
		private readonly IHttpContextAccessor _accessor;

		public LikeApplication(ILikeRepository likeRepository, IHttpContextAccessor accessor)
		{
			_likeRepository = likeRepository;
			_accessor = accessor;
		}

		public void LikeOrDisLike(long newsId, bool isLike)
		{
			var ip = _accessor.HttpContext?.Connection?.RemoteIpAddress.ToString();
			var like = _likeRepository.GetLike(newsId, ip);

			if (like == null)
			{
				var newLike = new Like(newsId,ip,isLike);
				_likeRepository.Create(newLike);
				_likeRepository.SaveChanges();
			}
			else
			{
				like.IsLike(isLike);
			}
		}

		
	}
}
