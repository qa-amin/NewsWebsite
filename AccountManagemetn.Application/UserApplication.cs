using System.Linq.Dynamic.Core;
using System.Security.Claims;
using System.Security.Principal;
using _0_Framework.Application;
using AccountManagement.Application.Contrast.User;
using AccountManagement.Domain.RoleAgg;
using AccountManagement.Domain.UserAgg;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NewsWebsite.Common;

namespace AccountManagement.Application
{
    public class UserApplication : IUserApplication
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IFileUploader _fileUploader;

        public UserApplication(UserManager<User> userManager, IFileUploader fileUploader, RoleManager<Role> roleManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _fileUploader = fileUploader;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public OperationResult Create(CreateUser command)
        {
            var operation = new OperationResult();

            var path = $"profilePhotos";
            var imagePath = _fileUploader.Upload(command.ImageFile, path);

            var birthDate = command.PersianBirthDate.ConvertShamsiToMiladi();

            
            
            var user = new User(command.UserName, command.Email,command.PhoneNumber,
                command.FirstName,command.LastName, birthDate,
                imagePath,command.Gender);
            

            var roleName = _roleManager.FindByIdAsync(command.RoleId).Result;

            var result = _userManager.CreateAsync(user, command.Password).Result;
            if (result.Succeeded)
            {
               var userRole =  _userManager.AddToRoleAsync(user, roleName.Name).Result;
                return operation.Succeeded(ApplicationMessages.CreateUser);
            }
            
            var massage = "";
            foreach (var error in result.Errors.ToList())
            {
                massage += error.Description + Environment.NewLine;
            }
            return operation.Failed(massage);
        }

        public OperationResult Edit(EditUser command)
        {
            var operation = new OperationResult();

            var newRoleName = _roleManager.FindByIdAsync(command.RoleId).Result.Name;

            var path = $"profilePhotos";
            var imagePath = _fileUploader.Upload(command.ImageFile, path);

            var birthDate = command.PersianBirthDate.ConvertShamsiToMiladi();

            var editUser = _userManager.FindByIdAsync(command.Id.ToString()).Result;

            var oldeRoleId = GetUserWithRole(editUser.Id).RoleId;

            var oldRoleName = _roleManager.FindByIdAsync(oldeRoleId).Result.Name;

            if (_userManager.Users.Any(p => p.UserName == command.UserName && p.Id != command.Id))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }
            editUser.Edit(command.UserName,command.Email,command.PhoneNumber,command.FirstName,command.LastName
            ,birthDate, imagePath, command.Gender);

            var result = _userManager.UpdateAsync(editUser).Result;

            if (result.Succeeded)
            {
                if (oldeRoleId != command.RoleId)
                {
                    
                    var removeResult =_userManager.RemoveFromRoleAsync(editUser, oldRoleName).Result;
                   
                    var  addResult =  _userManager.AddToRoleAsync(editUser, newRoleName).Result;
                }

                return operation.Succeeded(ApplicationMessages.EditUser);
            }

            var massage = "";
            foreach (var error in result.Errors.ToList())
            {
                massage += error.Description + Environment.NewLine;
            }
            return operation.Failed(massage);

        }

        public OperationResult Delete(long id)
        {
            var operation = new OperationResult();

            var user = _userManager.FindByIdAsync(id.ToString()).Result;

            user.Delete();

            var result = _userManager.UpdateAsync(user).Result;

            if (result.Succeeded)
            {
                return operation.Succeeded(ApplicationMessages.DeleteUser);
            }

            var massage = "";
            foreach (var error in result.Errors.ToList())
            {
                massage += error.Description + Environment.NewLine;
            }
            return operation.Failed(massage);
        }

        public OperationResult Login(Login command)
        {
            
            var operation = new OperationResult();

            var user = _userManager.FindByNameAsync(command.UserName).Result;
            if (user == null)
            {
                return operation.Failed(ApplicationMessages.UserNotFound);
            }

            if (user.IsActive)
            {
                var result = _signInManager.PasswordSignInAsync(user, command.Password, command.RememberMe, true).Result;
                if (result.Succeeded)
                {
                    return operation.Succeeded(ApplicationMessages.UserLogin);
                }
                else if (result.IsLockedOut)
                    return operation.Failed(ApplicationMessages.LockAccount);

                //else if (result.RequiresTwoFactor)
                //    return RedirectToAction("SendCode", new { RememberMe = ViewModel.RememberMe });

                else
                {
                    return operation.Failed(ApplicationMessages.WrongPassword);
                }
            }
            else
            {
                return operation.Failed(ApplicationMessages.InActive);
            }
            



        }

        public OperationResult ChangePssword(ChangePass command, User user)
        {
            var operation = new OperationResult();
            _userManager.RemovePasswordAsync(user);
           var result =  _userManager.AddPasswordAsync(user, command.NewPassword).Result;
           if (result.Succeeded)
           {
               return operation.Succeeded(ApplicationMessages.ChangPass);
           }
           var massage = "";
           foreach (var error in result.Errors.ToList())
           {
               massage += error.Description + Environment.NewLine;
           }
           return operation.Failed(massage);
        }

        public void SignOut()
        {
            _signInManager.SignOutAsync();
        }

        public (List<UserViewModel>, long) Search(UserSearchModel command)
        {

            var allUsers = new List<UserViewModel>();

            int total = _userManager.Users.Count();

            if (string.IsNullOrWhiteSpace(command.Search))
                command.Search = "";

            if (command.Limit == 0)
                command.Limit = total;

            if (command.Sort == "نام")
            {
                if (command.Order == "asc")
                    allUsers = GetPaginateUsers(command.Offset, command.Limit, "FirstName", command.Search);
                else
                    allUsers = GetPaginateUsers(command.Offset, command.Limit, "FirstName desc", command.Search);
            }

            else if (command.Sort == "نام خانوادگی")
            {
                if (command.Order == "asc")
                    allUsers = GetPaginateUsers(command.Offset, command.Limit, "LastName", command.Search);
                else
                    allUsers = GetPaginateUsers(command.Offset, command.Limit, "LastName desc", command.Search);
            }

            else if (command.Sort == "ایمیل")
            {
                if (command.Order == "asc")
                    allUsers = GetPaginateUsers(command.Offset, command.Limit, "Email", command.Search);
                else
                    allUsers = GetPaginateUsers(command.Offset, command.Limit, "Email desc", command.Search);
            }

            else if (command.Sort == "نام کاربری")
            {
                if (command.Order == "asc")
                    allUsers = GetPaginateUsers(command.Offset, command.Limit, "UserName", command.Search);
                else
                    allUsers = GetPaginateUsers(command.Offset, command.Limit, "UserName desc", command.Search);
            }

            else if (command.Sort == "تاریخ عضویت")
            {
                if (command.Order == "asc")
                    allUsers = GetPaginateUsers(command.Offset, command.Limit, "RegisterDateTime", command.Search);
                else
                    allUsers = GetPaginateUsers(command.Offset, command.Limit, "RegisterDateTime desc", command.Search);
            }

            else
                allUsers = GetPaginateUsers(command.Offset, command.Limit, "RegisterDateTime desc", command.Search);

            if (command.Search != "")
                total = allUsers.Count();

            return (allUsers, total);
        }

        public EditUser GetUserWithRole(long id)
        {
            var user = _userManager.Users.Include(p => p.Roles)
                .FirstOrDefault(p => p.Id == id);

            var editUser = new EditUser
            {

                BirthDate = user.BirthDate,
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                Gender = user.Gender,
                RoleId = user.Roles.SingleOrDefault().RoleId.ToString(),
                Image = user.Image,
                PersianBirthDate = user.BirthDate.ConvertMiladiToShamsi("yyyy/MM/dd"),
                Roles = user.Roles,


            };

            return editUser;


        }

        public EditUser GetDetail(long id)
        {
            var user = _userManager.FindByIdAsync(id.ToString()).Result;
            return new EditUser
            {
                BirthDate = user.BirthDate,
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                Gender = user.Gender,
                Image = user.Image,

            };
        }

        public  User GetUser(ClaimsPrincipal command)
        {
           var user =  _userManager.GetUserAsync(command).Result;

           return  user;
        }

        private List<UserViewModel> GetPaginateUsers(int offset, int limit, string orderBy, string searchText)
        {
            var getDateTimesForSearch = searchText.GetDateTimeForSearch();

            
            var users =  _userManager.Users.Include(p => p.Roles)
                .Where(p => (!p.IsRemove) && (p.FirstName.Contains(searchText) || p.LastName.Contains(searchText) || p.Email.Contains(searchText) || p.UserName.Contains(searchText) || (p.RegisterDateTime >= getDateTimesForSearch.First() && p.RegisterDateTime <= getDateTimesForSearch.Last()) ))
                .OrderBy(orderBy)
                .Skip(offset).Take(limit)
                .Select(p => new UserViewModel
                {
                    Id = p.Id,
                    Email = p.Email,
                    UserName = p.UserName,
                    PhoneNumber = p.PhoneNumber,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    IsActive = p.IsActive,
                    Image = p.Image,
                    //Bio = p.Bio,
                    PersianBirthDate = p.BirthDate.ConvertMiladiToShamsi("yyyy/MM/dd"),
                    PersianRegisterDateTime = p.RegisterDateTime.ConvertMiladiToShamsi("yyyy/MM/dd ساعت HH:mm:ss"),
                    Gender = p.Gender == 1 ? "مرد" : "زن",
                    RoleId = p.Roles.Select(p => p.Role.Id).FirstOrDefault(),
                    RoleName = p.Roles.Select(p => p.Role.Name).FirstOrDefault()
                }).ToList();

            foreach (var item in users)
                item.Row = ++offset;

            return users;
        }
    }
}