using TaskKifyPro.Business.Abstract;
using TaskKifyPro.Business.Services;
using TaskKifyPro.Entity.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;
using TaskKifyPro.Business.Abstract;
using System.ComponentModel.Design;
using TaskKifyPro.DataAccess.Context;
using Microsoft.AspNetCore.SignalR;

namespace TaskKifyPro.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IDutyService _dutyService;
        private readonly IUserDutyService _userDutyService;
        private readonly IPerformanceService _performanceService;
        private readonly INotificationService _notificationService;
        private readonly IUserService _userService;

        public HomeController(IDutyService dutyService, IUserDutyService userDutyService, IPerformanceService performanceService, INotificationService notificationService, IUserService userService)
        {
            _dutyService = dutyService;
            _userDutyService = userDutyService;
            _performanceService = _performanceService;
            _notificationService = notificationService;
            _userService = userService;

        }
        TaskifyProDbContext context = new TaskifyProDbContext();


        //Anasayfada gösterilen bilgilendirmeler buradan geliyor.
        [HttpGet("GetAllNotification")]
        public IActionResult GetAllNotification(int teamId)
        {
            var notifications = _notificationService.GetAll()
                .Where(ug => ug.Status == true && ug.TeamId == teamId)
                .OrderBy(ug => ug.Title)
                .ToList(); 

            var users = _userService.GetAll()
                .Where(x => x.TeamId == teamId)
                .ToList();

            var userDuties = _userDutyService.GetAll()
                .Where(x => users.Select(u => u.Id).Contains(x.UserId))
                .ToList();

            var dutyIds = userDuties.Select(x => x.DutyId).Distinct().ToList();
            var duties = GetDutiesByIds(dutyIds);

            var currentTime = DateTime.Now;

            foreach (var duty in duties)
            {
                var dutyOwnerId = userDuties.FirstOrDefault(ud => ud.DutyId == duty.Id)?.UserId;
                var dutyOwner = users.FirstOrDefault(u => u.Id == dutyOwnerId);

                var notification = new Notification
                {
                    CreatedTime = currentTime,
                    UpdatedTime = currentTime,
                    CreatedUserId = dutyOwnerId ?? 0,
                    UpdatedUserId = dutyOwnerId ?? 0,
                    Status = true,
                    TeamId = duty.TeamId,
                    DutyOwnerId = dutyOwnerId ?? 0
                };

                if (duty.DeadLine.Date == currentTime.Date)
                {
                    notification.Title = "Görevin son günü";
                    notification.Message = $"{duty.Title} görevinin son günü bugündür. {dutyOwner?.Name} isimli kullanıcı tarafından tamamlanması gerekmektedir.";
                }
                else if (duty.DeadLine < currentTime)
                {
                    notification.Title = "Görev Başarısız";
                    notification.Message = $"{duty.Title} görevinin deadline'ı geçmiştir. {dutyOwner?.Name} isimli kullanıcı tarafından tamamlanması gerekmektedir.";
                }

                if (duty.DeadLine.Date == currentTime.Date || duty.DeadLine < currentTime)
                {
                    notifications.Add(notification);
                }
            }

            var sortedNotifications = notifications.OrderBy(n => n.Title).ToList();

            return Ok(sortedNotifications);
        }


        [HttpGet("GetDutiesByIds")]

        public List<Duty> GetDutiesByIds(List<int> dutyIds)
        {
            if (dutyIds == null || !dutyIds.Any())
            {
                return new List<Duty>(); 
            }

           
            var duties = context.Duties
                   .Where(duty => dutyIds.Contains(duty.Id) && duty.Status)
                   .ToList();

            return duties;
        }



    }
}

