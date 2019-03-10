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
    public class ProductCategoryController : Controller
    {
        // GET: ProductCategory
        [AuthenticationFilter]
        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// tìm kiếm thông tin của loại sản phẩm 
        /// </summary>
        DbConnection con = new DbConnection();
        
        /// <summary>
        /// Phan Đình Kiên : Tìm kiếm thông tin loại sản phẩm
        /// </summary>
        /// <param name="Page">trang cần hiển thị </param>
        /// <param name="ProductCategoryName">tên của loại sản phẩm </param>
        /// <param name="GroupCategoryId">Tên nhóm của loại sản phẩm</param>
        /// <returns></returns>
        [HttpGet]
        [AuthenticationFilter]
        public PartialViewResult Seach(int Page, string ProductCategoryName, long GroupCategoryId)
        {

            try
            {

                var query = from product_Category in con.ProductCategories
                            where product_Category.Active == true
                            select product_Category;

                if (ProductCategoryName != null)
                {
                    if (ProductCategoryName.Trim() != "")
                    {
                        query = query.Where(u => u.Name.Contains(ProductCategoryName));
                    }
                }

                if (GroupCategoryId != 0)
                {
                    query = query.Where(u => u.ParentID == GroupCategoryId);

                }

                if (query != null && query.Count() > 0)
                {
                    List<GetProductCategoryModels> List = new List<GetProductCategoryModels>();

                    foreach (ProductCategory data in query)
                    {
                        GetProductCategoryModels getProductCategoryModels = new GetProductCategoryModels();
                        getProductCategoryModels.Name = data.Name;
                        getProductCategoryModels.ID = data.ID;
                        //getProductCategoryModels.MetaTitle = data.MetaTitle;
                        //getProductCategoryModels.ParentID = (long)data.ParentID;
                        getProductCategoryModels.DisplayOrder = (int)data.DisplayOrder;
                        //getProductCategoryModels.SeoTitle = data.SeoTitle;
                        getProductCategoryModels.CreatedDate = (DateTime)data.CreatedDate;
                        getProductCategoryModels.CreatedBy = data.CreatedBy;
                        getProductCategoryModels.ModifiedDate = (DateTime)data.ModifiedDate;
                        getProductCategoryModels.ModifiedBy = data.ModifiedBy;

                        //getProductCategoryModels.MetaKeywords = data.MetaKeywords;
                        //getProductCategoryModels.MetaDescriptions = data.MetaDescriptions;
                        getProductCategoryModels.ShowOnHome = (bool)data.ShowOnHome;
                        getProductCategoryModels.Image = data.Image;
                        getProductCategoryModels.AltImage = data.AltImage;
                        ProductCategory ProductCategoryParent = con.ProductCategories.Find(data.ParentID);

                        if (ProductCategoryParent != null)
                        {
                            getProductCategoryModels.ParentName = ProductCategoryParent.Name;
                        }
                        List.Add(getProductCategoryModels);
                    }
                    return PartialView("_List", List.ToPagedList(Page, Constants.MAX_ROW_IN_LIST));
                }
                else
                {
                    return PartialView("_List", new List<GetProductCategoryModels>().ToPagedList(1, 1));
                }
            }
            catch
            {
                return PartialView("_List", new List<GetProductCategoryModels>().ToPagedList(1, 1));
            }
        }


        /// <summary>
        /// Phan Đình Kiên : Tìm kiếm thông tin của loại sản phẩm 
        /// </summary>
        /// <param name="ProductCategoryId">Mã của loại sản phẩm cần truyền vào </param>
        /// <returns></returns>
        [AuthenticationFilter]
        public ActionResult Update(long ProductCategoryId)
        {
            try
            {
                
                var query = con.ProductCategories.Find(ProductCategoryId);
                if(query!= null)
                {
                    EditProductCategoryModels editProductCategoryModels = new EditProductCategoryModels()
                    {
                        Name = query.Name,
                        Image = query.Image,
                        AltImage = query.AltImage,
                        MetaDescriptions = query.MetaDescriptions,
                        CreatedBy = query.CreatedBy , 
                        CreatedDate = query.CreatedDate,
                        Descriptions = query.Descriptions, 
                        DisplayOrder = query.DisplayOrder, 
                        MetaKeywords = query.MetaKeywords,
                        MetaTitle = query.MetaTitle, 
                        ModifiedBy = query.ModifiedBy, 
                        ModifiedDate = query.ModifiedDate, 
                        ParentID = query.ParentID, 
                        SeoTitle = query.SeoTitle, 
                        ShowOnHome = query.ShowOnHome, 
                        ID = query.ID, 

                    };
                    return View(editProductCategoryModels); 
                }
                else
                {
                    return View(new EditProductCategoryModels()); 
                }
               
            }
            catch
            {

                return View(new EditProductCategoryModels());
            }
           
            
        }

        /// <summary>
        /// Phan Đình Kiên : Trang Thêm Mới thông tin của loại sản phẩm 
        /// </summary>
        /// <returns></returns>
        [AuthenticationFilter]
        public ActionResult Create()
        {
            return View(); 
        }

        /// <summary>
        /// Phan Đình Kiên : thêm mới thông tin loại sản phẩm 
        /// </summary>
        /// <param name="addProductCategory">Thông tin loại sản phẩm sau khi được thêm </param>
        /// <returns></returns>
        [ValidateInput(false)]
        [HttpPost]
        [AuthenticationFilter]
        public int AddProductCategory(AddProductCategory addProductCategory)
        {
            try
            {
                LoginUserModels loginUserModels = Session["Login"] as LoginUserModels;
                ProductCategory productCategory = new ProductCategory()
                {
                    Name = addProductCategory.Name,
                    SeoTitle = addProductCategory.SeoTitle,
                    MetaDescriptions = addProductCategory.MetaDescriptions,
                    MetaKeywords = addProductCategory.MetaKeywords,
                    MetaTitle = addProductCategory.MetaTitle,
                    ParentID = addProductCategory.ParentID,
                    Descriptions = addProductCategory.Descriptions,
                    Image = addProductCategory.Image,
                    AltImage = addProductCategory.AltImage,
                    DisplayOrder = addProductCategory.DisplayOrder,
                    CreatedDate = DateTime.Now,
                    CreatedBy = loginUserModels.Name,
                    ModifiedDate = DateTime.Now,
                    ModifiedBy = loginUserModels.Name,
                    Active = true,
                    ShowOnHome = addProductCategory.ShowOnHome

                };
                con.ProductCategories.Add(productCategory);
                con.SaveChanges();
                return Constants.RETURN_TRUE;
            }
            catch
            {
                return Constants.RETURN_FALSE;
            }
        }


        /// <summary>
        /// Phan Đình Kiên : lấy thông tin của loại sản phẩm đưa vào combobox 
        /// </summary>
        /// <returns></returns>
        [AuthenticationFilter]
        public JsonResult GetSelectProductCategory()
        {
            try
            {
                var query = from data in con.ProductCategories
                            where data.Active == true
                            select new GetProductCategoryModels
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
                    return Json(new List<GetProductCategoryModels>(), JsonRequestBehavior.AllowGet);
                }

            }

            catch
            {
                return Json(new List<GetProductCategoryModels>(), JsonRequestBehavior.AllowGet);
            }
        }

        
        /// <summary>
        /// Phan Đình Kiên : Xóa thông tin của loại sản phẩm 
        /// </summary>
        /// <param name="ProductCategoryId">Mã của loại sản phẩm</param>
        /// <returns></returns>
        public int DeleteProductCategory(long ProductCategoryId)
        {
            try
            {
                var data = con.ProductCategories.Find(ProductCategoryId);
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
        /// Phan Đình Kiên : sửa thông tin của loại sản phẩm 
        /// </summary>
        /// <param name="editProductCategoryModels">Thông tin của loại sản phẩm sau khi được chỉnh sửa</param>
        /// <returns></returns>
        public int EditProductCategory(EditProductCategoryModels editProductCategoryModels)
        {
            try
            {
                LoginUserModels loginUserModels = Session["Login"] as LoginUserModels;
                var Data = con.ProductCategories.Find(editProductCategoryModels.ParentID);

                if(Data!= null)
                {
                    Data.Name = editProductCategoryModels.Name;
                    Data.ParentID = editProductCategoryModels.ParentID;
                    Data.MetaDescriptions = editProductCategoryModels.MetaDescriptions;
                    Data.MetaKeywords = editProductCategoryModels.MetaKeywords;
                    Data.MetaTitle = editProductCategoryModels.MetaTitle;
                    Data.Image = editProductCategoryModels.Image;
                    Data.AltImage = editProductCategoryModels.AltImage;
                    Data.DisplayOrder = editProductCategoryModels.DisplayOrder;
                    Data.ModifiedDate = DateTime.Now;
                    Data.ModifiedBy = loginUserModels.Name;
                    Data.Descriptions = editProductCategoryModels.Descriptions; 

                }
                return Constants.RETURN_TRUE; 
            }
            catch
            {
                return Constants.RETURN_FALSE; 
            }
        }

        

    }
}