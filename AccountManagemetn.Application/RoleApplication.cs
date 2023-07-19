using AccountManagement.Application.Contrast.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using AccountManagement.Domain.RoleAgg;
using AccountManagement.Domain.UserAgg;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace AccountManagement.Application
{
    public class RoleApplication : IRoleApplication
    {
        private readonly RoleManager<Role> _roleManager;
        

        public RoleApplication(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }

        public OperationResult Create(CreateRole command)
        {
            var operation = new OperationResult();

            var role = new Role(command.Name, command.Description);

            var result = _roleManager.CreateAsync(role).Result;
            if (result.Succeeded)
            {
                return operation.Succeeded(ApplicationMessages.CreateRole);
            }


            var massage = "";
            foreach (var error in result.Errors.ToList())
            {
                massage += error.Description + Environment.NewLine;
            }
            return operation.Failed(massage);

        }

        public List<Role> GetAllRoles()
        {
            return _roleManager.Roles.ToList();
        }

        public OperationResult Edit(EditRole command)
        {
            var operation = new OperationResult();

            var role =  _roleManager.FindByIdAsync(command.Id.ToString()).Result;
            if (role == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }

            role.Edit(command.Name, command.Description);

            var result =  _roleManager.UpdateAsync(role).Result;

            if (result.Succeeded)
            {
                return operation.Succeeded(ApplicationMessages.EditRole);
            }


            var massage = "";
            foreach (var error in result.Errors.ToList())
            {
                massage += error.Description + Environment.NewLine;
            }
            return operation.Failed(massage);


        }

        public OperationResult Delete(EditRole command)
        {
            var operation = new OperationResult();

            var role = _roleManager.FindByIdAsync(command.Id.ToString()).Result;
            if (role == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }

            role.Delete();

            var result = _roleManager.UpdateAsync(role).Result;

            if (result.Succeeded)
            {
                return operation.Succeeded(ApplicationMessages.EditRole);
            }


            var massage = "";
            foreach (var error in result.Errors.ToList())
            {
                massage += error.Description + Environment.NewLine;
            }
            return operation.Failed(massage);

        }

        public (List<RoleViewModel>, int) Search(RoleSearchModel command)
        {
            var roles = new List<RoleViewModel>();

            int total = _roleManager.Roles.Count();

            if (string.IsNullOrWhiteSpace(command.Search))
                command.Search = "";

            if (command.Limit == 0)
                command.Limit = total;


            if (command.Sort == "عنوان نقش")
            {
                if (command.Order == "asc")
                    roles = GetPaginateRoles(command.Offset, command.Limit, true, command.Search);
                else
                    roles = GetPaginateRoles(command.Offset, command.Limit, false, command.Search);
            }

            else
                roles = GetPaginateRoles(command.Offset, command.Limit, null, command.Search);

            if (command.Search != "")
                total = roles.Count();

            return (roles, total);
        }

        public Role FindClaimsInRole(long roleId)
        {
            return _roleManager.Roles.Include(p => p.Claims).FirstOrDefault(c => c.Id == roleId);
        }


        private List<RoleViewModel> GetPaginateRoles(int offset, int limit, bool? roleNameSortAsc, string searchText)
        {
            var roles = _roleManager.Roles.Where(p => p.Name.Contains(searchText))
                .Select(p => new RoleViewModel
                {
                    Description = p.Description,
                    Id = p.Id,
                    Name = p.Name,
                    UsersCount = p.Users.Count

                }).Skip(offset).Take(limit).ToList();

            if (roleNameSortAsc != null)
            {
                roles = roles.OrderBy(p => (roleNameSortAsc == true && roleNameSortAsc != null) ? p.Name : "")
                    .OrderByDescending(p => (roleNameSortAsc == false && roleNameSortAsc != null) ? p.Name : "")
                    .ToList();
            }

            foreach (var item in roles)
                item.Row = ++offset;

            return roles;
        }

    }
}
