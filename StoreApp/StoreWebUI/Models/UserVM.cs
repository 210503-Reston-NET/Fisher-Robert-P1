using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StoreModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StoreWebUI.Models
{
    public class UserVM
    {
        public UserVM() { }
        public UserVM(User user)
        {
            Code = user.Code;
            UserName = user.UserName;
            Password = user.Password;
            FirstName = user.FirstName;
            LastName = user.LastName;
        }
        public UserVM(string userName, string password, string FirstName, string LastName)
        {
            this.UserName = UserName;
            this.Password = Password;
            this.FirstName = FirstName;
            this.LastName = LastName;
        }
        public UserVM(string userName, string password, string FirstName, string LastName, string Code): this(userName, password, FirstName, LastName)
        { 
            this.Code = Code;
        }
        [DisplayName("Emloyee ID")]
        public string Code { get; set; } = "000";
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
