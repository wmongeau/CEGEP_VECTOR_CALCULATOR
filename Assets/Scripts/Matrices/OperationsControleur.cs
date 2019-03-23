using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Les différentes opérations que l'on peut faire sur une matrice
/// </summary>
enum OpérationsMatricielles { ADDITON, SOUSTRACTION, PRODUIT_SCALAIRE, TRANSPOSITION, PRODUIT, FAUX_PRODUIT, DETERMINANT, INVERSE_ADJOINTE };

/// <summary>
/// La classe qui gère les opérations de la matrice
/// </summary>
public class OperationsControleur : OperationsControleurAbstrait
{

    public GameObject EditeurMatriceGauche;
    public GameObject EditeurMatriceDroite;
    public GameObject EditeurScalaire;
    public GameObject ZoneResultatMatricielle;
    public GameObject ZoneRésultatDéterminant;
    MatriceControleur scriptMatriceGauche;
    MatriceControleur scriptMatriceDroite;
    ScalaireControleur scriptScalaire;
    ResultatControleurMatrice scriptResultatMatrice;
    RésultatControleurScalaire scriptResultatDéterminant;
    GradateurControleur scriptGradateurLigneGauche;
    GradateurControleur scriptGradateurColonneGauche;
    GradateurControleur scriptGradateurLigneDroit;
    GradateurControleur scriptGradateurColonneDroit;
    Dropdown listOpérations;
    Button calculer;
    OpérationsMatricielles opérationActive;

    /// <summary>
    /// S'occupe de récolter les Game Objects dans Unity
    /// </summary>
    void Start()
    {
        calculer = gameObject.GetComponentInChildren<Button>();
        listOpérations = gameObject.GetComponentInChildren<Dropdown>();

        scriptMatriceGauche = EditeurMatriceGauche.GetComponent<MatriceControleur>();
        scriptMatriceDroite = EditeurMatriceDroite.GetComponent<MatriceControleur>();
        scriptScalaire = EditeurScalaire.GetComponent<ScalaireControleur>();

        scriptResultatMatrice = ZoneResultatMatricielle.GetComponent<ResultatControleurMatrice>();
        scriptResultatDéterminant = ZoneRésultatDéterminant.GetComponent<RésultatControleurScalaire>();

        scriptGradateurLigneGauche = scriptMatriceGauche.CtrlLignes.GetComponent<GradateurControleur>();
        scriptGradateurColonneGauche = scriptMatriceGauche.CtrlColonnes.GetComponent<GradateurControleur>();
        scriptGradateurLigneDroit = scriptMatriceDroite.CtrlLignes.GetComponent<GradateurControleur>();
        scriptGradateurColonneDroit = scriptMatriceDroite.CtrlColonnes.GetComponent<GradateurControleur>();

        IdentifierOpérationActive();
    }

    /// <summary>
    /// Identifie l'opération dropdown et active les différentes composantes utiles pour l'opération
    /// </summary>
    public override void IdentifierOpérationActive()
    {
        opérationActive = (OpérationsMatricielles)listOpérations.value;
        switch (opérationActive)
        {
            case OpérationsMatricielles.ADDITON:
                EditeurScalaire.SetActive(false);
                EditeurMatriceDroite.SetActive(true);
                ZoneResultatMatricielle.SetActive(true);
                ZoneRésultatDéterminant.SetActive(false);
                if (scriptGradateurColonneDroit.Valeur == scriptGradateurColonneGauche.Valeur && scriptGradateurLigneGauche.Valeur
                    == scriptGradateurLigneDroit.Valeur)
                {
                    calculer.interactable = true;
                }
                else
                {
                    calculer.interactable = false;
                }
                break;
            case OpérationsMatricielles.SOUSTRACTION:
                EditeurScalaire.SetActive(false);
                EditeurMatriceDroite.SetActive(true);
                ZoneResultatMatricielle.SetActive(true);
                ZoneRésultatDéterminant.SetActive(false);
                if (scriptGradateurColonneDroit.Valeur == scriptGradateurColonneGauche.Valeur && scriptGradateurLigneGauche.Valeur
                    == scriptGradateurLigneDroit.Valeur)
                {
                    calculer.interactable = true;
                }
                else
                {
                    calculer.interactable = false;
                }
                break;
            case OpérationsMatricielles.PRODUIT_SCALAIRE:
                EditeurScalaire.SetActive(true);
                EditeurMatriceDroite.SetActive(false);
                ZoneResultatMatricielle.SetActive(true);
                ZoneRésultatDéterminant.SetActive(false);
                calculer.interactable = true;
                break;
            case OpérationsMatricielles.TRANSPOSITION:
                EditeurScalaire.SetActive(false);
                EditeurMatriceDroite.SetActive(false);
                ZoneResultatMatricielle.SetActive(true);
                ZoneRésultatDéterminant.SetActive(false);
                calculer.interactable = true;
                break;
            case OpérationsMatricielles.PRODUIT:
                EditeurScalaire.SetActive(false);
                EditeurMatriceDroite.SetActive(true);
                ZoneResultatMatricielle.SetActive(true);
                ZoneRésultatDéterminant.SetActive(false);
                if (scriptGradateurColonneGauche.Valeur == scriptGradateurLigneDroit.Valeur)
                {
                    calculer.interactable = true;
                }
                else
                {
                    calculer.interactable = false;
                }
                break;
            case OpérationsMatricielles.FAUX_PRODUIT:
                EditeurScalaire.SetActive(false);
                EditeurMatriceDroite.SetActive(true);
                ZoneResultatMatricielle.SetActive(true);
                ZoneRésultatDéterminant.SetActive(false);
                if (scriptGradateurColonneDroit.Valeur == scriptGradateurColonneGauche.Valeur && scriptGradateurLigneGauche.Valeur
                    == scriptGradateurLigneDroit.Valeur)
                {
                    calculer.interactable = true;
                }
                else
                {
                    calculer.interactable = false;
                }
                break;
            case OpérationsMatricielles.DETERMINANT:
                EditeurScalaire.SetActive(false);
                EditeurMatriceDroite.SetActive(false);
                ZoneResultatMatricielle.SetActive(false);
                ZoneRésultatDéterminant.SetActive(true);
                if (scriptGradateurColonneGauche.Valeur == scriptGradateurLigneGauche.Valeur)
                {
                    calculer.interactable = true;
                }
                else
                {
                    calculer.interactable = false;
                }
                break;
            case OpérationsMatricielles.INVERSE_ADJOINTE:
                EditeurScalaire.SetActive(false);
                EditeurMatriceDroite.SetActive(false);
                ZoneResultatMatricielle.SetActive(true);
                ZoneRésultatDéterminant.SetActive(false);
                if (scriptGradateurColonneGauche.Valeur == 7 && scriptGradateurLigneGauche.Valeur == 7)
                {
                    calculer.interactable = true;
                }
                else if (scriptGradateurColonneGauche.Valeur == scriptGradateurLigneGauche.Valeur)
                {
                    if (Matrice.Déterminant(new Matrice(scriptMatriceGauche.GetDataMatrice)) != 0)
                    {
                        calculer.interactable = true;
                    }
                    else
                        calculer.interactable = false;
                }
                else
                {
                    calculer.interactable = false;
                }
                break;

        }
    }

    /// <summary>
    /// S'occupe de faire le calcul demandé dans l'éditeur
    /// </summary>
    public void Calculer()
    {
        Matrice résultat = null;
        float déterminant = 0;
        bool scalaire = false;
        switch (opérationActive)
        {
            case OpérationsMatricielles.ADDITON:
                résultat = Matrice.Additionner(new Matrice(scriptMatriceGauche.GetDataMatrice), new Matrice(scriptMatriceDroite.GetDataMatrice));
                scalaire = false;
                break;
            case OpérationsMatricielles.SOUSTRACTION:
                résultat = Matrice.Soustraction(new Matrice(scriptMatriceGauche.GetDataMatrice), new Matrice(scriptMatriceDroite.GetDataMatrice));
                scalaire = false;
                break;
            case OpérationsMatricielles.PRODUIT_SCALAIRE:
                résultat = Matrice.ProduitScalaire(new Matrice(scriptMatriceGauche.GetDataMatrice), scriptScalaire.Valeur);
                scalaire = false;
                break;
            case OpérationsMatricielles.TRANSPOSITION:
                résultat = Matrice.Transposée(new Matrice(scriptMatriceGauche.GetDataMatrice));
                scalaire = false;
                break;
            case OpérationsMatricielles.PRODUIT:
                résultat = Matrice.Produit(new Matrice(scriptMatriceGauche.GetDataMatrice), new Matrice(scriptMatriceDroite.GetDataMatrice));
                scalaire = false;
                break;
            case OpérationsMatricielles.FAUX_PRODUIT:
                résultat = Matrice.FauxProduit(new Matrice(scriptMatriceGauche.GetDataMatrice), new Matrice(scriptMatriceDroite.GetDataMatrice));
                scalaire = false;
                break;
            case OpérationsMatricielles.DETERMINANT:
                déterminant = Matrice.Déterminant(new Matrice(scriptMatriceGauche.GetDataMatrice));
                scalaire = true;
                break;
            case OpérationsMatricielles.INVERSE_ADJOINTE:
                résultat = Matrice.MatriceInverse(new Matrice(scriptMatriceGauche.GetDataMatrice));
                scalaire = false;
                break;

        }
        if (scalaire)
        {
            scriptResultatDéterminant.AfficherRésultatScalaire(déterminant);
        }
        scriptResultatMatrice.AfficherRésultat(résultat);
    }

}
