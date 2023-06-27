using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0_Framework.Domain
{
	public class EntityBase<T>
	{
		public T Id { get; set; }
		public DateTime CreationDate { get; set; }
		public DateTime? UpdateDate { get; set; }
		public DateTime? RemoveDate { get; set; }
		public bool IsRemove { get; set; }


		public EntityBase()
		{
			
			IsRemove = false;
            CreationDate = DateTime.Now;
        }
	}
}
