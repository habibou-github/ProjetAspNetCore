using migrationProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using migrationProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;

namespace gestion_ordre_mission.Controllers
{
    public class LoginController : Controller
    {

        private readonly DbContextIndimnite db;



        //login traitement

        public IActionResult Login()
        {
            HttpContext.Session.SetString("nompersonnel", " ");
            HttpContext.Session.SetInt32("idpersonnel", 0);
            HttpContext.Session.SetString("isConnected", " ");

            return View();
        }


        [HttpPost]
        public IActionResult Authorize(Personnel pers)
        {

                Personnel userdetails = db.personnels.Where(a => a.Username == pers.Username && a.Password == pers.Password).FirstOrDefault();

                if (userdetails == null)
                {
                    ViewBag.error = "Verifier vos information!";

                    return View("Login");

                }
                else
                {

                    if (CheckRole(userdetails) == 1)
                    {
                        HttpContext.Session.SetString("nompersonnel", userdetails.Nom + " " + userdetails.Prenom );
                        HttpContext.Session.SetInt32("idpersonnel", userdetails.IdPers);
                        HttpContext.Session.SetString ("isConnected", "true");

                    



                        return RedirectToAction("IndexPersonnel", "Home");

                    }
                    else if (CheckRole(userdetails) == 0)
                    {
                     
                        HttpContext.Session.SetString("nomeconomique", userdetails.Nom + " " + userdetails.Prenom);
                        HttpContext.Session.SetString("isConnected", "true");
                        return RedirectToAction("Index", "Home");

                    }
                    else
                    {
                        ViewBag.error = "les informations sont incorrect !";

                        return View("Login");
                    }


                }
            

        }
        public int CheckRole(Personnel pers)
        {
                var userdetails = db.personnels.Where(a => a.Username == pers.Username && a.Password == pers.Password).FirstOrDefault();

                if (userdetails.Role == "personnel")
                {
                    return 1;

                }
                else if (userdetails.Role == "economique")
                {
                    return 0;

                }
                else
                {
                    return -1;
                }


            }

        public IActionResult error()
        {
            return View();
        }
    }
}

