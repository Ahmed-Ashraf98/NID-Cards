using Microsoft.AspNetCore.Mvc;
using NID_Cards.Data.DataBase_Context;
using NID_Cards.FormsModel;
using NID_Cards.Models;
using NID_Cards.Extension_Methods;
using NToastNotify;
using NID_Cards.Data.Enums;
using Microsoft.EntityFrameworkCore;

namespace NID_Cards.Controllers
{
    public class CitizenController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IToastNotification _toastNotify;
        private readonly List<string> _allowedExtensions = new() { ".jpg", ".png" };
        public CitizenController(AppDbContext dbContext, IToastNotification toastNotification)
        {
            _appDbContext = dbContext;
            _toastNotify = toastNotification; 
        }


        
        // --------------------------------------Methods-----------------------------------------
        private IQueryable<Governorate> GetAllEgyGovs()=>_appDbContext.Governorates.OrderBy(x => x.GovernorateName);

        private IQueryable<BirthSite> GetAllBirthSites() =>_appDbContext.BirthSites.OrderBy(x => x.BirthSiteName);


        private List<string> GetRecivedImageInputNames(IFormFileCollection formFiles)
        {
            var imageInputNames = new List<string>();

            if (formFiles.Count != 0)
            {
                foreach (var file in formFiles)
                {
                    imageInputNames.Add(file.Name);
                }
            }

            return imageInputNames;
        }

        private bool ImageExtensionsIsValid(IFormFileCollection formFiles)
        {
            Int16 errorCount = 0;

            foreach (var file in formFiles)
            {
                if (!_allowedExtensions.Contains(Path.GetExtension(file.FileName).ToLower()))
                {
                    ModelState.AddModelError($"{file.Name}", "Only .jpg ,.png images are allowed");
                    errorCount++;
                }
            }

            return errorCount > 0 ? false : true;
         }

        private async Task<Dictionary<string, MemoryStream>> StoreImagesInMemory(IFormFileCollection formFiles)
        {
            Dictionary<string, MemoryStream> memoryImages = new();

            foreach (var file in formFiles)
            {
                using (var dataStream = new MemoryStream())
                {
                    await file.CopyToAsync(dataStream);
                    memoryImages.Add(file.Name, dataStream);
                }
            }

            return memoryImages;
        }

        private  Citizen MapToCitizen(FormsCitizen formsCitizen, Dictionary<string, MemoryStream> memoryImages)
        {

            return new Citizen
            {
                NID = formsCitizen.NID,
                Name = formsCitizen.Name,
                BirthDate = formsCitizen.BirthDate,
                Address = formsCitizen.Address,
                DateOfIssue = formsCitizen.DateOfIssue,
                PlaceOfIssue = formsCitizen.PlaceOfIssue,
                ExpiryDate = formsCitizen.DateOfIssue.AddYears(7),
                JobTitle = formsCitizen.JobTitle,
                Religion = formsCitizen.Religion,
                Gender = formsCitizen.Gender,
                MaritalStatus = formsCitizen.MaritalStatus,
                HusbandName = formsCitizen.HusbandName ?? "",
                NIDIsActive = (formsCitizen.DateOfIssue.AddYears(7).FromCurrentDate()) == false ? false : true,
                PersonalPhoto = memoryImages["PersonalPhoto"].ToArray(),
                NIDFrontImage = memoryImages["NIDFrontImage"].ToArray(),
                NIDBackImage = memoryImages["NIDBackImage"].ToArray(),
                GovernorateID = formsCitizen.GovernorateID,
                BirthSiteID = formsCitizen.BirthSiteID,
            };
        }

        private FormsCitizen MapToFormsCitizen(Citizen citizen)
        {
            return new FormsCitizen()
            {
                NID = citizen.NID,
                Name = citizen.Name,
                BirthDate = citizen.BirthDate,
                Address = citizen.Address,
                DateOfIssue = citizen.DateOfIssue,
                PlaceOfIssue = citizen.PlaceOfIssue,
                JobTitle = citizen.JobTitle,
                Religion = citizen.Religion,
                Gender = citizen.Gender,
                HusbandName = citizen.HusbandName,
                PersonalPhoto = citizen.PersonalPhoto,
                NIDFrontImage = citizen.NIDFrontImage,
                NIDBackImage = citizen.NIDBackImage,
                MaritalStatus = citizen.MaritalStatus,
                GovernorateID = citizen.GovernorateID,
                BirthSiteID = citizen.BirthSiteID
            };
        }

        private bool CheckHusbandNameIsRequired(FormsCitizen formsCitizen)
        {
            var gender = formsCitizen.Gender;
            var age = DateTime.Now.Year - formsCitizen.BirthDate.Year;
            var maritalStatus = formsCitizen.MaritalStatus;
            if (gender == "F" && age >= 18 && maritalStatus == MaritalStatusEnum.Married) return true;

            return false;
        }
        // -------------------------------------------------------------------------------
        public  IActionResult Index()
        {
            var getAllCitizen = _appDbContext.Citizens.ToList();

            return View(getAllCitizen);
        }

        public IActionResult Create()
        {
            // pass the form model

            var formObject = new FormsCitizen();

            ViewBag.AllEgyGovs = GetAllEgyGovs();
            ViewBag.AllBirthSites = GetAllBirthSites();
            ViewBag.CreateMode = true; 
            return View("NIDCRUDForm", formObject);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FormsCitizen formsCitizen)
       {

        //    formsCitizen.MaritalStatus = formsCitizen.MaritalStatus == 0 ? MaritalStatusEnum.Single : formsCitizen.MaritalStatus;
            // save data
            ViewBag.AllEgyGovs = GetAllEgyGovs();
            ViewBag.AllBirthSites = GetAllBirthSites();
            ViewBag.CreateMode = true;
            Dictionary<string, MemoryStream> memoryImages = new();


            if (!ModelState.IsValid)
            {
                return View("NIDCRUDForm", formsCitizen);
            }

            Citizen? nidIsUnique = await _appDbContext.Citizens.FindAsync(formsCitizen.NID); 

            if (nidIsUnique != null)
            {
                _toastNotify.AddErrorToastMessage("There is already a citizen with this NID");
                return View("NIDCRUDForm", formsCitizen);
            }

            var validateHusbandName = CheckHusbandNameIsRequired(formsCitizen);

            if (validateHusbandName && formsCitizen.HusbandName == null)
            {
                ModelState.AddModelError("HusbandName", "Husband Name is required since your status is Married");
                _toastNotify.AddErrorToastMessage("Husband Name is required");
                return View("NIDCRUDForm", formsCitizen);
            }

            var formFiles = Request.Form.Files;

            var allRecivedFileInputNames = GetRecivedImageInputNames(formFiles);

            if (formFiles.Count != 3) // if user forget one image or more to upload
            {
                if(!allRecivedFileInputNames.Contains("PersonalPhoto"))
                ModelState.AddModelError("PersonalPhoto", "Please add the Personal pic ");
                
                if (!allRecivedFileInputNames.Contains("NIDFrontImage"))
                ModelState.AddModelError("NIDFrontImage", "Please add the NID Front Image ");
                
                if (!allRecivedFileInputNames.Contains("NIDBackImage"))
                ModelState.AddModelError("NIDBackImage", "Please add the NID Back Image ");

                _toastNotify.AddErrorToastMessage("All Images are required!!");

                return View("NIDCRUDForm", formsCitizen);
            }

            var imagesIsValid = ImageExtensionsIsValid(formFiles);

            if (imagesIsValid)
            {
                memoryImages = await StoreImagesInMemory(formFiles);
            }
            else
            {
                return View("NIDCRUDForm", formsCitizen);
            }

            var citizen =  MapToCitizen(formsCitizen, memoryImages);

            _appDbContext.Add(citizen);
            _appDbContext.SaveChanges();

            _toastNotify.AddSuccessToastMessage("The National ID has been created successfully ");

            return RedirectToAction("Index");
        }



        public async Task<IActionResult> Edit(long? nid)
        {
            // pass the form model

            if (nid == null)
            {
                return BadRequest();
            }

            var citizen =await _appDbContext.Citizens.FindAsync(nid);

            if (citizen == null)
            {
                return NotFound();
            }

            var formObject = MapToFormsCitizen(citizen);

            ViewBag.AllEgyGovs = GetAllEgyGovs();
            ViewBag.AllBirthSites = GetAllBirthSites();
            ViewBag.CreateMode = false;
            return View("NIDCRUDForm", formObject);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? NID,FormsCitizen formsCitizen)
        {
            if (NID == 0)
            {
                return BadRequest();
            }
           // formsCitizen.MaritalStatus = formsCitizen.MaritalStatus == 0 ? MaritalStatusEnum.Single : formsCitizen.MaritalStatus;

            ViewBag.AllEgyGovs = GetAllEgyGovs();
            ViewBag.AllBirthSites = GetAllBirthSites();
            ViewBag.CreateMode = false;
            Dictionary<string,MemoryStream> memoryImages = new();


            if (!ModelState.IsValid)
            {
                return View("NIDCRUDForm", formsCitizen);
            }

            var validateHusbandName = CheckHusbandNameIsRequired(formsCitizen);

            if (validateHusbandName && formsCitizen.HusbandName == null)
            {
                ModelState.AddModelError("HusbandName", "Husband Name is required since your status is Married");
                _toastNotify.AddErrorToastMessage("Husband Name is required");
                return View("NIDCRUDForm", formsCitizen);
            }

            Citizen? personCard = await _appDbContext.Citizens.FindAsync(NID);

            if (personCard != null) { 

            var formFiles = Request.Form.Files;

            var imagesIsValid = ImageExtensionsIsValid(formFiles);

            if (formFiles.Count != 0) // if the user updated one image or more 
            {
                if (imagesIsValid)
                {
                        memoryImages = await StoreImagesInMemory(formFiles);
                }else {
                        
                        formsCitizen.PersonalPhoto = personCard.PersonalPhoto;
                        formsCitizen.NIDFrontImage = personCard.NIDFrontImage;
                        formsCitizen.NIDBackImage = personCard.NIDBackImage;

                        return View("NIDCRUDForm", formsCitizen);

                }
            }


            personCard.Name = formsCitizen.Name;// NIDFrontImage / NIDBackImage
            personCard.BirthDate = formsCitizen.BirthDate;
            personCard.Address = formsCitizen.Address;
            personCard.DateOfIssue = formsCitizen.DateOfIssue;
            personCard.PlaceOfIssue = formsCitizen.PlaceOfIssue;
            personCard.ExpiryDate = formsCitizen.DateOfIssue.AddYears(7);
            personCard.JobTitle = formsCitizen.JobTitle;
            personCard.Religion = formsCitizen.Religion;
            personCard.Gender = formsCitizen.Gender;
            personCard.MaritalStatus = formsCitizen.MaritalStatus;
            personCard.HusbandName = formsCitizen.HusbandName ?? "";
            personCard.NIDIsActive = (formsCitizen.DateOfIssue.AddYears(7).FromCurrentDate()) == false ? false : true;
            personCard.PersonalPhoto = memoryImages.ContainsKey("PersonalPhoto") ? memoryImages["PersonalPhoto"].ToArray() : personCard!.PersonalPhoto;
            personCard.NIDFrontImage = memoryImages.ContainsKey("NIDFrontImage") ? memoryImages["NIDFrontImage"].ToArray() : personCard!.NIDFrontImage;
            personCard.NIDBackImage = memoryImages.ContainsKey("NIDBackImage") ? memoryImages["NIDBackImage"].ToArray() : personCard!.NIDBackImage;
            personCard.GovernorateID = formsCitizen.GovernorateID;
            personCard.BirthSiteID = formsCitizen.BirthSiteID;

            _appDbContext.Update(personCard);
            _appDbContext.SaveChanges();
            _toastNotify.AddSuccessToastMessage("The National ID has been updated successfully ");

            return RedirectToAction("Index");
            }


            return View("NIDCRUDForm", formsCitizen);
        }



        public async Task<IActionResult> Details(long? NID)
        {
            if (NID == null)
            {
                return BadRequest();
            }

            var citizen = await _appDbContext.Citizens.FindAsync(NID);

            if (citizen == null)
            {
                return NotFound();
            }

            ViewBag.AllEgyGovs = GetAllEgyGovs();
            ViewBag.AllBirthSites = GetAllBirthSites();

            return View(citizen);

        }


        public async Task<IActionResult> Delete(long? nid)
        {
            if (nid == null)
            {
                return BadRequest();
            }

            var citizen = await _appDbContext.Citizens.FindAsync(nid);

            if (citizen == null)
            {
                return NotFound();
            }

            _appDbContext.Remove(citizen);
            _appDbContext.SaveChanges();

            return Ok();

        }

      

    }
}
