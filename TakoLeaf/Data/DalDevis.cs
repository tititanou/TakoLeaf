using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TakoLeaf.Models;

namespace TakoLeaf.Data
{
    public class DalDevis : IdalDevis
    {

        private BddContext _bddContext;

        public DalDevis()
        {
            this._bddContext = new BddContext();
        }

        public void Dispose()
        {
            this._bddContext.Dispose();
        }

        public DemandeDevis CreationDemandeDevis(int idC, int idP, int idV, DateTime dateDemande, DateTime dateVoulue, string message)
        {
            DemandeDevis demandeDevis = new DemandeDevis { ConsumerId = idC, ProviderId = idP, VoitureId = idV, DateDemande = dateDemande, DateDebutVoulue = dateVoulue, Message = message };
            _bddContext.DemandeDevis.Add(demandeDevis);
            _bddContext.SaveChanges();
            return demandeDevis;
        }
        public void CreationListeDevisCompetence (int idC, int idDemandeDevis )
        {
            DemandeDevisListeCompetence ddlc = new DemandeDevisListeCompetence { CompetenceId = idC, DemandeDevisId = idDemandeDevis };
            _bddContext.DemandesDevisListeCompetence.Add(ddlc);
            _bddContext.SaveChanges();

        }

        public void CreationListeDevisRessource(int idR, int idDemandeDevis)
        {
            DemandeDevisListeRessource ddl = new DemandeDevisListeRessource { RessourceId = idR, DemandeDevisId = idDemandeDevis };
            _bddContext.DemandesDevisListeRessource.Add(ddl);
            _bddContext.SaveChanges();

        }

        public void CreationDevis(int idP, int idC, int idV, int iDe, DateTime dateEmi, DateTime dateDebut, DateTime datefin, double prix, string description, int idAdresse)
        {
            Random random = new Random();
            string chars = "AZERTYUIOPMLKJHGFDSQWXCVBN123456789";
            var stringChars = new char[8];

            for(int i =0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            string finalstring = new string(stringChars);

            Devis devis = new Devis {
                NumeroDevis = finalstring,
                ProviderId = idP,
                ConsumerId = idC,
                VoitureId = idV,
                DemandeDevisId = iDe,
                DateEmission = dateEmi,
                DateDebut = dateDebut,
                DateFin = datefin,
                Tarif = prix,
                DescriptionPresta = description,
                LieuPrestaId = idAdresse,
                EtatDevis = EtatDevis.EN_ATTENTE,               
            };
            _bddContext.Devis.Add(devis);
            _bddContext.SaveChanges();
        }

        public void CreationPrestation(Devis devis)
        {

            Prestation prestation = new Prestation
            {
                ConsumerId = devis.ConsumerId,
                DateDebut = devis.DateDebut,
                EtatPresta = Prestation.Etat.En_cours,
                NumeroDevis = devis.NumeroDevis,
                Prix = devis.Tarif,
                ProviderId = devis.ProviderId,
                VoitureId = devis.VoitureId

            };

            _bddContext.Prestations.Add(prestation);
            _bddContext.SaveChanges();
        }

        public void CreationPrestationRefusee(Devis devis)
        {
            Prestation prestation = new Prestation
            {
                ConsumerId = devis.ConsumerId,
                DateDebut = devis.DateDebut,
                EtatPresta = Prestation.Etat.Refuse,
                NumeroDevis = devis.NumeroDevis,
                Prix = devis.Tarif,
                ProviderId = devis.ProviderId,
                VoitureId = devis.VoitureId

            };


            _bddContext.Prestations.Add(prestation);
            _bddContext.SaveChanges();
        }
        
    }
}
