using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using ProjektTest2.Data;
using ProjektTest2.Models;
using Task = ProjektTest2.Models.Task;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;
using ProjektTest2.Utilities;
using Hangfire;

namespace ProjektTest2.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUsers> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _uow;
        private readonly IGenericRepository<Task> _repo;
        private readonly IMailSender _mailSender;
        private readonly IMailManager _mailManager;

        public HomeController(UserManager<ApplicationUsers> userManager, ApplicationDbContext context, IUnitOfWork unit, IGenericRepository<Task> repo, IMailSender mailSender, IMailManager mailManager)
        {
            _userManager = userManager;
            _context = context;
            _uow = unit;
            _repo = repo;
            _mailSender = mailSender;
            _mailManager = mailManager;
        }

        // GET: Driver
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _uow.Context.Tasks.Where(t => t.ApplicationUsers_Email == _userManager.GetUserName(HttpContext.User)).ToListAsync());
           
        }

        // GET: Driver/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _uow.Context.Users.FirstOrDefaultAsync(m => m.Equals(id));
          
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // GET: Driver/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Driver/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AddedDay, FinishDay, FinishTime, TaskContent")] Task task)
        {

            if (ModelState.IsValid)
            {
                
            var user = _userManager.GetUserAsync(HttpContext.User);
            task.ApplicationUsers_Email = user.Result.Email;
            task.AddTime = DateTime.Now;
                var setDay = task.FinishDay.AddDays(-1);
                var setTime = task.FinishTime;
                var SendData = new DateTime(setDay.Year, setDay.Month, setDay.Day, setTime.Hour, setTime.Minute, setTime.Second);
                BackgroundJob.Schedule(() => SendMail(task), SendData);// TimeSpan.FromSeconds(1));
                _uow.Context.Tasks.Add(task);
                await _uow.Context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(task);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _uow.Context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TaskId,AddedDay,AddedTime,FinishTime,TaskContent")] Task task)
        {
            var user = _userManager.GetUserAsync(HttpContext.User);
            if (user.Id == id)
            {
      
                return View("Error");
            }

            if (ModelState.IsValid)
            {
                try
                {
                 
                    task.ApplicationUsers_Email = user.Result.Email;
                    _uow.Context.Update(task);
                    await _uow.Context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                  
                    if (user == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(task);
        }

        // GET: Driver/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _uow.Context.Tasks.FirstOrDefaultAsync(m => m.TaskId == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // POST: Driver/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Task task)
        {
            _uow.Context.Tasks.Remove(task);
            await _uow.Context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //private bool TaskExists(int id)
        //{
        //    return _uow.Context.Tasks.Any(m => m.TaskId == id);
        //}
        [Authorize]
        public void SendMail(Task task) // czy tworzy się metody asynchroniczne dla void ?
        {

            MailManager _mailManager = new MailManager();
            MailSender _mailSender = new MailSender();
            var message = _mailManager.CreateMessage("Task reminder", "chorzowski3@gmail.com", "Babel", task.ApplicationUsers_Email, "Task to do on: " + task.FinishTime.ToString(), task.TaskContent);
            _mailSender.SendMessage(message);

        }

    }
}
