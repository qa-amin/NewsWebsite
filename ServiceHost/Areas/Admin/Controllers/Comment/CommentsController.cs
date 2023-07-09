using Microsoft.AspNetCore.Mvc;
using NewsManagement.Application.Contrasts.Comment;
using NewsManagement.Application.Contrasts.Tag;

namespace ServiceHost.Areas.Admin.Controllers.Comment
{
	public class CommentsController : Controller
	{
		private readonly ICommentApplication _commentApplication;

		public CommentsController(ICommentApplication commentApplication)
		{
			_commentApplication = commentApplication;
		}

		[Area("Admin")]
		[Route("admin/comments/index")]
		public IActionResult Index(long? newsId, bool? isConfirm)
		{
			return View(new CommentViewModel()
			{
				IsConfirm = isConfirm,
				NewsId = newsId
			});
		}

		[Area("Admin")]
		[Route("admin/comments/GetComments/{newsId?}/{isConfirm?}")]
		public IActionResult GetComments(string search, string order, int offset, int limit, string sort, long? newsId, bool? isConfirm)
		{
			var searchModel = new CommentSearchModel()
			{
				Limit = limit,
				Sort = sort,
				Offset = offset,
				Order = order,
				Search = search,
				IsConfirm = isConfirm,
				NewsId = newsId
				
			};

			var (comments, total) = _commentApplication.Search(searchModel);
			

			return Json(new {total = total, rows = comments});
		}
	}
}
