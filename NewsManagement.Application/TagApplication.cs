using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using NewsManagement.Application.Contrasts.NewsCategory;
using NewsManagement.Application.Contrasts.Tag;
using NewsManagement.Domain.TagAgg;

namespace NewsManagement.Application
{
    public class TagApplication : ITagApplication
    {
        private readonly ITagRepository _tagRepository;

        public TagApplication(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public OperationResult Create(CreateTag command)
        {
            var operation = new OperationResult();

            if (_tagRepository.Exists(p => p.TagName == command.TagName))
            {
                return operation.Succeeded(ApplicationMessages.DuplicatedRecord);
            }

            var tag = new Tag(command.TagName);

            _tagRepository.Create(tag);
            _tagRepository.SaveChanges();

            return operation.Succeeded(ApplicationMessages.TagAdded);
        }

        public OperationResult Edit(EditTag command)
        {
            var operation = new OperationResult();

            if (_tagRepository.Exists(p => p.TagName == command.TagName))
            {
                return operation.Succeeded(ApplicationMessages.DuplicatedRecord);
            }

            var tag = _tagRepository.Get(command.Id);

            if (tag == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }

            tag.Edit(command.TagName);

            _tagRepository.SaveChanges();

            return operation.Succeeded(ApplicationMessages.TagEdited);


        }

        public OperationResult Delete(long id )
        {
            var operation = new OperationResult();
            var tag = _tagRepository.Get(id);
            if (tag == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }

            tag.Delete();
            _tagRepository.SaveChanges();
            return operation.Succeeded(ApplicationMessages.RecordDelete);
        }

        public (List<TagViewModel>, int) Search(TagSearchModel searchModel)
        {
            return _tagRepository.Search(searchModel);
        }

        public EditTag GetDetails(long id)
        {
            var editTag = _tagRepository.GetDetail(id);

            return editTag;
        }

        public List<TagViewModel> GetAllTags()
        {
           var allTags = _tagRepository.Get().Select(p => new TagViewModel
           {
               TagName = p.TagName
           }).ToList();

           return allTags;
        }
    }
}
