using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
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

		public EditComment GetDetails(long id)
		{
			var comment = _commentRepository.Get(id);
			return new EditComment
			{
				NewsId = comment.NewsId,
				Name = comment.Name,
				Id = comment.Id,
				Desription = comment.Description,
				Email = comment.Email,
				IsConfirm = comment.IsConfirm
			};
		}

		public OperationResult Delete(EditComment command)
		{
			var operation = new OperationResult();

			var comment = _commentRepository.Get(command.Id);
			if (comment == null)
			{
				return operation.Failed(ApplicationMessages.RecordNotFound);
			}

			comment.Delete();

			DeleteChild(comment.Id);

			_commentRepository.SaveChanges();
			return operation.Succeeded(ApplicationMessages.DeleteComment);
		}

		public OperationResult ConfirmOrInconfirm(long id)
		{
			var operation = new OperationResult();
			var comment = _commentRepository.Get(id);
			if (comment == null)
			{
				return operation.Failed(ApplicationMessages.RecordNotFound);
			}

			if (comment.IsConfirm)
			{
				comment.InActive();
			}
			else
			{
				comment.Active();
			}

			_commentRepository.SaveChanges();
			return operation.Succeeded();
		}

		private void DeleteChild(long commentId)
		{
			var child = _commentRepository.findChildOfComments(commentId);
			foreach (var item in child)
			{
				DeleteChild(item.Id);
				item.Delete();
			}
		}
	}
}
