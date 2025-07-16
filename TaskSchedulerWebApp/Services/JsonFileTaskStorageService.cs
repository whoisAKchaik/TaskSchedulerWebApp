using System.Text.Json;
using TaskSchedulerWebApp.Models;

namespace TaskSchedulerWebApp.Services
{
    public class JsonFileTaskStorageService : ITaskStorageService
    {
        private readonly string _filePath = "ScheduledTasks.json";

        public List<ScheduledTask> GetAllTasks()
        {
            if (!File.Exists(_filePath))
                return new List<ScheduledTask>();

            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<ScheduledTask>>(json) ?? new List<ScheduledTask>();
        }

        public ScheduledTask AddTask(ScheduledTask task)
        {
            var tasks = GetAllTasks();

            // Auto-increment ID
            task.Id = tasks.Count > 0 ? tasks.Max(t => t.Id) + 1 : 1;

            tasks.Add(task);
            var updatedJson = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, updatedJson);

            return task;
        }
    }
}
