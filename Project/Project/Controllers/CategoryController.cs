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
    public class CategoryController : Controller
    {
        //chuỗi connect
        DbConnection con = new DbConnection();

        // GET: Category
        [AuthenticationFilter]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// tìm kiếm thông tin của category 
        /// </summary>
        [HttpGet]
        [AuthenticationFilter]
        public PartialViewResult Seach(int Page, string CategoryName, long GroupCategoryId)
        {

            try
            {

                var query = from x in con.Categories
                            where x.Active == true
                            select x;

                if (CategoryName != null)
                {
                    if (CategoryName.Trim() != "")
                    {
                        query = query.Where(u => u.Name.Contains(CategoryName));
                    }
                }

                if (GroupCategoryId != 0)
                {
                    query = query.Where(u => u.ParentID == GroupCategoryId);

                }

                if (query != null && query.Count() > 0)
                {
                    List<GetCategoryModels> List = new List<GetCategoryModels>();

                    foreach (Category data in query)
                    {
                        GetCategoryModels getCategoryModels = new GetCategoryModels();

                        getCategoryModels.Name = data.Name;
                        getCategoryModels.MetaTitle = data.MetaTitle;
                        //getCategoryModels.ParentID = (long)data.ParentID;
                        getCategoryModels.DisplayOrder = (int)data.DisplayOrder;
                        //getCategoryModels.SeoTitle = data.SeoTitle;
                        getCategoryModels.CreatedDate = (DateTime)data.CreatedDate;
                        getCategoryModels.CreatedBy = data.CreatedBy;
                        getCategoryModels.ModifiedDate = (DateTime)data.ModifiedDate;
                        getCategoryModels.ModifiedBy = data.ModifiedBy;

                        //getCategoryModels.MetaKeywords = data.MetaKeywords;
                        //getCategoryModels.MetaDescriptions = data.MetaDescriptions;
                        getCategoryModels.ShowOnHome = (bool)data.ShowOnHome;
                        getCategoryModels.Image = data.Image;
                        getCategoryModels.AltImage = data.AltImage;

                        Category categoryParent = con.Categories.Find(data.ParentID);

                        if (categoryParent != null)
                        {
                            getCategoryModels.ParentName = categoryParent.Name;
                        }
                        List.Add(getCategoryModels);
                    }
                    return PartialView("_List", List.ToPagedList(Page, Constants.MAX_ROW_IN_LIST));
                }
                else
                {
                    return PartialView("_List", new List<GetCategoryModels>().ToPagedList(1, 1));
                }
            }
            catch
            {
                return PartialView("_List", new List<GetCategoryModels>().ToPagedList(1, 1));
            }
        }


        /// <summary>
        /// update
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public ActionResult Update(long categoryId)
        {
            try
            {

                var query = con.Categories.Find(categoryId);
                if (query != null)
                {
                    EditCategoryModels editCategoryModels = new EditCategoryModels()
                    {
                        Name = query.Name,
                        MetaTitle = query.MetaTitle,
                        ParentID = query.ParentID,
                        DisplayOrder = query.DisplayOrder,
                        SeoTitle = query.SeoTitle,
                        CreatedBy = query.CreatedBy,
                        CreatedDate = query.CreatedDate,
                        ModifiedBy = query.ModifiedBy,
                        MetaKeywords = query.MetaKeywords,
                        MetaDescriptions = query.MetaDescriptions,
                        Status = query.Status,
                        ShowOnHome = query.ShowOnHome,
                        Image = query.Image,
                        AltImage = query.AltImage,
                        Active = query.Active

                    };
                    return View(editCategoryModels);
                }
                else
                {
                    return View(new EditCategoryModels());
                }

            }
            catch
            {

                return View(new EditCategoryModels());
            }


        }

        /// <summary>
        /// Create
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View();
        }


        /// <summary>
        /// json
        /// </summary>
        /// <returns></returns>
        public JsonResult GetSelectCategory()
        {
            try
            {
                var query = from data in con.Categories
                            where data.Active == true
                            select new GetCategoryModels
                            {
                                Name = data.Name,
                                ID = data.ID
                            };
                if (query != null)
                {
                    return Json(query, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new List<GetCategoryModels>(), JsonRequestBehavior.AllowGet);
                }

            }

            catch
            {
                return Json(new List<GetCategoryModels>(), JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// thêm mới thông tin của loại sản phẩm 
        /// </summary>
        /// <param name="addProductCategory">loại sản phẩm sau khi đã thêm</param>
        /// <returns></returns>
        [ValidateInput(false)]
        [HttpPost]
        public int AddCategory(AddCategoryModels addCategory)
        {
            try
            {
                LoginUserModels loginUserModels = Session["Login"] as LoginUserModels;
                Category category = new Category()
                {
                    Name = addCategory.Name,
                    MetaTitle = addCategory.MetaTitle,
                    ParentID = addCategory.ParentID,
                    DisplayOrder = addCategory.DisplayOrder,
                    SeoTitle = addCategory.SeoTitle,
                    MetaKeywords = addCategory.MetaKeywords,
                    MetaDescriptions = addCategory.MetaDescriptions,
              
                    ShowOnHome = addCategory.ShowOnHome,
                    Image = addCategory.Image,
                    AltImage = addCategory.AltImage,
                    Active = addCategory.Active

                };
                con.Categories.Add(category);
                con.SaveChanges();
                return Constants.RETURN_TRUE;


            }
            catch
            {
                return Constants.RETURN_FALSE;
            }
        }

        
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="ProductCategoryId"></param>
        /// <returns></returns>
        public int DeleteCategory(long categoryId)
        {
            try
            {
                var data = con.Categories.Find(categoryId);
                if (data != null)
                {
                    data.Active = false;
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
        /// edit
        /// </summary>
        /// <param name="editCategoryModels"></param>
        /// <returns></returns>
        public int EditCategory(EditCategoryModels editCategoryModels)
        {
            try
            {
                var Data = con.Categories.Find(editCategoryModels.ParentID);

                return Constants.RETURN_TRUE;
            }
            catch
            {
                return Constants.RETURN_FALSE;
            }
        }
    }
}