using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using NewsManagement.Application.Contrasts.NewsLetter;
using NewsManagement.Domain.NewsLetterAgg;

namespace NewsManagement.Application
{
	public class NewsLetterApplication : INewsLetterApplication
	{
		private readonly INewsLetterRepository _newsLetterRepository;

		public NewsLetterApplication(INewsLetterRepository newsLetterRepository)
		{
			_newsLetterRepository = newsLetterRepository;
		}

		public OperationResult Create(CreateNewsLetter command)
		{
			var operation = new OperationResult();
			var newsLetter = _newsLetterRepository.GetNewsLetterWithEmail(command.Email);

			if (newsLetter == null)
			{
				var createNewsLetter = new NewsLetter(command.Email);
				_newsLetterRepository.Create(createNewsLetter);
				_newsLetterRepository.SaveChanges();
				return operation.Succeeded(ApplicationMessages.RegisterSuccess);
			}
			else
			{
				if (newsLetter.IsActive)
				{
					return operation.Failed($"شما با ایمیل '{command.Email}' قبلا عضو خبرنامه شده اید.");
				}
				else
				{
					var newsletter = _newsLetterRepository.Get(command.Email);
					newsletter.Active();
					_newsLetterRepository.SaveChanges();
					return operation.Succeeded(ApplicationMessages.RegisterSuccess);
				}
			}
		}

		public OperationResult Delete(EditNewsLetter command)
		{
			var operation = new OperationResult();

			var newsLeter = _newsLetterRepository.Get(command.Email);
			if (newsLeter == null)
			{
				return operation.Failed(ApplicationMessages.RecordNotFound);
			}
			newsLeter.Delete();

			_newsLetterRepository.SaveChanges();

			return operation.Succeeded(ApplicationMessages.DeleteNewsLetter);
		}

		public (List<NewsLetterViewModel>, int) Search(NewsLetterSearchModel searchModel)
		{
			var (newsLeter, total) = _newsLetterRepository.Search(searchModel);
			return (newsLeter, total);
		}

		public EditNewsLetter GetDetails(string email)
		{
			var newsLetter = _newsLetterRepository.Get(email);
			return new EditNewsLetter
			{
				Email = newsLetter.Email
			};
		}

		public OperationResult ActiveOrInActiveMembers(string email)
		{
			var operation = new OperationResult();
			var newsLetter = _newsLetterRepository.Get(email);

			if (newsLetter == null)
			{
				return operation.Failed(ApplicationMessages.RecordNotFound);
			}

			if (newsLetter.IsActive)
			{
				newsLetter.DeActive();
			}
			else
			{
				newsLetter.Active();
			}

			_newsLetterRepository.SaveChanges();
			return operation.Succeeded("Success");
		}
	}
}
