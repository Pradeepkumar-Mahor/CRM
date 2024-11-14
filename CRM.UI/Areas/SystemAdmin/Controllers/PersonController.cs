using MGMTApp.DataAccess.Repositories;
using MGMTApp.Domain.Person;
using MGMTApp.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net;
using static MGMTApp.WebApp.Classes.DTModel;

namespace MGMTApp.WebApp.Controllers
{
    public class PersonController : Controller
    {
        private readonly IPersonRepository _personRepository;

        public PersonController(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> GetListsPerson(string paramJson)
        {
            DTResult<PersonDataTableList> result = new();
            var param = JsonConvert.DeserializeObject<DTParameters>(paramJson);
            int totalRow = 0;

            try
            {
                if (param.Start is 0)
                {
                    param.Start = 1;
                }
                var data = await _personRepository
                            .GetAllPersonAsync(
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
        public IActionResult PersonCreate()
        {
            PersonCreateViewModel personCreateViewModel = new();

            return PartialView("_ModalPersonCreate", personCreateViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PersonCreate([FromForm] PersonCreateViewModel personCreateViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    AddPerson addPersonModel = new AddPerson()
                    {
                        Email = personCreateViewModel.Email,
                        UserName = personCreateViewModel.UserName,
                        Address = personCreateViewModel.Address,
                    };
                    bool addPerson = await _personRepository.AddAsync(addPersonModel);
                    if (addPerson)
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

            return PartialView("_ModalPersonCreate", personCreateViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> PersonEdit(int id)
        {
            PersonUpdateViewModel personUpdateViewModel = new();
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var person = await _personRepository.GetAllPersonAsync(id);

                if (person == null)
                {
                    throw new Exception();
                }
                else
                {
                    var t = person.First();

                    personUpdateViewModel.Id = id;
                    personUpdateViewModel.Email = t.Email;
                    personUpdateViewModel.UserName = t.UserName;
                    personUpdateViewModel.Address = t.Address;
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, response = ex.Message });
            }

            return PartialView("_ModalPersonEdit", personUpdateViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PersonEdit([FromForm] PersonUpdateViewModel personUpdateViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UpdatePerson updatePersonModel = new UpdatePerson()
                    {
                        Id = personUpdateViewModel.Id,
                        Email = personUpdateViewModel.Email,
                        UserName = personUpdateViewModel.UserName,
                        Address = personUpdateViewModel.Address,
                    };
                    bool updatePerson = await _personRepository.UpdateAsync(updatePersonModel);
                    if (updatePerson)
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

            return PartialView("_ModalPersonEdit", personUpdateViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> PersonDelete(int id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                var deleteResult = await _personRepository.DeleteAsync(id);

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

        //public async Task<IActionResult> personExcelDownload()
        //{
        //    try
        //    {
        //        var retVal = await _filterRepository.FilterRetrieveAsync(new Domain.Models.FilterRetrieve
        //        {
        //            PModuleName = CurrentUserIdentity.CurrentModule,
        //            PMetaId = CurrentUserIdentity.MetaId,
        //            PPersonId = CurrentUserIdentity.EmployeeId,
        //            PMvcActionName = ConstFilterBayIndex
        //        });
        //        FilterDataModel filterDataModel = new();
        //        if (!string.IsNullOrEmpty(retVal.OutPFilterJson))
        //        {
        //            filterDataModel = JsonConvert.DeserializeObject<FilterDataModel>(retVal.OutPFilterJson);
        //        }

        //        string StrFimeName;

        //        var timeStamp = DateTime.Now.ToFileTime();
        //        StrFimeName = "Bay_" + timeStamp.ToString();

        //        string strUser = User.Identity.Name;

        //        IEnumerable<BayDataTableList> data = await _bayDataTableListRepository.BayDataTableListForExcelAsync(
        //            BaseSpTcmPLGet(),
        //            new ParameterSpTcmPL
        //            {
        //            });

        //        if (data == null) { return NotFound(); }

        //        var json = JsonConvert.SerializeObject(data);

        //        IEnumerable<personDataTableExcel> excelData = JsonConvert.DeserializeObject<IEnumerable<personDataTableExcel>>(json);

        //        byte[] byteContent = _utilityRepository.ExcelDownloadFromIEnumerable(excelData, "Bays", "Bays");

        //        var mimeType = MimeTypeMap.GetMimeType("xlsx");

        //        FileContentResult file = File(byteContent, mimeType, StrFimeName);

        //        return Json(ResponseHelper.GetMessageObject("File downloaded successfully.", file));
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { success = false, response = ex.Message });
        //    }
        //}

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddPerson person)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(person);
                }

                bool addPerson = await _personRepository.AddAsync(person);
                if (addPerson)
                {
                    TempData["msg"] = "Successfully Added";
                }
                else
                {
                    TempData["msg"] = "Could Not Added";
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = "Could Not Added";
            }
            return RedirectToAction(nameof(Add));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var person = await _personRepository.GetByIdAsync(id);
            if (person == null)
            {
                throw new Exception();
            }
            return View("Edit", person);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdatePerson person)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(person);
                }
                var updateResult = await _personRepository.UpdateAsync(person);
                if (updateResult)
                {
                    TempData["msg"] = "Edit Successfully.";
                    return RedirectToAction(nameof(DisplayAllPerson));
                }
                else
                {
                    TempData["msg"] = "Could Not Edit.";
                    return View(person);
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = "Could Not Edit.";
                return View(person);
            }
        }

        [HttpGet]
        public async Task<IActionResult> DisplayAllPerson()
        {
            try
            {
                var personAll = await _personRepository.GetAllPersonAsync(1, 1000);

                return View(personAll);
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteResult = await _personRepository.DeleteAsync(id);
            return RedirectToAction(nameof(DisplayAllPerson));
        }
    }
}