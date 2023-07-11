using Microsoft.AspNetCore.Mvc;
using NewsManagement.Application.Contrasts.Comment;
using NewsManagement.Application.Contrasts.Tag;
using Newtonsoft.Json;

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

		[Area("Admin")]
		[Route("admin/comments/Delete")]
		[HttpGet]
		public IActionResult Delete(long id)
		{
			var comment = _commentApplication.GetDetails(id);

			return PartialView("_Delete",comment);
		}

		[Area("Admin")]
		[Route("admin/comments/Delete")]
		[HttpPost]
		public void Delete(EditComment command)
		{
			var result = _commentApplication.Delete(command);
			TempData["ShowMassage"] = JsonConvert.SerializeObject(result);


		}

		[Area("Admin")]
		[Route("admin/comments/ConfirmOrInconfirm")]
		[HttpGet]
		public IActionResult ConfirmOrInconfirm(long id)
		{
			var comment = _commentApplication.GetDetails(id);

			return PartialView("_ConfirmOrInconfirm", comment);

		}

		[Area("Admin")]
		[Route("admin/comments/ConfirmOrInconfirm")]
		[HttpPost]
		public void ConfirmOrInconfirm(EditComment command)
		{
			var result = _commentApplication.ConfirmOrInconfirm(command.Id);

			TempData["ShowMassage"] = JsonConvert.SerializeObject(result);


		}

		[Area("Admin")]
		[Route("admin/comments/SendComment")]
		[HttpGet]
		public IActionResult SendComment(long? parentCommentId, long newsId)
		{
			var newComment = new CreateComment
			{
				NewsId = newsId,
				ParentCommentId = parentCommentId
			};

			return PartialView("_SendComment", newComment);

		}
		[Area("Admin")]
		[Route("admin/comments/SendComment")]
		[HttpPost]
		public IActionResult SendComment(CreateComment command)
		{
			if (ModelState.IsValid)
			{
				var result = _commentApplication.SendComment(command);
				
				TempData["ShowMassage"] = JsonConvert.SerializeObject(result);
				
				

			}


			return PartialView("_SendComment",command);



		}


	}
}
