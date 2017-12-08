using System;
using System.Net.Http;
using Android.Content;
using Newtonsoft.Json;
using Android.App;
using System.Threading.Tasks;
using System.Text;

namespace UBER.Models
{
    public class ser1
    {
        HttpClient client = new HttpClient();
        public Clients GetCusts(string email, string password)
        {
            string url = "http://10.0.2.2:8080/api/ClientsLogin?Email=" + email + "&password=" + password + "";
            HttpResponseMessage response = client.GetAsync(url).Result;
            Clients data = new Clients();
            data = JsonConvert.DeserializeObject<Clients>(response.Content.ReadAsStringAsync().Result);
         

            return data;
        }

            //Clients customer = null;
            //public async void UGetCusts(string email, string password)
            //{
            //    string url = "http://10.0.2.2:8080/api/ClientsLogin?Email=" + email + "&password=" + password + "";
            //    var response = await client.GetAsync(url);
            //    if (response.IsSuccessStatusCode)
            //    {
            //        var content = await response.Content.ReadAsStringAsync();
            //        customer = JsonConvert.DeserializeObject<Clients>(content);

            //        var loggedOn = Application.Context.GetSharedPreferences("CustomerList", FileCreationMode.Private);
            //        var loggedEdit = loggedOn.Edit();

            //        loggedEdit.PutInt("CustId", JsonConvert.DeserializeObject<Clients>(content).CustId);
            //        loggedEdit.PutString("Firstname", JsonConvert.DeserializeObject<Clients>(content).Firstname);
            //        loggedEdit.PutString("Lastname", JsonConvert.DeserializeObject<Clients>(content).Lastname);
            //        loggedEdit.PutString("Mobile", JsonConvert.DeserializeObject<Clients>(content).Mobile);
            //        loggedEdit.PutString("Email", JsonConvert.DeserializeObject<Clients>(content).Email);
            //        loggedEdit.PutString("password", JsonConvert.DeserializeObject<Clients>(content).password);
                  
            //        loggedEdit.Commit();
            //    }
            //}

       

        public async void Update(Clients cust, int id)
        {
            HttpContent content = new StringContent(JsonConvert.SerializeObject(cust),Encoding.UTF8 , "application/json");
            HttpResponseMessage response = null;
            response = await client.PutAsync("http://10.0.2.2:8080/api/ClientsUpdate?id=" + id,content);
            var jasonC = content.ReadAsStringAsync().Result;

        }


    }
}