using Microsoft.AspNetCore.Mvc;
using Factory.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Factory.Controllers
{
    public class MachinesController : Controller
    {
        private readonly FactoryContext _db;
        public MachinesController(FactoryContext db)
        {
            _db = db;
        }
        
        public ActionResult Index(string MachineSearch)
        {
            ViewBag.MachineId = new SelectList(_db.Machines, "EngineerId", "Name");
            List<Machine> model = _db.Machines.ToList();
            if(MachineSearch!=null) {
                model = _db.Machines.Where(machine => machine.Name.Contains(MachineSearch)).ToList();
            }
            return View(model);
        }

        public ActionResult Create()
        {
            ViewBag.EngineerId = new SelectList(_db.Engineers, "EngineerId", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Machine machine, int EngineerId)
        {
            _db.Machines.Add(machine);
            if  (EngineerId != 0)
        {
            _db.EngineerMachine.Add(new EngineerMachine() { EngineerId = EngineerId, MachineId = machine.MachineId });
        }
            _db.SaveChanges();
            return RedirectToAction("Index");  
        }
        
        public ActionResult Details(int id)
        {
            var thisMachine = _db.Machines
                .Include(machine => machine.Engineers)
                .ThenInclude(join => join.Engineer)
                .FirstOrDefault(machine => machine.MachineId == id);
            return View(thisMachine);
        }

        public ActionResult Edit(int id)
        {
            var thisMachine = _db.Machines.FirstOrDefault(machines => machines.MachineId ==id);
            ViewBag.EngineerId = new SelectList(_db.Engineers, "EngineerId", "Name");
            return View(thisMachine);
        }

        [HttpPost]
        public ActionResult Edit(Machine machine, int EngineerId)
        {
            var joinConfirm = _db.EngineerMachine.FirstOrDefault(join => join.MachineId == machine.MachineId && join.EngineerId == EngineerId);

        if(joinConfirm != null)
        {
            _db.Entry(machine).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Details", new { id = machine.MachineId});
        }
            if (EngineerId != 0)
        {
            _db.EngineerMachine.Add(new EngineerMachine() { EngineerId = EngineerId, MachineId = machine.MachineId });
        }
            _db.Entry(machine).State =EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Details", new {id = machine.MachineId});
        }

        public ActionResult Delete(int id)
        {
            var thisMachine = _db.Machines.FirstOrDefault(machines => machines.MachineId ==id);
            return View(thisMachine);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var thisMachine = _db.Machines.FirstOrDefault(machines => machines.MachineId == id);
            _db.Machines.Remove(thisMachine);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult DeleteEngineer(int joinId)
        {
            var joinEntry = _db.EngineerMachine.FirstOrDefault(entry => entry.EngineerMachineId == joinId);
            _db.EngineerMachine.Remove(joinEntry);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteAll()
        {
            var allMachines = _db.Machines.ToList();
            return View();
        }

        [HttpPost, ActionName("DeleteAll")]
            public ActionResult DeleteAllConfirmed()
        {
            var allMachines = _db.Machines.ToList();

        foreach (var machine in allMachines)
        {
            _db.Machines.Remove(machine);
        }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

