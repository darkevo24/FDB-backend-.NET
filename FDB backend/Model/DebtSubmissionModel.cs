// FDB_backend.Model/DebtSubmissionModel.cs
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FDB_backend.Model
{
    public class DebtSubmissionModel
    {
        public int Id { get; set; }
        public bool IgnoreMultipleCurrencies { get; set; }
        public string Invoice { get; set; }
        public string Currency { get; set; }
        public decimal AmountDue { get; set; }
        public DateTime[] Dates { get; set; }
        public IFormFile FileUpload { get; set; }
        public YourDetails YourDetails { get; set; }
        public TheirDetails TheirDetails { get; set; }
        public DebtOwed DebtOwed { get; set; }
    }

    public class DebtOwed
    {
        public int Id { get; set; }
        public string Bankruptcy { get; set; }
        public string Jurisdiction { get; set; }
        public string BankruptcyNumber { get; set; }
        public string ReportedToAgency { get; set; }

        public AgencyInformation AgencyInformation { get; set; }
        public string Invoice { get; set; }
        public string Currency { get; set; }
        public decimal AmountDue { get; set; }
        public bool IgnoreMultipleCurrencies { get; set; }
        [NotMapped]
        public List<IFormFile> UploadDocuments { get; set; }
    }

    public class AgencyInformation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string PhoneNumber { get; set; }
        public string AgencyFileNumber { get; set; }
        public DateTime Date { get; set; }
    }

    public class YourDetails
    {
        public int Id { get; set; }
        // Properties for YourDetails model
        public string Company { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string Contact { get; set; }
        public string Title { get; set; }
        public string Tel { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string URL { get; set; }
    }

    public class TheirDetails
    {
        public int Id { get; set; }
        // Properties for TheirDetails model
        public string Company { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string OwnerName { get; set; }
        public string OwnerCountry { get; set; }
        public string Contact { get; set; }
        public string Title { get; set; }
        public string Tel { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string URL { get; set; }
    }
}
