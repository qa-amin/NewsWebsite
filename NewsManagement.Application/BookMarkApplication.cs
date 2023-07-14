using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using NewsManagement.Application.Contrasts.BookMark;
using NewsManagement.Domain.BookMarkAgg;

namespace NewsManagement.Application
{
	public class BookMarkApplication : IBookMarkApplication
	{
		private readonly IBookMarkRepository _bookMarkRepository;


		public BookMarkApplication(IBookMarkRepository bookMarkRepository)
		{
			_bookMarkRepository = bookMarkRepository;
		}

		public bool BookMarkNews(long newsId, long userId)
		{
			var bookMark = _bookMarkRepository.GetBookMark(newsId, userId);
			if (bookMark == null)
			{
				var newBookMark = new BookMark(newsId, userId);
				_bookMarkRepository.Create(newBookMark);
				_bookMarkRepository.SaveChanges();
				return true;
			}
			else
			{
				_bookMarkRepository.Delete(bookMark);
				_bookMarkRepository.SaveChanges();
				return false;
			}
		}

		public OperationResult Delete(DeleteBookMark command)
		{
			var operation = new OperationResult();
			var bookMark = _bookMarkRepository.GetBookMark(command.NewsId, command.UserId);
			if (bookMark == null)
			{
				return operation.Failed(ApplicationMessages.RecordNotFound);
			}

			_bookMarkRepository.Delete(bookMark);
			_bookMarkRepository.SaveChanges();

			return operation.Succeeded();
		}

		public DeleteBookMark GetBookMark(long newsId, long userId)
		{
			var bookMark = _bookMarkRepository.GetBookMark(newsId, userId);
			return new DeleteBookMark()
			{
				UserId = userId,
				NewsId = newsId,
			};
		}
	}
}
