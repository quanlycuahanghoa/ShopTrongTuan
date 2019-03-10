using Project.Util;
using Project.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Project.Models;
using Project.App_Start;

namespace Project.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        [AuthenticationFilter]
        public ActionResult Index()
        {
           
            return View();
        }
        DbConnection con = new DbConnection();

        /// <summary>
        /// tìm kiếm thông tin khách hàng 
        /// </summary>
        /// <param name="Page"></param>
        /// <param name="FullName"></param>
        /// <param name="UserName"></param>
        /// <param name="Phone"></param>
        /// <param name="Group"></param>
        /// <returns></returns>
        /// 

        [AuthenticationFilter]
        public PartialViewResult Seach(int Page, string FullName, string UserName, string Phone, int Group = 0)
        {
            try
            {

                var query = from User in con.Users
                            where User.IsActive == true
                            select User;
                if(FullName != null)
                {
                    if (FullName.Trim() != "")
                    {
                        query = query.Where(u => u.Name.Contains(FullName));
                    }
                }

                if (UserName != null)
                {
                    if (UserName.Trim() != "")
                    {
                        query = query.Where(u => u.UserName.Contains(UserName));
                    }
                }

                if (Phone != null)
                {
                    if (Phone.Trim() != "")
                    {
                        query = query.Where(u => u.Phone.Contains(Phone));
                    }
                }

                if (Group != 0)
                {
                    query = query.Where(u => u.GroupId ==Group);
                }

              
                if (query != null && query.Count() > 0)
                {
                    List<GetUserModels> listUser = new List<GetUserModels>(); 
                    foreach(User us in query)
                    {
                        GetUserModels userModels = new GetUserModels();
                        userModels.Name = us.Name;
                        userModels.ID = us.ID;
                        userModels.UserName = us.UserName;
                        userModels.Password = us.Password;
                        userModels.GroupId = (long)us.GroupId;
                        userModels.Phone = us.Phone;
                        userModels.Email = us.Email;
                        userModels.IsActive = (bool)us.IsActive;
                        userModels.Status = (bool)us.Status;
                        userModels.CreatedDate = (DateTime)us.CreatedDate;
                        userModels.CreatedBy = us.CreatedBy;
                        userModels.ModifiedDate = (DateTime)us.ModifiedDate;
                        userModels.ModifiedBy = us.ModifiedBy;


                        UserGroup userGroup = con.UserGroups.Find(us.GroupId); 
                        if(userGroup != null)
                        {
                            userModels.GroupName = userGroup.GroupName; 
                        }
                        listUser.Add(userModels); 
                    }
                    return PartialView("_List", listUser.ToPagedList(Page, Constants.MAX_ROW_IN_LIST));
                }
                else
                {
                    return PartialView("_List", new List<GetUserModels>().ToPagedList(1, 1));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// thêm mới thông tin khách hàng 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [AuthenticationFilter]
        public int AddUser(AddUserModels user)
        {
            try
            {
                // kiểm tra có trung user 
                var query = (from us in con.Users
                            where us.IsActive == true && (us.UserName + "").Trim() == user.UserName.Trim()
                            select us).SingleOrDefault(); 
                if(query!= null)
                {
                    // nếu trung thì return ra -1 ; 
                    return -1; 
                }

                LoginUserModels loginUserModels = Session["Login"] as LoginUserModels;
                User Us = new User()
                {

                    UserName = user.UserName,
                    GroupId = user.GroupId,
                    Password = Util.Util.CreateMD5(user.Password),
                    Status = true,
                    IsActive = true,
                    Phone = user.Phone,
                    Email = user.Email,
                    Name = user.Name,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    ModifiedBy = loginUserModels.Name,
                    CreatedBy = loginUserModels.Name,
                };
                con.Users.Add(Us);
                con.SaveChanges();
                return 1;
            }
            catch
            {
                return 0; 
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [AuthenticationFilter]
        public JsonResult GetUserById(long Id)
        {
            try
            {
                var query = con.Users.Find(Id); 

                if (query != null)
                {
                    GetUserModels userModels = new GetUserModels()
                    {
                        ID = query.ID, 
                        UserName = query.UserName,
                        GroupId = (long)query.GroupId,
                        Status = (bool)query.Status,
                        IsActive = true,
                        Phone = query.Phone,
                        Email = query.Email,
                        Name = query.Name,
                    }; 
                    return Json(userModels, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new GetUserModels(), JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json(new GetUserModels(), JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Sửa Thông Tin User 
        /// </summary>
        /// <param name="editUserModels">Thông Tin User Cần Sủa</param>
        /// <returns></returns>
        [AuthenticationFilter]
        public int EditUser(EditUserModels editUserModels)
        {
            try
            {
                LoginUserModels loginUserModels = Session["Login"] as LoginUserModels;
                User user = con.Users.Find(editUserModels.ID);

                if (user != null)
                {
                    user.UserName = editUserModels.UserName;
                    user.GroupId = (long)editUserModels.GroupId;
                    user.Status = (bool)editUserModels.Status;
                    user.IsActive = true;
                    user.Phone = editUserModels.Phone;
                    user.Email = editUserModels.Email;
                    user.Name = editUserModels.Name;
                    user.ModifiedDate = DateTime.Now;
                    user.ModifiedBy = loginUserModels.Name; 
                    con.SaveChanges(); 
                    return Constants.RETURN_TRUE; 
                }
                else
                {
                    return Constants.RETURN_FALSE; 
                }
            }
            catch
            {
                return Constants.RETURN_FALSE;
            }
        }

        /// <summary>
        /// Xóa Thông Tin User
        /// </summary>
        /// <param name="Id">Mã của User</param>
        /// <returns></returns>
        [AuthenticationFilter]
        public int DeleteUser(long Id)
        {

            try
            {
                User user = con.Users.Find(Id);
                if(User!= null)
                {
                    user.IsActive = false;
                    con.SaveChanges();
                    return Constants.RETURN_TRUE; 

                }
                return Constants.RETURN_FALSE; 
                
            }
            catch
            {
                return Constants.RETURN_FALSE;
            }
        }

        /// <summary>
        /// đăng nhập thông tin của nhân viên 
        /// </summary>
        /// <returns></returns>
        public string LoginAction()
        {
            try
            {
                string UserName = Request["UserName"];
                string Password = Request["Password"];
                //Kiem tra user va password xem co hop le khong
                var user = (from u in con.Users
                            where u.IsActive== true && u.UserName.Equals(UserName)
                            select u).FirstOrDefault();
                if (user != null)
                {
                    if(user.Status == false)
                    {
                        // thông báo tài khoàn đã bị khóa
                        return "-1"; 
                    }
                    if (user.Password.Trim() == Util.Util.CreateMD5(Password).Trim())
                    {
                        LoginUserModels loginUserModels = new LoginUserModels()
                        {
                            ID = user.ID,
                            Name = user.Name,
                            UserName = user.UserName,
                            GroupId = (long)user.GroupId
                        }; 
                        Session["Login"] = loginUserModels;
                        return "1";
                    }
                    else
                    {
                        return "0";
                    }
                }
                else
                {
                    return "0";
                }
            }
            catch
            {
                return "0";
            }

        }


        /// <summary>
        /// thoát tài khoản 
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            //Xóa biên session
            Session["Login"] = null;
            return View("Login");
        }

        public ActionResult Login()
        {
            return View();
        }
    }

}