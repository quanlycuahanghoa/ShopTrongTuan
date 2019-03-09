using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class AddProductCategory
    {
        public string Name { get; set; }
        public string MetaTitle { get; set; }
        public Nullable<long> ParentID { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
        public string SeoTitle { get; set; }
        public string Descriptions { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescriptions { get; set; }
        public Nullable<bool> ShowOnHome { get; set; }
        public string Image { get; set; }
        public string AltImage { get; set; }
        public Nullable<bool> Active { get; set; }
    }
}