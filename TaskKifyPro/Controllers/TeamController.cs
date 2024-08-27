using TaskKifyPro.Business.Abstract;
using TaskKifyPro.Business.Services;
using TaskKifyPro.Entity.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;
using TaskKifyPro.Business.Abstract;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using TaskKifyPro.DataAccess.Migrations;
using TaskKifyPro.WebAPI.Controllers;
using TaskKifyPro.Entity.Concrete;
using System.ComponentModel.Design;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore.Internal;

namespace TaskKifyPro.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;
        private readonly IUserService _userService;


        public TeamController(ITeamService teamService , IUserService userService)
        {
            _teamService = teamService;
            _userService = userService;
        }

        [HttpPost("DeleteTeamsMultiply")]
        public IActionResult DeleteTeamsMultiply([FromQuery] string selectedIds)
        {
            var idList = selectedIds.Split(',').ToList();

            foreach (var id in idList)
            {
                var user = _teamService.GetById(Convert.ToInt32(id));
                if (user != null)
                {
                    user.Status = false;
                    user.UpdatedTime = DateTime.Now; 
                    _teamService.Update(user);
                }
            }

            return Ok("İşlem Başarılı");
        }

        //Takımın bilgisini verir.
        [HttpGet("GetTheTeam")]
        public IActionResult GetTheTeam(int id)
        {
            var result = _teamService.GetAll().Where(ug => ug.Status == true && ug.Id == id).OrderBy(ug => ug.Name);

            return Ok(result);
        }


        [HttpGet("GetTeamById")]
        public IActionResult GetTeamById(int id)
        {

            var result = _teamService.GetById(id);
            return Ok(result);
        }

        //Takım ekler.
        [HttpPost("AddTeam")]
        public IActionResult AddTeam(TeamModal teammodal)
        {
            Team team = new Team
            {   Name = teammodal.Name,
                CreatedTime = DateTime.Now,
                UpdatedTime = DateTime.Now,
                CreatedUserId = teammodal.CreatedUserId,
                UpdatedUserId = teammodal.CreatedUserId,
                Status = true
            };
            _teamService.Add(team);

            User us = new User();            {

                us.Name = teammodal.RegisterName;
                us.SurName = teammodal.RegisterSurName;
                us.Email = teammodal.RegisterEmail;
                us.Adres = teammodal.RegisterAdres;
                us.Phone = teammodal.RegisterPhone;
                us.PasswordHash = EncryptionService.HashPassword(teammodal.RegisterPassword);
                us.Type = teammodal.Type;
                us.TeamId = teammodal.TeamId;
            };

            _userService.Add(us);



            return Ok("İşlem Başarılı");
        }



        public class TeamModal
        {
            public int Id { get; set; }
            public DateTime CreatedTime { get; set; }
            public DateTime UpdatedTime { get; set; }
            public int CreatedUserId { get; set; }
            public int UpdatedUserId { get; set; }
            public bool Status { get; set; }
            public string Name { get; set; }

            // Eksik özellikleri ekleyin
            public string RegisterName { get; set; }
            public string RegisterSurName { get; set; }
            public string RegisterEmail { get; set; }
            public string RegisterAdres { get; set; }
            public string RegisterPhone { get; set; }
            public string RegisterPassword { get; set; }
            public bool Type { get; set; }  // Type özelliği
            public int TeamId { get; set; } // TeamId özelliği
        }

        [HttpDelete("DeleteTeam")]
        public IActionResult DeleteCompany(int id)
        {
            var team = _teamService.GetById(id);
            team.Status = false;
            team.UpdatedTime = DateTime.Now;
            _teamService.Update(team);
            return Ok("İşlem Başarılı");
        }


        //Takımı günceller.
        [HttpPut("UpdateTeam")]
        public IActionResult UpdateTeam(Team _team)
        {
            var team = _teamService.GetById(_team.Id);
            team.UpdatedTime = DateTime.Now;

            team.Name = _team.Name;
            


            _teamService.Update(team);
            return Ok("İşlem Başarılı");
        }


    }
}

