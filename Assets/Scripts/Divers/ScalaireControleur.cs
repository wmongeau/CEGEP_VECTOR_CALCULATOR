using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Gère les scalaires utilisés dans l'éditeur
/// </summary>
public class ScalaireControleur : MonoBehaviour
{ 
    public float Valeur { get; private set; }
    InputField txtScalaire;
    
    /// <summary>
    /// Récupère les composants utiles au scalaire
    /// </summary>
    void Start()
    {
        Valeur = 0;
        txtScalaire = GetComponentInChildren<InputField>();
        txtScalaire.text = Valeur.ToString();
    }

    /// <summary>
    /// Change la valeur du scalaire selon ce qui est entré
    /// </summary>
    public void ModificationScalaire()
    {
        Valeur = float.Parse(txtScalaire.text);
    }

    /// <summary>
    /// Fait un scalaire nul
    /// </summary>
    public void Scalaire0()
    {
        txtScalaire.text = "0";
    }

    /// <summary>
    /// Fait un scalaire unitaire
    /// </summary>
    public void Scalaire1()
    {
        txtScalaire.text = "1";
    }

    /// <summary>
    /// Fair t un scalaire aléatoire entre -9 et 9
    /// </summary>
    public void ScalaireAléatoire()
    {
        System.Random rnd = new System.Random();
        txtScalaire.text = rnd.Next(-9, 10).ToString();
    }
}
