using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Les différents états de la matrice
/// </summary>
enum ÉtatsMatricielles { NULLE, UNITAIRE, ALÉATOIRE, IDENTITÉ };

/// <summary>
/// La classe qui s'occupe de la matrice dans Unity
/// </summary>
public class MatriceControleur : MonoBehaviour
{
    const int MARGE = 6;
    public GameObject CtrlLignes;
    public GameObject CtrlColonnes;
    public GameObject CtrlComposants;
    public GameObject PanneauComposant;
    public GameObject controleurOperations;
    Matrice matrice;
    GradateurControleur scriptGradateurLigne;
    GradateurControleur scriptGradateurColonne;
    OperationsControleurAbstrait scriptOperations;
    Transform[,] composants;
    InputField[,] zonesDeTexte2D;
    Button[] identité;
    Button id;
    int nbLignesMatrice;
    int nbColonnesMatrice;
    ÉtatsMatricielles opérationActive;

    
    /// <summary>
    /// S'occupe d'aller chercher les Game Objects dans Unity
    /// </summary>
    void Start()
    {
        matrice = null;
        scriptGradateurLigne = CtrlLignes.GetComponent<GradateurControleur>();
        scriptGradateurColonne = CtrlColonnes.GetComponent<GradateurControleur>();
        scriptOperations = controleurOperations.GetComponent<OperationsControleurAbstrait>();
        identité = GetComponentsInChildren<Button>();
        nbLignesMatrice = scriptGradateurLigne.Valeur;
        nbColonnesMatrice = scriptGradateurColonne.Valeur;
        GérerMatrice();
        GénérationDesComposants();
        ActivationDesPanneaux();
        TrouverBtnIdentité();
    }

    /// <summary>
    /// S'occupe du bon déroulement de l'affichage des matrices
    /// </summary>
    void Update()
    {
        if (scriptGradateurLigne.ValeurMiseÀJour || scriptGradateurColonne.ValeurMiseÀJour)
        {
            nbLignesMatrice = scriptGradateurLigne.Valeur;
            nbColonnesMatrice = scriptGradateurColonne.Valeur;
            ActivationDesPanneaux();
            GérerMatrice();
            for (int i = 0; i < matrice.NbLignes; ++i)
            {
                for (int j = 0; j < matrice.NbColonnes; ++j)
                {
                    string msg = matrice[i, j].ToString();
                    zonesDeTexte2D[i, j].text = msg;
                }
            }
            if (id != null)
            {
                ActivationBtnId();
            }
            scriptGradateurLigne.ValeurMiseÀJour = false;
            scriptGradateurColonne.ValeurMiseÀJour = false;
        }
    }
   

    /// <summary>
    /// Active le bouton identité seulement si la matrice est carrée
    /// </summary>
    public void ActivationBtnId()
    {
        if (matrice.NbLignes == matrice.NbColonnes)
        {
            id.interactable = true;
        }
        else
            id.interactable = false;
    }

    /// <summary>
    /// Trouve le bouton identité dans la liste des boutons avec son tag
    /// </summary>
    private void TrouverBtnIdentité()
    {
        for (int i = 0; i < identité.Length; i++)
        {
            if (identité[i].tag == "id")
            {
                id = identité[i];
            }

        }
    }

    /// <summary>
    /// Crée les composants de la matrice et les place par rapport au premier
    /// </summary>
    private void GénérationDesComposants()
    {
        composants = new Transform[Matrice.NB_LIGNES_MAX, Matrice.NB_COLONNES_MAX];
        zonesDeTexte2D = new InputField[Matrice.NB_LIGNES_MAX, Matrice.NB_COLONNES_MAX];
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
                zonesDeTexte2D[i, j] = composant.GetComponentInChildren<InputField>();
            }
        }
    }

    /// <summary>
    /// Active les input fields selon la valeur des gradateurs
    /// </summary>
    private void ActivationDesPanneaux()
    {
        for (int i = 0; i < Matrice.NB_LIGNES_MAX; ++i)
        {
            for (int j = 0; j < Matrice.NB_COLONNES_MAX; ++j)
            {
                composants[i, j].gameObject.SetActive(i < nbLignesMatrice && j < nbColonnesMatrice);
                if (zonesDeTexte2D[i, j].IsActive())
                {
                    zonesDeTexte2D[i, j] = zonesDeTexte2D[i, j];
                }
                else
                    zonesDeTexte2D[i, j].text = "0";
            }
        }

    }

    /// <summary>
    /// Crée une matrice pour pouvoir la modifier sans changer l'originale
    /// </summary>
    void GérerMatrice()
    {
        float[,] tableauDimensions = new float[nbLignesMatrice, nbColonnesMatrice];
        if (matrice != null)
        {
            float[,] ancienneMatrice = matrice.DataMatrice;
            int maxLignes = Mathf.Min(tableauDimensions.GetLength(0), ancienneMatrice.GetLength(0));
            int maxColonnes = Mathf.Min(tableauDimensions.GetLength(1), ancienneMatrice.GetLength(1));
            for (int i = 0; i < maxLignes; i++)
            {
                for (int j = 0; j < maxColonnes; j++)
                {
                    tableauDimensions[i, j] = ancienneMatrice[i, j];
                }
            }
        }
        matrice = new Matrice(tableauDimensions);
    }

    /// <summary>
    /// Rempli les inputs fields de la matrice avec la valeur appropriée
    /// </summary>
    public void ÉditerDimension()
    {
        float number;
        for (int i = 0; i < nbLignesMatrice; ++i)
        {
            for (int j = 0; j < nbColonnesMatrice; ++j)
            {
                bool result = float.TryParse(zonesDeTexte2D[i, j].text, out number);
                if (result)
                {
                        matrice[i, j] = float.Parse(zonesDeTexte2D[i, j].text);
                }
                else
                {
                    matrice[i, j] = 0;
                }
            }
        }
        scriptOperations.IdentifierOpérationActive();
    }

    /// <summary>
    /// Retourne les valeurs de la matrice tel que défini dans Matrice
    /// </summary>
    public float[,] GetDataMatrice
    {
        get { return matrice.DataMatrice; }
    }

    /// <summary>
    /// Choisi l'état Nulle de la matrice
    /// </summary>
    public void MatriceNulle()
    {
        opérationActive = ÉtatsMatricielles.NULLE;
        EffectuerOpération();
    }

    /// <summary>
    /// Choisi l'état Unitaire de la matrice
    /// </summary>
    public void MatriceUnitaire()
    {
        opérationActive = ÉtatsMatricielles.UNITAIRE;
        EffectuerOpération();
    }

    /// <summary>
    /// Choisi l'état Identité de la matrice
    /// </summary>
    public void MatriceIdentité()
    {
        opérationActive = ÉtatsMatricielles.IDENTITÉ;
        EffectuerOpération();
    }

    /// <summary>
    /// Choisi l'état Aléatoire de la matrice
    /// </summary>
    public void MatriceAléatoire()
    {
        opérationActive = ÉtatsMatricielles.ALÉATOIRE;
        EffectuerOpération();
    }

    /// <summary>
    /// Change l"état de la matrice selon l'état choisi
    /// </summary>
    public void EffectuerOpération()
    {
        switch (opérationActive)
        {
            case ÉtatsMatricielles.NULLE:
                for (int i = 0; i < zonesDeTexte2D.GetLength(0); i++)
                {
                    for (int j = 0; j < zonesDeTexte2D.GetLength(1); j++)
                    {
                        zonesDeTexte2D[i, j].text = "0";
                    }
                }
                break;
            case ÉtatsMatricielles.UNITAIRE:
                for (int i = 0; i < zonesDeTexte2D.GetLength(0); i++)
                {
                    for (int j = 0; j < zonesDeTexte2D.GetLength(1); j++)
                    {
                        zonesDeTexte2D[i, j].text = "1";
                    }
                }
                break;
            case ÉtatsMatricielles.IDENTITÉ:
                for (int i = 0; i < zonesDeTexte2D.GetLength(0); i++)
                {
                    for (int j = 0; j < zonesDeTexte2D.GetLength(1); j++)
                    {
                        if (i == j)
                        {
                            zonesDeTexte2D[i, j].text = "1";
                        }
                        else
                        {
                            zonesDeTexte2D[i, j].text = "0";
                        }
                    }
                }
                break;
            case ÉtatsMatricielles.ALÉATOIRE:
                System.Random rnd = new System.Random();
                for (int i = 0; i < zonesDeTexte2D.GetLength(0); i++)
                {
                    for (int j = 0; j < zonesDeTexte2D.GetLength(1); j++)
                    {
                        zonesDeTexte2D[i, j].text = rnd.Next(-9, 10).ToString();
                    }
                }
                break;
        }
    }
}
