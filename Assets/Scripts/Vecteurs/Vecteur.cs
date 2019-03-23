using System;
using UnityEngine;

public class Vecteur
{
    public const int NB_DIMENSIONS_MAX = 4;
    #region Informations et Constructeurs de Vecteurs
    /// <summary>
    /// Propriété représentant les dimensions du vecteur.
    /// </summary>
    public int NbDimensions
    {
        get { return DataVecteur.Length; }
    }

    float[] dataVecteur;
    /// <summary>
    /// Propriété représentant le vecteur sous forme de tableau de floats.
    /// </summary>
    public float[] DataVecteur
    {
        get
        {
            float[] duplicat = new float[dataVecteur.Length];
            for (int i = 0; i < dataVecteur.Length; ++i)
            {
                duplicat[i] = dataVecteur[i];
            }
            return duplicat;
        }
        private set
        {
            if ((value != null) && (value.Length <= NB_DIMENSIONS_MAX))
            {
                dataVecteur = new float[value.Length];
                for (int i = 0; i < dataVecteur.Length; ++i)
                {
                    dataVecteur[i] = value[i];
                }
            }
        }
    }
    /// <summary>
    /// Retourne la valeur à l'indice indiqué.
    /// </summary>
    /// <param name="index">Prends en paramètre un int représentant l'index désiré.</param>
    /// <returns>Retourne un float.</returns>
    public float this[int index]
    {
        get
        {
            float valeur;
            if (index < 0 || index >= NbDimensions)
            {
                valeur = 0;
            }
            else
            {
                valeur = dataVecteur[index];

            }
            return valeur;
        }
        set
        {
            if (index >= 0 && index < NbDimensions)
            {
                dataVecteur[index] = value;
            }
        }
    }
    /// <summary>
    /// Constructeur de vecteur à partir d'une dimension.
    /// </summary>
    /// <param name="nbDimensions">Prends en paramètre un int.</param>
    public Vecteur(int nbDimensions)
    {
        if (nbDimensions < 0 && nbDimensions > NB_DIMENSIONS_MAX)
        {
            nbDimensions = NB_DIMENSIONS_MAX;
        }
        DataVecteur = new float[nbDimensions];
    }
    /// <summary>
    /// Constructeur de vecteur à partir d'un tableau de floats.
    /// </summary>
    /// <param name="tableau">Prends en paramètre un tableau de floats.</param>
    public Vecteur(float[] tableau)
    {
        if (tableau == null)
        {
            DataVecteur = new float[NB_DIMENSIONS_MAX];
        }
        else if (tableau.Length > NB_DIMENSIONS_MAX)
        {
            float[] extrait = new float[NB_DIMENSIONS_MAX];
            Array.Copy(tableau, extrait, NB_DIMENSIONS_MAX);
            DataVecteur = extrait;
        }
        else
        {
            DataVecteur = tableau;
        }
    }
    #endregion
    #region Opérations Vectorielles
    /// <summary>
    /// Fait l'addition de deux vecteurs
    /// </summary>
    /// <param name="gauche">Le vecteur de gauche dans l'éditeur</param>
    /// <param name="droite">Le vecteur de droite dans l'éditeur</param>
    /// <returns>Un nouveau vecteur résultant</returns>
    static public Vecteur Additionner(Vecteur gauche, Vecteur droite) 
    {
        Vecteur résultat = null;

        if (gauche != null && droite != null && gauche.NbDimensions == droite.NbDimensions)
        {
            float[] dataSomme = new float[gauche.NbDimensions];
            float[] dataGauche = gauche.DataVecteur;
            float[] dataDroite = droite.DataVecteur;
            for (int i = 0; i < gauche.NbDimensions; ++i)
            {
                dataSomme[i] = dataGauche[i] + dataDroite[i];
            }
            résultat = new Vecteur(dataSomme);
        }
        return résultat;
    }

    /// <summary>
    /// Fait la soustraction de deux vecteurs
    /// </summary>
    /// <param name="gauche">Le vecteur de gauche dans l'éditeur</param>
    /// <param name="droite">Le vecteur de droite dans l'éditeur</param>
    /// <returns>Un nouveau vecteur résultant</returns>
    static public Vecteur Soustraire(Vecteur gauche, Vecteur droite)
    {
        Vecteur résultat = null;

        if (gauche != null && droite != null && gauche.NbDimensions == droite.NbDimensions)
        {
            float[] dataDifférence = new float[gauche.NbDimensions];
            float[] dataGauche = gauche.DataVecteur;
            float[] dataDroite = droite.DataVecteur;
            for (int i = 0; i < gauche.NbDimensions; ++i)
            {
                dataDifférence[i] = dataGauche[i] - dataDroite[i];
            }
            résultat = new Vecteur(dataDifférence);
        }
        return résultat;
    }

    /// <summary>
    /// Fait le produit par un scalaire d'un vecteur
    /// </summary>
    /// <param name="gauche">Le vecteur de gauche dans l'éditeur</param>
    /// <param name="droite">Le scalaire à droite dans l'éditeur</param>
    /// <returns>Le vecteur résultant</returns>
    static public Vecteur ProduitVecteurScalaire(Vecteur gauche, float droite)
    {
        Vecteur résultat = null;

        if (gauche != null)
        {
            float[] dataProduitScalaire = new float[gauche.NbDimensions];
            float[] dataGauche = gauche.DataVecteur;
            for (int i = 0; i < gauche.NbDimensions; ++i)
            {
                dataProduitScalaire[i] = dataGauche[i] * droite;
            }
            résultat = new Vecteur(dataProduitScalaire);
        }
        return résultat;
    }

    /// <summary>
    /// Calcule la norme d'un vecteur
    /// </summary>
    /// <param name="gauche">Le vecteur de gauche dans l'éditeur</param>
    /// <returns>La norme du vecteur</returns>
    static public float CalculNorme(Vecteur gauche)
    {
        float résultat;
        float intérieurRacine = 0;
        float[] vecteurGauche = gauche.DataVecteur;
        for (int i = 0; i < gauche.NbDimensions; i++)
        {
            intérieurRacine += Mathf.Pow(vecteurGauche[i], 2);
        }
        return résultat = Mathf.Sqrt(intérieurRacine);
    }

    /// <summary>
    /// Divise tous les composants du vecteur par la norme de celui-ci
    /// </summary>
    /// <param name="gauche">Le vecteur de gauche dans l'éditeur</param>
    /// <returns></returns>
    static public Vecteur Normalisation(Vecteur gauche)
    {
        float[] résultat = new float[gauche.NbDimensions];
        float[] vecteurGauche = gauche.DataVecteur;
        float norme = CalculNorme(gauche);
        for (int i = 0; i < gauche.NbDimensions; i++)
        {
            résultat[i] = vecteurGauche[i] / norme;
        }
        Vecteur résultatVecteur = new Vecteur(résultat);
        return résultatVecteur;
    }

    /// <summary>
    /// Fait la vérification si deux vecteurs forment un plan
    /// </summary>
    /// <param name="gauche">Le vecteur de gauche dans l'éditeur</param>
    /// <param name="centre">Le vecteur central dans l'éditeur</param>
    /// <returns>si les vecteurs forment un plan</returns>
    static public bool EstUnPlan(Vecteur gauche, Vecteur centre)
    {
        bool estUnPlan;

        estUnPlan = EstNul(gauche) && EstNul(centre) && EstParallèle(gauche, centre);

        return estUnPlan;
    }

    /// <summary>
    /// Vérifie si trois vecteurs forment un base
    /// </summary>
    /// <param name="gauche">Le vecteur de gauche dans l'éditeur</param>
    /// <param name="centre">Le vecteur central dans l'éditeur</param>
    /// <param name="droite">Le vecteur de droite dans l'éditeur</param>
    /// <returns>Si les vecteurs forment une base</returns>
    static public bool EstUnEspace(Vecteur gauche, Vecteur centre, Vecteur droit)
    {
        bool estUnEspace;
        float[,] matriceReprésentation = new float[gauche.NbDimensions,3];
        for (int i = 0; i < gauche.NbDimensions; i++)
        {
            matriceReprésentation[i, 0] = gauche[i];
        }
        for (int i = 0; i < centre.NbDimensions; i++)
        {
            matriceReprésentation[i, 1] = centre[i];
        }
        for (int i = 0; i < droit.NbDimensions; i++)
        {
            matriceReprésentation[i, 2] = droit[i];
        }
        estUnEspace = (Matrice.Déterminant(new Matrice(matriceReprésentation)) != 0);

        return estUnEspace;
    }

    /// <summary>
    /// Vérifie si un vecteur est non-nul
    /// </summary>
    /// <param name="gauche">Le vecteur de gauche dans l'éditeur</param>
    /// <returns>si le vecteur est non-nul</returns>
    static public bool EstNul(Vecteur gauche)
    {
        bool NonNul;
        float sommeVérif = 0;
        for (int i = 0; i < gauche.NbDimensions; i++)
        {
            sommeVérif += Mathf.Abs(gauche[i]);
        }
        if (sommeVérif == 0)
        {
            NonNul = false ;
        }
        else
            NonNul = true;
        return NonNul;
    }
    /// <summary>
    /// Retourne si deux vecteurs sont parallèles
    /// </summary>
    /// <param name="gauche">Le vecteur de gauche dans l'éditeur</param>
    /// <param name="droit">Le vecteur à droite dans l'éditeur</param>
    /// <returns></returns>
    static public bool EstParallèle(Vecteur gauche, Vecteur droit)
    {
        bool NonParallèle = true;
        float[] ks = new float[gauche.NbDimensions];
        for (int j = 0; j < gauche.NbDimensions; j++)
        {
            ks[j] = gauche[j] / droit[j];
        }
        int i = 0;
        while (NonParallèle && i < ks.Length - 1)
        {
            if (ks[i] == ks[i + 1])
            {
                NonParallèle = false;
            }
            else
                NonParallèle = true;
            i++;
        }

        return NonParallèle;
    }
    /// <summary>
    /// Retourne le vecteur résultant de deux points.
    /// </summary>
    /// <param name="gauche">Le point de gauche</param>
    /// <param name="droit">Le point de droite</param>
    /// <returns>Retourne un vecteur</returns>
    static public Vecteur VecteurDeuxPoints(Vecteur gauche, Vecteur droit)
    {
        Vecteur résultat = null;
        résultat = Soustraire(droit, gauche);
        return résultat;
    }
    /// <summary>
    /// Écris un vecteur dans un plan donné
    /// </summary>
    /// <param name="gauche">Premier vecteur du plan</param>
    /// <param name="centre">Deuxième vecteur du plan</param>
    /// <param name="bas">Vecteur que l'on désir écrire dans le plan</param>
    /// <returns>Retourne un vecteur.</returns>
    static public Vecteur ÉcritureVecteurDansUnPlan(Vecteur gauche,Vecteur centre,Vecteur bas)
    {
        float[] résultat = new float[bas.NbDimensions];
        float[,] baseMatrice = new float[gauche.NbDimensions,2];
        float[,] vecteurBas = new float[bas.NbDimensions,1];
        if(EstUnPlan(new Vecteur(gauche.DataVecteur),new Vecteur(centre.DataVecteur))&&gauche.NbDimensions==2)
        {
            for (int i = 0; i < gauche.NbDimensions; i++)
            {
                baseMatrice[i, 0] = gauche[i];
            }
            for (int i = 0; i < centre.NbDimensions; i++)
            {
                baseMatrice[i, 1] = centre[i];
            }
            for (int i = 0; i < bas.NbDimensions; i++)
            {
               vecteurBas[i, 0] = bas[i];
            }
            Matrice baseMatriceInverse = Matrice.MatriceInverse(new Matrice(baseMatrice));
            Matrice matriceRésultat = Matrice.Produit(new Matrice(baseMatriceInverse.DataMatrice), new Matrice(vecteurBas));
            for (int i = 0; i < matriceRésultat.NbLignes; i++)
            {
                résultat[i] = matriceRésultat[i, 0];
            }
        }
        return new Vecteur(résultat);
    }
    /// <summary>
    /// Écris un vecteur dans un espace donné.
    /// </summary>
    /// <param name="gauche">Premier vecteur de l'espace.</param>
    /// <param name="centre">Deuxième vecteur de l'espace.</param>
    /// <param name="droit">Troisième vecteur de l'espace.</param>
    /// <param name="bas">Vecteur que l'on désir écrire dans l'espace.</param>
    /// <returns>Retourne un vecteur.</returns>
    static public Vecteur ÉcritureVecteurDansUnEspace(Vecteur gauche, Vecteur centre,Vecteur droit, Vecteur bas)
    {
        float[] résultat = new float[bas.NbDimensions];
        float[,] baseMatrice = new float[gauche.NbDimensions, 3];
        float[,] vecteurBas = new float[bas.NbDimensions, 1];
        if (EstUnEspace(new Vecteur(gauche.DataVecteur), new Vecteur(centre.DataVecteur),new Vecteur(droit.DataVecteur))&&gauche.NbDimensions==3)
        {
            for (int i = 0; i < gauche.NbDimensions; i++)
            {
                baseMatrice[i, 0] = gauche[i];
            }
            for (int i = 0; i < centre.NbDimensions; i++)
            {
                baseMatrice[i, 1] = centre[i];
            }
            for (int i = 0; i < droit.NbDimensions; i++)
            {
                baseMatrice[i, 2] = droit[i];
            }
            for (int i = 0; i < bas.NbDimensions; i++)
            {
                vecteurBas[i, 0] = bas[i];
            }
            Matrice baseMatriceInverse = Matrice.MatriceInverse(new Matrice(baseMatrice));
            Matrice matriceRésultat = Matrice.Produit(new Matrice(baseMatriceInverse.DataMatrice), new Matrice(vecteurBas));
            for (int i = 0; i < matriceRésultat.NbLignes; i++)
            {
                résultat[i] = matriceRésultat[i, 0];
            }
        }
        return new Vecteur(résultat);
    }
    #endregion;
}
