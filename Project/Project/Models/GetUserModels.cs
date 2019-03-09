using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class GetUserModels
    {
        public long ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public long GroupId { get; set; }
        public bool Status { get; set; }
        public bool IsActive { get; set; }
        public string GroupName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

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
                return ModifiedDate.ToString("dd/MM.yyyy"); 
            }
        }

    }
}