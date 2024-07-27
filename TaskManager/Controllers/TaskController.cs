using Microsoft.AspNetCore.Mvc;
using TaskViewModel = TaskManager.Models.TaskViewModel;
using TaskManager.Services.Interfaces;
using System.Threading.Tasks;

namespace TaskManager.Controllers
{
    public class TaskController : Controller
    {
        private readonly ITaskService _serviceTask;

        public TaskController(ITaskService serviceTask)
        {
            _serviceTask = serviceTask;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var tasks = await _serviceTask.GetTasks();
                return View(tasks);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(TaskViewModel task)
        {
            try
            {
                await _serviceTask.CreateTask(task);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult> Edit(TaskViewModel task)
        {
            try
            {
                await _serviceTask.UpdateTask(task);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var task = await _serviceTask.GetTaskById(id);
                if (task == null)
                {
                    return NotFound();
                }
                return View(task);
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var task = await _serviceTask.GetTaskById(id);
                if (task == null)
                {
                    return NotFound();
                }
                return View(task);
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _serviceTask.DeleteTask(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}