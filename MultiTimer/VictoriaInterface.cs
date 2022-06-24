using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MultiTimer
{
    internal class VictoriaInterface
    {
        private static readonly HttpClient client = new HttpClient();

        public VictoriaInterface()
        {
            client.BaseAddress = new Uri("https://app.dcslsoftware.com/VictoriaUAT/");
        }

        public async Task<bool> Authenticate(string username, string password)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("username", username),
                new KeyValuePair<string, string>("password", password)

            });
            var response = await client.PostAsync("/api/v1/authorisation", content);
            response.EnsureSuccessStatusCode();
            var token = response.Content.ToString();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return true;
        }

        public async Task<IEnumerable<int>> GetProjectIdsForUser()
        {
            return null;
        }

        public async Task<IEnumerable<ProjectTask>> GetProjectTasks(int projectId)
        {
            return null;
        }

        private async Task<string> GetTimesheetToken()
        {
            return "";
        }

        private async Task<bool> AddTimesheetEntry(string token, DateTime date, TimesheetEntry timesheetEntry)
        {
            return false;
        }

        public async Task<bool> AddTimesheetEntries(DateTime date, IEnumerable<TimesheetEntry> timesheetEntry)
        {
            bool success = true;

            string token = await GetTimesheetToken();

            foreach (TimesheetEntry entry in timesheetEntry)
            {
                if (!await AddTimesheetEntry(token, date, entry))
                {
                    success = false;
                    break;
                }
            }

            return success;
        }
    }
}
