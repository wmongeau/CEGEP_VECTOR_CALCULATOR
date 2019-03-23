using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// S'occupe du string de résultat qui accompagne le SEL
/// </summary>
public class RésultatControleurÉquation : MonoBehaviour {

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
    /// Écrit dans le panneau de réponse
    /// </summary>
    /// <param name="réponse">L'équation de réponse du SEL</param>
    public void AfficherRésultat(string réponse)
    {
        txtRéponse.text = réponse;
    }
}
