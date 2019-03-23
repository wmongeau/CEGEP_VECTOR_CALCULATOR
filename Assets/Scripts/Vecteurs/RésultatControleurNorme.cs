using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// S'occupe de bien afficher la norme d'un vecteur une fois calculée
/// </summary>
public class RésultatControleurNorme : MonoBehaviour {

    public GameObject Norme;
    Text txtNorme;

    /// <summary>
    /// Trouve le composant text de la zone de réponse de la norme 
    /// </summary>
    void Start () {
        txtNorme = Norme.GetComponentInChildren<Text>();
    }

    /// <summary>
    /// Affiche la norme du vecteur
    /// </summary>
    /// <param name="norme">La valeur calculée de la norme</param>
    public void AfficherNorme(float norme)
    {
        txtNorme.text = norme.ToString();
    }
    
}
