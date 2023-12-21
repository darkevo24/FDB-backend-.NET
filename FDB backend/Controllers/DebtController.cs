using FDB_backend.Data;
using FDB_backend.Model;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FDB_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DebtController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public DebtController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("submit")]
        public IActionResult SubmitDebt([FromBody] DebtSubmissionModel submission)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid submission data");
            }

            var yourDetailsEntity = MapToEntity(submission.YourDetails);
            var theirDetailsEntity = MapToEntity(submission.TheirDetails);
            var debtOwedEntity = MapToEntity(submission.DebtOwed);

            _dbContext.YourDetails.Add(yourDetailsEntity);
            _dbContext.TheirDetails.Add(theirDetailsEntity);
            _dbContext.DebtOwed.Add(debtOwedEntity);

            _dbContext.SaveChanges();

            return Ok("Debt submitted successfully");
        }

        private YourDetails MapToEntity(YourDetails model)
        {
            return new YourDetails
            {
                Company = model.Company,
                Address = model.Address,
                City = model.City,
                State = model.State,
                Zip = model.Zip,
                Country = model.Country,
                Contact = model.Contact,
                Title = model.Title,
                Tel = model.Tel,
                Fax = model.Fax,
                Email = model.Email,
                URL = model.URL
                // Map other properties as needed
            };
        }

        private TheirDetails MapToEntity(TheirDetails model)
        {
            return new TheirDetails
            {
                Company = model.Company,
                Address = model.Address,
                City = model.City,
                State = model.State,
                Zip = model.Zip,
                Country = model.Country,
                OwnerName = model.OwnerName,
                OwnerCountry = model.OwnerCountry,
                Contact = model.Contact,
                Title = model.Title,
                Tel = model.Tel,
                Fax = model.Fax,
                Email = model.Email,
                URL = model.URL
                // Map other properties as needed
            };
        }

        private DebtOwed MapToEntity(DebtOwed model)
        {
            return new DebtOwed
            {
                Bankruptcy = model.Bankruptcy,
                Jurisdiction = model.Jurisdiction,
                BankruptcyNumber = model.BankruptcyNumber,
                ReportedToAgency = model.ReportedToAgency,
                AgencyInformation = new AgencyInformation
                {
                    Name = model.AgencyInformation?.Name,
                    Location = model.AgencyInformation?.Location,
                    PhoneNumber = model.AgencyInformation?.PhoneNumber,
                    AgencyFileNumber = model.AgencyInformation?.AgencyFileNumber,
                    Date = model.AgencyInformation?.Date ?? DateTime.MinValue
                },
                Invoice = model.Invoice,
                Currency = model.Currency,
                AmountDue = model.AmountDue,
                IgnoreMultipleCurrencies = model.IgnoreMultipleCurrencies,
                UploadDocuments = model.UploadDocuments
            };
        }


        private AgencyInformation MapToEntity(AgencyInformation model)
        {
            return new AgencyInformation
            {
                Name = model.Name,
                Location = model.Location,
                PhoneNumber = model.PhoneNumber,
                AgencyFileNumber = model.AgencyFileNumber,
                Date = model.Date
                // Map other properties as needed
            };
        }
    }
}
