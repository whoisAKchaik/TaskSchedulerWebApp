using TaskSchedulerWebApp.Models;

namespace TaskSchedulerWebApp.Services
{
    public interface ITaskStorageService
    {
        List<ScheduledTask> GetAllTasks();
        ScheduledTask AddTask(ScheduledTask task);
    }
}
