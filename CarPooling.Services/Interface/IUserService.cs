using CarPoolingWebApi.Models.Client;

namespace CarPoolingWebApi.Services.Interfaces
{
    public interface IUserService
    {
        bool AddNewUser(User user);

        Models.Client.User Authentication(Login credentials);

        bool DeleteUser(string id);

        bool UpdateUser(User newDetails, string id);

        User GetUser(string id);

        bool CheckUserName(string userName);
    }
}
