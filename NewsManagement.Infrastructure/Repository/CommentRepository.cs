using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using NewsManagement.Application.Contrasts.Comment;
using NewsManagement.Domain.CommentAgg;
using NewsManagement.Domain.NewsAgg;
using NewsManagement.Infrastructure.EFCore.Migrations;
using NewsWebsite.Common;

namespace NewsManagement.Infrastructure.EFCore.Repository
{
	public class CommentRepository : RepositoryBase<long,Comment>, ICommentRepository
	{
		private readonly NewsManagementDbContext _context;

		public CommentRepository( NewsManagementDbContext context) : base(context)
		{
			_context = context;
		}

		public (List<CommentViewModel>, int) Search(CommentSearchModel searchModel)
		{
			List<CommentViewModel> comments;

			string? newsId = searchModel.NewsId.ToString();

			int total = _context.Comments.Count();

			if (!searchModel.Search.HasValue())
				searchModel.Search = "";

			if (searchModel.Limit == 0)
				searchModel.Limit = total;

			if (!newsId.HasValue())
				newsId = "";

			if (searchModel.Sort == "نام")
			{
				if (searchModel.Order == "asc")
					comments = GetPaginateComments(searchModel.Offset, searchModel.Limit, "Name", searchModel.Search, newsId, searchModel.IsConfirm);
				else
					comments = GetPaginateComments(searchModel.Offset, searchModel.Limit, "Name desc", searchModel.Search, newsId, searchModel.IsConfirm);
			}


			else if (searchModel.Sort == "ایمیل")
			{
				if (searchModel.Order == "asc")
					comments = GetPaginateComments(searchModel.Offset, searchModel.Limit, "Email", searchModel.Search, newsId, searchModel.IsConfirm);
				else
					comments = GetPaginateComments(searchModel.Offset, searchModel.Limit, "Email desc", searchModel.Search, newsId, searchModel.IsConfirm);
			}

			else if (searchModel.Sort == "تاریخ ارسال")
			{
				if (searchModel.Order == "asc")
					comments = GetPaginateComments(searchModel.Offset, searchModel.Limit, "PostageDateTime", searchModel.Search, newsId, searchModel.IsConfirm);
				else
					comments = GetPaginateComments(searchModel.Offset, searchModel.Limit, "PostageDateTime desc", searchModel.Search, newsId, searchModel.IsConfirm);
			}
			else
				comments = GetPaginateComments(searchModel.Offset, searchModel.Limit, "PostageDateTime desc", searchModel.Search, newsId, searchModel.IsConfirm);

			if (searchModel.Search != "")
				total = comments.Count();


			return (comments,total);
		}


		public List<CommentViewModel> GetPaginateComments(int offset, int limit, string orderBy, string searchText,
			string newsId, bool? isConfirm)
		{

			var convertConfirm = Convert.ToBoolean(isConfirm);
			var getDateTimesForSearch = searchText.GetDateTimeForSearch();
			List<CommentViewModel> comments = _context.Comments
				.Where(n => (isConfirm == null || (convertConfirm == true ? n.IsConfirm : !n.IsConfirm)) && n.NewsId.ToString().Contains(newsId) && (n.Name.Contains(searchText) || n.Email.Contains(searchText) || (n.PostageDateTime >= getDateTimesForSearch.First() && n.PostageDateTime <= getDateTimesForSearch.Last())))
				.OrderBy(orderBy)
				.Skip(offset).Take(limit)
				.Select(l => new CommentViewModel { Id = l.Id, Name = l.Name, Email = l.Email, IsConfirm = l.IsConfirm, PersianPostageDateTime = l.PostageDateTime.ConvertMiladiToShamsi("yyyy/MM/dd ساعت HH:mm:ss"), Desription = l.Description })
				.ToList();

			foreach (var item in comments)
				item.Row = ++offset;

			return comments;
		}

		public List<Comment> findChildOfComments(long id)
		{
			return _context.Comments.Where(p => p.ParentCommentId == id).ToList();
		}
	}
}
