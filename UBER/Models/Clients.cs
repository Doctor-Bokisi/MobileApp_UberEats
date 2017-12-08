using System;
using UBER.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UBER.Models
{
    public class Clients
    {
        public int CustId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string password { get; set; }

        public Clients()
        {
            
        }

       

        public Clients(int id, string firstname, string lastname,string mobile,  string email, string Password)
        {
            CustId = id;
            Firstname = firstname;
            Lastname = lastname;
            Mobile = mobile;
            Email = email;
            password = Password;
        }

        public Clients(string firstname, string lastname, string mobile)
        {
           
            Firstname = firstname;
            Lastname = lastname;
            Mobile = mobile;
        }

        public Clients(string firstname, string lastname, string mobile, string email, string Password)
        {
           
            Firstname = firstname;
            Lastname = lastname;
            Mobile = mobile;
            Email = email;
            password = Password;
        }
    }
 }

