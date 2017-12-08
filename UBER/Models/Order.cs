using System;
namespace UBER.Models
{
    public class Order
    {
        public int order_Id { get; set; }
        public string totalAmount { get; set; }
        public string quantity { get; set; }
        public int cust_id { get; set; }
        public string address { get; set; }


        public Order()
        {

        }



        public Order(int id, string Cname, string cNumber, int c_id,string Address)
        {
            order_Id = id;
            totalAmount = Cname;
            quantity = cNumber;
            cust_id = c_id;
            address = Address;

        }

        public Order(string Cname, string cNumber, int c_id,string Address)
        {
            totalAmount = Cname;
            quantity = cNumber;
            cust_id = c_id;
            address = Address;
        }

        public Order(string Cname, string cNumber, string Address)
        {
            totalAmount = Cname;
            quantity = cNumber;
            address = Address;
        }
    }
}
