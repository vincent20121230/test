using IdentityModel;
using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        public static string Base64Encode(Encoding encodeType, string source)
        {
            string encode = string.Empty;
            byte[] bytes = encodeType.GetBytes(source);
            try
            {
                encode = Convert.ToBase64String(bytes);
            }
            catch
            {
                encode = source;
            }
            return encode;
        }

        static void Main(string[] args)
        {
            Console.Title = "identity client";

            //Console.WriteLine("secret".ToSha256());
            //POST http://localhost:5000/connect/token HTTP/1.1
            //Authorization: Basic Y2xpZW50OnNlY3JldA==
            //Accept: application / json
            //Content - Type: application / x - www - form - urlencoded
            //Content - Length: 40
            //Host: localhost: 5000

            //grant_type = client_credentials & scope = api1

            //Authorization: Basic Y2xpZW50OnNlY3JldA== 是这样算出来的
            //Console.WriteLine(Base64Encode(Encoding.UTF8, "client:secret"));

            MainAsync().GetAwaiter().GetResult();

            Console.ReadKey();
        }

        private static async Task MainAsync()
        {
            // 获取服务发现者对象
            var disco = await DiscoveryClient.GetAsync("http://localhost:5000");
            if (disco.IsError)
            {
                Console.WriteLine("disco error");
                return;
            }

            // 从identity server 获取授权
            var tokenClient = new TokenClient(disco.TokenEndpoint, "client", "secret");
            var tokenResponse = await tokenClient.RequestClientCredentialsAsync("api1");

            if(tokenResponse.IsError)
            {
                Console.WriteLine("token response error");
                return;
            }

            Console.WriteLine(tokenResponse.Json);

            // 调用被授权保护的接口
            var client = new HttpClient();
            // 如果不传入accessToken或错误的accesstoken都将返回401错误
            client.SetBearerToken(tokenResponse.AccessToken); 

            var response = await client.GetAsync("http://localhost:5001/api/values");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine((int)response.StatusCode + "," + response.StatusCode);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine((int)response.StatusCode + ","+ JArray.Parse(content));
            }
        }
    }
}
