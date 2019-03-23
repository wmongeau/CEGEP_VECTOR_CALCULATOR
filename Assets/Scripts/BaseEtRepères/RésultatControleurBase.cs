using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// S'occupe du résulat de la base en string
/// </summary>
public class RésultatControleurBase : MonoBehaviour {

    public GameObject PanneauRéponse;
    Text txtRéponse;

    /// <summary>
    /// Récupère le Text du panneau de réponse
    /// </summary>
    void Start()
    {
        txtRéponse = PanneauRéponse.GetComponentInChildren<Text>();
    }

    /// <summary>
    /// Affiche le résultat approprié du calcul
    /// </summary>
    /// <param name="ouiOuNon">Le booléen qui contient la réponse</param>
    public void AfficherRésultat(bool ouiOuNon)
    {
        if (ouiOuNon)
        {
            txtRéponse.text = "Oui!";
        }
        else
        {
            txtRéponse.text = "Non...";
        }
    }
}
