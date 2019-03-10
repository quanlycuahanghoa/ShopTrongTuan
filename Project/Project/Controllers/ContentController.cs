using PagedList;
using Project.App_Start;
using Project.Data;
using Project.Models;
using Project.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class ContentController : Controller
    {
        DbConnection con = new DbConnection();

        // GET: 
        [AuthenticationFilter]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Search Content
        /// </summary>
        /// <param name="Page"></param>
        /// <param name="Name"></param>
        /// <param name="CategoryId"></param>
        /// <param name="topHot"></param>
        /// <param name="GroupCategory"></param>
        /// <returns></returns>
        [AuthenticationFilter]
        public PartialViewResult Seach(int Page, string Name, long CategoryId, int topHot, int GroupCategory = 0)
        {
            try
            {

                var query = from x in con.Contents
                            where x.IsActive == 1
                            select x;
                if (Name != null)
                {
                    if (Name.Trim() != "")
                    {
                        query = query.Where(u => u.Name.Contains(Name));
                    }
                }

                if (CategoryId != 0)
                {
                    query = query.Where(x => x.CategoryID == CategoryId);
                }

                if (topHot != 0)
                {
                    query = query.Where(x => x.TopHot == topHot);
                }

                if (GroupCategory != 0)
                {
                    query = query.Where(u => u.CategoryID == GroupCategory);
                }


                if (query != null && query.Count() > 0)
                {
                    List<GetContentModels> listContent = new List<GetContentModels>();
                    foreach (Content us in query)
                    {
                        GetContentModels contentModels = new GetContentModels();
                        contentModels.Name = us.Name;
                        contentModels.Image = us.Image;
                        contentModels.AltImage = us.AltImage;
                        contentModels.MoreImages = us.MoreImages;
                        contentModels.CategoryID = us.CategoryID;
                        contentModels.Warranty = us.Warranty;
                        contentModels.TopHot = us.TopHot;
                        contentModels.ViewCount = us.ViewCount;
                        contentModels.IsActive = us.IsActive;
  
                        Category categoryGroup = con.Categories.Find(us.CategoryID);
                        if (categoryGroup != null)
                        {
                            contentModels.CategoryName = categoryGroup.Name;
                        }
                        listContent.Add(contentModels);
                    }
                    return PartialView("_List", listContent.ToPagedList(Page, Constants.MAX_ROW_IN_LIST));
                }
                else
                {
                    return PartialView("_List", new List<GetContentModels>().ToPagedList(1, 1));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Add content 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [AuthenticationFilter]
        public int AddContent(AddContentModels addcontent)
        {
            try
            {
                // kiểm tra có trung user 
                var query = (from us in con.Contents
                             where us.IsActive == 1 && (us.Name + "").Trim() == addcontent.Name.Trim()
                             select us).SingleOrDefault();
                if (query != null)
                {
                    // nếu trung thì return ra -1 ; 
                    return -1;
                }

                LoginUserModels loginUserModels = Session["Login"] as LoginUserModels;
                Content ct = new Content()
                {

                    Name = addcontent.Name,
                    Image = addcontent.Image,
                    AltImage = addcontent.AltImage,
                    MoreImages = addcontent.MoreImages,
                    CategoryID = (long)addcontent.CategoryID,
                    Warranty = (int)addcontent.Warranty,
                    TopHot = (int)addcontent.TopHot,

                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    ModifiedBy = loginUserModels.Name,
                    CreatedBy = loginUserModels.Name,
                };
                con.Contents.Add(ct);
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
        public JsonResult GetContentById(long Id)
        {
            try
            {
                var query = con.Contents.Find(Id);

                if (query != null)
                {
                    GetContentModels contentModels = new GetContentModels()
                    {
                        ID = query.ID,
                        Name = query.Name,
                        MetaTitle = query.MetaTitle,
                        Description = query.Description,
                        Image = query.Image,
                        AltImage = query.AltImage,
                        MoreImages = query.MoreImages,
                        CategoryID = (long)query.CategoryID,
                        DescriptionIdDetail = query.DescriptionIdDetail,
                        Warranty = (int)query.Warranty,
                        CreatedDate = (DateTime)query.CreatedDate,
                        CreatedBy = query.CreatedBy,
                        ModifiedDate = (DateTime)query.ModifiedDate,
                        ModifiedBy = query.ModifiedBy,
                        MetaKeywords = query.MetaKeywords,
                        MetaDescriptions = query.MetaDescriptions,
                        Status = (bool)query.Status,
                        TopHot = (int)query.TopHot,
                        ViewCount = (int)query.ViewCount,
                        IsActive = (int)query.IsActive,
                        Tag = query.Tag
                    };
                    return Json(contentModels, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new GetContentModels(), JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json(new GetContentModels(), JsonRequestBehavior.AllowGet);
            }
        }


        /// <summary>
        /// Sửa Thông Tin Content 
        /// </summary>
        /// <param name="editContentModels">Thông Tin User Cần Sủa</param>
        /// <returns></returns>
        [AuthenticationFilter]
        public int EditUser(EditContentModels editContentModels)
        {
            try
            {
                LoginUserModels loginUserModels = Session["Login"] as LoginUserModels;
                //User user = con.Users.Find(editContentModels.ID);
                Content user = con.Contents.Find(editContentModels.ID);
                if (user != null)
                {
   

                    user.Name = editContentModels.Name;
                    user.MetaTitle = editContentModels.MetaTitle;
                    user.Description = editContentModels.Description;
                    user.Image = editContentModels.Image;
                    user.AltImage = editContentModels.AltImage;
                    user.MoreImages = editContentModels.MoreImages;
                    user.CategoryID = (long)editContentModels.CategoryID;
                    user.DescriptionIdDetail = editContentModels.DescriptionIdDetail;
                    user.Warranty = (int)editContentModels.Warranty;
                    user.ModifiedDate = DateTime.Now;
                    user.ModifiedBy = loginUserModels.Name;
                    user.MetaKeywords = editContentModels.MetaKeywords;
                    user.MetaDescriptions = editContentModels.MetaDescriptions;
                    user.Status = (bool)editContentModels.Status;
                    user.TopHot = (int)editContentModels.TopHot;
                    user.ViewCount = (int)editContentModels.ViewCount;
                    user.IsActive = (int)editContentModels.IsActive;
                    user.Tag = editContentModels.Tag;
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
        /// Xóa Thông Tin Content
        /// </summary>
        /// <param name="Id">Mã của User</param>
        /// <returns></returns>
        [AuthenticationFilter]
        public int DeleteContent(long Id)
        {

            try
            {
                Content content = con.Contents.Find(Id);
                if (content != null)
                {
                    content.IsActive = 0;
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

       
    }
}