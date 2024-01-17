using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using VEHCILE.Data;
using VEHCILE.Models;
using VEHCILE.Repository;

namespace VEHCILE.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IConfiguration _configuration;
        private readonly IData _data;

        public HomeController(ApplicationDbContext db, IConfiguration configuration, IData data)
        {
            _db = db;
            _configuration = configuration;
            _data = data;
        }
        public IActionResult Index()
        {
            ViewBag.userSession = HttpContext.Session.GetString("EmployeeID");
            return View();
        }

        public IActionResult About()
        {
            ViewBag.userSession = HttpContext.Session.GetString("EmployeeID");
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.userSession = HttpContext.Session.GetString("EmployeeID");
            return View();
        }

        public IActionResult CompanyPolicy()
        {
            ViewBag.userSession = HttpContext.Session.GetString("EmployeeID");
            TempData["Time"] = DateTime.Now.ToString();
            var cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Append("LastLoginTime", DateTime.Now.ToString(), cookieOptions);
            Console.WriteLine(Request.Cookies["LastLoginTime"]);
            
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(
                new ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                }
            );
        }

        [HttpGet]
        public IActionResult CustomerRequest()
        {
            ViewBag.Session = HttpContext.Session.GetString("EmployeeID");
            var Request = _db.CustomerRequest.ToList();
            return View(Request);
        }

        [HttpPost]
        public IActionResult CustomerRequest(Bookebususer bookebususer)
        {
            ViewBag.Session = HttpContext.Session.GetString("Session");
            // Other processing logic...
            return RedirectToAction("BusSeat");
        }

        [HttpGet]
        public IActionResult Bus1()
        {
            ViewBag.userSession = HttpContext.Session.GetString("EmployeeID");
            List<Bus> buses1 = _db.Buses.ToList();
            ViewBag.busList = buses1;
            return View();
        }

        [HttpPost]
        public IActionResult Bus1(Bus obj)
        {
            if (true)
            {
                SqlConnection conn = new SqlConnection(
                    @"Data Source=.\SQLEXPRESS;Initial Catalog=enterprise;Integrated Security=true"
                );
                SqlCommand cmd = new SqlCommand( // the sqlcommand is being used by the log and signadmin method here to execute a query against the database.
                    "Select SeatingCapacity  from Buses where BusNumber=@BusNumber",
                    conn
                );
                cmd.Parameters.AddWithValue("@BusNumber", "TN 45 SM 9875");
                conn.Open();
                int SeatingCapacity = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();
                int passengers = 40;
                if (passengers == SeatingCapacity)
                {
                    ViewBag.Message = "Seating Capacity Full";
                }
                ViewBag.Session = HttpContext.Session.GetString("EmployeeID");
                Console.WriteLine(HttpContext.Session.GetString("EmployeeID"));
                Bookebususer requestObj = new Bookebususer();
                requestObj.EmployeeID = ViewBag.Session;
                // requestObj.EmployeeID = null;
                requestObj.PickUp = obj.PickUp;
                requestObj.DropOff = obj.DropOff;
                requestObj.senddate = obj.Date;
                requestObj.quantity = null;
                requestObj.status = null;
                _db.CustomerRequest.Add(requestObj);
                _db.SaveChanges();
                SeatBooked.BusLinking();
                return RedirectToAction("BusSeat");
            }

            Console.WriteLine("Seating Capacity of the Bus is full");
            return RedirectToAction("Index");
        }

        [HttpGet] // method used to handle request from the user
        public IActionResult Cab1()
        {
            ViewBag.userSession = HttpContext.Session.GetString("EmployeeID"); // viewbag is a property used to pass data from controller to view.
            ViewBag.userSession = HttpContext.Session.GetString("EmployeeID");
            List<Car> car1 = _db.Cars.ToList();
            ViewBag.busList = car1;
            return View();
        }

        [HttpPost] // used to handle requests from user.
        public IActionResult Cab1(Car obj1)
        {
            if (true)
            {
                ViewBag.Session = HttpContext.Session.GetString("EmployeeID");
                Bookcaruser requestObj1 = new Bookcaruser();
                requestObj1.EmployeeID = ViewBag.Session;
                requestObj1.PickUp = obj1.PickUp;
                requestObj1.DropOff = obj1.DropOff;
                requestObj1.senddate = obj1.Date;
                requestObj1.quantity = null;
                requestObj1.status = null;
                _db.CustomerCar.Add(requestObj1);
                _db.SaveChanges();
                SeatBooked.CarLinking();
                return RedirectToAction("BusSeat");
            }
            return RedirectToAction("Cab1");
        }

        public IActionResult UserBooking()
        {
            string empid = HttpContext.Session.GetString("EmployeeID");
            DataTable new11 = Repository.inner.displayBookingDetails(empid); // the datatable  class is used to represent a table of data.
            return View("UserBooking", new11);
        }

        public IActionResult UserCarHistory()
        {
            string empid = HttpContext.Session.GetString("EmployeeID");
            DataTable new12 = Repository.inner.displayBookingDetails(empid);
            return View("UserCarHistory", new12);
        }

        public IActionResult BikeHistory()
        {
            string empid = HttpContext.Session.GetString("EmployeeID");
            DataTable new13 = Repository.inner.displayBookingDetails(empid);
            return View("BikeHistory", new13);
        }

        public IActionResult UserBus()
        {
            string empid = HttpContext.Session.GetString("EmployeeID");
            Console.WriteLine(empid);
            DataTable new14 = Repository.inner.displayBookingDetails(empid);
            return View(new14);
        }

        public IActionResult Inside()
        {
            return View();
        }

        public IActionResult BusSeat()
        {
            ViewBag.userSession = HttpContext.Session.GetString("EmployeeID");
            return View();
        }

        public IActionResult Bike1()
        {
            ViewBag.userSession = HttpContext.Session.GetString("EmployeeID");
            return View();
        }

        public IActionResult BikeInside()
        {
            ViewBag.userSession = HttpContext.Session.GetString("EmployeeID");
            return View();
        }

        public IActionResult BikeInside2()
        {
            ViewBag.userSession = HttpContext.Session.GetString("EmployeeID");
            return View();
        }

        [HttpGet]
        public IActionResult Log() // from here the code is handling login and logout request of the users in web application.
        {
            ViewBag.userSession = HttpContext.Session.GetString("EmployeeID");
            return View();
        }

        [HttpPost]
        public IActionResult Log(LoginEmployee loginemployee)
        {
                using (SqlConnection connection = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=user;Integrated Security=true"))
                {
                    try
                    {
                        connection.Open();
                        Console.WriteLine("Okay");
                        String uname = loginemployee.EmployeeID;
                        String pwd = loginemployee.Password;
                        SqlCommand sqlcmd = new SqlCommand("select * from Vehcile where EmployeeID='" + uname + "' and password='" + pwd + "';", connection);
                        SqlDataReader reader = sqlcmd.ExecuteReader();
                        if (reader.Read())
                        {
                            if (Convert.ToString(reader["EmployeeID"]) == uname && Convert.ToString(reader["password"]) == pwd)
                            {

                                HttpContext.Session.SetString("EmployeeID", loginemployee.EmployeeID);
                                return View("Index");
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("Error : " + ex.Message.ToString());
                        ModelState.AddModelError("CustomError", "Invalid ID or Password");
                    }
                    finally
                    {
                        Console.WriteLine("Press any key to exit.....");
                        connection.Close();
                    }
                    connection.Close();
                    return View("Log");
                }
            //}

            // string connectionString =_configuration.GetConnectionString("DefaultConnection1"); // actual connection string

            // try 
            // {
            //     using (SqlConnection connection = new SqlConnection(connectionString))
            //     {
            //         connection.Open();
            //         string query = "SELECT * FROM Users WHERE EmployeeID=@uname AND Password=@pwd";
            //         using (SqlCommand sqlcmd = new SqlCommand(query, connection))
            //         {
            //             sqlcmd.Parameters.AddWithValue("@uname", loginemployee.EmployeeID);
            //             sqlcmd.Parameters.AddWithValue("@pwd", loginemployee.Password);

            //             SqlDataReader reader = sqlcmd.ExecuteReader();
            //             if (reader.Read())
            //             {
            //                 HttpContext.Session.SetString("EmployeeID", loginemployee.EmployeeID);
            //                 return RedirectToAction("Index"); // Redirect to the dashboard or any other view after successful login
            //             }
            //             else
            //             {
            //                 ModelState.AddModelError("", "Invalid ID or Password");
            //                 return View(); // Return to the login page with an error message
            //             }
            //         }
            //     }
            // }
            // catch (SqlException ex)
            // {
            //     Console.WriteLine("Error: " + ex.Message);
            //     ModelState.AddModelError("", "An error occurred during login.");
            //     return View(); // Return to the login page with an error message
            // }

        }

        public IActionResult Logout()
        {
            //Delete the Session object.
            HttpContext.Session.Remove("EmployeeID");
            return RedirectToAction("Log" );
        }

        public IActionResult LiveTrack()
        {
            ViewBag.userSession = HttpContext.Session.GetString("EmployeeID");
            return View();
        }

        [HttpGet]
        public IActionResult SignAdmin()
        {
            ViewBag.userSession = HttpContext.Session.GetString("EmployeeID");
            return View();
        }

        [HttpPost]
        public IActionResult SignAdmin(LoginAdmin admin)
        {
            ViewBag.userSession = HttpContext.Session.GetString("EmployeeID");
            string connectionString = _configuration.GetConnectionString("DefaultConnection1");
            try{
                using (
                SqlConnection connection = new SqlConnection(connectionString))
            {       connection.Open();
                    Console.WriteLine("Okay");
                    string query = "SELECT * FROM Vehicle WHERE EmployeeID=@uname AND Password=@pwd";
                    using (SqlCommand sqlcmd = new SqlCommand(query, connection)){
                        sqlcmd.Parameters.AddWithValue("@uname", admin.EmployeeID);
                        sqlcmd.Parameters.AddWithValue("@pwd", admin.Password);

                        SqlDataReader reader = sqlcmd.ExecuteReader();
                        if (reader.Read())
                        {
                            HttpContext.Session.SetString("EmployeeID", admin.EmployeeID);
                            return RedirectToAction("Details");
                        }
                    }
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Error : " + ex.Message.ToString());
                    
                }
                ModelState.AddModelError("", "Invalid username or Password");
                return View();
            }
        

        [HttpGet]
        public IActionResult Admin(){
            ViewBag.userSession = HttpContext.Session.GetString("EmployeeID");
            return View();
        }

        [HttpPost]
        public IActionResult Admin(LoginAdmin admin)
        {
            ViewBag.userSession = HttpContext.Session.GetString("EmployeeID");
            string connectionString = _configuration.GetConnectionString("DefaultConnecton1");
            try{
                using (
                SqlConnection connection = new SqlConnection(connectionString))
            {       connection.Open();
                    Console.WriteLine("Okay");
                    string query = "SELECT * FROM Vehicle WHERE EmployeeID=@uname AND Password=@pwd";
                    using (SqlCommand sqlcmd = new SqlCommand(query, connection)){
                        sqlcmd.Parameters.AddWithValue("@uname", admin.EmployeeID);
                        sqlcmd.Parameters.AddWithValue("@pwd", admin.Password);

                        SqlDataReader reader = sqlcmd.ExecuteReader();
                        if (reader.Read())
                        {
                            HttpContext.Session.SetString("EmployeeID", admin.EmployeeID);
                            return RedirectToAction("Details");
                        }
                    }
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Error : " + ex.Message.ToString());
                    
                }
                ModelState.AddModelError("", "Invalid username or Password");
                return View();
        }

        [HttpPost]
        public IActionResult SubscribeToNewsletter(string Email)
        {
            return Ok();
        }

        public IActionResult Details()
        {
            ViewBag.userSession = HttpContext.Session.GetString("EmployeeID");

            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.userSession = HttpContext.Session.GetString("EmployeeID");
            return View();
        }

        [HttpPost]
        public IActionResult Create(LoginAdmin admin1)
        {
            Regex usernamepattern = new Regex("^[A-Z][A-Za-z0-9_@]{4,}$");
            Regex passwordpattern = new Regex(@"((?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[\W]).{6,20})");

            String EmployeeID = admin1.EmployeeID;

            String password = admin1.Password;

            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString =
                    @"Data Source=LALIT\SQLEXPRESS;Initial Catalog=user;Integrated Security=SSPI";
                try
                {
                    connection.Open();
                    Console.WriteLine("Okay");
                    string query =
                        "INSERT INTO Vehcile(EmployeeID,password) Values('"
                        + EmployeeID
                        + "','"
                        + password
                        + "')";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Record Inserted");
                        return View("Log");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Not ok" + e);
                    ModelState.AddModelError("", "Invalid username or Password");
                }
                finally
                {
                    connection.Close();
                }
                return View();
            }
        }

        [HttpGet]
        public IActionResult signup()
        {
            return View();
        }

        [HttpPost]
        public IActionResult signup(LoginEmployee signup)
        {
            ViewBag.userSession = HttpContext.Session.GetString("EmployeeID");
            Regex usernamepattern = new Regex("^[A-Z][A-Za-z0-9_@]{4,}$");
            Regex passwordpattern = new Regex(@"((?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[\W]).{6,20})");

            String EmployeeID = signup.EmployeeID;

            String password = signup.Password;

            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString =
                    @"Data Source=LALIT\SQLEXPRESS;Initial Catalog=user;Integrated Security=SSPI";
                try
                {
                    connection.Open();
                    Console.WriteLine("Okay");
                    string query =
                        "INSERT INTO Vehcile(EmployeeID,password) Values('"
                        + EmployeeID
                        + "','"
                        + password
                        + "')";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Record Inserted");
                        return View("Log");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Not ok" + e);
                    ModelState.AddModelError("", "Invalid username or Password");
                }
                finally
                {
                    connection.Close();
                }
                return View();
            }
        }

        [HttpGet]
        public IActionResult Forgot()
        {
            return View();
        }

        [HttpPost]
        public void Forgot(string password)
        {
            ViewBag.userSession = HttpContext.Session.GetString("EmployeeID");

            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString =
                    @"Data Source=LALIT\SQLEXPRESS;Initial Catalog=user;Integrated Security=SSPI";
                try
                {
                    connection.Open();
                    Console.WriteLine("Okay");
                    string query2 =
                        "UPDATE Vehcile SET password='"
                        + password
                        + "' WHERE name='"
                        + password
                        + "'";

                    using (SqlCommand cmd = new SqlCommand(query2, connection))
                    {
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Record Updated");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Not ok" + e);
                }
                finally
                {
                    connection.Close();
                }
            }
            Console.WriteLine("you have suuccesfully changed your password");
            return;
        }

        public IActionResult Feedback()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Feedback(Feedback feedback)
        {
            feedback.emailid = Request.Form["emailid"];
            feedback.rating = Convert.ToInt32(Request.Form["rating"]);
            feedback.feedback = Request.Form["feedback"];
            Console.WriteLine(feedback.emailid);
            HttpClient httpClient = new HttpClient();
            string apiUrl = "http://localhost:5005/api/Feedback";
            var jsondata = JsonConvert.SerializeObject(feedback);
            var data = new StringContent(jsondata, Encoding.UTF8, "application/json");
            var httpresponse = httpClient.PostAsync(apiUrl, data);
            var result = await httpresponse.Result.Content.ReadAsStringAsync();
            Console.WriteLine(result);
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Employees employees1)
        {
            SqlConnection con = new SqlConnection(
                @"Data Source=LALIT\SQLEXPRESS;Initial Catalog=enterprise;Integrated Security=SSPI;"
            );
            con.Open();
            string que = String.Format(
                $"Insert into EmployeeDetails(Firstname,Middlename,Lastname,Department,Gender,Phone,CurrentAddress,EmployeeID,Password) values('{employees1.Firstname}','{employees1.Middlename}','{employees1.Lastname}','{employees1.Department}','{employees1.Gender}','{employees1.Phone}','{employees1.CurrentAddress}','{employees1.EmployeeID}','{employees1.Password}')",
                employees1.Firstname,
                employees1.Middlename,
                employees1.Lastname,
                employees1.Department,
                employees1.Gender,
                employees1.Phone,
                employees1.CurrentAddress,
                employees1.EmployeeID,
                employees1.Password
            );
            // SqlCommand cmd=new SqlCommand("Insert into EmployeeDetails"+"(Firstname,Middlename,Lastname,Department,Gender,Phone,CurrentAddress,EmployeeID,Password) values(@Firstname,@Middlename,@Lastname,@Department,@Gender,@Phone,@CurrentAddress,@EmployeeID,@Password)",con);
            SqlCommand cmd = new SqlCommand(que, con);
            cmd.ExecuteNonQuery();
            Console.WriteLine("Reached register function");
            return View();
        }
    }
}

public class InvalidUserException : Exception
{
    public InvalidUserException()
        : base() { }

    public InvalidUserException(string message)
        : base(message) { }

    public InvalidUserException(string message, Exception innerException)
        : base(message, innerException) { }
}
