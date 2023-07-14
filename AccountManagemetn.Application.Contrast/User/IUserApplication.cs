﻿using System.Security.Claims;
using _0_Framework.Application;

namespace AccountManagement.Application.Contrast.User
{
	public interface IUserApplication
    {
        OperationResult Create(CreateUser command);
        OperationResult Register(CreateUser command);
        OperationResult Edit(EditUser command);
        (OperationResult,string) EditProfile(ProfileViewModel command);
        OperationResult Delete(long id);
        OperationResult Login(Login command);
        OperationResult ChangePssword(ChangePass command, Domain.UserAgg.User user);
        OperationResult ResetPass(ResetPass command, Domain.UserAgg.User user);
        void SignOut();
        (List<UserViewModel>, long) Search(UserSearchModel searchModel);

        EditUser GetUserWithRole(long id);

        EditUser GetDetail(long id);
        Domain.UserAgg.User GetUser(ClaimsPrincipal command);
        ProfileViewModel GetProfileDetail( long id);

        UserViewModel GetUserInfo(long userId);


    }
}
