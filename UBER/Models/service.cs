using System;
using System.Text;
using System.Net.Http;
using Android.Content;
using Newtonsoft.Json;
using Android.App;

namespace UBER.Models
{
    public class service
    {
        HttpClient client = new HttpClient();
        Clients customer = null;
        public async void GetCusts(string email, string password)
        {
            string url = @"http://10.0.2.2:8080/api/ClientsLogin?Email=" + email + "&password=" + password + "";
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                customer = JsonConvert.DeserializeObject<Clients>(content);

                var loggedOn = Application.Context.GetSharedPreferences("CustomerList", FileCreationMode.Private);
                var loggedEdit = loggedOn.Edit();

                loggedEdit.PutString("firstName", JsonConvert.DeserializeObject<Clients>(content).firstName);
                loggedEdit.PutString("lastName", JsonConvert.DeserializeObject<Clients>(content).lastName);
                loggedEdit.PutString("Mobile", JsonConvert.DeserializeObject<Clients>(content).Mobile);
                loggedEdit.PutString("Email", JsonConvert.DeserializeObject<Clients>(content).Email);
                loggedEdit.PutString("password", JsonConvert.DeserializeObject<Clients>(content).password);

                loggedEdit.Commit();
            }
        }
    }
}
