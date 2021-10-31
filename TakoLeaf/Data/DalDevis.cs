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
        
    }
}
