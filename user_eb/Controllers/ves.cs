using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using user_eb.Models;

namespace user_eb.Controllers
{
    public class ves : System.Web.Http.ApiController
    {
        static data acc = new data();

        //Registering customers on the mobile application
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Register")]
        public string PostCust(cus cust)
        {
            if (cust != null)
            {
                return acc.RegisterCustomer(cust);
            }
            return "Unable to add";
        }
    }
}