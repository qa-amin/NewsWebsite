using _0_Framework.Application;
using NewsManagement.Application.Contrasts.NewsCategory;
using NewsManagement.Domain.NewsCategoryAgg;
using NewsWebsite.Entities;

namespace NewsManagement.Application
{
	public class NewsCategoryApplication : INewsCategoryApplication
	{
		private readonly INewsCategoryRepository _newCategoryRepository;

		public NewsCategoryApplication(INewsCategoryRepository newCategoryRepository)
		{
			_newCategoryRepository = newCategoryRepository;
		}


		public (List<NewsCategoryViewModel>,int) Search(NewsCategorySearchModel searchModel)
		{
			return _newCategoryRepository.Search(searchModel);
		}

        public List<NewsCategoryTreeViewModel> GetAllCategories()
        {
            return _newCategoryRepository.GetAllCategories();
        }

        public NewsCategoryViewModel FindByCategoryName(string categoryName)
        {
            return _newCategoryRepository.FindByCategoryName(categoryName);
        }

        public OperationResult Create(CreateNewsCategory command)
        {
            var operation = new OperationResult();

            if (_newCategoryRepository.Exists(p => p.CategoryName == command.CategoryName))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }

            int? parentCategoryId = null;
            if (command.ParentCategoryName != null)
            {
                parentCategoryId = _newCategoryRepository.FindByCategoryName(command.ParentCategoryName).CategoryId;
            }
            var url = command.Url.Slugify();
            var newsCategory = new NewsCategory(command.CategoryName,url, parentCategoryId);

            _newCategoryRepository.Create(newsCategory);
            _newCategoryRepository.SaveChanges();

            return operation.Succeeded();
        }

        public OperationResult Edit(EditNewsCategory command)
        {
            var operation = new OperationResult();

            var newsCategory = _newCategoryRepository.Get(command.Id);
           
            if (newsCategory == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_newCategoryRepository.Exists(p => p.CategoryName == command.CategoryName && p.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            int? parentCategoryId = null;
            if (command.ParentCategoryName != null)
            {
                parentCategoryId = _newCategoryRepository.FindByCategoryName(command.ParentCategoryName).CategoryId;
            }

            var url = command.Url.Slugify();
            newsCategory.Edit(command.CategoryName,url, parentCategoryId);
            _newCategoryRepository.SaveChanges();

            return operation.Succeeded(ApplicationMessages.RecordEdit);
        }

        public OperationResult Delete(EditNewsCategory command)
        {
            var operation = new OperationResult();

            var newsCategory = _newCategoryRepository.Get(command.Id);

            if (newsCategory == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);


            newsCategory.Delete();
            DeleteChild(command.Id);

            var stop = 1;

            _newCategoryRepository.SaveChanges();

            return operation.Succeeded(ApplicationMessages.RecordDelete);
        }

        public EditNewsCategory Getdetails(int id)
        {
            return _newCategoryRepository.GetDetails(id);
        }

        public void DeleteChild(int id)
        {
            var newsCategoryChild = _newCategoryRepository.findChildCategories(id);
            foreach (var item in newsCategoryChild)
            {
                DeleteChild(item.Id);
                item.Delete();
            }
        }
    }
}