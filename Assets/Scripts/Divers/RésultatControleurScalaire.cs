using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Gère l'écriture des scalaires
/// </summary>
public class RésultatControleurScalaire : MonoBehaviour
{
    Text txtDéterminant;

    /// <summary>
    /// Récupère le Text du Game Object qui représente scalaire
    /// </summary>
    void Start()
    {
        txtDéterminant = gameObject.GetComponentInChildren<Text>();
    }

    /// <summary>
    /// Affiche la valeur du scalaire dans le texte
    /// </summary>
    /// <param name="résultat">La valeur entrée du scalaire</param>
    public void AfficherRésultatScalaire(float résultat)
    {
        txtDéterminant.text = résultat.ToString();
    }
}
