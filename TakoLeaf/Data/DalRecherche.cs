using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TakoLeaf.Models;

namespace TakoLeaf.Data
{
    public class DalRecherche : IDalRecherche
    {
        private BddContext _bddContext;

        public DalRecherche()
        {
            this._bddContext = new BddContext();
        }

        public void Dispose()
        {
            this._bddContext.Dispose();
        }

        public bool EstAmi(int id1, int id2)
        {
            DalProfil dalProfil = new DalProfil();
            List<Amitie> amities = dalProfil.ObtenirAmities().Where(a => a.AdherentCourantId == id2).ToList();
            bool res = false;
            foreach (var item in amities)
            {
                if (item.AdherentAmiId == id1)
                {
                    res = true;
                    break;
                }

                else
                {
                    res = false;
                }
            }

            return res;
        }

        public List<Adresse> GetAllAdresses()
        {
            return this._bddContext.Adresses.ToList();
        }

        public List<Adherent> RechercheAdherent(int code, string nom, string prenom, int competence)
        {
            List<Adherent> adherents = new List<Adherent>();
            if (nom != null && prenom != null)
            {
                adherents = this._bddContext.Adherents.Where(a => a.Nom.Equals(nom) && a.Prenom.Equals(prenom)).ToList();
            }
            else if(competence != null || !competence.Equals(""))
            {
                SsCateCompetence ssCateCompetence = this._bddContext.SsCateCompetences.FirstOrDefault(c => c.Id == competence);
                if (this._bddContext.Competences.Where(c => c.Id == ssCateCompetence.Id).ToList() != null)
                {
                    List<Competence> competences = this._bddContext.Competences.Where(c => c.Id == ssCateCompetence.Id).ToList();
                    foreach (Competence comp in competences)
                    {
                        Provider provider = this._bddContext.Providers.FirstOrDefault(p => p.Id == comp.ProviderId);
                        Adherent adherent = this._bddContext.Adherents.FirstOrDefault(a => a.Id == provider.AdherentId);
                        if (adherents.Count == 0)
                        {
                            adherents.Add(adherent);
                        }
                        else
                        {
                            if (!adherents.Contains(adherent))
                            {
                                adherents.Add(adherent);
                            }
                        }
                    }
                }
            }
            else
            {
                adherents = null;
            }
            return adherents;
        }

        public Dictionary<string, List<SsCateCompetence>> RechercheCompetence()
        {
            List<SsCateCompetence> ssCates = this._bddContext.SsCateCompetences.Include(x => x.CateCompetence).ToList();
            ssCates.Sort((x, y) => x.CateCompetence.Intitule.CompareTo(y.CateCompetence.Intitule));
            Dictionary<string, List<SsCateCompetence>> Dico = new Dictionary<string, List<SsCateCompetence>>();
            foreach(var item in ssCates)
            {
                if (Dico.ContainsKey(item.CateCompetence.Intitule))
                {
                    Dico[item.CateCompetence.Intitule].Add(item);
                    Dico[item.CateCompetence.Intitule].Sort((x, y) => x.Intitule.CompareTo(y.Intitule));
                }
                else
                {
                    Dico[item.CateCompetence.Intitule] = new List<SsCateCompetence>() { item };
                }
            }
            return Dico;
        }

        public Dictionary<string, List<int>> RechercheCodePostal()
        {
            List<Adresse> adresses = this._bddContext.Adresses.ToList();
            adresses.Sort((x, y) => x.Departement.CompareTo(y.Departement));
            Dictionary<string, List<int>> Dico = new Dictionary<string, List<int>>();
            foreach (var item in adresses)
            {
                if (Dico.ContainsKey(item.Departement.ToString()))
                {
                    if (!Dico[item.Departement.ToString()].Contains(item.CodePostal))
                    {
                        Dico[item.Departement.ToString()].Add(item.CodePostal);
                        Dico[item.Departement.ToString()].Sort();
                    }
                }
                else
                {
                    Dico[item.Departement.ToString()] = new List<int>() { item.CodePostal };
                }
            }
            return Dico;
        }
    }
}
