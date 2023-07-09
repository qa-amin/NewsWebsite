using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using NewsManagement.Domain.VisitAgg;

namespace NewsManagement.Infrastructure.EFCore.Repository
{
	public class VisitRepository : RepositoryBase<long , Visit>, IVisitRepository
	{
		private readonly NewsManagementDbContext _context;

		public VisitRepository( NewsManagementDbContext context) : base(context)
		{
			_context = context;
		}
	}
}
