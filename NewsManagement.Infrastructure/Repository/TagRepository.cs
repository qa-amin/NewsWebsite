using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using NewsManagement.Application.Contrasts.Tag;
using NewsManagement.Domain.TagAgg;
using NewsManagement.Infrastructure.EFCore.Migrations;
using System.Linq.Dynamic.Core;

namespace NewsManagement.Infrastructure.EFCore.Repository
{
    public class TagRepository : RepositoryBase<long, Tag>, ITagRepository
    {
        private readonly NewsManagementDbContext _context;

        public TagRepository(NewsManagementDbContext context) : base(context)
        {
            _context = context;
        }

        public EditTag GetDetail(long id)
        {
            var editTag = _context.Tags.Select(p => new EditTag
                {
                    Id = p.Id,
                    TagName = p.TagName

                }).FirstOrDefault(p => p.Id == id);
            return editTag;


        }

        public (List<TagViewModel>,int) Search(TagSearchModel searchModel)
        {
            List<TagViewModel> tags;
            int total = _context.Tags.Count();
            if (!searchModel.Search.HasValue())
                searchModel.Search = "";

            if (searchModel.Limit == 0)
                searchModel.Limit = total;

            if (searchModel.Sort == "برچسب")
            {
                if (searchModel.Order == "asc")
                    tags = GetPaginateTags(searchModel.Offset, searchModel.Limit, "TagName", searchModel.Search);
                else
                    tags = GetPaginateTags(searchModel.Offset, searchModel.Limit, "TagName desc", searchModel.Search);
            }

            else
                tags = GetPaginateTags(searchModel.Offset, searchModel.Limit, "TagName", searchModel.Search);

            if (searchModel.Search != "")
                total = tags.Count();

            return (tags, total);
        }

        private List<TagViewModel> GetPaginateTags(int offset, int limit, string Orderby, string searchText)
        {
            var tags = new List<TagViewModel>();
            tags = _context.Tags.Where(p => p.TagName.Contains(searchText))
                .OrderBy(Orderby)
                .Skip(offset).Take(limit)
                .Select(p => new TagViewModel
                {
                    Id = p.Id,
                    TagName = p.TagName,
                }).ToList();

            foreach (var item in tags)
                item.Row = ++offset;

            return tags;
        }
    }
}
