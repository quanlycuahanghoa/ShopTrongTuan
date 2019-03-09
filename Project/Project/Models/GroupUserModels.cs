using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class GroupUserModels
    {
        // Quyền của người dùng fick cứng : có 4 loại AddMin , Quản Lý , Nhân Viên , Khách Hàng 
        public long ID { get; set; }
        public string GroupName { get; set; }
        public string permission { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsActive { get; set; }

        
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