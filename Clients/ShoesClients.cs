using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace shoesAPI.Clients
{
    public class ShoesClient
    {
        private HttpClient _client;

        public ShoesClient(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<string> GetShoesbyBrands(string shoesmodel = "James Arizumi x Nike SB Dunk Low Pro «What the Dunk»")
        {
           
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://sneaker-database-stockx.p.rapidapi.com/getproducts?keywords={shoesmodel}&limit=1"),
                Headers =
                {
                    { "X-RapidAPI-Key", "8bcd5c855fmsh95d5b7d18acd558p1167a2jsn0fdbd42c363a" },
                    { "X-RapidAPI-Host", "sneaker-database-stockx.p.rapidapi.com" },
                },
            };

            using (var response = await _client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                return body;
            }
        }
    }
}
