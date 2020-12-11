using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtusFitApi.Models;

namespace VirtusFitApi.DAL
{
    public interface IUserAccountActionsRepository
    {
        List<UserAccountAction> GetAllUserAccountActions();
        List<UserAccountAction> GetAllUserAccountActionsById(string id);
        UserAccountAction GetUserAccountActionById(int id);
        bool AddUserAccountAction(UserAccountAction action);
    }
}
