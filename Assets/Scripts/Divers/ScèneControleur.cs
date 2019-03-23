using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Gère le changement de scènes
/// </summary>
public class ScèneControleur : MonoBehaviour
{
    /// <summary>
    /// Change la scène selon la valeur associée par le build
    /// </summary>
    /// <param name="scene">La valeur de la scène que l'on désire afficher</param>
    public void ChangementScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    /// <summary>
    /// Fait quitter le build
    /// </summary>
    public void Quitter()
    {
        Application.Quit();
    }
}
