using Microsoft.AspNetCore.Mvc;
using migrationProject.Models;
using Rotativa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace migrationProject.Controllers
{
    public class TabKiloController1 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private readonly DbContextIndimnite db;

        public TabKiloController1(DbContextIndimnite context)
        {
            db = context;
        }




        public IActionResult choixKilometrage()
        {
            //var mylist = new List<OrdreMission>();

            //var list = db.ordremission.AsEnumerable().GroupBy(p => p.personel.IdPers);
            //foreach (var result in list)
            //{
            //    mylist.Add(result.ToList().ElementAt(0));
            //}
            //List<Personnel> list = new List<Personnel>();
            //var x = from p in db.personnels select new { p.Nom, p.Prenom };
            //foreach (var item in x)
            //{
            //    list.Add(new Personnel(item.Nom, item.Prenom));
            //}
            //ViewBag.list = list;
            return View(db.ordremission.ToList());
        }
        public IActionResult Kilo()
        {
            string nomP = Request.Form["NomPersonel"].ToString();
            string anneeOrdre = Request.Form["annee"].ToString();
            string moisOrdre = Request.Form["mois"].ToString();
            ViewBag.selectedValue = nomP;
            ViewBag.valueYear = anneeOrdre;
            ViewBag.valueMonth = moisOrdre;

            int nbAller = 0;
            int nbRetour = 0;
            double kms = 0;
            int year = Convert.ToInt32(anneeOrdre);
            int month = Convert.ToInt32(moisOrdre);
            List<OrdreMission> orders = db.ordremission.Where(s => s.dateDepart.Year == year && (s.dateDepart.Month == month || s.dateArrivee.Month == month) && s.personel.Nom
             == nomP).ToList();
            List<int> jours = new List<int>();
            List<GrilleKilo> listGrilleKilo = new List<GrilleKilo>();
            int id_trajet = 0;
            foreach (var order in orders)
            {
                ViewData["nom"] = nomP;
                ViewData["mois"] = moisOrdre;
                DateTime now = DateTime.Now;
                ViewData["date"] = now.ToString("MM/dd/yyyy");
                ViewData["taux"] = 1.2;
                if (order.dateDepart.Month == month && order.dateArrivee.Month != month)
                {
                    nbAller = nbAller + 1;
                    id_trajet = order.id_trajet;
                    Trajet t = db.trajet.Find(id_trajet);
                    kms = kms + t.distance;

                }
                else if (order.dateDepart.Month != month && order.dateArrivee.Month == month)
                {
                    nbRetour = nbRetour + 1;
                    id_trajet = order.id_trajet;
                    Trajet t = db.trajet.Find(id_trajet);
                    kms = kms + t.distance;
                }
                else if (order.dateDepart.Month == month && order.dateArrivee.Month == month)
                {
                    nbAller = nbAller + 1;
                    nbRetour = nbRetour + 1;
                    id_trajet = order.id_trajet;
                    Trajet t = db.trajet.Find(id_trajet);
                    kms = kms + t.distance;
                }
                ///test nb cheuveux
                if (order.nombreCheuvaux > 6 && order.nombreCheuvaux < 10)
                {
                    ViewData["taux"] = 1.75;
                }
                else if (order.nombreCheuvaux > 10)
                {
                    ViewData["taux"] = 2.30;
                }
                ViewData["kms"] = kms;
                @ViewData["aller"] = nbAller;
                @ViewData["retour"] = nbRetour;
                ViewData["total"] = (double)ViewData["taux"] * (int)kms * (nbAller + nbRetour);
                jours.Add(order.dateDepart.Day);
            }
            //remplir la grille
            foreach (var order in orders)
            {
                id_trajet = order.id_trajet;
                Trajet t = db.trajet.Find(id_trajet);
                if (order.dateDepart.Month == month && order.dateArrivee.Month != month)
                {
                    listGrilleKilo.Add(new GrilleKilo(order.dateDepart.Day, t.villeDepart, t.villeArrivee, order.heureDepart, t.distance));
                    jours.Add(order.dateDepart.Day);
                }
                else if (order.dateDepart.Month != month && order.dateArrivee.Month == month)
                {
                    listGrilleKilo.Add(new GrilleKilo(order.dateArrivee.Day, t.villeArrivee, t.villeDepart, order.heureArrivee, t.distance));
                    jours.Add(order.dateArrivee.Day);
                }
                else if (order.dateDepart.Month == month && order.dateArrivee.Month == month)
                {
                    listGrilleKilo.Add(new GrilleKilo(order.dateDepart.Day, t.villeDepart, t.villeArrivee, order.heureDepart, t.distance));
                    listGrilleKilo.Add(new GrilleKilo(order.dateArrivee.Day, t.villeArrivee, t.villeDepart, order.heureArrivee, t.distance));
                    jours.Add(order.dateDepart.Day);
                    jours.Add(order.dateArrivee.Day);
                }
            }
            jours.Sort();

            listGrilleKilo.Sort();
            ViewBag.list = listGrilleKilo;
            ViewData["size"] = listGrilleKilo.Count();
            ViewData["jours"] = jours;
            ViewData["size1"] = jours.Count();
            if (orders.Count() > 0)
            {
                OrdreMission ordreMession = orders[0];

                var pdf = new ViewAsPdf(ordreMession, ViewData);
                return pdf;
            }
            else
            {

                var pdf = new ViewAsPdf("Kilo");
                return pdf;
            }
        }


        // calcule repas soir

        // calcule Taux

        public List<Boolean> verifierRepasCasDepart(OrdreMission ordre)
        {
            List<Boolean> resultComparaison = new List<Boolean>();
            DateTime HeureDepart = DateTime.Parse(ordre.heureDepart, System.Globalization.CultureInfo.CurrentCulture);

            DateTime HeureConditionTime14 = DateTime.Parse("14:00", System.Globalization.CultureInfo.CurrentCulture);
            DateTime HeureConditionTime21 = DateTime.Parse("21:00", System.Globalization.CultureInfo.CurrentCulture);

            int resultTime14 = DateTime.Compare(HeureDepart, HeureConditionTime14);
            int resultTime21 = DateTime.Compare(HeureDepart, HeureConditionTime21);

            // n'a pas de repas dejeuner et de diner
            if (resultTime21 > 0)
            {
                resultComparaison.Add(false);
                resultComparaison.Add(false);
            }
            // il a les deux repas
            else if (resultTime14 <= 0)
            {
                resultComparaison.Add(true);
                resultComparaison.Add(true);
            }
            // il n'a pas de dejeuner et il a le diner
            else if (resultTime14 >= 0)
            {
                resultComparaison.Add(false);
                resultComparaison.Add(true);
            }



            return resultComparaison;
        }

        // verifier Repas
        public List<Boolean> VerifierRepas(int jour, OrdreMission ordre, int mounth)
        {
            List<Boolean> resultat = new List<Boolean>();
            List<Boolean> resultat2 = new List<Boolean>();

            if (jour != ordre.dateDepart.Day && jour != (ordre.dateArrivee.Day - 1))
            {
                resultat.Add(true);
                resultat.Add(true);
            }
            if (jour == ordre.dateDepart.Day)
            {
                resultat2 = verifierRepasCasDepart(ordre);
                resultat.Add(resultat2[0]);
                resultat.Add(resultat2[1]);
            }
            if (jour == ordre.dateArrivee.Day - 1)
            {
                resultat2 = verifierRepasCasArrivee(ordre);
                resultat.Add(resultat2[0]);
                resultat.Add(resultat2[1]);
            }

            return resultat;
        }
        public List<Boolean> verifierRepasCasArrivee(OrdreMission ordre)
        {
            List<Boolean> resultComparaison = new List<Boolean>();
            DateTime HeurArrivee = DateTime.Parse(ordre.heureArrivee, System.Globalization.CultureInfo.CurrentCulture);

            DateTime HeureConditionTime11 = DateTime.Parse("11:00", System.Globalization.CultureInfo.CurrentCulture);
            DateTime HeureConditionTime14 = DateTime.Parse("14:00", System.Globalization.CultureInfo.CurrentCulture);
            DateTime HeureConditionTime19 = DateTime.Parse("19:00", System.Globalization.CultureInfo.CurrentCulture);
            int resultTime11 = DateTime.Compare(HeurArrivee, HeureConditionTime11);
            int resultTime14 = DateTime.Compare(HeurArrivee, HeureConditionTime14);
            int resultTime19 = DateTime.Compare(HeurArrivee, HeureConditionTime19);
            // n'a pas de repas dejeuner et de diner
            if (resultTime11 <= 0)
            {
                resultComparaison.Add(false);
                resultComparaison.Add(false);
            }
            // il a les deux repas
            if (resultTime19 >= 0)
            {
                resultComparaison.Add(true);
                resultComparaison.Add(true);
            }
            // il n'a pas de dejeuner et il a le diner
            if (resultTime19 < 0 && resultTime14 > 0)
            {
                resultComparaison.Add(true);
                resultComparaison.Add(false);
            }

            return resultComparaison;
        }


    }
}
