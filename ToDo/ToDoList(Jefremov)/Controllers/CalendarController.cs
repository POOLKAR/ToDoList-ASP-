using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToDoList_Jefremov_.Models;
using Microsoft.AspNet.Identity;

namespace DanaTask_2.Controllers
{
    public class CalendarController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //Обычный год
        private int[] DaysInMonths_28 = new int[] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        //Високосный
        private int[] DaysInMonths_29 = new int[] { 31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

        //Календарь текущего юзера
        [HttpGet]
        public ActionResult Index(int month = -1, int year = -1)
        {
            //Если юзер не залогинен, выдаем ошибку 403
            if (!User.Identity.IsAuthenticated)
                return new HttpStatusCodeResult(403);

            //Если год = -1, то берем текущий год
            if (year == -1)
                year = DateTime.Now.Year;
            //Тоже самое с месяцем
            if (month == -1)
                month = DateTime.Now.Month;
            //Если кто сломал URL и вписал свое число, то делаем так, что бы оно было в диапазоне от 1 до 12
            if (month > 12)
                month = 12;
            else if (month < 1)
                month = 1;

            //Получаем информацию о задачах в выбраном году и месяце
            string userId = User.Identity.GetUserId();
            ToDo[] tasks = db.ToDo.Where(x => x.User.Id == userId && x.Date.Year == year && x.Date.Month == month).ToArray();

            //Високосный год или нет
            int[] usingCalendar = DaysInMonths_28;
            if (year % 4 == 0)
                usingCalendar = DaysInMonths_29;

            //Передаем значения на страницу
            ViewBag.Year = year;
            ViewBag.Month = month;
            ViewBag.MonthDays = usingCalendar[month - 1];
            ViewBag.MonthString = new DateTime(year, month, 1).ToString("MMM", CultureInfo.InvariantCulture);
            ViewBag.UsingCalendar = usingCalendar;
            ViewBag.Tasks = tasks;

            return View();
        }

        //Добавление задачи к выбранной дате
        [HttpGet]
        public ActionResult AddTask(int month, int year, int day)
        {
            //Если юзер не залогинен, выдаем ошибку 403
            if (!User.Identity.IsAuthenticated)
                return new HttpStatusCodeResult(403);

            ViewBag.Errors = new string[] { };
            return RedirectToAction("Create", "ToDoes",
            new ToDo() { Date = new DateTime(year, month, day) });
        }

        //Просмотр всех задач, привязанных к выбранной дате
        [HttpGet]
        public ActionResult Tasks(int year, int month, int day)
        {
            //Если юзер не залогинен, выдаем ошибку 403
            if (!User.Identity.IsAuthenticated)
                return new HttpStatusCodeResult(403);

            //Получаем информацию о задачах в выбраном году и месяце
            string userId = User.Identity.GetUserId();
            ToDo[] tasks = db.ToDo.Where(x => x.User.Id == userId && x.Date.Year == year && x.Date.Month == month && x.Date.Day == day).ToArray();

            //Передаем данные на страницу
            ViewBag.Tasks = tasks;

            ViewBag.Day = day;
            ViewBag.Month = month;
            ViewBag.Year = year;

            ViewBag.MonthString = new DateTime(year, month, day).ToString("MMM", CultureInfo.InvariantCulture);

            return View();
        }
    }
}