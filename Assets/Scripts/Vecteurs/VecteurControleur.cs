using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Les différents états que les vecteurs peuvent prendre.
/// </summary>
enum ÉtatsVectoriels { NULLE, UNITAIRE, ALÉATOIRE };

/// <summary>
/// La classe qui s'occupe du bon fonctionnement des vecteurs dans la scène
/// </summary>
public class VecteurControleur : MonoBehaviour
{
    const int MARGE_VERTICALE = 2;
    public GameObject CtrlNbDimensions;
    public GameObject CtrlComposants;
    public GameObject PanneauComposant;
    public GameObject controleurOperations;
    Vecteur vecteur;
    GradateurControleur scriptGradateur;
    OperationsControleurAbstrait scriptOperations;
    Transform[] composants;
    InputField[] zonesDeTexte;
    int nbDimensionsVecteur;
    ÉtatsVectoriels étatActif;

    /// <summary>
    /// Retourne les composants du vecteur tel que défini dans la classe Vecteur
    /// </summary>
    public float[] GetDataVecteur
    {
        get { return vecteur.DataVecteur; }
    }

    /// <summary>
    /// S'occupe d'initialiser composant et accède à leurs scripts pour pouvoir <para>
    /// accéder à certaines fonctionnalités des objects</para>
    /// </summary>
    void Start()
    {
        vecteur = null;
        GérerVecteur();
        scriptGradateur = CtrlNbDimensions.GetComponent<GradateurControleur>();
        scriptOperations = controleurOperations.GetComponent<OperationsControleurAbstrait>();
        nbDimensionsVecteur = scriptGradateur.Valeur;
        GénérationDesComposants();
        zonesDeTexte = CtrlComposants.GetComponentsInChildren<InputField>();
        ActivationDesPanneaux();
    }

    /// <summary>
    /// Crée les inputs fields des vecteurs et les place au bon endroit par rapport au premier
    /// </summary>
    private void GénérationDesComposants()
    {
        composants = new Transform[Vecteur.NB_DIMENSIONS_MAX];
        RectTransform rt = PanneauComposant.GetComponent<RectTransform>();
        Vector3 positionDeBase = rt.position;
        float hauteurPanneau = rt.rect.height + MARGE_VERTICALE;
        Quaternion rotationInitiale = Quaternion.identity;
        composants[0] = PanneauComposant.transform;
        for (int i = 1; i < Vecteur.NB_DIMENSIONS_MAX; ++i)
        {
            Vector3 position = new Vector3(positionDeBase.x, positionDeBase.y - hauteurPanneau * i);
            GameObject composant = Instantiate(PanneauComposant, position, rotationInitiale, CtrlComposants.transform);
            composants[i] = composant.transform;
        }
    }

    /// <summary>
    /// Active les input fields des vecteurs selon la valeur du gradateur
    /// </summary>
    void ActivationDesPanneaux()
    {
        for (int i = 0; i < Vecteur.NB_DIMENSIONS_MAX; ++i)
        {
            composants[i].gameObject.SetActive(i < nbDimensionsVecteur);
        }
    }

    /// <summary>
    /// Crée un vecteur pour pouvoir faire des modifications sans changer l'original
    /// </summary>
    void GérerVecteur()
    {
        if (vecteur == null)
        {
            vecteur = new Vecteur(nbDimensionsVecteur);
        }
        else
        {
            float[] nouveauVecteur = new float[nbDimensionsVecteur];
            float[] ancienVecteur = vecteur.DataVecteur;
            int i = 0;
            while (i < nbDimensionsVecteur && i < vecteur.NbDimensions)
            {
                nouveauVecteur[i] = ancienVecteur[i];
                ++i;
            }
            vecteur = new Vecteur(nouveauVecteur);
        }
    }

    /// <summary>
    /// Édite les zones de texte des input fields lorsque les composantes sont affichées
    /// </summary>
    public void ÉditerDimension()
    {
        for (int i = 0; i < vecteur.NbDimensions; ++i)
        {
                vecteur[i] = float.Parse(zonesDeTexte[i].text);
        }
        scriptOperations.IdentifierOpérationActive();
    }

    /// <summary>
    /// Crée un vecteur nul
    /// </summary>
    public void VecteurNul()
    {
        étatActif = ÉtatsVectoriels.NULLE;
        EffectuerOpération();
    }

    /// <summary>
    /// Crée un vecteur unitaire
    /// </summary>
    public void VecteurUnitaire()
    {
        étatActif = ÉtatsVectoriels.UNITAIRE;
        EffectuerOpération();
    }

    /// <summary>
    /// Crée un vecteur aléatoire entre -9 et 9
    /// </summary>
    public void VecteurAléatoire()
    {
        étatActif = ÉtatsVectoriels.ALÉATOIRE;
        EffectuerOpération();
    }

    /// <summary>
    /// Affiche le vecteur selon son état
    /// </summary>
    public void EffectuerOpération()
    {
        switch (étatActif)
        {
            case ÉtatsVectoriels.NULLE:
                for (int i = 0; i < zonesDeTexte.Length; i++)
                {
                    zonesDeTexte[i].text = "0";
                }
                break;
            case ÉtatsVectoriels.UNITAIRE:
                for (int i = 0; i < zonesDeTexte.Length; i++)
                {
                    zonesDeTexte[i].text = "1";
                }
                break;

            case ÉtatsVectoriels.ALÉATOIRE:
                System.Random rnd = new System.Random();
                for (int i = 0; i < zonesDeTexte.Length; i++)
                {
                    zonesDeTexte[i].text = rnd.Next(-9, 10).ToString();
                }
                break;
        }
    }

    /// <summary>
    /// S'occcupe de vérifier si le gradateur a été modifié pour ensuite l'afficher correctement.
    /// </summary>
    void Update()
    {
        if (scriptGradateur.ValeurMiseÀJour)
        {
            nbDimensionsVecteur = scriptGradateur.Valeur;
            GérerVecteur();
            for (int i = 0; i < vecteur.NbDimensions; ++i)
            {
                zonesDeTexte[i].text = vecteur[i].ToString();
            }
            ActivationDesPanneaux();
            scriptGradateur.ValeurMiseÀJour = false;
        }
    }
}
