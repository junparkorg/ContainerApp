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

        public Todo Todo { get; private set; }

        public string ResultToDisplay { get; set; }


        public async Task OnGet()
        {
            Todo = await GetTodoAsync();

            try
            {
                var results = await CallAPI();
                ResultToDisplay = results;

            }catch(Exception ex)
            {
                ResultToDisplay = ex.Message + " " + ex.StackTrace;
            }

        }

        private async Task<string> CallAPI()
        {
            string result = string.Empty;
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync("http://20.200.240.108/APITest");
                result = await response.Content.ReadAsStringAsync();

            }
            return result;
        }


        private async Task<Todo> GetTodoAsync(CancellationToken cancellationToken = default)
        {
            Random random = new Random();
            var index = random.Next(1, 100);

            using var httpclient = new HttpClient();
            var response =
                await httpclient.GetAsync($"https://jsonplaceholder.typicode.com/todos/{index}"
                , cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Something went wrong!!");
            }

            return await response.Content.ReadFromJsonAsync<Todo>();
        }
    }
}