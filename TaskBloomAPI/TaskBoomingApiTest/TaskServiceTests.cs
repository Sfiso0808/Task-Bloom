using Xunit;

using TaskBloomAPI.Services;
using TaskBloomAPI.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace TaskBoomingApiTest
{
    public class TaskServiceTests
    {
        private readonly TaskService _taskService;

        public TaskServiceTests()
        {
            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "ConnectionStrings:DefaultConnection", "your_mysql_connection_string_here" }
                })
                .Build();

            _taskService = new TaskService(config);
        }

        [Fact]
        public void GetAllTasks_ReturnsList()
        {
            var result = _taskService.GetAllTasks();
            Assert.NotNull(result);
            Assert.IsType<List<TaskBloomAPI.Models.Task>>(result);

        }
    }
}
