using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class GetProductCategoryModels
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public string ParentName
        {
            get;
            set;
        }
        public int DisplayOrder { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool Status { get; set; }
        public bool ShowOnHome { get; set; }
        public string Image { get; set; }
        public string AltImage { get; set; }


        public string GetParentName()
        {
            if (this.ParentName == null || this.ParentName == "")
            {
                return "Doanh mục gốc";
            }
            else
            {
                return this.ParentName; 
            }
        }

        public string GetImage()
        {
            if(this.Image == null || this.Image == "")
            {
                return Util.Constants.NO_PART; 
            }
            else
            {
                return this.Image;
            }
        }


        public string GetStringCreateDate
        {
            get
            {
                return CreatedDate.ToString("dd/MM/yyyy");
            }
        }

        public string GetStringModifiedDate
        {
            get
            {
                return ModifiedDate.ToString("dd/MM/yyyy");
            }
        }
    }
}