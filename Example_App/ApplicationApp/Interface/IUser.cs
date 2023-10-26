using Example_App.Infraestructura.Model;
using Example_App.Infraestructura.Model;
namespace Example_App.ApplicationApp.Interface_Controller
{
    public interface IUser
    {
        public Task<object> GetUser();
        public Task<object> CreateUser(User user);
        public int UpdateUser(User user);
        public Task<object> DisableUser(int id);
        public Task<object> DeleteUser(int id);

    }
}
