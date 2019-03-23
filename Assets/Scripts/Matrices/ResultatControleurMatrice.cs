using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// S'occupe de la section résultat de l'éditeur
/// </summary>
public class ResultatControleurMatrice : MonoBehaviour
{
    const int MARGE = 6;
    public GameObject CtrlComposants;
    public GameObject PanneauComposant;
    Transform[,] composants;
    int nbLignes;
    int nbColonnes;
    Text[,] zonesTexte;

    /// <summary>
    /// Appelle les fonctions nécessaires à l'initialisation de la matrice résultat
    /// </summary>
    void Start()
    {
        nbColonnes = 0;
        nbLignes = 0;
        GénérationDesComposants();
        ActivationDesPanneaux();
    }

    /// <summary>
    /// Génère les composants de la matrice résultat
    /// </summary>
    private void GénérationDesComposants()
    {
        composants = new Transform[Matrice.NB_LIGNES_MAX, Matrice.NB_COLONNES_MAX];
        zonesTexte = new Text[Matrice.NB_LIGNES_MAX, Matrice.NB_COLONNES_MAX];
        RectTransform rt = PanneauComposant.GetComponent<RectTransform>();
        Vector3 positionDeBase = rt.position;
        float hauteurPanneau = rt.rect.height + MARGE;
        float largeurPanneau = rt.rect.width + MARGE;
        Quaternion rotationInitiale = Quaternion.identity;
        composants[0, 0] = PanneauComposant.transform;
        for (int i = 0; i < Matrice.NB_LIGNES_MAX; i++)
        {
            for (int j = 0; j < Matrice.NB_COLONNES_MAX; j++)
            {
                GameObject composant;
                if (i == 0 && j == 0)
                {
                    composant = PanneauComposant;
                }
                else
                {
                    Vector3 position = new Vector3(positionDeBase.x + largeurPanneau * j, positionDeBase.y - hauteurPanneau * i);
                    composant = Instantiate(PanneauComposant, position, rotationInitiale, CtrlComposants.transform);
                }
                composants[i, j] = composant.transform;
                composants[i, j].gameObject.SetActive(false);
                zonesTexte[i, j] = composant.GetComponentInChildren<Text>();
            }
        }
    }

    /// <summary>
    /// Active les panneaux nécessaires à la matrice résultat
    /// </summary>
    private void ActivationDesPanneaux() 
    {
        for (int i = 0; i < Matrice.NB_LIGNES_MAX; ++i)
        {
            for (int j = 0; j < Matrice.NB_COLONNES_MAX; ++j)
            {
                composants[i, j].gameObject.SetActive(i < nbLignes && j < nbColonnes);
                if (zonesTexte[i, j].IsActive())
                {
                    zonesTexte[i, j] = zonesTexte[i, j];
                }
                else
                    zonesTexte[i, j].text = "0";
            }
        }

    }

    /// <summary>
    /// Affiche la matrice résultat dans la zone de résultat
    /// </summary>
    /// <param name="résultat">La matrice issue du calcul</param>
    public void AfficherRésultat(Matrice résultat)
    {
        nbLignes = 0;
        nbColonnes = 0;
        if (résultat != null)
        {
            float[,] dataRésultat = résultat.DataMatrice;
            nbLignes = résultat.NbLignes;
            nbColonnes = résultat.NbColonnes;
            for (int i = 0; i < nbLignes; i++)
            {
                for (int j = 0; j < nbColonnes; j++)
                {
                    zonesTexte[i, j].text = dataRésultat[i, j].ToString();
                }

            }
        }
        ActivationDesPanneaux();
    }
    
}