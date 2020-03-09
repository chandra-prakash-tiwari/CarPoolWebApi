using CarPoolingEf;
using CarPoolingEf.Models;
using CarPoolingEf.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolingEf.Services.Services
{
    public class UserServices : IUserServices<User>
    {
        private CarPoolingContext Db { get; set; }

        public UserServices(CarPoolingContext context)
        {
            this.Db = context;
        }

        public void AddNewUser(User user)
        {
            user.Id = Guid.NewGuid().ToString();
            this.Db.Users.Add(user);
            this.Db.SaveChanges();
        }

        public User Authentication(Login credentials)
        {
            User user = this.Db.Users?.FirstOrDefault(a => a.UserName == credentials.UserName && a.Password == credentials.Password);

            if (user != null)
                return user;

            return null;
        }

        public bool DeleteUser(string id)
        {
            this.Db?.Users?.Remove(this.Db.Users?.FirstOrDefault(a => a.Id == id));
            return this.Db.SaveChanges() > 0;
        }

        public bool UpdateUser(User newDetails, string id)
        {
            User oldDetails = this.Db?.Users?.FirstOrDefault(a => a.Id == id);
            if (oldDetails != null)
            {
                oldDetails.Name = newDetails.Name;
                oldDetails.Address = newDetails.Address;
                oldDetails.Mobile = newDetails.Mobile;

                return this.Db.SaveChanges() > 0;
            }

            return false;
        }

        public User GetUser(string id)
        {
            return this.Db.Users?.FirstOrDefault(a => a.Id == id);
        }

        public bool CheckUserName(string userName)
        {
            return this.Db.Users?.FirstOrDefault(a => a.UserName == userName) != null;
        }
    }
}
