using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsManagement.Domain.NewsLetterAgg
{
	public class NewsLetter
	{
		public string Email { get; private set; }
		public DateTime? RegisterDateTime { get; private set; }
		public bool IsActive { get; private set; }

		public DateTime? UpdateDate { get; private set; }
		public DateTime? RemoveDate { get; private set; }
		public bool IsRemove { get; private set; }


		public NewsLetter(string email)
		{
			Email = email;
			RegisterDateTime = DateTime.Now;
			IsActive = true;
		}
		public void Active()
		{
			IsActive = true;
			UpdateDate = DateTime.Now;
		}
		public void DeActive()
		{
			IsActive = false;
			UpdateDate = DateTime.Now;
		}
		public void Delete()
		{
			IsRemove = true;
			RemoveDate = DateTime.Now;
		}
	}
}
