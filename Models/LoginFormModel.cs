using Warehouse.Classes;
using Warehouse.Classes.Encryption;
using Warehouse.Classes.User;

namespace Warehouse.Models;

public class LoginFormModel
{
    public bool ExistUser(string name, string password)
    {
        password=EncryptionHelper.Encrypt(password);
        var users = UserRepository.GetAllUsers();
        var encPassword = EncryptionHelper.Encrypt(password);
        foreach (var user in users)
        {
            if (user.Name == name && user.Password == encPassword)
            {
                return true;
            }
        }

        return false;
    }
}