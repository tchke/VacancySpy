using System;
using System.Net.Http;

namespace VacancySpy
{
    public class Fetcher
    {
        string url;

        public Fetcher(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentException("Supplied parameter url can't be null, or empty string");

            this.url = url;
        }

        public string Fetch()
        {
            using (var client = new HttpClient())
            {
                return client.GetStringAsync(url).Result;
            }
        }
    }
}
