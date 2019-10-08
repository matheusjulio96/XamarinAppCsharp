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
            if (Connectivity.NetworkAccess == NetworkAccess.Local || Connectivity.NetworkAccess == NetworkAccess.None)
            {
                bool answer = await Application.Current.MainPage.DisplayAlert("Você não possui acesso a internet", null, "OK", "Configurações");
                if (answer)
                {
                    //abre as configuracoes
                }
                return null;
            }
            HttpClient client = new HttpClient();
            HttpResponseMessage resultHorario = null;
            try
            {
                resultHorario = await client.GetAsync(Constants.WorldtimeapiEndpoint);
            }
            catch
            {
                await Application.Current.MainPage.DisplayAlert("Ocorreu um erro", $"Tente Novamente. NetworkAccess: {Connectivity.NetworkAccess}", "OK");
                return null;
            }
            var strHorario = resultHorario.Content.ReadAsStringAsync().Result;
            var horario = JsonConvert.DeserializeObject<Timezone>(strHorario);
            return horario;
        }

    }
}
