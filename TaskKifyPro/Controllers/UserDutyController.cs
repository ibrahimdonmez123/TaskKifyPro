using TaskKifyPro.Business.Abstract;
using TaskKifyPro.Business.Services;
using TaskKifyPro.Entity.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;
using TaskKifyPro.Business.Abstract;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TaskKifyPro.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDutyController : ControllerBase
    {
        private readonly IUserDutyService _userDutyService;
        private readonly IUserService _userService;
        private readonly IDutyService _dutyService;

        public UserDutyController(IUserDutyService userDutyService , IDutyService dutyService , IUserService userService)
        {
            _userDutyService = userDutyService;
            _dutyService = dutyService;
            _userService = userService;

        }


        [HttpPost("DeleteUserDutiesMultiply")]
        public IActionResult DeleteUserDutiesMultiply([FromQuery] string selectedIds)
        {
            var idList = selectedIds.Split(',').ToList();

            foreach (var id in idList)
            {
                var user = _userDutyService.GetById(Convert.ToInt32(id));
                if (user != null)
                {
                    user.Status = false;
                    user.UpdatedTime = DateTime.Now; 
                    _userDutyService.Update(user);
                }
            }

            return Ok("İşlem Başarılı");
        }

        //Bireysel görevlerin hepsini listeler.
        [HttpGet("GetAllPersonelDuties")]
        public IActionResult GetAllPersonelDuties(int userId)
        {
            var result = _userDutyService.GetAll()
                .Where(ug => ug.Status == true && ug.UserId == userId);

            List<DutyList> dutyLists = new List<DutyList>();

            foreach (var userduty in result)
            {
                var duties = _dutyService.GetAll()
                    .Where(d => d.Status == true && d.Id == userduty.DutyId)
                    .ToList();

                foreach (var duty in duties)
                {
                    var dutyList = new DutyList
                    {
                        Id = duty.Id,
                        CreatedTime = duty.CreatedTime,
                        UpdatedTime = duty.UpdatedTime,
                        CreatedUserId = duty.CreatedUserId,
                        UpdatedUserId = duty.UpdatedUserId,
                        Status = duty.Status,
                        Title = duty.Title,
                        Explanation = duty.Explanation,
                        DeadLine = duty.DeadLine,
                        Emergency = duty.Emergency,
                        Progress = duty.Progress,
                        EmergencyName = duty.Emergency switch
                        {
                            1 => "Az",
                            2 => "Orta",
                            3 => "Çok",
                            _ => "Bilinmiyor"
                        },
                        ProgressName = duty.Progress switch
                        {
                            1 => "Yapılıyor",
                            2 => "Tamamlandı",
                            3 => "Başarısız",
                            _ => "Bilinmiyor"
                        },
                        UserId = userId.ToString(), 
                        UserName = _userService.GetById(userId)?.Name 
                    };

                    dutyLists.Add(dutyList);
                }
            }

            return Ok(dutyLists);
        }
        //Ekibindekilerin görevlerini verir.
        [HttpGet("GetSubordinatesDuties")]
        public IActionResult GetSubordinatesDuties(int userId)
        {
            User user = _userService.GetById(userId);
            List<User> subordinates  = _userService.GetAll()
                .Where(u => u.Status == true && u.TeamId == user.TeamId)
                .ToList();

            List<DutyList> dutyLists = new List<DutyList>();

            foreach (var subordinate in subordinates)
            {
                var userDuties = _userDutyService.GetAll()
                    .Where(ud => ud.Status == true && ud.UserId == subordinate.Id)
                    .ToList();

                foreach (var userDuty in userDuties)
                {
                    var duties = _dutyService.GetAll()
                        .Where(d => d.Status == true && d.Id == userDuty.DutyId)
                        .ToList();

                    foreach (var duty in duties)
                    {
                        var dutyList = new DutyList
                        {
                            Id = duty.Id,
                            CreatedTime = duty.CreatedTime,
                            UpdatedTime = duty.UpdatedTime,
                            CreatedUserId = duty.CreatedUserId,
                            UpdatedUserId = duty.UpdatedUserId,
                            Status = duty.Status,
                            Title = duty.Title,
                            Explanation = duty.Explanation,
                            DeadLine = duty.DeadLine,
                            Emergency = duty.Emergency,
                            Progress = duty.Progress,
                            EmergencyName = duty.Emergency switch
                            {
                                1 => "Az",
                                2 => "Orta",
                                3 => "Çok",
                                _ => "Bilinmiyor"
                            },
                            ProgressName = duty.Progress switch
                            {
                                1 => "Yapılıyor",
                                2 => "Tamamlandı",
                                3 => "Başarısız",
                                _ => "Bilinmiyor"
                            },
                            UserId = subordinate.Id.ToString(), 
                            UserName = $"{subordinate.Name} {subordinate.SurName}" 
                        };

                        dutyLists.Add(dutyList);
                    }
                }
            }

            return Ok(dutyLists);
        }


        [HttpGet("GetAllOthers")]
        public IActionResult GetAllOthers(int userId)
        {
            
            User theuser = _userService.GetById(userId);

            List<User> users = _userService.GetAll()
                .Where(user => user.TeamId == theuser.TeamId)
                .ToList();

            List<UserDuty> userDuties = new List<UserDuty>();

            foreach (var us in users)
            {
                var result = _userDutyService.GetAll()
                    .Where(ug => ug.Status == true && ug.UserId == us.Id)
                    .ToList();  

                userDuties.AddRange(result);  
            }

            return Ok(userDuties);
        }

       

       

        [HttpGet("GetUserDutyById")]
        public IActionResult GetUserDutyById(int id)
        {
            var result = _userDutyService.GetById(id);

            return Ok(result);
        }


        [HttpPost("AddUserDuty")]
        public IActionResult AddUserDuty(UserDuty userDuty)
        {
            _userDutyService.Add(userDuty);
            return Ok("İşlem Başarılı");
        }
        [HttpDelete("DeleteUserDuty")]
        public IActionResult DeleteUserDuty(int id)
        {
            var userDuty = _userDutyService.GetById(id);
            userDuty.Status = false;
            userDuty.UpdatedTime = DateTime.Now;
            _userDutyService.Update(userDuty);
            return Ok("İşlem Başarılı");
        }


        [HttpPut("UpdateUserDuty")]
        public IActionResult UpdateUserDuty(UserDuty _userDuty)
        {
            var userDuty = _userDutyService.GetById(_userDuty.Id);
            userDuty.UpdatedTime = DateTime.Now;

            userDuty.UserId = _userDuty.UserId;
            userDuty.DutyId = _userDuty.DutyId;


            _userDutyService.Update(userDuty);
            return Ok("İşlem Başarılı");
        }

        public class DutyList
        {
         
            public int Id { get; set; }
            public DateTime CreatedTime { get; set; }
            public DateTime UpdatedTime { get; set; }
            public int CreatedUserId { get; set; }
            public int UpdatedUserId { get; set; }
            public bool Status { get; set; }


            public string Title { get; set; }
            public string Explanation { get; set; }
            public DateTime DeadLine { get; set; }

            public int Emergency { get; set; }

            public int Progress { get; set; }

            public string EmergencyName { get; set; }

            public string ProgressName { get; set; }
            public string UserName { get; set; }
            public string UserId { get; set; }

        }


    }
}

