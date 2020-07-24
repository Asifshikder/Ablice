using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RugerRumble.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public string ImgUrl { get; set; }
    }
}
