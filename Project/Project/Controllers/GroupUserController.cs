using Project.Data;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class GroupUserController : Controller
    {
        // GET: GroupUser
        public ActionResult Index()
        {
            try
            {

                var query = from Group in con.UserGroups
                            where Group.IsActive == true
                            select Group;



                if (query != null && query.Count() > 0)
                {
                    List<GroupUserModels> listGroup = new List<GroupUserModels>();
                    foreach (UserGroup us in query)
                    {
                        GroupUserModels groupUserModels = new GroupUserModels();
                        groupUserModels.ID = us.ID;
                        groupUserModels.GroupName = us.GroupName;
                        groupUserModels.permission = us.permission;
                        groupUserModels.CreatedBy = us.CreatedBy;
                        groupUserModels.CreatedDate = (DateTime)us.CreatedDate;
                        groupUserModels.ModifiedBy = us.ModifiedBy;
                        groupUserModels.ModifiedDate = (DateTime)us.ModifiedDate;
                        groupUserModels.IsActive = (bool)us.IsActive;
                        listGroup.Add(groupUserModels);
                    }
                    return View(listGroup);
                }
                else
                {
                    return View(new List<GroupUserModels>());
                }
            }
            catch
            {
                return View(new List<GroupUserModels>());
            }
        
        }

        DbConnection con = new DbConnection();

        public JsonResult GetListGroup()
        {
            try
            {
                var query = from groupUs in con.UserGroups
                            where groupUs.IsActive == true
                            select groupUs;


                if (query != null)
                {
                    List<GetSelectGroupUserModels> listGroup = new List<GetSelectGroupUserModels>(); 
                    foreach(UserGroup gr in query)
                    {
                        GetSelectGroupUserModels GetSelectGroupUserModels = new GetSelectGroupUserModels();
                        GetSelectGroupUserModels.ID = gr.ID;
                        GetSelectGroupUserModels.GroupName = gr.GroupName;
                        listGroup.Add(GetSelectGroupUserModels); 
                    }
                    return Json(listGroup, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new GetSelectGroupUserModels(), JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json(new GetSelectGroupUserModels(), JsonRequestBehavior.AllowGet);
            }
        }

    }
}