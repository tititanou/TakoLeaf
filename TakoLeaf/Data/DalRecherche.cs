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

        public List<Adherent> RechercheAdherent(string choix, int code, string nom, string prenom, int competence, string ressource, string input)
        {
            List<Adherent> adherents = new List<Adherent>();
            if (code != 0)
            {
                if (choix.Equals("Un utilisateur"))
                {
                    if (nom != null && !nom.Equals(""))
                    {
                        if (prenom != null && !prenom.Equals(""))
                        {
                            adherents = this._bddContext.Adherents.Where(a => a.Nom.Equals(nom) && a.Prenom.Equals(prenom) && a.Adresse.CodePostal == code).ToList();
                        }
                        else
                        {
                            adherents = this._bddContext.Adherents.Where(a => a.Nom.Equals(nom) && a.Adresse.CodePostal == code).ToList();
                        }
                    }
                    else if (nom == null || nom.Equals(""))
                    {
                        if (prenom != null && !prenom.Equals(""))
                        {
                            adherents = this._bddContext.Adherents.Where(a => a.Prenom.Equals(prenom) && a.Adresse.CodePostal == code).ToList();
                        }
                        else
                        {
                            adherents = this._bddContext.Adherents.Where(a => a.Adresse.CodePostal == code).ToList();
                        }
                    }

                }
                else
                {
                    if (competence != 0)
                    {
                        SsCateCompetence ssCateCompetence = this._bddContext.SsCateCompetences.FirstOrDefault(c => c.Id == competence);
                        if (this._bddContext.Competences.Where(c => c.Id == ssCateCompetence.Id && c.Provider.Adherent.Adresse.CodePostal == code).ToList() != null)
                        {
                            List<Competence> competences = this._bddContext.Competences.Where(c => c.Id == ssCateCompetence.Id && c.Provider.Adherent.Adresse.CodePostal == code).ToList();
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
                    else if (ressource != null)
                    {
                        if (input != null || !input.Equals(""))
                        {
                            List<Ressource> ressources = this._bddContext.Ressources.Include(r => r.Adresse).Where(r => r.Categorie == (CateRessource)Enum.Parse(typeof(CateRessource), ressource) && r.Intitule.ToLower().Contains(input.ToLower()) && r.Adresse.CodePostal == code).ToList();
                            foreach (Ressource ress in ressources)
                            {
                                Provider provider = this._bddContext.Providers.FirstOrDefault(p => p.Id == ress.ProviderId);
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
                        else
                        {
                            List<Ressource> ressources = this._bddContext.Ressources.Where(r => r.Categorie == (CateRessource)Enum.Parse(typeof(CateRessource), ressource) && r.Adresse.CodePostal == code).ToList();
                            foreach (Ressource ress in ressources)
                            {
                                Provider provider = this._bddContext.Providers.FirstOrDefault(p => p.Id == ress.ProviderId);
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
                    
                }
            }
            else
            {
                if (nom != null && !nom.Equals(""))
                {
                    if (prenom != null && !prenom.Equals(""))
                    {
                        adherents = this._bddContext.Adherents.Where(a => a.Nom.Equals(nom) && a.Prenom.Equals(prenom)).ToList();
                    }
                    else
                    {
                        adherents = this._bddContext.Adherents.Where(a => a.Nom.Equals(nom)).ToList();
                    }
                }
                else if ((nom == null || nom.Equals("")) && (prenom != null && !prenom.Equals("")))
                {
                    adherents = this._bddContext.Adherents.Where(a => a.Prenom.Equals(prenom)).ToList();
                }
                else if (competence != 0)
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
                else if (ressource != null)
                {
                    if(input != null)
                    {
                        if (!input.Equals(""))
                        {
                            List<Ressource> ressources = this._bddContext.Ressources.Where(r => r.Categorie == (CateRessource)Enum.Parse(typeof(CateRessource),ressource) && r.Intitule.ToLower().Contains(input.ToLower())).ToList();
                            foreach (Ressource ress in ressources)
                            {
                                Provider provider = this._bddContext.Providers.FirstOrDefault(p => p.Id == ress.ProviderId);
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
                        List<Ressource> ressources = this._bddContext.Ressources.Where(r => r.Categorie == (CateRessource)Enum.Parse(typeof(CateRessource), ressource)).ToList();
                        foreach (Ressource ress in ressources)
                        {
                            Provider provider = this._bddContext.Providers.FirstOrDefault(p => p.Id == ress.ProviderId);
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
            }
            if(adherents.Count() == 0)
            {
                adherents = this._bddContext.Adherents.ToList();
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

        public CompteUser GetCompteUser(int id)
        {
            return this._bddContext.CompteUsers.Include(u => u.Adherent).ThenInclude(a => a.Adresse).FirstOrDefault(u => u.AdherentId == id);
        }
    }
}
