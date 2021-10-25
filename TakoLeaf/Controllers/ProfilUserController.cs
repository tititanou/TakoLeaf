﻿using Microsoft.AspNetCore.Mvc;
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

        public IActionResult ProfilConsumer(int id)
        {
            Adherent adherent = dal.ObtenirAdherents().FirstOrDefault(a => a.Id == id);
            CompteUser compteUser = dal.ObtenirCompteUser().FirstOrDefault(c => c.AdherentId == id);
            Consumer consumer = dal.ObtenirConsumers().FirstOrDefault(c => c.AdherentId == id);
            Voiture voiture = dal.ObtenirVoiture().FirstOrDefault(v => v.ConsumerId == consumer.Id);
            int idcarte = consumer.CarteId;
            Carte carte = dal.ObtenirCartes().FirstOrDefault(c => c.Id == idcarte);
            int idmodele = voiture.ModeleId;
            Modele modele = dal.ObtenirModeles().FirstOrDefault(m => m.Id == idmodele);
            UtilisateurViewModel uvm = new UtilisateurViewModel { Adherent = adherent, CompteUser = compteUser, Voiture = voiture, Carte = carte, Consumer = consumer, Modele = modele };

            return View(uvm);
        }

        public IActionResult ProfilProvider(int id)
        {
            Adherent adherent = dal.ObtenirAdherents().FirstOrDefault(a => a.Id == id);
            CompteUser compteUser = dal.ObtenirCompteUser().FirstOrDefault(c => c.AdherentId == id);
            Provider provider = dal.ObtenirProviders().FirstOrDefault(p => p.AdherentId == id);
            List<Competence> competence = dal.ObtenirCompetences().Where(c => c.ProviderId == provider.Id).ToList();


            ProviderViewModel pvm = new ProviderViewModel { Adherent = adherent, CompteUser = compteUser, Competence = competence, Provider = provider };

            return View(pvm);
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
                CompteUser compteUser = dal.ObtenirCompteUser().Where(c => c.AdherentId == id).FirstOrDefault();
                if(compteUser.Role.Equals("Consumer"))
                {
                    return Redirect("/ProfilUser/ProfilConsumer?id=" + id);
                }
                
                else if (compteUser.Role.Equals("Provider"))
                {
                    return Redirect("/ProfilUser/ProfilProvider?id=" + id);
                }

                return View();
               
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
                Adherent adherent = dal.ObtenirAdherents().Where(a => a.Id == id).FirstOrDefault();
                if (compteUser.Role.Equals("Consumer"))
                {
                    return Redirect("/ProfilUser/ProfilConsumer?id=" + id);
                }

                else if (compteUser.Role.Equals("Provider"))
                {
                    return Redirect("/ProfilUser/ProfilProvider?id=" + id);
                }

                return View();
            }
            
        }

        //TODO faire la methode et voir sur la vue InscriptionProvider pour ajouter les Competences

    }   
}
