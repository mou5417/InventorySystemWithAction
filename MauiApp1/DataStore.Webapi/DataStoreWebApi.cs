using Microsoft.Maui.Storage;
using Entities;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using ApiService;

namespace DataStore.Webapi
{
    // All the code in this file is included in all platforms.
    public class DataStoreWebApi : IServiceGeneric
    {
        private HttpClient _httpClient;
        private JsonSerializerOptions _serializerOptions;
        public DataStoreWebApi()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };
            _httpClient = new HttpClient(handler);
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,

            };
        }
        public async Task CreateDataAsync<T, TId>(T itemAddRequest) where T : class
        {

           
            string json = System.Text.Json.JsonSerializer.Serialize<T>(itemAddRequest, _serializerOptions);
            
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            var testvcontent = content.ReadAsStringAsync();
            Uri uri = new Uri($"{Constant.WebApiBaseUrl}/Post/{typeof(T).Name.ToLower()}s");
            if (typeof(T).Name == "Item")
            {
                uri=new Uri($"{Constant.WebApiBaseUrl}/Post/{typeof(T).Name.ToLower()}s");
            }
            try
            {
              var response=await _httpClient.PostAsync(uri, content);
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }

        }


        public async Task<List<T>> GetStore<T>(string filterText) where T : new()
        {
            await SetAuthToken();
            var tempdata = new List<T>();
            Uri uri;
            if (String.IsNullOrEmpty(filterText))
            {
                uri = new Uri($"{Constant.WebApiBaseUrl}/Get/{typeof(T).Name.ToLower()}s");
            }
            else
            {
                uri = new Uri($"{Constant.WebApiBaseUrl}/Get{typeof(T).Name.ToLower()}s={filterText}");
            }

                
            var response = await _httpClient.GetAsync(uri);


            if (response.IsSuccessStatusCode)
            {
                try
                {
                    string content = await response.Content.ReadAsStringAsync();
                    tempdata = System.Text.Json.JsonSerializer.Deserialize<List<T>>(content, _serializerOptions);
                }
                catch (System.Text.Json.JsonException ex) {
                    Console.WriteLine($"JSON Deserialization Error: {ex.Message} at path: {ex.Path}");
                }
                
            }
            return tempdata;
        }

        public async Task<T> GetDataByMatchAsync<T, TId>(string type, Guid id)
        {
            Uri uri = new Uri($"{Constant.WebApiBaseUrl}/Get/{type}s/{id}");
            
            T tempdata=default(T);
            var response = await _httpClient.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                try
                {
                    tempdata = System.Text.Json.JsonSerializer.Deserialize<T>(content, _serializerOptions);
                }
                catch(Exception ex)
                {
                    var msg = ex.Message;
                }
                
            }
            return tempdata;
        }

        async Task IServiceGeneric.RemoveDataAsync<T, TId>(string type, Guid id)
        {
            Uri uri = new Uri($"{Constant.WebApiBaseUrl}/{type}s/{id}");
            try
            {
               var response= await _httpClient.DeleteAsync(uri);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {

            }
            
        }

        async Task IServiceGeneric.UpdateDataAsync<T, TId>(T item, Guid id)
        {
            string json = System.Text.Json.JsonSerializer.Serialize<T>(item, _serializerOptions);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            Uri uri = new Uri($"{Constant.WebApiBaseUrl}/Put/{typeof(T).Name.ToLower()}s/{id}");
            try
            {
               var response = await _httpClient.PutAsync(uri, content);
            }
            catch (Exception ex)
            {

                var message = ex.Message;
            }
           
        }

        public async Task<AuthResponseModel> Login(LoginModel loginModel)
        {
            Uri uri = new Uri($"{Constant.WebApiBaseUrl}/login");
            try
            {
                    var respons = await _httpClient.PostAsJsonAsync(uri, loginModel);
                    respons.EnsureSuccessStatusCode();
                if (respons.StatusCode == System.Net.HttpStatusCode.Unauthorized) return null;
               
                return JsonConvert.DeserializeObject<AuthResponseModel>(await respons.Content.ReadAsStringAsync());   
                
            }
            catch (Exception ex)
            {

               
                throw;
            }
            

        }

        
        public async Task SetAuthToken()
        {
            var token = await SecureStorage.GetAsync("Token");
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }
    }
}
