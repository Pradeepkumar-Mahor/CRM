using CMR.DataAccess.Repositories;
using CMR.Domain.DataClass;
using CRM.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static CRM.UI.Classes.DTModel;

namespace CRM.UI.Areas.SystemAdmin.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _ProductRepository;

        public ProductController(IProductRepository ProductRepository)
        {
            _ProductRepository = ProductRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> GetListsProduct(string paramJson)
        {
            DTResult<ProductDataTableList> result = new();
            var param = JsonConvert.DeserializeObject<DTParameters>(paramJson);
            int totalRow = 0;

            try
            {
                if (param.Start is 0)
                {
                    param.Start = 1;
                }
                var data = await _ProductRepository
                            .GetAllProductAsync(
                               pageNumber: param.Start,
                               rowsOfPage: param.Length
                             );

                if (data.Any())
                {
                    totalRow = (int)data.FirstOrDefault().TotalRow.Value;
                }

                result.draw = param.Draw;
                result.recordsTotal = totalRow;
                result.recordsFiltered = totalRow;
                result.data = data.ToList();

                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult ProductCreate()
        {
            AddProductViewModel model = new();

            return PartialView("_ModalProductCreate", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductCreate([FromForm] AddProductViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    AddProduct addProductModel = new AddProduct()
                    {
                        ProductName = model.ProductName,
                        ProductDescription = model.ProductDescription,
                        ProductIsActive = model.ProductIsActive,
                        ImgUrl = model.ImgUrl,
                    };
                    bool addProduct = await _ProductRepository.AddAsync(addProductModel);
                    if (addProduct)
                    {
                        TempData["msg"] = "Successfully Added";
                        return Json(new { success = true, response = TempData["msg"] });
                    }
                    else
                    {
                        TempData["msg"] = "Could Not Added";
                        return Json(new { success = false, response = TempData["msg"] });
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, response = ex.Message });
            }

            return PartialView("_ModalProductCreate", model);
        }

        [HttpGet]
        public async Task<IActionResult> ProductEdit(int id)
        {
            UpdateProductViewModel model = new();
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var Product = await _ProductRepository.GetByIdAsync(id);

                if (Product == null)
                {
                    throw new Exception();
                }
                else
                {
                    model.Id = id;
                    model.ProductName = Product.ProductName;
                    model.ProductDescription = Product.ProductDescription;
                    model.ProductIsActive = Product.ProductIsActive;
                    model.ImgUrl = Product.ImgUrl; ;
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, response = ex.Message });
            }

            return PartialView("_ModalProductEdit", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductEdit([FromForm] UpdateProductViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UpdateProduct updateProductModel = new UpdateProduct()
                    {
                        Id = model.Id,
                        ProductName = model.ProductName,
                        ProductDescription = model.ProductDescription,
                        ImgUrl = model.ImgUrl,
                        ProductIsActive = model.ProductIsActive
                    };
                    bool updateProduct = await _ProductRepository.UpdateAsync(updateProductModel);
                    if (updateProduct)
                    {
                        TempData["msg"] = "Edit Successfully.";
                        return Json(new { success = true, response = TempData["msg"] });
                    }
                    else
                    {
                        TempData["msg"] = "Could Not Edit.";
                        return Json(new { success = false, response = TempData["msg"] });
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, response = ex.Message });
            }

            return PartialView("_ModalProductEdit", model);
        }

        [HttpPost]
        public async Task<IActionResult> ProductDelete(int id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                var deleteResult = await _ProductRepository.DeleteAsync(id);

                if (deleteResult)
                {
                    TempData["msg"] = "Edit Successfully.";
                    return Json(new { success = true, response = TempData["msg"] });
                }
                else
                {
                    TempData["msg"] = "Could Not Edit.";
                    return Json(new { success = false, response = TempData["msg"] });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, response = ex.Message });
            }
        }
    }
}