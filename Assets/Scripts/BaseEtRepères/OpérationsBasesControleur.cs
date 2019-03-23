using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Le différentes opérations à faire pour les bases
/// </summary>
enum VérificationBase { TEST_PLAN, TEST_ESPACE, ÉCRITURE_VECTEUR_PLAN, ÉCRITURE_VECTEUR_ESPACE };

public class OpérationsBasesControleur : OperationsControleurAbstrait{
    public GameObject ÉditeurVecteurGauche;
    public GameObject ÉditeurVecteurCentre;
    public GameObject ÉditeurVecteurDroite;
    public GameObject ÉditeurVecteurBas;
    public GameObject ZoneVérifPlan;
    public GameObject ZoneVérifEspace;
    public GameObject ZoneÉcritureBase;
    Dropdown listOpérations;
    Button calculer;
    VecteurControleur scriptVecteurGauche;
    VecteurControleur scriptVecteurCentre;
    VecteurControleur scriptVecteurDroite;
    VecteurControleur scriptVecteurBas;
    GradateurControleur scriptGradateurGauche;
    GradateurControleur scriptGradateurDroit;
    GradateurControleur scriptGradateurCentre;
    GradateurControleur scriptGradateurBas;
    RésultatControleurBase scriptRésultatPlan;
    RésultatControleurBase scriptRésultatEspace;
    RésultatControleurVecteur scriptRésultatVecteur;
    VérificationBase typeVérif;

    /// <summary>
    /// Récupère les différents composants nécesaires à l'initialisation
    /// </summary>
    void Start()
    {
        calculer = gameObject.GetComponentInChildren<Button>();
       
        scriptVecteurGauche = ÉditeurVecteurGauche.GetComponent<VecteurControleur>();
        scriptVecteurCentre = ÉditeurVecteurCentre.GetComponent<VecteurControleur>();
        scriptVecteurDroite = ÉditeurVecteurDroite.GetComponent<VecteurControleur>();
        scriptVecteurBas = ÉditeurVecteurBas.GetComponent<VecteurControleur>();

        scriptGradateurDroit = scriptVecteurDroite.CtrlNbDimensions.GetComponent<GradateurControleur>();
        scriptGradateurGauche = scriptVecteurGauche.CtrlNbDimensions.GetComponent<GradateurControleur>();
        scriptGradateurCentre = scriptVecteurCentre.CtrlNbDimensions.GetComponent<GradateurControleur>();
        scriptGradateurBas = scriptVecteurBas.CtrlNbDimensions.GetComponent<GradateurControleur>();

        scriptRésultatPlan = ZoneVérifPlan.GetComponent<RésultatControleurBase>();
        scriptRésultatEspace = ZoneVérifEspace.GetComponent<RésultatControleurBase>();
        scriptRésultatVecteur = ZoneÉcritureBase.GetComponent<RésultatControleurVecteur>();
        listOpérations = gameObject.GetComponentInChildren<Dropdown>();
        IdentifierOpérationActive();
    }

    /// <summary>
    /// Identifie l'opération du dropdown et active les différents composants utile à l'opération
    /// </summary>
    public override void IdentifierOpérationActive()
    {
        typeVérif = (VérificationBase)listOpérations.value;
        switch (typeVérif)
        {
            case VérificationBase.TEST_PLAN:
                ÉditeurVecteurDroite.SetActive(false);
                ÉditeurVecteurCentre.SetActive(true);
                ÉditeurVecteurGauche.SetActive(true);
                ÉditeurVecteurBas.SetActive(false);
                ZoneVérifPlan.SetActive(true);
                ZoneÉcritureBase.SetActive(false);
                ZoneVérifEspace.SetActive(false);
                if (scriptGradateurGauche.Valeur == 2 && scriptGradateurCentre.Valeur == 2)
                {
                    calculer.interactable = true;
                }
                else
                {
                    calculer.interactable = false;
                }
                break;
            case VérificationBase.TEST_ESPACE:
                ÉditeurVecteurDroite.SetActive(true);
                ÉditeurVecteurCentre.SetActive(true);
                ÉditeurVecteurGauche.SetActive(true);
                ÉditeurVecteurBas.SetActive(false);
                ZoneVérifPlan.SetActive(false);
                ZoneÉcritureBase.SetActive(false);
                ZoneVérifEspace.SetActive(true);
                if (scriptGradateurGauche.Valeur == 3 && scriptGradateurCentre.Valeur == 3 && scriptGradateurDroit.Valeur == 3)
                {
                    calculer.interactable = true;
                }
                else
                {
                    calculer.interactable = false;
                }
                break;
            case VérificationBase.ÉCRITURE_VECTEUR_PLAN:
                ÉditeurVecteurDroite.SetActive(false);
                ÉditeurVecteurCentre.SetActive(true);
                ÉditeurVecteurGauche.SetActive(true);
                ÉditeurVecteurBas.SetActive(true);
                ZoneVérifPlan.SetActive(false);
                ZoneÉcritureBase.SetActive(true);
                ZoneVérifEspace.SetActive(false);
                if (scriptGradateurGauche.Valeur == 2 && scriptGradateurCentre.Valeur == 2 && scriptGradateurBas.Valeur == 2)
                {
                    calculer.interactable = true;
                }
                else
                {
                    calculer.interactable = false;
                }
                break;
            case VérificationBase.ÉCRITURE_VECTEUR_ESPACE:
                ÉditeurVecteurDroite.SetActive(true);
                ÉditeurVecteurCentre.SetActive(true);
                ÉditeurVecteurGauche.SetActive(true);
                ÉditeurVecteurBas.SetActive(true);
                ZoneVérifPlan.SetActive(false);
                ZoneÉcritureBase.SetActive(true);
                ZoneVérifEspace.SetActive(false);
                if (scriptGradateurGauche.Valeur == 3 && scriptGradateurCentre.Valeur == 3 && scriptGradateurDroit.Valeur 
                    == 3 && scriptGradateurBas.Valeur == 3)
                {
                    calculer.interactable = true;
                }
                else
                {
                    calculer.interactable = false;
                }
                break;
        }
    }

    /// <summary>
    /// S'occupe de calculer les différentes opérations sur les plans et les bases
    /// </summary>
    public void Calculer()
    {
        bool EstPlanOuEspace;
        switch (typeVérif)
        {
            case VérificationBase.TEST_PLAN:
                EstPlanOuEspace = Vecteur.EstUnPlan(new Vecteur(scriptVecteurGauche.GetDataVecteur), new Vecteur(scriptVecteurCentre.GetDataVecteur));
                scriptRésultatPlan.AfficherRésultat(EstPlanOuEspace);
                break;

            case VérificationBase.TEST_ESPACE:
                EstPlanOuEspace = Vecteur.EstUnEspace(new Vecteur(scriptVecteurGauche.GetDataVecteur), new Vecteur(scriptVecteurCentre.GetDataVecteur), new Vecteur(scriptVecteurDroite.GetDataVecteur));
                scriptRésultatEspace.AfficherRésultat(EstPlanOuEspace);
                break;

            case VérificationBase.ÉCRITURE_VECTEUR_PLAN:
                Vecteur résultatPlan = Vecteur.ÉcritureVecteurDansUnPlan(new Vecteur(scriptVecteurGauche.GetDataVecteur), new Vecteur(scriptVecteurCentre.GetDataVecteur), new Vecteur(scriptVecteurBas.GetDataVecteur));
                scriptRésultatVecteur.AfficherRésultat(résultatPlan);
                break;
            case VérificationBase.ÉCRITURE_VECTEUR_ESPACE:
                Vecteur résultatEspace = Vecteur.ÉcritureVecteurDansUnEspace(new Vecteur(scriptVecteurGauche.GetDataVecteur), new Vecteur(scriptVecteurCentre.GetDataVecteur), new Vecteur(scriptVecteurDroite.GetDataVecteur), new Vecteur(scriptVecteurBas.GetDataVecteur));
                scriptRésultatVecteur.AfficherRésultat(résultatEspace);
                break;
        }

    }
    public void Update()
    {

       // IdentifierOpérationActive();

    }
}

