using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Smiles.Common.Http
{
    public class JSONtoString
    {
        private string _DefaultJSONHeader = "application/json";
        private string _StringJSON;
        
        public string JSONHeader
        {
            get { return _DefaultJSONHeader; }
            private set { _DefaultJSONHeader = value; }
        }

        public JSONtoString()
        {
        }

        public JSONtoString(string header)
        {
            this.JSONHeader = header;
        }
        
        public string GetJSON(string URL)
        {
            JSONRequest(URL).Wait();
            return _StringJSON;
        }

        private async Task JSONRequest(string URL)
        {
            using(HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(this.JSONHeader));

                HttpResponseMessage response = await client.GetAsync(URL);
                if(response.IsSuccessStatusCode)
                {
                    _StringJSON = await response.Content.ReadAsStringAsync();
                }
                else
                {
                    _StringJSON = null;
                }
            }
        }
    }
}
