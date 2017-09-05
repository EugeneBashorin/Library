using LibraryProject.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace LibraryProject.Controllers
{
    public class AccountController : Controller
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Login model)
        {
            List<User> userList = new List<User>();
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("Select * From Users", con);
                    try
                    {
                        con.Open();
                        SqlDataReader dr = command.ExecuteReader();
                        if(dr.HasRows)
                            foreach(DbDataRecord result in dr)
                            {
                                userList.Add(new User() {  Id = result.GetInt32(0) , Email = result.GetString(1) , Password = result.GetString(2) , Age = result.GetInt32(3) });
                            }
                    }
                    catch(Exception)
                    {    }
                }
                User user = userList.Where(u => u.Email == model.Name && u.Password == model.Password).FirstOrDefault();
                if(user != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Name, true);
                        return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "User with this password and login does not exist");
                }
            }
            return View(model);
        }
        //****************************************************************************************************************************************************//
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Register model)
        {
            List<User> userList = new List<User>();
            if (ModelState.IsValid)
            {
                //Search 
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("Select * From Users", con);
                    try
                    {
                        con.Open();
                        SqlDataReader dr = command.ExecuteReader();
                        if (dr.HasRows)
                            foreach (DbDataRecord result in dr)
                            {
                                userList.Add(new User() { Id = result.GetInt32(0), Email = result.GetString(1), Password = result.GetString(2), Age = result.GetInt32(3) });
                            }
                    }
                    catch
                    {
                    }
                }
                User user = userList.Where(u => u.Email == model.Name).FirstOrDefault();
                if (user == null)
                {
                    //Write to DB***************************************************************************************************************||||||||||||||||||*******************************
                    string insertUser = String.Format("INSERT INTO Users ([Id], [Email], [Password], [Age]) VALUES('{0}','{1}','{2}',{3})", userList.Count+1, model.Name, model.Password, model.Age);
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        SqlCommand command = new SqlCommand(insertUser, con);
                        try
                        {
                            con.Open();
                            command.ExecuteNonQuery();                      
                        }
                        catch(Exception)
                        {   }
                    }
                    //Search () if add to list and db without search second time
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        SqlCommand command = new SqlCommand("Select * From Users", con);
                        try
                        {
                            con.Open();
                            SqlDataReader dr = command.ExecuteReader();
                            if (dr.HasRows)
                                foreach (DbDataRecord result in dr)
                                {
                                    userList.Add(new User() { Id = result.GetInt32(0), Email = result.GetString(1), Password = result.GetString(2), Age = result.GetInt32(3) });
                                }
                        }
                        catch
                        {
                        }
                    }
                    user = userList.Where(u => u.Email == model.Name && u.Password == model.Password).FirstOrDefault();
                    if(user != null)
                    {
                        FormsAuthentication.SetAuthCookie(model.Name, true);
                        return RedirectToAction("Index", "Home");
                    }                   
                }
                else
                {
                    ModelState.AddModelError("", "User with this password and login exist");
                }
            }
            return View(model);
        }

        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}