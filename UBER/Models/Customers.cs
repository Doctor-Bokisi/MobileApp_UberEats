using System;
namespace UBER.Models
{
    public class Customer
    {
       

        public int cust_Id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string password { get; set; }



        public Customer(string firstname, string lastname,string mobile, string email, string Password)
        {
            firstName = firstname;
            lastName = lastname;
            Mobile = mobile;
            Email = email;
            password = password;
        }

        public Customer(int id, string firstname, string lastname,string mobile, string email, string Password)
        {
            cust_Id = id;
            firstName = firstname;
            lastName = lastname;
            Mobile = mobile;
            Email = email;
            password = Password;
           
        }
       

        public Customer(string emails, string Passwords)
        {
            Email = emails;
            password = Passwords;
        }
        public Customer()
        {
        }

       
    }
}
