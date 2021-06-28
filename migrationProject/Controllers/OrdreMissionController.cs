using Microsoft.AspNetCore.Mvc;
using migrationProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace migrationProject.Controllers
{
    public class OrdreMissionController : Controller
    {
        private readonly DbContextIndimnite _context;

        public OrdreMissionController(DbContextIndimnite context)
        {
            _context = context;
        }

        public IActionResult OrdreMissionList()
        {
            var ordreMissions = _context.ordremission.ToList();

            return View(ordreMissions);
        }

        public IActionResult OrdreMissionInfo()
        {
            
            ViewBag.P = _context.personnels.ToList();
            ViewBag.T = _context.trajet.ToList();

            return View(new OrdreMission { idOrdremission = 0 });
        }

        public ActionResult SaveOrdreMission(OrdreMission om)
        {
            ViewBag.P = _context.personnels.ToList();
            ViewBag.T = _context.trajet.ToList();
            int IdPers = Convert.ToInt32(Request.Form["IdPers"]);
            int id_trajet = Convert.ToInt32(Request.Form["id_trajet"]);

            if (!ModelState.IsValid)
                return View("OrdreMissionInfo", om);

            if (om.idOrdremission == 0) { 
                ViewBag.create = 0;
                _context.ordremission.Add(om);
            }
            else
            {
                ViewBag.create = 1;
                var omFormDb = _context.ordremission.FirstOrDefault(o => o.idOrdremission == om.idOrdremission);
                if (omFormDb == null)
                    return NotFound();



                omFormDb.numero = om.numero;
                omFormDb.etat = om.etat;
                omFormDb.name_respo_mission = om.name_respo_mission;
                omFormDb.dateDepart = om.dateDepart;
                omFormDb.dateArrivee = om.dateArrivee;
                omFormDb.heureDepart = om.heureDepart;
                omFormDb.heureArrivee = om.heureArrivee;
                omFormDb.echlon = om.echlon;
                omFormDb.matricule = om.matricule;
                omFormDb.grade = om.grade;
                omFormDb.objetDepart = om.objetDepart;
                omFormDb.moyenTransport = om.moyenTransport;
                omFormDb.nombreCheuvaux = om.nombreCheuvaux;
                omFormDb.montant_total = om.montant_total;
                omFormDb.IdPers = IdPers;
                omFormDb.id_trajet = id_trajet;
            }

            _context.SaveChanges();
            return RedirectToAction("OrdreMissionList");
        }


        public IActionResult Edit(int? id)
        {
            ViewBag.P = _context.personnels.ToList();
            ViewBag.T = _context.trajet.ToList();
            ViewBag.create = 1;
            if (id == null)
                return NotFound();

            var om = _context.ordremission.FirstOrDefault(o => o.idOrdremission == id);
            om.idOrdremission = (int)id;



            return View("OrdreMissionInfo", om);
        }

        public IActionResult Document(int? id)
        {
            var om = _context.ordremission.FirstOrDefault(o => o.idOrdremission == id);

            ViewBag.grad = om.grade;
            ViewBag.objetDepart = om.objetDepart;
            ViewBag.dateDepart = om.dateDepart + " " + om.heureDepart;
            ViewBag.dateArrivee = om.dateArrivee + " " + om.heureArrivee;
            ViewBag.moyenTransport = om.moyenTransport;
            ViewBag.matricule = om.matricule;
            var personne = _context.personnels.FirstOrDefault(p => p.IdPers == om.IdPers);

            ViewBag.nom = personne.Nom;
            ViewBag.prenom = personne.Prenom;
            ViewBag.nomarab = personne.nomarabe;
            ViewBag.prenomarab = personne.prenomarabe;
            ViewBag.role = personne.Role;

            string na = personne.nomarabe;
            char[] charArrayna = na.ToCharArray();
            Array.Reverse(charArrayna);
            ViewBag.nomarabe = new string(charArrayna);

            string pa = personne.prenomarabe;
            char[] charArraypa = pa.ToCharArray();
            Array.Reverse(charArraypa);
            ViewBag.orenomarabe = new string(charArraypa);

            string mmme = " طلب للسيد / للسيدة: ";
            char[] charArraymmme = mmme.ToCharArray();
            Array.Reverse(charArraymmme);
            ViewBag.mmme = new string(charArraymmme);

            string s = "المطلوب من رجال السلطة أن يقدموا معونتهم ومساعداتهم لحامل هذا الأمر بالمهمة";
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            ViewBag.ss = new string(charArray);

            ViewBag.currentTime = DateTime.Now;
            return View(om);
        }


        public IActionResult Delete(int? id)
        {

            if (id == null)
                return NotFound();

            var om = _context.ordremission.FirstOrDefault(o => o.idOrdremission == id);

            _context.ordremission.Remove(om);
            _context.SaveChanges();

            return RedirectToAction("OrdreMissionList");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
