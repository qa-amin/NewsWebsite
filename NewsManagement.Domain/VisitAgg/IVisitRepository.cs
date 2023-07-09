using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Domain;

namespace NewsManagement.Domain.VisitAgg
{
	public interface IVisitRepository : IRepository<long,Visit>
	{
		Visit GetVisit(long newsId, string ip);
	}
}
