using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class AddProductModels
    {
        public string Name { get; set; }
        public string MetaTitle { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string AltImage { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<int> Percent { get; set; }
        public Nullable<decimal> PromotionPrice { get; set; }
        //DateTime Date = (DateTime)CreatedDate;
        public int Quantity { get; set; }
        public Nullable<long> CategoryID { get; set; }
        public string CategoryName { get; set; } //bo
        public string DescriptionIdDetail { get; set; }
        public Nullable<int> Warranty { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescriptions { get; set; } 

    }
}