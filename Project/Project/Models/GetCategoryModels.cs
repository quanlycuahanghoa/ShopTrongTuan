using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class GetCategoryModels
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public string MetaTitle { get; set; }
        public Nullable<long> ParentID { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
        public string SeoTitle { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescriptions { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<bool> ShowOnHome { get; set; }
        public string Image { get; set; }
        public string AltImage { get; set; }
        public Nullable<bool> Active { get; set; }

        public string ParentName
        {
            get; set;
        }
        public string GetParentName()
        {
            if (this.ParentName == null || this.ParentName == "")
            {
                return "Danh muc goc";
            }
            else
            {
                return this.ParentName;
            }
        }

        public string GetImage()
        {
            if (this.Image == null || this.Image == "")
            {
                return Util.Constants.NO_PART;
            }
            else
            {
                return this.Image;
            }
        }



        public string GetStringCreateDate()
        {
            if (this.CreatedDate != null)
            {
                DateTime Date = (DateTime)CreatedDate;
                return Date.ToString("dd/MM/yyyy");
            }
            else
            {
                return DateTime.Now.ToString("dd/MM/yyyy");
            }
        }

        public string GetStringModifierDate()
        {
            if (this.ModifiedDate != null)
            {
                DateTime Date = (DateTime)ModifiedDate;
                return Date.ToString("dd/MM/yyyy");
            }
            else
            {
                return DateTime.Now.ToString("dd/MM/yyyy");
            }
        }
    }
}