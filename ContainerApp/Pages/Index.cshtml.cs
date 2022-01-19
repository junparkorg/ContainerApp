using ContainerApp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContainerApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public async Task OnGet()
        {
            Todo todo = await GetTodoAsync();
            

        }

        private async Task<Todo> GetTodoAsync(CancellationToken cancellationToken = default)
        {
            using var httpclient = new HttpClient();
            var response =
                await httpclient.GetAsync("https://jsonplaceholder.typicode.com/todos/1", cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Something went wrong!!");
            }

            return await response.Content.ReadFromJsonAsync<Todo>();
        }
    }
}