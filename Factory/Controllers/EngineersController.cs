using Microsoft.AspNetCore.Mvc;
using Factory.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Factory.Controllers
{   
    [Authorize]
    public class EngineersController : Controller
    {   
        private readonly FactoryContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public EngineersController(UserManager<ApplicationUser> userManager, FactoryContext db)
    {
        _userManager = userManager;
        _db = db;
    }
        // public ActionResult Index(string EngineerSearch)
        // {   
        //     List<Engineer> model = _db.Engineers.ToList();
        //     if(EngineerSearch!=null) {
        //         model = _db.Engineers.Where(engineers => engineers.EngineerName.Contains(EngineerSearch)).ToList();
        //     }
        //     return View(model);
        // }
        public async Task<ActionResult> Index(string EngineerSearch)
        { 

            List<Engineer> model = _db.Engineers.ToList();
            if(EngineerSearch!=null) {
                model = _db.Engineers.Where(engineers => engineers.EngineerName.Contains(EngineerSearch)).ToList();
            }
            // return View(model);
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            // var userItems = _db.Engineers.Where(entry => entry.User.Id == currentUser.Id).ToList();
            return View(model);
        }
        // public async Task<ActionResult> Index()
        // { 
        //     var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        //     var currentUser = await _userManager.FindByIdAsync(userId);
        //     var userItems = _db.Engineers.Where(entry => entry.User.Id == currentUser.Id).ToList();
        //     return View(userItems);
        // }git add
        public ActionResult Create()
        {
            ViewBag.MachineId = new SelectList(_db.Machines, "MachineId", "MachineName");
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Engineer engineer, int MachineId)
        {    
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            engineer.User = currentUser;

            _db.Engineers.Add(engineer);
            if (MachineId != 0)
            {
                _db.EngineerMachine.Add(new EngineerMachine() { MachineId = MachineId, EngineerId = engineer.EngineerId });
            }

            _db.SaveChanges();
            return RedirectToAction("Index");
        } 
    
        public ActionResult Details(int id)
        {
            var thisEngineer = _db.Engineers
                .Include(engineer => engineer.Machines)
                .ThenInclude(join => join.Machine)
                .FirstOrDefault(engineer => engineer.EngineerId == id);
            return View(thisEngineer);
        }

        public ActionResult Edit(int id)
        {
            var thisEngineer = _db.Engineers.FirstOrDefault(engineer => engineer.EngineerId == id);
            ViewBag.EngineerId = new SelectList(_db.Engineers, "EngineerId", "EngineerName");
            return View(thisEngineer);
        }

        [HttpPost]
        public ActionResult Edit(Engineer engineer)
        {
            _db.Entry(engineer).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Details", new { id = engineer.EngineerId });
        }
    
        public ActionResult Delete(int id)
        {
            var thisEngineer = _db.Engineers.FirstOrDefault(engineer => engineer.EngineerId== id);
            return View(thisEngineer);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var thisEngineer = _db.Engineers.FirstOrDefault(engineer => engineer.EngineerId ==id);
            _db.Engineers.Remove(thisEngineer);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteAll()
        {
            var allEngineers = _db.Engineers.ToList();
            return View();
        }

        [HttpPost, ActionName("DeleteAll")]
            public ActionResult DeleteAllConfirmed()
        {
            var allEngineers = _db.Engineers.ToList();

        foreach (var engineer in allEngineers)
        {
            _db.Engineers.Remove(engineer);
        }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult AddMachine(int id)
        {
            var thisEngineer = _db.Engineers.FirstOrDefault(engineer => engineer.EngineerId == id);
            ViewBag.MachineId = new SelectList(_db.Machines, "MachineId", "MachineName");
            return View(thisEngineer);
        }

        [HttpPost]
        public ActionResult AddMachine(Engineer engineer, int MachineId)
        {
            if (MachineId != 0)
            {  
                _db.EngineerMachine.Add(new EngineerMachine() { EngineerId = engineer.EngineerId,  MachineId = MachineId });
            }
            _db.SaveChanges();
            return RedirectToAction("Details", "Engineers", new {id = engineer.EngineerId});
        }
    }
}