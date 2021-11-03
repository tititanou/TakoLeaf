using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TakoLeaf.Models;

namespace TakoLeaf.Data
{
    public class DalPrestation
    {
        private BddContext _bddContext;

        public DalPrestation()
        {
            this._bddContext = new BddContext();
        }

        public void Dispose()
        {
            this._bddContext.Dispose();
        }

        public void ChangerEtatPrestation(int id)
        {
            Prestation prestation =_bddContext.Prestations.Find(id);
            prestation.EtatPresta = Prestation.Etat.Valide;
            _bddContext.SaveChanges();
        }

      
        public void AjoutHistorique(Prestation prestation)
        {
            Adherent adherentC = _bddContext.Adherents.Find(prestation.Consumer.AdherentId);
            Adherent adherentP = _bddContext.Adherents.Find(prestation.Provider.AdherentId);
            Historique historiqueC = _bddContext.Historiques.FirstOrDefault(h => h.AdherentId == adherentC.Id);
            Historique historiqueP = _bddContext.Historiques.FirstOrDefault(h => h.AdherentId == adherentP.Id);

            HistoriquePresta histoC = new HistoriquePresta { HistoriqueId = historiqueC.Id, PrestationId = prestation.Id };
            _bddContext.HistoriquePrestas.Add(histoC);
            HistoriquePresta histoP = new HistoriquePresta { HistoriqueId = historiqueP.Id, PrestationId = prestation.Id };
            _bddContext.HistoriquePrestas.Add(histoP);
            _bddContext.SaveChanges();


        }

        public void CreationAvis(int idC, int idP, double note, string contenu, int idPresta)
        {
            Avis avis = new Avis { ConsumerId = idC, ProviderId = idP, Note = note, Contenu = contenu, PrestationId = idPresta, DateCreation = DateTime.Now };
            _bddContext.Avis.Add(avis);
            _bddContext.SaveChanges();
            Provider provider = _bddContext.Providers.Find(idP);
            List<HistoriquePresta> historique = _bddContext.HistoriquePrestas.Where(p => p.HistoriqueId == provider.AdherentId && p.Prestation.EtatPresta == Prestation.Etat.Valide).ToList();
            int nbr = historique.Count;
            DalProfil dalProfil = new DalProfil();
            List<Avis> Avis = dalProfil.ObtenirAvis();
            double noteP = 0;
            foreach(Avis item in Avis)
            {
                noteP = noteP + item.Note;
            }
            provider.Note = noteP / Avis.Count ;
            _bddContext.SaveChanges();
        }

    }
}
