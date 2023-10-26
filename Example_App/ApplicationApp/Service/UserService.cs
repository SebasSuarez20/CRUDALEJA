using Example_App.ApplicationApp.Interface_Controller;
using Example_App.Infraestructura.Model;
using Google.Protobuf.WellKnownTypes;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using MySqlConnector;
using System.Dynamic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Example_App.ApplicationApp.Service
{
    public class UserService : IUser
    {

        private AppDbContext _context;
        
        public UserService(AppDbContext context,IConfiguration configuration) { 
        
            _context = context;
        }


        public async Task<object> GetUser()
        {
            dynamic response = new ExpandoObject();

            try
            {
                var Sql = await _context.user.Where(s=>s.Enabled==true).ToListAsync();


                if (Sql!=null)
                {
                    response.items = Sql;
                    return response;
                }
                else
                {
                    response.items = null;
                    return response;
                }
             
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<object> CreateUser(User usuario)
        {
            dynamic response=new ExpandoObject();

          try
            {
                if (usuario.IdControl==null)
                {
                    _context.user.Add(usuario);
                    await _context.SaveChangesAsync();
                    response.message = "Se Creo correctamente el usuario";
                }
                return response;

            }
            catch(Exception ex){
                response.error=ex.Message;
                return response;
            }
        }

        public int UpdateUser(User user)
        {
            try
            {


                dynamic response = new ExpandoObject();
                var Sql = $"UPDATE aspirantes SET Name='{user.Name}',Surname='{user.Surname}'," +
                    $"Address='{user.Address}',Age='{user.Age}',Phone='{user.Phone}',Enabled={user.Enabled}" +
                    $" WHERE idControl=@idControl";


                var Params = new MySqlConnector.MySqlParameter[]
                {
            new MySqlConnector.MySqlParameter("@idControl",user.IdControl)
                };

                var RowAffected = _context.Database.ExecuteSqlRaw(Sql, Params);
                return RowAffected;
            }catch(Exception ex)
            {
                 return 400;
            }
        }

        public  async Task<object> DisableUser(int id)
        {

            dynamic response = new ExpandoObject();

            try
            {
                var UserDelete = await _context.user.FirstOrDefaultAsync(a => a.IdControl == id);

                if (UserDelete != null)
                {
                    UserDelete.Enabled = false;
                    _context.SaveChanges();
                    response.Message = "Eliminado correctamente";
               
                }
                return response;
            }
            catch (Exception ex)
            {
                response.error=ex.Message;
                return response;
            }
        }

        public async Task<object> DeleteUser(int idAdmin)
        {
            string query = "DELETE FROM aspirantes WHERE idControl = @idAdmin";
            int items = _context.Database.ExecuteSqlRaw(query, new MySqlConnector.MySqlParameter("@idAdmin", idAdmin));
            return items % 2 == 0 ? false : true;
        }

       
    }

}
