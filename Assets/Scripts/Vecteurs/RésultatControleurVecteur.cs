using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RésultatControleurVecteur : MonoBehaviour
{
    const int MARGE_VERTICALE = 2;
    public GameObject CtrlComposants;
    public GameObject PanneauComposant;
    
    Transform[] composants;
    int nbDimensionsVecteur;
    Text[] textes;

    void Start()
    {
        nbDimensionsVecteur = 0;
        
        GénérationDesComposants();
        textes = CtrlComposants.GetComponentsInChildren<Text>(true).Where<Text>(x => x.name.Substring(0, 6) == "TxtVal").ToArray<Text>();
        ActivationDesPanneaux();
    }

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

    void ActivationDesPanneaux()
    {
        for (int i = 0; i < Vecteur.NB_DIMENSIONS_MAX; ++i)
        {
            composants[i].gameObject.SetActive(i < nbDimensionsVecteur);
        }
    }

    public void AfficherRésultat(Vecteur vecteurRésultat)
    {
        nbDimensionsVecteur = 0;
        if (vecteurRésultat != null)
        {
            float[] dataRésultat = vecteurRésultat.DataVecteur;
            nbDimensionsVecteur = dataRésultat.Length;
            for (int i = 0; i < nbDimensionsVecteur; ++i)
            {
                textes[i].text = dataRésultat[i].ToString();
            }
        }
        ActivationDesPanneaux();
    }
   
}
