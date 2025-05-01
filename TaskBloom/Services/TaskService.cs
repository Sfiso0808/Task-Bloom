using TaskBloom.Models;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace TaskBloom.Services
{
    public class TaskService
    {
        private string connectionString = "server=localhost;user=root;password=;database=todo_app;";

        public List<TaskBloom.Models.Task> GetAllTasks()
        {
            List<TaskBloom.Models.Task> tasks = new List<TaskBloom.Models.Task>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM tasks";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TaskBloom.Models.Task task = new TaskBloom.Models.Task
                            {
                                Id = reader.GetInt32("id"),
                                Title = reader.GetString("title"),
                                Description = reader.GetString("description"),
                                DueDate = reader.GetDateTime("due_date"),
                                Priority = reader.GetInt32("priority"),
                                IsCompleted = reader.GetBoolean("is_completed")
                            };
                            tasks.Add(task);
                        }
                    }
                }
            }

            return tasks;
        }

        public void AddTask(TaskBloom.Models.Task task)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"INSERT INTO tasks (title, description, due_date, priority, is_completed) 
                                 VALUES (@Title, @Description, @DueDate, @Priority, @IsCompleted)";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Title", task.Title);
                    cmd.Parameters.AddWithValue("@Description", task.Description);
                    cmd.Parameters.AddWithValue("@DueDate", task.DueDate);
                    cmd.Parameters.AddWithValue("@Priority", task.Priority);
                    cmd.Parameters.AddWithValue("@IsCompleted", task.IsCompleted);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateTask(TaskBloom.Models.Task task)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"UPDATE tasks 
                                 SET title = @Title, description = @Description, due_date = @DueDate, 
                                     priority = @Priority, is_completed = @IsCompleted 
                                 WHERE id = @Id";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Title", task.Title);
                    cmd.Parameters.AddWithValue("@Description", task.Description);
                    cmd.Parameters.AddWithValue("@DueDate", task.DueDate);
                    cmd.Parameters.AddWithValue("@Priority", task.Priority);
                    cmd.Parameters.AddWithValue("@IsCompleted", task.IsCompleted);
                    cmd.Parameters.AddWithValue("@Id", task.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteTask(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM tasks WHERE id = @Id";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void MarkComplete(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE tasks SET is_completed = 1 WHERE id = @Id";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
