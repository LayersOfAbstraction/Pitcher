﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pitcher.Models;
using Pitcher.Data;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace Pitcher.Controllers
{
    public class HomeController : Controller
    {
        private readonly TeamContext _context;

        public HomeController(TeamContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
        //public async Task<ActionResult> About()
        //{
        //    List<EnrollmentDateGroup> groups = new List<EnrollmentDateGroup>();
        //    var conn = _context.Database.GetDbConnection();
        //    try
        //    {
        //        await conn.OpenAsync();
        //        using (var command = conn.CreateCommand())
        //        {
        //            string query = "SELECT EnrollmentDate, COUNT(*) AS StudentCount "
        //                + "FROM Person "
        //                + "WHERE Discriminator = 'Student' "
        //                + "GROUP BY EnrollmentDate";
        //            command.CommandText = query;
        //            DbDataReader reader = await command.ExecuteReaderAsync();

        //            if (reader.HasRows)
        //            {
        //                while (await reader.ReadAsync())
        //                {
        //                    var row = new EnrollmentDateGroup { EnrollmentDate = reader.GetDateTime(0), StudentCount = reader.GetInt32(1) };
        //                    groups.Add(row);
        //                }
        //            }
        //            reader.Dispose();
        //        }
        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }
        //    return View(groups);
        //}
    }
}
