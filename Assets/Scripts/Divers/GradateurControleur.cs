using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// S'occupe des gradateurs de l'éditeur
/// </summary>
public class GradateurControleur : MonoBehaviour
{
    public bool ValeurMiseÀJour { get; set; }
    public int Valeur{ get; set; }
 
    Slider gradateur;
    Text txtvaleur;
    public GameObject operationsControleur;
    OperationsControleurAbstrait scriptOperations;

    /// <summary>
    /// Récupère les différentes informations du gradateur
    /// </summary>
    void Start()
    {
        scriptOperations = operationsControleur.GetComponent<OperationsControleurAbstrait>();
        gradateur = GetComponentInChildren<Slider>();
        gradateur.value = gradateur.minValue;
        Valeur = (int)gradateur.value;
        txtvaleur = GetComponentInChildren<Text>();
        txtvaleur.text = Valeur.ToString();
        ValeurMiseÀJour = true;
    }

    /// <summary>
    /// Agis lorsque le gradateur est mis à jour
    /// </summary>
    public void ModificationGradateur()
    {
        ValeurMiseÀJour = Valeur != (int)gradateur.value;
        Valeur = (int)gradateur.value;
        txtvaleur.text = Valeur.ToString();
        scriptOperations.IdentifierOpérationActive();
    }
}
