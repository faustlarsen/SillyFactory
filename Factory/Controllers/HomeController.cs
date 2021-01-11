using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Factory.Models;


namespace Factory.Controllers
{
  public class HomeController : Controller
  {
    private readonly FactoryContext _db;

    public HomeController(FactoryContext db)
    {
      _db = db;
    }

    [HttpGet("/")]
    public ActionResult Index()
    {

      var FullList = new SplashList();
      FullList.EngineersList = _db.Engineers.ToList();
      FullList.MachinesList = _db.Machines.ToList();
      return View(FullList);
      // ViewBag.Machines = _db.Machines.ToList();
      // return View(_db.Engineers.ToList());
    }
  }
}
