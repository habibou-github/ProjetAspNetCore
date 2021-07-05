using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using migrationProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace migrationProject.Controllers
{
    public class OrdrePaimentKiloController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult OrdrePaimentKilo()
        {
            return View();
        }

        public ActionResult PrintOrdrePaimentKilo()
        {
            //string s0 = (string)Session["personnel"];
            //String s1 = (string)Session["mois"];
            //String s2 = (string)Session["annee"];

            //String s3 = (string)Session["ov"];
            //String s4 = (string)Session["bp"];

            string s0 = HttpContext.Session.GetString("personnel");
            string s1 = HttpContext.Session.GetString("mois");
            string s2 = HttpContext.Session.GetString("annee");
            string s3 = HttpContext.Session.GetString("ov");
            string s4 = HttpContext.Session.GetString("bp");

            int id = Convert.ToInt32(s0);
            int mois = Convert.ToInt32(s1);
            int annee = Convert.ToInt32(s2);

            int ov = Convert.ToInt32(s3);
            int bp = Convert.ToInt32(s4);

            DbContextIndimnite db = new DbContextIndimnite();
            Personnel p = db.personnels.Find(id);

            int art = 0;
            int and = 0;
            int lig = 0;

            char nomPersonnel = p.Nom[0];
            string prenomPersonnel = p.Prenom;

            if (p.Role.ToString() == "Personnel")
            {
                Parametre_paiement pm = db.param_paiement.Find(3);
                art = pm.Art;
                and = pm.num1;
                lig = pm.num2;
            }

            else if (p.Role == "Professeur")
            {
                Parametre_paiement pm = db.param_paiement.Find(5);
                art = pm.Art;
                and = pm.num1;
                lig = pm.num2;
            }

            ViewBag.ART = art;
            ViewBag.and = and;
            ViewBag.Lig = lig;

            ViewBag.ov = ov;
            ViewBag.bp = bp;

            ViewBag.nomPersonnel = nomPersonnel;
            ViewBag.prenomPersonnel = prenomPersonnel;
            return new Rotativa.AspNetCore.ViewAsPdf("OrdrePaimentKilo");
        }
    }
}
