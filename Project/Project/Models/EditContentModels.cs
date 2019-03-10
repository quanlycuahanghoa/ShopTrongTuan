using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class EditContentModels
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public string MetaTitle { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string AltImage { get; set; }
        public string MoreImages { get; set; }
        public Nullable<long> CategoryID { get; set; }
        public string DescriptionIdDetail { get; set; }
        public Nullable<int> Warranty { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescriptions { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<int> TopHot { get; set; }
        public Nullable<int> ViewCount { get; set; }
        public Nullable<int> IsActive { get; set; }
        public string Tag { get; set; }


    }
}