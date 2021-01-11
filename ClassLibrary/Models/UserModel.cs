using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary.Models
{
    public class UserModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        //public string LastName { get; set; }
        public string Email { get; set; }


        public UserModel(string id, string userName, string email, string lastName)
        {
            Id = id;
            UserName = userName + " " + lastName;
            //LastName = lastName;
            Email = email;
        }
    }
}
