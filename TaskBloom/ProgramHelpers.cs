using System;
using System.IO;
using TaskBloom.Models; // Correctly import your Models!

internal static class ProgramHelpers
{
    public static void LogCompletedTask(TaskBloom.Models.Task task)
    {
        string logPath = "completed_log.txt";

        using (var writer = new StreamWriter(logPath, append: true))
        {
            writer.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | ID: {task.Id} | Title: {task.Title}");
        }
    }
}
