using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtusFitApi.Models;

namespace VirtusFitApi.DAL
{
    public class UserAccountActionsRepository : IUserAccountActionsRepository
    {

        private readonly ApiContext _context;

        public UserAccountActionsRepository(ApiContext context)
        {
            _context = context;
        }

        public List<UserAccountAction> GetAllUserAccountActions()
        {
            return _context.UserAccountActions.ToList();
        }

        public List<UserAccountAction> GetAllUserAccountActionsById(string id)
        {
            return _context.UserAccountActions.Where(user => user.UserId==id).ToList();
        }

        public UserAccountAction GetUserAccountActionById(int id)
        {
            var action = _context.UserAccountActions.FirstOrDefault(action => action.Id == id);
            return action;
        }

        public bool AddUserAccountAction(UserAccountAction action)
        {
            _context.UserAccountActions.Add(action);
            _context.SaveChanges();
            return true;
        }
    }
}
