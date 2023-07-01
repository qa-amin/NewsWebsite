using _0_Framework.Application;

namespace AccountManagement.Application.Contrast.User
{
	public interface IUserApplication
    {
        OperationResult Create(CreateUser command);
        OperationResult Edit(EditUser command);
        OperationResult Delete(long id);
        (List<UserViewModel>, long) Search(UserSearchModel searchModel);

        EditUser GetUserWithRole(long id);

        EditUser GetDetail(long id);


    }
}
