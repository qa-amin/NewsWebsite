using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using NewsManagement.Application.Contrasts.NewsLetter;
using NewsManagement.Domain.NewsLetterAgg;
using NewsManagement.Infrastructure.EFCore.Migrations;
using NewsWebsite.Common;
using System.Linq.Dynamic.Core;

namespace NewsManagement.Infrastructure.EFCore.Repository
{
	public class NewsLetterRepository : RepositoryBase<string,NewsLetter>, INewsLetterRepository
	{
		private readonly NewsManagementDbContext _context;

		public NewsLetterRepository( NewsManagementDbContext context) : base(context)
		{
			_context = context;
		}

		public NewsLetterViewModel GetNewsLetterWithEmail(string email)
		{
			return _context.NewsLetters.Select(p => new NewsLetterViewModel
			{
				Email = p.Email,
				IsActive = p.IsActive,
				RegisterDateTime = p.RegisterDateTime
			}).FirstOrDefault(p => p.Email == email);
		}

		public (List<NewsLetterViewModel>, int) Search(NewsLetterSearchModel searchModel)
		{
			List<NewsLetterViewModel> newsletter;
			int total = _context.NewsLetters.Count();

			if (!searchModel.Search.HasValue())
				searchModel.Search = "";

			if (searchModel.Limit == 0)
				searchModel.Limit = total;

			if (searchModel.Sort == "Id")
			{
				if (searchModel.Order == "asc")
					newsletter = GetPaginateNewsletter(searchModel.Offset, searchModel.Limit, "Email", searchModel.Search);
				else
					newsletter = GetPaginateNewsletter(searchModel.Offset, searchModel.Limit, "Email desc", searchModel.Search);
			}

			else if (searchModel.Sort == "تاریخ عضویت")
			{
				if (searchModel.Order == "asc")
					newsletter = GetPaginateNewsletter(searchModel.Offset, searchModel.Limit, "RegisterDateTime", searchModel.Search);
				else
					newsletter = GetPaginateNewsletter(searchModel.Offset, searchModel.Limit, "RegisterDateTime desc", searchModel.Search);
			}

			else
				newsletter = GetPaginateNewsletter(searchModel.Offset, searchModel.Limit, "RegisterDateTime desc", searchModel.Search);

			if (searchModel.Search != "")
				total = newsletter.Count();

			return (newsletter,total);
		}

		public List<NewsLetterViewModel> GetPaginateNewsletter(int offset, int limit, string orderBy, string searchText)
		{
			var getDateTimesForSearch = searchText.GetDateTimeForSearch();
			List<NewsLetterViewModel> newsletter =  _context.NewsLetters.Where(c => c.Email.Contains(searchText) || (c.RegisterDateTime >= getDateTimesForSearch.First() && c.RegisterDateTime <= getDateTimesForSearch.Last()))
				.OrderBy(orderBy)
				.Skip(offset).Take(limit)
				.Select(l => new NewsLetterViewModel
				{
					Email = l.Email, IsActive = l.IsActive,
					PersianRegisterDateTime = l.RegisterDateTime.ConvertMiladiToShamsi("yyyy/MM/dd ساعت HH:mm:ss")
				}).ToList();
			foreach (var item in newsletter)
				item.Row = ++offset;
			return newsletter;
		}
	}
}
