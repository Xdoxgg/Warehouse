using Warehouse.Classes;
namespace Warehouse.Models;

public class MainWindowModel
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