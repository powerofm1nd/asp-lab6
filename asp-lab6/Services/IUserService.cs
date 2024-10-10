using asp_lab6.Models;

namespace asp_lab6.Services;

public interface IUserService
{
    UserModel CreateUser(string firstName, string lastName, int age);
    
    UserModel GetUser(Guid id);
}