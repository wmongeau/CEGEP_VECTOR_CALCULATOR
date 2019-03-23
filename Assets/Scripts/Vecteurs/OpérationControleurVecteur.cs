using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Les différentes opérations possible à faire sur un vecteur
/// </summary>
enum OpérationsVectorielles { ADDITON, SOUSTRACTION, PRODUIT_SCALAIRE, CALCUL_NORME, NORMALISATION, VECTEUR_RÉSULTANT };

/// <summary>
/// S'occupe du déroulement des opérations sur les vecteurs
/// </summary>
public class OpérationControleurVecteur : OperationsControleurAbstrait
{
    public GameObject EditeurVecteurGauche;
    public GameObject EditeurVecteurDroite;
    public GameObject EditeurScalaire;
    public GameObject ZoneResultatVectoriel;
    public GameObject ZoneNorme;
    VecteurControleur scriptVecteurGauche;
    VecteurControleur scriptVecteurDroite;
    ScalaireControleur scriptScalaire;
    RésultatControleurVecteur scriptResultatVecteur;
    RésultatControleurNorme scriptRésultatNorme;
    GradateurControleur scriptGradateurGauche;
    GradateurControleur scriptGradateurDroit;
    Vecteur opérandeVecteurGauche;
    Dropdown listOpérations;
    Button calculer;
    OpérationsVectorielles opérationActive;

    /// <summary>
    /// Recherche l'information nécessaire sur les Game Objects
    /// </summary>
    void Start()
    {
        calculer = gameObject.GetComponentInChildren<Button>();

        scriptVecteurGauche = EditeurVecteurGauche.GetComponent<VecteurControleur>();
        scriptVecteurDroite = EditeurVecteurDroite.GetComponent<VecteurControleur>();
        scriptGradateurDroit = scriptVecteurDroite.CtrlNbDimensions.GetComponent<GradateurControleur>();
        scriptGradateurGauche = scriptVecteurGauche.CtrlNbDimensions.GetComponent<GradateurControleur>();
        scriptScalaire = EditeurScalaire.GetComponent<ScalaireControleur>();
        scriptResultatVecteur = ZoneResultatVectoriel.GetComponent<RésultatControleurVecteur>();
        scriptRésultatNorme = ZoneNorme.GetComponent<RésultatControleurNorme>();

        opérandeVecteurGauche = new Vecteur(scriptVecteurGauche.GetDataVecteur);
        listOpérations = gameObject.GetComponentInChildren<Dropdown>();
        IdentifierOpérationActive();
    }

    /// <summary>
    /// Identifie l'opération sur le dropdown et active les composants nécessaires à l'opération à faire
    /// </summary>
    public override void IdentifierOpérationActive()
    {
        opérationActive = (OpérationsVectorielles)listOpérations.value;
        switch (opérationActive)
        {
            case OpérationsVectorielles.ADDITON:
                EditeurScalaire.SetActive(false);
                EditeurVecteurDroite.SetActive(true);
                ZoneResultatVectoriel.SetActive(true);
                ZoneNorme.SetActive(false);
                if (scriptGradateurGauche.Valeur == scriptGradateurDroit.Valeur)
                {
                    calculer.interactable=true;
                }
                else
                {
                    calculer.interactable = false;
                }
                break;
            case OpérationsVectorielles.SOUSTRACTION:
                EditeurScalaire.SetActive(false);
                EditeurVecteurDroite.SetActive(true);
                ZoneResultatVectoriel.SetActive(true);
                ZoneNorme.SetActive(false);
                if (scriptGradateurGauche.Valeur == scriptGradateurDroit.Valeur)
                {
                    calculer.interactable = true;
                }
                else
                {
                    calculer.interactable = false;
                }
                break;
            case OpérationsVectorielles.PRODUIT_SCALAIRE:
                EditeurScalaire.SetActive(true);
                EditeurVecteurDroite.SetActive(false);
                ZoneResultatVectoriel.SetActive(true);
                ZoneNorme.SetActive(false);
                calculer.interactable = true;
                break;
            case OpérationsVectorielles.CALCUL_NORME:
                EditeurScalaire.SetActive(false);
                EditeurVecteurDroite.SetActive(false);
                ZoneResultatVectoriel.SetActive(false);
                ZoneNorme.SetActive(true);
                calculer.interactable = true;
                break;
            case OpérationsVectorielles.NORMALISATION:
                EditeurScalaire.SetActive(false);
                EditeurVecteurDroite.SetActive(false);
                ZoneResultatVectoriel.SetActive(true);
                ZoneNorme.SetActive(false);
                calculer.interactable = true;
                break;
            case OpérationsVectorielles.VECTEUR_RÉSULTANT:
                EditeurScalaire.SetActive(false);
                EditeurVecteurDroite.SetActive(true);
                ZoneResultatVectoriel.SetActive(true);
                ZoneNorme.SetActive(false);
                if (scriptGradateurGauche.Valeur == scriptGradateurDroit.Valeur)
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
    /// Fait le calcul sur le(s) vecteur(s) selon l'opération sélectionnée et affiche la valeur dans le vecteur réponse ou la norme
    /// </summary>
    public void Calculer()
    {
        Vecteur résultat = null;
        bool retourneVecteur = true;
        float résultatNorme = 0;
        switch (opérationActive)
        {
            case OpérationsVectorielles.ADDITON:
                résultat = Vecteur.Additionner(new Vecteur(scriptVecteurGauche.GetDataVecteur), new Vecteur(scriptVecteurDroite.GetDataVecteur));
                retourneVecteur = true;
                break;

            case OpérationsVectorielles.SOUSTRACTION:
                résultat = Vecteur.Soustraire(new Vecteur(scriptVecteurGauche.GetDataVecteur), new Vecteur(scriptVecteurDroite.GetDataVecteur));
                retourneVecteur = true;
                break;

            case OpérationsVectorielles.PRODUIT_SCALAIRE:
                résultat = Vecteur.ProduitVecteurScalaire(new Vecteur(scriptVecteurGauche.GetDataVecteur), scriptScalaire.Valeur);
                retourneVecteur = true;
                break;

            case OpérationsVectorielles.CALCUL_NORME:
                résultatNorme = Vecteur.CalculNorme(new Vecteur(scriptVecteurGauche.GetDataVecteur));
                retourneVecteur = false;
                break;

            case OpérationsVectorielles.NORMALISATION:
                résultat = Vecteur.Normalisation(new Vecteur(scriptVecteurGauche.GetDataVecteur));
                retourneVecteur = true;
                break;
            case OpérationsVectorielles.VECTEUR_RÉSULTANT:
                résultat = Vecteur.VecteurDeuxPoints(new Vecteur(scriptVecteurGauche.GetDataVecteur), new Vecteur(scriptVecteurDroite.GetDataVecteur));
                retourneVecteur = true;
                break;
        }
        if (retourneVecteur)
        {
            scriptResultatVecteur.AfficherRésultat(résultat);
        }
        else scriptRésultatNorme.AfficherNorme(résultatNorme);
    }
}

