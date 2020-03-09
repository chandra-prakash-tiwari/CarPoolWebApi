using CarPoolingEf.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolingEf.Services.Interfaces
{
    public interface IUserServices<TEntity>
    {
        void AddNewUser(TEntity user);

        User Authentication(Login credentials);

        bool DeleteUser(string id);

        bool UpdateUser(User newDetails, string id);

        User GetUser(string id);

        bool CheckUserName(string userName);
    }
}
