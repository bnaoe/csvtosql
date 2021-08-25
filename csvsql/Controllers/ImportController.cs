using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using csvsql.Models;

namespace csvsql.Controllers
{
    public class ImportController : Controller
    {

        private ApplicationDbContext _context;

        public ImportController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Import
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase postedFile)
        {
            if (postedFile != null)
            {
                try
                {
                    string fileExtension = Path.GetExtension(postedFile.FileName);

                    //Validate uploaded file and return error.
                    if (fileExtension != ".csv")
                    {
                        ViewBag.Message = "Please select the csv file with .csv extension";
                        return View();
                    }


                    var employees = new List<Employee>();
                    using (var sreader = new StreamReader(postedFile.InputStream))
                    {
                        //First line is header. If header is not passed in csv then we can neglect the below line.
                        string[] headers = sreader.ReadLine().Split(',');
                        //Loop through the records
                        while (!sreader.EndOfStream)
                        {
                            string[] rows = sreader.ReadLine().Split(',');

                            employees.Add(new Employee
                            {
                                EmployeeId = int.Parse(rows[0].ToString()),
                                EmployeeName = rows[1].ToString(),
                                Designation = rows[2].ToString(),
                                Salary = int.Parse(rows[3].ToString())
                            });


                        }

                        foreach (var e in employees)
                        {
                            _context.Employees.Add(e);
                        }

                        _context.SaveChanges();

                    }

                    return View("View", employees);
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                }
            }
            else
            {
                ViewBag.Message = "Please select the file first to upload.";
            }
            return View();
        }

    }

}