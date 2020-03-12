using CarPoolingWebApi.Context;
using CarPoolingWebApi.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace CarPoolingWebApi.Services.Service
{
    public class UserService : IUserService
    {
        private CarPoolingContext Db { get; set; }

        public UserService(CarPoolingContext context)
        {
            this.Db = context;
        }

        public bool AddNewUser(Models.Client.User user)
        {
            user.Id = Guid.NewGuid().ToString();
            var userData = Mapper.Map<Models.Client.User, Models.Data.User>(user);
            this.Db.Users.Add(userData);
            return this.Db.SaveChanges() > 0;
        }

        public Models.Client.User Authentication(Models.Client.Login credentials)
        {
            return Mapper.Map<Models.Data.User, Models.Client.User>(this.Db.Users?.FirstOrDefault(a => a.UserName == credentials.UserName && a.Password == credentials.Password));
        }

        public bool DeleteUser(string id)
        {
            this.Db?.Users?.Remove(this.Db.Users?.FirstOrDefault(a => a.Id == id));
            return this.Db.SaveChanges() > 0;
        }

        public bool UpdateUser(Models.Client.User newDetails, string id)
        {
            Models.Data.User oldDetails = this.Db?.Users?.FirstOrDefault(a => a.Id == id);
            if (oldDetails != null)
            {
                oldDetails.Name = newDetails.Name;
                oldDetails.Address = newDetails.Address;
                oldDetails.Mobile = newDetails.Mobile;

                return this.Db.SaveChanges() > 0;
            }

            return false;
        }

        public Models.Client.User GetUser(string id)
        {
            return Mapper.Map<Models.Data.User, Models.Client.User>(this.Db.Users?.FirstOrDefault(a => a.Id == id));
        }

        public bool CheckUserName(string userName)
        {
            return this.Db.Users?.FirstOrDefault(a => a.UserName == userName) != null;
        }
    }
}
