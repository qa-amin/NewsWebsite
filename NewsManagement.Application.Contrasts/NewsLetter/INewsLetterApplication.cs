using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;

namespace NewsManagement.Application.Contrasts.NewsLetter
{
	public interface INewsLetterApplication
	{
		OperationResult Create(CreateNewsLetter command);
		OperationResult Delete(EditNewsLetter command);

		(List<NewsLetterViewModel>, int) Search(NewsLetterSearchModel searchModel);

		EditNewsLetter GetDetails(string email);

		OperationResult ActiveOrInActiveMembers(string email);
	}
}
