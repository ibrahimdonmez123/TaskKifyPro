using TaskKifyPro.Business.Abstract;
using TaskKifyPro.Business.Services;
using TaskKifyPro.Entity.Concrete;
using TaskKifyPro.DataAccess.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;
using TaskKifyPro.Business.Abstract;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Numerics;
using Microsoft.AspNetCore.Identity;
using System.Text;
using System.Diagnostics.Eventing.Reader; 


namespace TaskKifyPro.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITeamService _teamService;

        public UserController(IUserService userService , ITeamService teamService)
        {
            _userService = userService;
            _teamService = teamService; 
        }


        TaskifyProDbContext context = new TaskifyProDbContext();

        //Toplu silme işlemi.
        [HttpPost("DeleteUsersMultiply")]
        public IActionResult DeleteUsersMultiply([FromQuery] string selectedIds)
        {
            var idList = selectedIds.Split(',').ToList();

            foreach (var id in idList)
            {
                var user = _userService.GetById(Convert.ToInt32(id));
                if (user != null)
                {
                    user.Status = false;
                    user.UpdatedTime = DateTime.Now; 
                    _userService.Update(user);
                }
            }

            return Ok("İşlem Başarılı");
        }

        //Ekipteki tüm kullanıcıları verir.
        [HttpGet("GetAll")]
        public IActionResult GetAll(int TeamId)
        {
            var result = _userService.GetAll().Where(ug => ug.Status == true && ug.TeamId == TeamId).OrderBy(ug => ug.Name);
            return Ok(result);
        }


        [HttpGet("GetAllTeams")]
        public IActionResult GetAllTeams(int TeamId)
        {
            var result = _teamService.GetAll().Where(ug => ug.Status == true && ug.Id == TeamId).OrderBy(ug => ug.Name);
            return Ok(result);
        }

        //Ekib liderinin ekibindeki herkesi listeler.
        [HttpGet("GetAllSubordinates")]
        public IActionResult GetAllSubordinates(int teamId)
        {

            var result = _userService.GetAll()
                                     .Where(ug => ug.Status == true && ug.TeamId == teamId)
                                     .OrderBy(ug => ug.Name);
            return Ok(result);
        }


        //Login işleminden sonra localde veri tutmak için kullanılıyor.
        [HttpGet("GetUserById")]
        public IActionResult GetUserById(int id)
        {
            var result = _userService.GetById(id);
            
            return Ok(result);
        }

      



        //Kullanıcıyı siler.
        [HttpDelete("DeleteUser")]
        public IActionResult DeleteUser(int id)
        {
            var user = _userService.GetById(id);
            user.Status = false;
            user.UpdatedTime = DateTime.Now;
            _userService.Update(user);
            return Ok("İşlem Başarılı");
        }

        //Kullanıcıyı günceller.
        [HttpPut("UpdateUser")]
        public IActionResult UpdateUser(User _user)
        {
            var user = _userService.GetById(_user.Id);
            user.UpdatedTime = DateTime.Now;

            user.Name = _user.Name;
            user.SurName = _user.SurName;



            _userService.Update(user);
            return Ok("İşlem Başarılı");
        }


        //Kullanıcı kaydı oluşturur.
        [HttpPost("RegisterUser")]
        public IActionResult RegisterUser(TaskKifyPro.Entity.Concrete.RegisterRequest _registerRequest)
        {
            User user = new User();

            user.CreatedTime = DateTime.Now;
            user.UpdatedTime = DateTime.Now;
            user.CreatedUserId = 1;
            user.UpdatedUserId = 1;
            user.Status = true;
            user.TeamId = 1;
            user.Name = _registerRequest.Name;
            user.SurName = _registerRequest.SurName;
            user.Email = _registerRequest.Email;
            user.Adres = _registerRequest.Adres;
            user.Phone = _registerRequest.Phone;
            user.Type = _registerRequest.Type;

            // Şifreyi kriptolama
            user.PasswordHash = EncryptionService.HashPassword(_registerRequest.Password);

            _userService.Add(user);
            return Ok("İşlem Başarılı");
        }

        //Kullanıcı girişi.
        [HttpPost("Login")]
        public IActionResult Login(LoginRequest loginRequest)
        {
            User user = context.Users.SingleOrDefault(x => x.Email == loginRequest.Email);

            if (user == null || !EncryptionService.VerifyPassword(user.PasswordHash, loginRequest.Password))
            {
                return Unauthorized("E-posta veya şifre yanlış");
            }

            return Ok(new { TeamId = user.TeamId, Id = user.Id });
        }
 

      

     
        public class LoginRequest
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }





    }
}


    


