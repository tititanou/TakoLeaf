using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TakoLeaf.Data;
using TakoLeaf.Models;
using TakoLeaf.ViewModels;

namespace TakoLeaf.Controllers
{
    public class ProfilUserController : Controller
    {
        private IdalProfil dal;

        public ProfilUserController()
        {
            this.dal = new DalProfil();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Profil(int id)
        {
            Adherent adherent = dal.ObtenirAdherents().FirstOrDefault(a => a.Id == id);
            CompteUser compteUser = dal.ObtenirCompteUser().FirstOrDefault(c => c.AdherentId == id);

            UtilisateurViewModel uvm = new UtilisateurViewModel { Adherent = adherent, CompteUser = compteUser };

            return View(uvm);
        }

        public IActionResult ModifInfosAdherent(int id)
        {
            Adherent adherent = dal.ObtenirAdherents().FirstOrDefault(a => a.Id == id);
            return View(adherent);
        }

        [HttpPost]
        public IActionResult ModifInfosAdherent(Adherent adherent)
        {
            using(DalProfil dal = new DalProfil())
            {
                dal.ModifierInfosAdherent(adherent.Id, adherent.Nom, adherent.Prenom, adherent.Date_naissance, adherent.Adresse, adherent.Telephone);
                int id = adherent.Id;
                return Redirect("/ProfilUser/Profil?id="+ id);
            }
            
        }

        public IActionResult ModifCompte(int id)
        {
            CompteUser compteUser = dal.ObtenirCompteUser().FirstOrDefault(c => c.AdherentId == id);
            return View(compteUser);
        }

        [HttpPost]
        public IActionResult ModifCompte(CompteUser compteUser)
        {
            using(IdalProfil dal = new DalProfil())
            {
                dal.ModifierCompteUser(compteUser.Mail, compteUser.MotDePasse, compteUser.Avatar, compteUser.Description);
                int id = compteUser.AdherentId;
                return View("/ProfilUser/Profil?id=" + id);
            }
            
        }

    }   
}
