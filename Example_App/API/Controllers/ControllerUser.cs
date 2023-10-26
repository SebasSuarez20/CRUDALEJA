using Example_App.ApplicationApp.Interface_Controller;
using Example_App.ApplicationApp.Service;
using Example_App.Infraestructura.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Example_App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControllerUser : ControllerBase
    {

        //Aca llamamos la interface para poder llamar los metodos internos creados.
        private readonly IUser _context;

        public ControllerUser(IUser _user)
        {
            _context = _user;
        }

        [HttpGet("GetUser")]
        public async Task<object> GetAllUser()
        {
            return await _context.GetUser();
        }

        [HttpPost("CreateUser")]
        public async Task<object> CreateUser(User user)
        {
            return await _context.CreateUser(user);
        }

        [HttpPut("UpdateUser")]

        public int UpdateUser(User user)
        {
            return _context.UpdateUser(user);
        }

        [HttpDelete("DeleteUser")]

        public async Task<object> DeleteUser(int id)
        {
             return await _context.DeleteUser(id);
        }
    }
}
