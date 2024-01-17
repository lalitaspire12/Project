using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using VEHCILE.Models;
using VEHCILE.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VEHCILE.Data;

namespace VEHCILE.Controllers
{
    [Actions]
    public class DriverController : Controller
    {
        private readonly IData data1;

        public DriverController(IData _data1)
        {
            data1 = _data1;
        }

        public IActionResult Index()
        {
            var list122 = data1.GetAllDrivers();
            return View(list122);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Driver newdriver) //to add new car
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                int totalPassengers = data1.GetAllDrivers().Sum(d => d.AvailableSeats);
                if (totalPassengers + newdriver.AvailableSeats >34) // here 34 is the constant with the max number of seats
                {
                    ViewBag.SeatsFullMessage = "Seats are full. Cannot add more passengers.";
                    return View();
                }
                bool inSaved = data1.AddNewDriver(newdriver);
                ViewBag.inSaved = inSaved;
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult DeleteDriver()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DeleteDriver(string id, Driver deletedriver)
        {
            bool s11saved = data1.DeDriver(id, deletedriver);
            ViewBag.inSaved = s11saved;
            return RedirectToAction("Index");
        }
    }
}
