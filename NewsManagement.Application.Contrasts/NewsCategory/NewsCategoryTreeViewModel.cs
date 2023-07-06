using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsManagement.Application.Contrasts.NewsCategory
{
	public class NewsCategoryTreeViewModel
	{
		public NewsCategoryTreeViewModel()
		{
			subs = new List<NewsCategoryTreeViewModel>();
		}
		public int id { get; set; }
		public string title { get; set; }
		public string Url { get; set; }
		public List<NewsCategoryTreeViewModel> subs { get; set; }
	}
}
