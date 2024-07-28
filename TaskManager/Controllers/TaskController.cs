using Microsoft.AspNetCore.Mvc;
using TaskViewModel = TaskManager.Models.TaskViewModel;
using TaskManager.Services.Interfaces;

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
                return View("Error");
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(TaskViewModel task)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _serviceTask.CreateTask(task);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    return View("Error");
                }
            }
            return View(task);
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
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Edit(TaskViewModel task)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _serviceTask.UpdateTask(task);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    return View("Error");
                }
            }
            return View(task);
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
                return View("Error");
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
                return View("Error");
            }
        }
    }
}
