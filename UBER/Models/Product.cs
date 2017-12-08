using System;
namespace UBER.Models
{
    public class Product
    {
        public int prodId { get; set; }
        public string ProdName { get; set; }
        public string ProdpPrice { get; set; }
        public byte[] Image { get; set; }

        public Product()
        {

        }

        public Product(string tmp)
        {
            ProdName = tmp;

        }
        public Product(string name, string price, byte[] image)
        {
            ProdName = name;
            ProdpPrice = price;
            Image = image;

        }
        public Product(int id, string name, string price, byte[] image)
        {
            prodId = id;
            ProdName = name;
            ProdpPrice = price;
            Image = image;
        }
    }
}
