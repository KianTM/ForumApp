using ForumApp.Services.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ForumApp.Services
{
    public class ProfanityFilterAPI
    {
        public string GetCensoredString(string input)
        {
            using (var client = new WebClient())
            {
                string json = client.DownloadString($"https://www.purgomalum.com/service/json?text={input}");
                CensoredString censored = JsonConvert.DeserializeObject<CensoredString>(json);
                return censored.Result;
            }
        }
    }
}
