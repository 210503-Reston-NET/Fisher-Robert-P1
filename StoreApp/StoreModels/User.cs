using System;

namespace StoreModels
{
    public class User
    {
        public string Code { get; set; } = "000";
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime created = new DateTime();
        public User ()
        {
            this.UserName = "";
            this.Password = "";
            this.FirstName = "";
            this.LastName = "";
            this.created = DateTime.Now;
        }
        public User (string userName, string password): this()
        {
            this.UserName = userName;
            this.Password = password;
        }
        public User (string userName, string password, string FirstName, string LastName): this(userName, password)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
        }
        public User (string userName, string password, string FirstName, string LastName, string Code): this(userName, password, FirstName, LastName)
        {
            this.Code = Code;
        }
        public User (string userName, string password, string FirstName, string LastName, string Code, DateTime Created): this(userName, password, FirstName, LastName, Code)
        {
            this.created = Created;
        } 
        public override string ToString()
        {
            return "UserName: " + this.UserName + "\nPassword: " + this.Password + 
            "\nFirstName: " + this.FirstName + "\nLastName: " + this.LastName + "\nCode: " + this.Code;
        }
    }
}