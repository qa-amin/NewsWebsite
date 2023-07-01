using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;

namespace AccountManagement.Application.Contrast.Role
{
    public interface IRoleApplication
    {
        OperationResult Create(CreateRole command);
        List<Domain.RoleAgg.Role> GetAllRoles();

        OperationResult Edit(EditRole command);
        OperationResult Delete(EditRole command);

        (List<RoleViewModel>, int) Search(RoleSearchModel command);

        
    }
}
