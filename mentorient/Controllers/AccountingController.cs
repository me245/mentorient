using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using mentorient.Data;
using mentorient.Models;
using mentorient.Models.Accounting;
using mentorient.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Extensions.Internal;
using Microsoft.EntityFrameworkCore.Storage;

namespace mentorient.Controllers
{
    public class AccountingController : Controller
    {
        private ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public AccountingController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(User);
            var entries = _context.Users.Include(usr => usr.AccountingEntries)
                .Single(usr => usr.Id == userId)
                .AccountingEntries;
            List<Tuple<Entry, string>> values = new List<Tuple<Entry, string>>();

            foreach(Entry entry in entries)
            {
                var tenant = _context.Tenants.Single(ten => ten.Id == entry.TenantId);
                string name = $"{tenant.FirstName} {tenant.LastName}";
                values.Add(Tuple.Create(entry, name));
                
            }

            return View(values);
        }

        public IActionResult Create()
        {
            var userId = _userManager.GetUserId(User);
            var vm = new AccountingEntryViewModel();
            var tenants = _context.Users.Include(usr => usr.Tenants)
                .Single(usr => usr.Id == userId)
                .Tenants;

            vm.Tenants = tenants.Select(a => new SelectListItem()
                {
                    Value = a.Id.ToString(),
                    Text = a.FirstName + " " + a.LastName
                })
                .ToList();
            return View(vm);
        }

        [HttpPost]
        public IActionResult Save(Entry accountingEntry)
        {
            var userId = _userManager.GetUserId(User);
            var tenantId = accountingEntry.TenantId;
            var tenant = _context.Tenants.SingleOrDefault(t => t.Id == tenantId);

            _context.Users.Single(usr => usr.Id == userId).AccountingEntries.Add(accountingEntry);

            _context.Entries.Add(accountingEntry);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}