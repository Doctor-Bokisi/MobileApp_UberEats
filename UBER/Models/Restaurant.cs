using System;
namespace UBER.Models
{
    public class Restaurant
    {
        public int rest_Id { get; set; }
        public string Res_Name { get; set; }
        public string Res_Location { get; set; }
        public string Res_City { get; set; }
        public byte[] Image { get; set; }

        public Restaurant()
        {
        }

        public Restaurant(string name, string location, string city, byte[] image)
        {
            Res_Name = name;
            Res_Location = location;
            Res_City = city;
            Image = image;

        }
        public Restaurant(int id, string name, string location, string city, byte[] image)
        {
            rest_Id = id;
            Res_Name = name;
            Res_Location = location;
            Res_City = city;
            Image = image;
        }

        public Restaurant(int id, string name, string location, string city)
        {
            rest_Id = id;
            Res_Name = name;
            Res_Location = location;
            Res_City = city;


        }
    }
}
