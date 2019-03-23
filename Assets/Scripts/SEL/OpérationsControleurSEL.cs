using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Les différentes opérations possibles pour un SEL
/// </summary>
enum OpérationsSEL { GAUSS, INVERSE, FACTORISATION};

/// <summary>
/// Gère les opérations sur le SEL
/// </summary>
public class OpérationsControleurSEL : OperationsControleurAbstrait
{
    public GameObject PnlSel;
    public GameObject PnlMatriceRésultat;
    public GameObject PnlMatriceURésultat;
    public GameObject PnlÉquationRésultat;
    public GameObject PnlRésultat;
    MatriceControleur scriptMatriceSEL;
    ResultatControleurMatrice scriptRésultatGauche;
    ResultatControleurMatrice scriptRésultatU;
    RésultatControleurÉquation scriptRésultatÉquation;
    Dropdown listeOpérations;
    Button calculer;
    OpérationsSEL opérationActive;

    /// <summary>
    /// Récupère les Game Objects nécessaire à l'initialisation
    /// </summary>
    void Start ()
    {
        calculer = gameObject.GetComponentInChildren<Button>();
        scriptMatriceSEL = PnlSel.GetComponent<MatriceControleur>();
        scriptRésultatGauche = PnlMatriceRésultat.GetComponent<ResultatControleurMatrice>();
        scriptRésultatU = PnlMatriceURésultat.GetComponent<ResultatControleurMatrice>();
        scriptRésultatÉquation = PnlÉquationRésultat.GetComponent<RésultatControleurÉquation>();
        listeOpérations = gameObject.GetComponentInChildren<Dropdown>();
        IdentifierOpérationActive();
    }

    /// <summary>
    /// Identifie l'opération sur le dropdown et active les composants nécessaires à l'opération
    /// </summary>
    public override void IdentifierOpérationActive()
    {
        opérationActive = (OpérationsSEL)listeOpérations.value;
        switch (opérationActive)
        {

            case OpérationsSEL.GAUSS:
                PnlMatriceURésultat.SetActive(false);
                PnlMatriceRésultat.SetActive(true);
                PnlÉquationRésultat.SetActive(true);
                PnlRésultat.SetActive(true);
                calculer.interactable = true;
                break;
            case OpérationsSEL.INVERSE:
                PnlMatriceURésultat.SetActive(false);
                PnlMatriceRésultat.SetActive(true);
                PnlÉquationRésultat.SetActive(false);
                PnlRésultat.SetActive(true);
                calculer.interactable = true;
                break;
            case OpérationsSEL.FACTORISATION:
                PnlMatriceURésultat.SetActive(true);
                PnlMatriceRésultat.SetActive(true);
                PnlÉquationRésultat.SetActive(false);
                PnlRésultat.SetActive(false);
                if (scriptMatriceSEL.GetDataMatrice.GetLength(0) == scriptMatriceSEL.GetDataMatrice.GetLength(1))
                {
                    if (Matrice.Déterminant(new Matrice(scriptMatriceSEL.GetDataMatrice)) != 0)
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
    /// Gère les calculs pour résoudre les SEL
    /// </summary>
    public void Calculer()
    {
        Matrice résultat = null;
        Matrice résultatU = null;
        string équation = "";
        switch (opérationActive)
        {

            case OpérationsSEL.GAUSS:
                résultat = Matrice.MéthodeGauss(new Matrice(scriptMatriceSEL.GetDataMatrice));
                équation = Matrice.ÉquationGauss(new Matrice(résultat.DataMatrice));
                scriptRésultatGauche.AfficherRésultat(résultat);
                scriptRésultatÉquation.AfficherRésultat(équation);
                break;
            case OpérationsSEL.INVERSE:
                résultat = Matrice.InverseSEL(new Matrice(scriptMatriceSEL.GetDataMatrice));
                scriptRésultatGauche.AfficherRésultat(résultat);
                break;
            case OpérationsSEL.FACTORISATION:
                résultat = Matrice.DecompositionL(new Matrice(scriptMatriceSEL.GetDataMatrice));
                résultatU = Matrice.DecompositionU(new Matrice(scriptMatriceSEL.GetDataMatrice));
                scriptRésultatGauche.AfficherRésultat(résultat);
                scriptRésultatU.AfficherRésultat(résultatU);
                break;
        }
    }
}
