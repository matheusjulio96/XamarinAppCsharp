using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using XamarinAppCsharp.Models;

namespace XamarinAppCsharp.Data
{
    public class TimezoneService
    {
        HttpClient _client;

        public List<Timezone> Items { get; private set; }

        public TimezoneService()
        {
            _client = new HttpClient();
        }

        public async Task<Timezone> GetTimezoneAsync()
        {
            var resultHorario = await HttpGet(Constants.WorldtimeapiEndpoint);
            var strHorario = resultHorario.Content.ReadAsStringAsync().Result;
            var horario = JsonConvert.DeserializeObject<Timezone>(strHorario);
            return horario;
        }

        public async Task<HttpResponseMessage> HttpGet(string url)
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Local || Connectivity.NetworkAccess == NetworkAccess.None)
            {
                bool answer = await Application.Current.MainPage.DisplayAlert("Você não possui acesso a internet", null, "OK", "Configurações");
                if (answer)
                {
                    //abre as configuracoes
                }
                return null;
            }
            HttpResponseMessage result = null;
            try
            {
                result = await _client.GetAsync(url);
            }
            catch(Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Ocorreu um erro", $"Tente Novamente. NetworkAccess: {Connectivity.NetworkAccess}", "OK");
                return null;
            }
            return result;
        }

        public async Task<byte[]> HttpGetByteArray(string url)
        {
            var result = await HttpGet(url);
            if (result != null) 
                return await result.Content.ReadAsByteArrayAsync();
            else
                return await Task.FromResult(new byte[0]);
        }

        public async Task<string> HttpGetString(string url)
        {
            var result = await HttpGet(url);
            
            if (result != null)
                return await result.Content.ReadAsStringAsync();
            else
                return await Task.FromResult("");

        }
    }
}
