using TaskKifyPro.Business.Abstract;
using TaskKifyPro.Business.Services;
using TaskKifyPro.Entity.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;
using TaskKifyPro.Business.Abstract;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static TaskKifyPro.WebAPI.Controllers.UserDutyController;

namespace TaskKifyPro.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerformanceController : ControllerBase
    {
        private readonly IPerformanceService _performanceService;
        private readonly IUserService _userService;

        public PerformanceController(IPerformanceService performanceService , IUserService userService)
        {
            _performanceService = performanceService;
            _userService = userService;
        }

        [HttpPost("DeletePerformancesMultiply")]
        public IActionResult DeletePerformancesMultiply([FromQuery] string selectedIds)
        {
            var idList = selectedIds.Split(',').ToList();

            foreach (var id in idList)
            {
                var user = _performanceService.GetById(Convert.ToInt32(id));
                if (user != null)
                {
                    user.Status = false;
                    user.UpdatedTime = DateTime.Now;
                    _performanceService.Update(user);
                }
            }

            return Ok("İşlem Başarılı");
        }

        //Ekip liderinin ekibindeki herkesin performanslarını verir.
        [HttpGet("GetAllSubordinatesPerformances")]
        public IActionResult GetAllSubordinatesPerformances(int id)
        {
            User user = _userService.GetById(id);

            if (user == null)
            {
                return NotFound("Kullanıcı bulunamadı.");
            }

            List<User> subordinates = _userService.GetAll()
                .Where(u => u.Status == true && u.TeamId == user.TeamId)
                .ToList();

            List<Performance> performanceList = new List<Performance>();

            foreach (var subordinate in subordinates)
            {
                List<Performance> performances = _performanceService.GetAll()
                    .Where(ud => ud.Status == true && ud.UserId == subordinate.Id)
                    .ToList();

                performanceList.AddRange(performances);
            }

            

            performanceList.AddRange(performanceList);

            List<PerformanceModal> pers = new List<PerformanceModal>();
            foreach (var item in performanceList)
            {
                User u = _userService.GetById(item.UserId);
                PerformanceModal p = new PerformanceModal();
                p.Id = item.Id;
                p.CreatedTime = item.CreatedTime;
                p.UpdatedTime = item.UpdatedTime;
                p.CreatedUserId = item.CreatedUserId;
                p.UpdatedUserId = item.UpdatedUserId;
                p.Status = item.Status;
                p.TeamId = item.TeamId;
                p.UserId = item.UserId;
                p.PerformanceNote = item.PerformanceNote;
                p.OwnerName = u.Name;

                pers.Add(p);
            }


            return Ok(pers);
        }

        //Biresyel performansı döndürür.
        [HttpGet("GetAllPersonel")]
        public IActionResult GetAllPersonel(int id)
        {
            var result = _performanceService.GetAll().Where(ug => ug.Status == true && ug.UserId == id).OrderBy(ug => ug.PerformanceNote);
            return Ok(result);
        }

        [HttpGet("GetPerformanceById")]
        public IActionResult GetPerformanceById(int id)
        {
            var result = _performanceService.GetById(id);
            return Ok(result);
        }
        [HttpPost("AddPerformance")]
        public IActionResult AddPerformance(Performance performance)
        {
            _performanceService.Add(performance);
            return Ok("İşlem Başarılı");
        }
        [HttpDelete("DeletePerformance")]
        public IActionResult DeletePerformance(int id)
        {
            var performance = _performanceService.GetById(id);
            performance.Status = false;
            performance.UpdatedTime = DateTime.Now;
            _performanceService.Update(performance);
            return Ok("İşlem Başarılı");
        }


        [HttpPut("UpdatePerformance")]
        public IActionResult UpdatePerformance(Performance _performance)
        {
            var performance = _performanceService.GetById(_performance.Id);
            performance.UpdatedTime = DateTime.Now;

            performance.UserId = _performance.UserId;
            performance.PerformanceNote = _performance.PerformanceNote;


            _performanceService.Update(performance);
            return Ok("İşlem Başarılı");
        }

        public class PerformanceModal
    {
       
        public int Id { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }
        public int CreatedUserId { get; set; }
        public int UpdatedUserId { get; set; }
        public bool Status { get; set; }

        public int TeamId { get; set; }



        public int UserId { get; set; }
        public int PerformanceNote { get; set; }

        public string OwnerName { get; set; }



        }

    }
}

