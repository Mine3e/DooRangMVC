using Business.Exceptions;
using Business.Services.Absracts;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DiaSymReader;

namespace Doorang.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class WorldController : Controller
    {
        private readonly IWorldService _worldService;
        public WorldController(IWorldService worldService)
        {
            _worldService = worldService;
        }
        public IActionResult Index()
        {
            var worlds=_worldService.GetAllWorlds();
            return View(worlds);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(World world) 
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                _worldService.AddWorld(world);
            }
            catch(FilecontentTypeException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch(FileSizeException ex)
            {
                ModelState.AddModelError(ex.propertyname, ex.Message);
                return View();
            }
            catch(WorldNullException ex)
            {
                ModelState.AddModelError(ex.PropertyName,ex.Message);
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            var existWorld= _worldService.GetWorld(x=>x.Id==id);
            if(existWorld==null)
            {
                return NotFound();
            }
            return View(existWorld);
        }
        [HttpPost]
        public IActionResult Deleteworld(int id)
        {
           
            try
            {
                _worldService.DeleteWorld(id);
            }
            catch(FileNotFoundException ex)
            {
                return NotFound();
            }
            catch(EntityNotFound ex)
            {
                return NotFound();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int id)
        {
            var world = _worldService.GetWorld(x=>x.Id==id);    
            if(world==null)
            {
                return NotFound();
            }
            return View(world);
        }
        [HttpPost]
        public IActionResult Update(World world)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            _worldService.UpdateWorld(world.Id, world); 
            return RedirectToAction(nameof(Index));
        }
    }
}
