using Microsoft.AspNetCore.Mvc;
using Factory.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Factory.Controllers
{
    public class EngineersController : Controller
    {
        private readonly FactoryContext _db;

        public EngineersController(FactoryContext db)
        {
            _db = db;
        }

        public ActionResult Index(string EngineerSearch)
        {   
            List<Engineer> model = _db.Engineers.ToList();
            if(EngineerSearch!=null) {
                model = _db.Engineers.Where(engineers => engineers.Name.Contains(EngineerSearch)).ToList();
            }
            return View(model);
        }

        public ActionResult Create()
        {
            ViewBag.MachineId = new SelectList(_db.Machines, "MachineId", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Engineer engineer)
        {
            _db.Engineers.Add(engineer);
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
            ViewBag.EngineerId = new SelectList(_db.Engineers, "EngineerId", "Name");
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
            ViewBag.MachineId = new SelectList(_db.Machines, "MachineId", "Name");
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