using TaskKifyPro.Business.Abstract;
using TaskKifyPro.Business.Services;
using TaskKifyPro.Entity.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;
using TaskKifyPro.Business.Abstract;
using System.ComponentModel.Design;
using TaskKifyPro.DataAccess.Context;

namespace TaskKifyPro.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DutyController : ControllerBase
    {
        private readonly IDutyService _dutyService;
        private readonly IUserDutyService _userDutyService;
        private readonly IPerformanceService _performanceService;
        private readonly INotificationService _notificationService;
        private readonly IUserService _userService;

        public DutyController(IDutyService dutyService, IUserDutyService userDutyService , IPerformanceService performanceService , INotificationService notificationService , IUserService userService)
        {
            _dutyService = dutyService;
            _userDutyService = userDutyService;
            _performanceService = _performanceService;
            _notificationService = notificationService;
            _userService = userService;

        }
        TaskifyProDbContext context = new TaskifyProDbContext();


        //Toplu görev silimi
        [HttpPost("DeleteDutiesMultiply")]
        public IActionResult DeleteDutiesMultiply([FromQuery] string selectedIds)
        {
            var idList = selectedIds.Split(',').ToList();

            foreach (var id in idList)
            {
                var user = _dutyService.GetById(Convert.ToInt32(id));
                if (user != null)
                {
                    user.Status = false;
                    user.UpdatedTime = DateTime.Now; 
                    _dutyService.Update(user);
                }
            }

            return Ok("İşlem Başarılı");
        }


        [HttpGet("GetAll")]
        public IActionResult GetAll(int userId)
        {
            var result = _dutyService.GetAll().Where(ug => ug.Status == true).OrderBy(ug => ug.Title);
            return Ok(result);
        }

        [HttpGet("GetDutyById")]
        public IActionResult GetDutyById(int id)
        {
            var result = _dutyService.GetById(id);
            return Ok(result);
        }
        
        //Biresysel görev ekleme.
        [HttpPost("AddPersonelDuty")]
        public IActionResult AddPersonelDuty(Duty duty)
        {
            duty.CreatedTime = DateTime.Now;
            duty.UpdatedTime = DateTime.Now;
            duty.Status = true;
            _dutyService.Add(duty);

            UserDuty userduty = new UserDuty();

            userduty.CreatedTime = DateTime.Now;
            userduty.UpdatedTime = DateTime.Now;
            userduty.Status = true;
            userduty.DutyId = duty.Id;
            userduty.UserId = duty.CreatedUserId;

            _userDutyService.Add(userduty);

            return Ok("İşlem Başarılı");
        }


        //Ekip lideri ekibinden birini bu metodla görevlendirebilir.
        [HttpPost("AddDutySubordinates")]
        public IActionResult AddDutySubordinates([FromBody] AddDutyRequest request)
        {
            if (request == null || request.Duty == null || !request.Ids.Any())
            {
                return BadRequest("Geçersiz veri.");
            }

            var duty = request.Duty;
            duty.CreatedTime = DateTime.Now;
            duty.UpdatedTime = DateTime.Now;
            duty.Status = true;

            _dutyService.Add(duty);

            foreach (var id in request.Ids)
            {
                var userDuty = new UserDuty
                {
                    CreatedTime = DateTime.Now,
                    UpdatedTime = DateTime.Now,
                    CreatedUserId = request.CreatedUserId,
                    UpdatedUserId = request.UpdatedUserId,
                    Status = true,
                    TeamId = request.TeamId,
                    UserId = id,
                    DutyId = duty.Id
                };

                _userDutyService.Add(userDuty);
            }

            return Ok("İşlem Başarılı");
        }

        public class AddDutyRequest
        {
            public Duty Duty { get; set; }
            public List<int> Ids { get; set; }
            public int CreatedUserId { get; set; }
            public int UpdatedUserId { get; set; }
            public int TeamId { get; set; }
        }



       //Görevi silme.
        [HttpDelete("DeleteDuty")]
        public IActionResult DeleteDuty(int id)
        {
            var duty = _dutyService.GetById(id);
            duty.Status = false;
            duty.UpdatedTime = DateTime.Now;
            _dutyService.Update(duty);
            return Ok("İşlem Başarılı");
        }

        //Görevi güncelleme.
        [HttpPut("UpdateDuty")]
        public IActionResult UpdateDuty(Duty _duty)
        {
            var duty = _dutyService.GetById(_duty.Id);
            duty.UpdatedTime = DateTime.Now;

            duty.Title = _duty.Title;
            duty.Explanation = _duty.Explanation;
            duty.DeadLine = _duty.DeadLine;
            duty.Emergency = _duty.Emergency;
            duty.Progress = _duty.Progress;


            _dutyService.Update(duty);
            return Ok("İşlem Başarılı");
        }


        //Görevi tamamlandı olarak işaretle.
        [HttpPost("DutyCompleated")]
        public IActionResult DutyCompleated(int id)
        {
            var duty = _dutyService.GetById(id);

            if (duty == null)
            {
                return NotFound("Görev bulunamadı.");
            }

            duty.UpdatedTime = DateTime.Now;
            duty.Progress = 2; 

            _dutyService.Update(duty);

            var userDuty = context.UserDuties.FirstOrDefault(x => x.DutyId == duty.Id);

            if (userDuty == null)
            {
                return NotFound("Kullanıcı görevi bulunamadı.");
            }

            var performance = context.Performances.FirstOrDefault(x => x.UserId == userDuty.UserId);

            if (performance != null)
            {
                performance.PerformanceNote += 1; 
                performance.UpdatedTime = DateTime.Now; 

                context.Performances.Update(performance);
            }
            else
            {
                var newPerformance = new Performance
                {
                    CreatedTime = DateTime.Now,
                    UpdatedTime = DateTime.Now,
                    CreatedUserId = userDuty.UserId,
                    UpdatedUserId = userDuty.UserId,
                    Status = true,
                    TeamId = duty.TeamId,
                    UserId = userDuty.UserId,
                    PerformanceNote = 1
                };

                context.Performances.Add(newPerformance);
            }

            context.SaveChanges();


            User us = _userService.GetById(userDuty.UserId);

            Notification not = new Notification();
            not.CreatedTime = DateTime.Now;
            not.UpdatedTime = DateTime.Now;
            not.CreatedUserId = userDuty.UserId;
            not.UpdatedUserId = userDuty.UserId;
            not.Status = true;
            not.Title = "Görev Tamamlandı";

            not.Message = duty.Title + " görevi" + us.Name + " İsimli kullancı tarafından tamamlanmıştır";

            not.DutyOwnerId = userDuty.UserId;

            not.TeamId = duty.TeamId;

            _notificationService.Add(not);


            return Ok("İşlem başarılı.");
        }



    }
}

