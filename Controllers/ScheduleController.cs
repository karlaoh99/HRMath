using System;
using Microsoft.AspNetCore.Mvc;
using HRMath.Models;
using HRMath.Data;
using System.Linq;


namespace HRMath.Controllers
{
    public class  ScheduleController : Controller
    {
        private IClassRepository classRepository;
        private IScheduleRepository scheduleRepository;

        public ScheduleController(IClassRepository classRepository, IScheduleRepository scheduleRepository)
        {
            this.classRepository = classRepository;
            this.scheduleRepository = scheduleRepository;
        }

        public IActionResult Index(int? scheduleId)
        {
            if (scheduleId == null)
            {
                try {
                    var schedule = scheduleRepository.Schedules.First();
                    return View(classRepository.GetBySchedule(4));
                } 
                catch (InvalidOperationException)
                {
                    return View(null);
                }
            }
            return View(classRepository.GetBySchedule((int)scheduleId));
        }

    }


}