using EquipmentAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EquipmentAPI.ViewModels
{

    public class LoginViewModel
    {
        public string username { get; set; }
        public string password { get; set; }
    }


    public class UsersViewModel
    {
        private GOT_EquipmentContext _context;
        public int ID { get; set; }
        public string UserName {get;set;}

        public string ShortName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Token { get; set; }

        public bool Authenticate(string username, string password)
        {
            if (password == null) password = string.Empty;
            var user = this._context.Users.Where(u => u.UserName == username && u.Password.Trim() == password.Trim()).SingleOrDefault();
            if (user == null) return false;

            this.UserName = user.UserName;
            this.ShortName = user.Short;
            this.ID = user.UserId;            

            return true;
            
        }
        public UsersViewModel(GOT_EquipmentContext context)
        {
            this._context = context;
        }

        
    }
}
