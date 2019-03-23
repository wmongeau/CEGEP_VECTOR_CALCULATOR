using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Gère ce qu'il se passe sur le clavier
/// </summary>
public class GestionClavier : MonoBehaviour
{
    /// <summary>
    /// Vérifie si Escape est enfoncé pour fermer le build
    /// </summary>
    void Update ()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
        if (Input.GetKeyDown("tab"))
        {
          //Insert magic here -->  <--And a little more here
        }
    }
}
