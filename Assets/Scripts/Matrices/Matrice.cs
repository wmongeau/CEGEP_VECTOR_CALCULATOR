using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Description d'une matrice en C#
/// </summary>
public class Matrice
{
    public const int NB_COLONNES_MAX = 7;
    public const int NB_LIGNES_MAX = 7;

    #region Informations et Constructeur De Matrice
    /// <summary>
    /// Propriété représentant le  nombre de colonnes de la matrice.
    /// </summary>
    public int NbLignes
    {
        get { return dataMatrice.GetLength(0); }
    }
    /// <summary>
    /// Propriété représentant le  nombre de colonnes de la matrice.
    /// </summary>
    public int NbColonnes
    {
        get { return dataMatrice.GetLength(1); }
    }

    float[,] dataMatrice;
    /// <summary>
    ///Propriété représentant la matrice sous forme de tableau
    /// </summary>
    public float[,] DataMatrice
    {
        get
        {
            float[,] nouvelleMatrice = new float[NbLignes, NbColonnes];
            for (int i = 0; i < NbLignes; ++i)
                for (int j = 0; j < NbColonnes; ++j)
                {
                    nouvelleMatrice[i, j] = dataMatrice[i, j];
                }
            return nouvelleMatrice;
        }
        private set
        {
            if ((value != null) && (value.GetLength(0) <= NB_LIGNES_MAX) && (value.GetLength(1) <= NB_COLONNES_MAX))
            {
                dataMatrice = new float[value.GetLength(0), value.GetLength(1)];
                for (int i = 0; i < NbLignes; ++i)
                {
                    for (int j = 0; j < NbColonnes; ++j)
                    {
                        dataMatrice[i, j] = value[i, j];
                    }
                }
            }
        }
    }

    /// <summary>
    /// Retourne la valeur de la matrice à l'indice indiqué en paramètre.
    /// </summary>
    /// <param name="ligne">La ligne de la valeur désirée</param>
    /// <param name="colonne">La colonne de la valeur désirée</param>
    /// <returns>Retourne un float </returns>
    public float this[int ligne, int colonne]
    {
        get
        {
            float valeur;
            if (ligne < 0 || ligne >= NbLignes || colonne < 0 || colonne >= NbColonnes)
            {
                valeur = 0;
            }
            else
            {
                valeur = dataMatrice[ligne, colonne];
            }
            return valeur;
        }
        set
        {
            if (ligne >= 0 && ligne < NbLignes && colonne >= 0 && colonne < NbColonnes)
            {
                dataMatrice[ligne, colonne] = value;
            }
        }
    }

    /// <summary>
    /// Constructeur d'une matrice qui prend en paramètre un tableau de float
    /// </summary>
    /// <param name="tableau">Tableau de floats</param>
    public Matrice(float[,] tableau)
    {
        if (tableau == null)
        {
            DataMatrice = new float[NB_LIGNES_MAX, NB_COLONNES_MAX];
        }
        else if (tableau.GetLength(0) > NB_LIGNES_MAX || tableau.GetLength(1) > NB_COLONNES_MAX)
        {
            float[,] extrait = new float[tableau.GetLength(0), tableau.GetLength(1)];
            for (int i = 0; i < tableau.GetLength(0); i++)
            {
                for (int j = 0; j < tableau.GetLength(1); j++)
                {
                    extrait[i, j] = tableau[i, j];
                }
            }
            DataMatrice = extrait;
        }
        else
        {
            DataMatrice = tableau;
        }
    }
    #endregion

    #region Calculs Et Décomposition De Matrices

    /// <summary>
    /// Fait l'addition de matrices
    /// </summary>
    /// <param name="gauche">La matrice à gauche dans l'éditeur</param>
    /// <param name="droite">La matrice à droite dans l'éditeur</param>
    /// <returns>Une matrice résultante</returns>
    static public Matrice Additionner(Matrice gauche, Matrice droite)
    {
        Matrice résultat = null;
        float[,] dataSomme = new float[gauche.NbLignes, gauche.NbColonnes];
        float[,] dataGauche = gauche.DataMatrice;
        float[,] dataDroite = droite.DataMatrice;
        if (gauche != null && droite != null && gauche.NbColonnes == droite.NbColonnes && gauche.NbLignes == droite.NbLignes)
        {
            for (int i = 0; i < gauche.NbLignes; i++)
            {
                for (int j = 0; j < gauche.NbColonnes; j++)
                {
                    dataSomme[i, j] = dataGauche[i, j] + dataDroite[i, j];
                }
            }
            résultat = new Matrice(dataSomme);
        }
        return résultat;

    }

    /// <summary>
    /// Fait la soustraction de deux matrices
    /// </summary>
    /// <param name="gauche">La matrice à gauche dans l'éditeur</param>
    /// <param name="droite">La matrice à droite dans l'éditeur</param>
    /// <returns>La matrice résultante</returns>
    static public Matrice Soustraction(Matrice gauche, Matrice droite)
    {
        Matrice résultat = null;
        float[,] dataDifférence = new float[gauche.NbLignes, gauche.NbColonnes];
        float[,] dataGauche = gauche.DataMatrice;
        float[,] dataDroite = droite.DataMatrice;
        if (gauche != null && droite != null && gauche.NbColonnes == droite.NbColonnes && gauche.NbLignes == droite.NbLignes)
        {
            for (int i = 0; i < gauche.NbLignes; i++)
            {
                for (int j = 0; j < gauche.NbColonnes; j++)
                {
                    dataDifférence[i, j] = dataGauche[i, j] - dataDroite[i, j];
                }
            }
            résultat = new Matrice(dataDifférence);
        }
        return résultat;

    }

    /// <summary>
    /// Fait le produit par un scalaire d'une matrice
    /// </summary>
    /// <param name="gauche">La matrice à gauche dans l'éditeur</param>
    /// <param name="scalaire">Le scalaire qui fait la multiplication sur la matrice</param>
    /// <returns>La matrice résultante</returns>
    static public Matrice ProduitScalaire(Matrice gauche, float scalaire)
    {
        Matrice résultat = null;
        float[,] dataProduitScalaire = new float[gauche.NbLignes, gauche.NbColonnes];
        float[,] dataGauche = gauche.DataMatrice;

        if (gauche != null)
        {
            for (int i = 0; i < gauche.NbLignes; i++)
            {
                for (int j = 0; j < gauche.NbColonnes; j++)
                {
                    dataProduitScalaire[i, j] = dataGauche[i, j] * scalaire; ;
                }
            }
            résultat = new Matrice(dataProduitScalaire);
        }
        return résultat;

    }

    /// <summary>
    /// Fait une transposition de matrice
    /// </summary>
    /// <param name="gauche">La matrice à gauche dans l'éditeur</param>
    /// <returns>La matrice transposée de gauche</returns>
    static public Matrice Transposée(Matrice gauche)
    {
        Matrice résultat = null;
        float[,] dataTransposée = new float[gauche.NbColonnes, gauche.NbLignes];
        float[,] dataGauche = gauche.DataMatrice;
        if (gauche != null)
        {
            for (int i = 0; i < gauche.NbColonnes; i++)
            {
                for (int j = 0; j < gauche.NbLignes; j++)
                {
                    dataTransposée[i, j] = dataGauche[j, i];
                }
            }
            résultat = new Matrice(dataTransposée);
        }
        return résultat;

    }

    /// <summary>
    /// Fait le produit de deux matrices
    /// </summary>
    /// <param name="gauche">La matrice à gauche dans l'éditeur</param>
    /// <param name="droite">La matrice à droite dans l'éditeur</param>
    /// <returns>La matrice résultante</returns>
    static public Matrice Produit(Matrice gauche, Matrice droite)
    {
        Matrice résultat = null;
        float[,] dataProduit = new float[gauche.NbLignes, droite.NbColonnes];
        float[,] dataGauche = gauche.DataMatrice;
        float[,] dataDroite = droite.DataMatrice;

        if (gauche != null && droite != null && gauche.NbColonnes == droite.NbLignes)
        {
            for (int i = 0; i < gauche.NbLignes; i++)
            {
                for (int j = 0; j < droite.NbColonnes; j++)
                {
                    for (int k = 0; k < gauche.NbColonnes; k++)
                    {
                        dataProduit[i, j] += dataGauche[i, k] * dataDroite[k, j];
                    }
                }
            }
            résultat = new Matrice(dataProduit);
        }
        return résultat;

    }

    /// <summary>
    /// Fait le faux produit de deux matrices
    /// </summary>
    /// <param name="gauche">La matrice à gauche dans l'éditeur</param>
    /// <param name="droite">La matrice à droite dans l'éditeur</param>
    /// <returns>La matrice résultante du calcul</returns>
    static public Matrice FauxProduit(Matrice gauche, Matrice droite)
    {
        Matrice résultat = null;
        float[,] dataFauxProduit = new float[gauche.NbLignes, gauche.NbColonnes];
        float[,] dataGauche = gauche.DataMatrice;
        float[,] dataDroite = droite.DataMatrice;
        if (gauche != null && droite != null && gauche.NbColonnes == droite.NbColonnes && gauche.NbLignes == droite.NbLignes)
        {
            for (int i = 0; i < gauche.NbLignes; i++)
            {
                for (int j = 0; j < gauche.NbColonnes; j++)
                {
                    dataFauxProduit[i, j] = dataGauche[i, j] * dataDroite[i, j];
                }
            }
            résultat = new Matrice(dataFauxProduit);
        }
        return résultat;

    }

    /// <summary>
    /// Choisi que fonction appeler pour calculer le déterminant d'une matrice.
    /// </summary>
    /// <param name="gauche">La matrice à gauche dans l'éditeur</param>
    /// <returns>Le déterminant de la matrice</returns>
    static public float Déterminant(Matrice gauche)
    {
        float déterminant = 0;
        if (gauche.NbLignes == 2 && gauche.NbColonnes == 2)
        {
            déterminant = Déterminant2X2(new Matrice(gauche.DataMatrice));
        }
        else if (gauche.NbLignes == 3 && gauche.NbColonnes == 3)
        {
            déterminant = Déterminant3X3(new Matrice(gauche.DataMatrice));
        }
        else
        {
            déterminant = DéterminantNXN(new Matrice(gauche.DataMatrice));
        }
        return déterminant;
    }

    /// <summary>
    /// Le déterminant d'une matrice 2X2
    /// </summary>
    /// <param name="gauche">La matrice à gauche dans l'éditeur</param>
    /// <returns>Le déterminant</returns>
    static public float Déterminant2X2(Matrice gauche)
    {
        float déterminant;
        déterminant = gauche[0, 0] * gauche[1, 1] - gauche[0, 1] * gauche[1, 0];
        return déterminant;
    }

    /// <summary>
    /// Le determinant 3X3 d'une matrice
    /// </summary>
    /// <param name="gauche">La matrice à gauche dans l'éditeur</param>
    /// <returns>Le determinant de la matrice gauche</returns>
    static public float Déterminant3X3(Matrice gauche)
    {
        float déterminant = 0;
        int k = 1;
        Matrice matriceRéduite = null;
        for (int i = 0; i < gauche.NbColonnes; i++)
        {
            if (i % 2 != 0)
            {
                k = -1;
            }
            else
            {
                k = 1;
            }
            matriceRéduite = Réduire(0, i, new Matrice(gauche.DataMatrice));
            déterminant += Déterminant2X2(new Matrice(matriceRéduite.DataMatrice)) * gauche[0, i] * k;
        }
        return déterminant;
    }

    /// <summary>
    /// Calcule le determinant d'une matrice de taille NXN. Est très long pour une matrice 7X7
    /// </summary>
    /// <param name="gauche">La matrice à gauche dans l'éditeur</param>
    /// <returns>Le déterminant de la matrice</returns>
    static public float DéterminantNXN(Matrice gauche)
    {
        float déterminant = 0;
        int négatif = 1;
        Matrice matriceRéduite = null;
        for (int i = 0; i < gauche.NbLignes; i++)
        {
            for (int j = 0; j < gauche.NbColonnes; j++)
            {
                if ((i + j) % 2 != 0)
                {
                    négatif = -1;
                }
                else
                {
                    négatif = 1;
                }
                matriceRéduite = Réduire(i, j, new Matrice(gauche.DataMatrice));
                déterminant += gauche[0, j] * (Déterminant(new Matrice(matriceRéduite.DataMatrice)) * négatif);
            }
        }
        return déterminant;
    }

    /// <summary>
    /// Réduit la matrice d'une ligne et d'une colonne
    /// </summary>
    /// <param name="ligne">La ligne à réduire de la matrice</param>
    /// <param name="colonne">La colonne è réduire de la matrice</param>
    /// <param name="matrice">La matrice à réduire</param>
    /// <returns>La matrice réduite</returns>
    public static Matrice Réduire(int ligne, int colonne, Matrice matrice)
    {
        float[,] resultat = new float[matrice.NbLignes - 1, matrice.NbColonnes - 1];
        float[,] tableauMatrice = matrice.DataMatrice;

        for (int i = 0, j = 0; i < tableauMatrice.GetLength(0); i++)
        {
            if (i == ligne)
                continue;

            for (int k = 0, u = 0; k < tableauMatrice.GetLength(1); k++)
            {
                if (k == colonne)
                    continue;

                resultat[j, u] = tableauMatrice[i, k];
                u++;
            }
            j++;
        }
        Matrice matriceRéduite = new Matrice(resultat);
        return matriceRéduite;
    }

    /// <summary>
    /// Fait l'inverse de la matrice par la méthode de l'adjointe
    /// </summary>
    /// <param name="gauche">La matrice à gauche dans l'éditeur</param>
    /// <returns>La matrice inverse</returns>
    public static Matrice MatriceInverse(Matrice gauche)
    {

        float déterminant;
        float[,] tableauGauche = gauche.DataMatrice;
        float[,] matriceInverse = new float[gauche.NbLignes, gauche.NbColonnes];
        Matrice matriceInversée = new Matrice(matriceInverse);
        if (Déterminant(gauche) != 0)
        {
            if (gauche.NbLignes == 2 && gauche.NbLignes == 2)
            {
                float[,] gaucheAdjointe = new float[tableauGauche.GetLength(0), tableauGauche.GetLength(1)];
                gaucheAdjointe[0, 0] = tableauGauche[1, 1];
                gaucheAdjointe[1, 1] = tableauGauche[0, 0];
                gaucheAdjointe[1, 0] = -tableauGauche[1, 0];
                gaucheAdjointe[0, 1] = -tableauGauche[0, 1];
                déterminant = 1 / Déterminant(new Matrice(gauche.DataMatrice));
                matriceInversée = ProduitScalaire(new Matrice(gaucheAdjointe), déterminant);
            }
            else
            {
                Matrice cofacteur = FaireMatriceCofacteur(new Matrice(tableauGauche));
                Matrice adjointe = Transposée(new Matrice(cofacteur.DataMatrice));
                déterminant = 1 / Déterminant(new Matrice(gauche.DataMatrice));

                matriceInversée = ProduitScalaire(new Matrice(adjointe.DataMatrice), déterminant);
            }
        }
        for (int i = 0; i < gauche.NbLignes; i++)
        {
            for (int j = 0; j < gauche.NbColonnes; j++)
            {
                if ((i + j) % 2 != 0)
                {
                    matriceInversée[i, j] = matriceInversée[i, j] * -1;
                }
            }
        }

        return matriceInversée;
    }

    /// <summary>
    /// Fait la matrice des cofacteurs de la matrice
    /// </summary>
    /// <param name="gauche">La matrice à gauche dans l'éditeur</param>
    /// <returns>La matrice de cofacteurs</returns>
    static public Matrice FaireMatriceCofacteur(Matrice gauche)
    {
        float[,] indice = new float[gauche.NbLignes, gauche.NbColonnes];
        for (int i = 0; i < gauche.NbLignes; i++)
        {
            for (int j = 0; j < gauche.NbColonnes; j++)
            {
                Matrice matriceRéduite = Réduire(i, j, new Matrice(gauche.DataMatrice));
                indice[i, j] = Déterminant(new Matrice(matriceRéduite.DataMatrice));
            }
        }
        Matrice matriceCofacteur = new Matrice(indice);
        return matriceCofacteur;
    }

    /// <summary>
    /// Fait la décomposition L de la matrice
    /// </summary>
    /// <param name="gauche">La matrice dans l'éditeur</param>
    /// <returns>La matrice inférieure L</returns>
    static public Matrice DecompositionL(Matrice gauche)
    {
        float[,] L = new float[gauche.NbLignes, gauche.NbColonnes];
        float[,] tableauGauche = gauche.DataMatrice;
        float déterminant = Déterminant(new Matrice(tableauGauche));
        if (déterminant != 0 && gauche.NbColonnes == 3 && gauche.NbLignes == 3)
        {
            float[] diviseurs = new float[3];
            diviseurs[0] = tableauGauche[1, 0] / tableauGauche[0, 0];
            diviseurs[1] = tableauGauche[2, 0] / tableauGauche[0, 0];
            diviseurs[2] = (tableauGauche[2, 1] - diviseurs[1] * tableauGauche[0, 1]) / (tableauGauche[1, 1] - diviseurs[0] * tableauGauche[0, 1]);
            for (int i = 0; i < gauche.NbColonnes; i++)
            {
                for (int j = 0; j < gauche.NbLignes; j++)
                {
                    if (i == j)
                    {
                        L[i, j] = 1;
                    }
                    else if (i < j)
                    {
                        L[i, j] = 0;
                    }

                }
            }
            L[1, 0] = diviseurs[0];
            L[2, 0] = diviseurs[1];
            L[2, 1] = diviseurs[2];
        }
        return new Matrice(L);
    }

    /// <summary>
    /// Fait la décomposition U de la matrice
    /// </summary>
    /// <param name="gauche">La matrice dans l'éditeur</param>
    /// <returns>La matrice supérieure de la matrice</returns>
    static public Matrice DecompositionU(Matrice gauche)
    {
        float[,] U = new float[gauche.NbLignes, gauche.NbColonnes];
        float[,] tableauGauche = gauche.DataMatrice;
        float déterminant = Déterminant(new Matrice(tableauGauche));
        if (déterminant != 0 && gauche.NbColonnes == 3 && gauche.NbLignes == 3)
        {
            float[] diviseurs = new float[3];
            diviseurs[0] = tableauGauche[1, 0] / tableauGauche[0, 0];
            diviseurs[1] = tableauGauche[2, 0] / tableauGauche[0, 0];
            diviseurs[2] = (tableauGauche[2, 1] - diviseurs[1] * tableauGauche[0, 1]) / (tableauGauche[1, 1] - diviseurs[0] * tableauGauche[0, 1]);
            U[0, 0] = tableauGauche[0, 0];
            U[1, 1] = tableauGauche[1, 1] - diviseurs[0] * tableauGauche[0, 1];
            U[2, 2] = tableauGauche[2, 2] - diviseurs[1] * tableauGauche[0, 2] - diviseurs[2] * (tableauGauche[1, 2] - diviseurs[0] * tableauGauche[0, 2]);
            U[1, 0] = 0;
            U[2, 0] = 0;
            U[2, 1] = 0;
            U[0, 1] = tableauGauche[0, 1];
            U[0, 2] = tableauGauche[0, 2];
            U[1, 2] = tableauGauche[1, 2] - diviseurs[0] * tableauGauche[0, 2];
        }
        return new Matrice(U);
    }

    /// <summary>
    /// Fait la méthode de Gauss pour résoudre un SEL
    /// </summary>
    /// <param name="matrice">La matrice dans l'éditeur</param>
    /// <returns>La matrice échelonnée selon Gauss</returns>
    static public Matrice MéthodeGauss(Matrice matrice)
    {
        Matrice matriceRésultat = new Matrice(matrice.DataMatrice);

        for (int i = 0; i < matrice.NbColonnes; i++)
        {
            if (matrice[0, 0] == 0)
            {
                Matrice tampon = new Matrice(matrice.DataMatrice);
                Matrice nouvelle = new Matrice(matrice.DataMatrice);

                for (int k = 0; k < matrice.NbColonnes; k++)
                {
                    nouvelle[0, k] = matrice[1, k];
                    nouvelle[1, k] = matrice[0, k];
                }
                matrice = nouvelle;
            }
            matriceRésultat[1, i] = matrice[0, 0] * matrice[1, i] - matrice[1, 0] * matrice[0, i];
        }

        if (matrice.NbLignes == 2 && matrice.NbColonnes == 3)
        {
            if (matrice[1, 1] != 0)
            {
                matriceRésultat[1, 2] = matriceRésultat[1, 2] / matriceRésultat[1, 1];
                matriceRésultat[1, 1] = 1;
            }
        }
        else if (matrice.NbLignes == 3 && matrice.NbColonnes == 3)
        {
            for (int i = 0; i < matrice.NbColonnes; i++)
            {
                matriceRésultat[2, i] = matrice[0, 0] * matrice[2, i] - matrice[2, 0] * matrice[0, i];
            }
        }
        else if (matrice.NbLignes == 3 && matrice.NbColonnes == 4)
        {
            for (int i = 0; i < matrice.NbColonnes; i++)
            {
                matriceRésultat[2, i] = matrice[0, 0] * matrice[2, i] - matrice[2, 0] * matrice[0, i];
            }
            float indice = matriceRésultat[2, 1];
            for (int i = 0; i < matrice.NbColonnes; i++)
            {
                matriceRésultat[2, i] = matriceRésultat[1, 1] * matriceRésultat[2, i] - indice * matriceRésultat[1, i];
            }
            if (matriceRésultat[2, 2] != 0)
            {
                matriceRésultat[2, 3] = matriceRésultat[2, 3] / matriceRésultat[2, 2];
                matriceRésultat[2, 2] = 1;
            }
        }
        else if (matrice.NbLignes == 2 && matrice.NbColonnes == 4)
        {
            if (matriceRésultat[1, 1] != 0)
            {
                matriceRésultat[1, 3] = matriceRésultat[1, 3] / matriceRésultat[1, 1];
                matriceRésultat[1, 2] = matriceRésultat[1, 2] / matriceRésultat[1, 1];
                matriceRésultat[1, 1] = 1;
            }
        }
        return matriceRésultat;
    }

    /// <summary>
    /// Donne l'équation du SEL résolu
    /// </summary>
    /// <param name="matrice">La matrice dans l'éditeur</param>
    /// <returns>Une phrase qui contient la valeur des variables</returns>
    static public string ÉquationGauss(Matrice matrice)
    {
        string réponse = "";
        if (matrice.NbLignes == 2 && matrice.NbColonnes == 3)
        {
            if (matrice[1, 1] == 0 && matrice[1, 2] != 0)
            {
                réponse = "Il n'a pas de solution pour ce sytème";
            }
            else if (matrice[1, 1] != 0 && matrice[1, 2] != 0)
            {
                réponse = "X = " + String.Format("{0:0.00}", (matrice[0, 2] - matrice[1, 2] * matrice[0, 1]) / matrice[0, 0])
                    + ", Y = " + String.Format("{0:0.00}", matrice[1, 2]);
            }
            else
            {
                réponse = "X = (" + String.Format("{0:0.00}", matrice[0, 2]) + "-" + String.Format("{0:0.00}", matrice[0, 1]) + "t/" +
                    String.Format("{0:0.00}", matrice[0, 0]) + ") , Y=t | t est élément des réels";
            }
        }
        else if(matrice.NbLignes == 3 && matrice.NbColonnes == 3)
        {
            if (matrice[1, 1] != 0 && matrice[2, 1] != 0)
            {
                if (matrice[1, 2] / matrice[1, 1] == matrice[2, 2] / matrice[2, 1])
                {
                    float y = matrice[1, 2] / matrice[1, 1];
                    float x = (matrice[0, 2] - (y * matrice[0, 1])) / matrice[0, 0];
                    réponse = "X =" + String.Format("{0:0.00}", x) + ", Y=" + String.Format("{0:0.00}", y);
                }
                else
                    réponse = "Il n'y a aucune solution pour ce système";
            }
            else if (matrice[1, 1] != 0 && matrice[2, 1] == 0 && matrice[2, 2] == 0)
            {
                float y = matrice[1, 2] / matrice[1, 1];
                float x = (matrice[0, 2] - (y * matrice[0, 1])) / matrice[0, 0];
                réponse = "X = " + String.Format("{0:0.00}", x) + ", Y = " + String.Format("{0:0.00}", y);
            }
            else if (matrice[1, 1] == 0 && matrice[2, 1] != 0 && matrice[2, 2] == 0)
            {
                float y = matrice[2, 2] / matrice[2, 1];
                float x = (matrice[0, 2] - (y * matrice[0, 1])) / matrice[0, 0];
                réponse = "X = " + String.Format("{0:0.00}", x) + ", Y = " + String.Format("{0:0.00}", y);
            }
            else
            {
                réponse = "X = (" + String.Format("{0:0.00}", matrice[0, 2]) + "-" + String.Format("{0:0.00}", matrice[0, 1]) + "t/" +
                    String.Format("{0:0.00}", matrice[0, 0]) + " ), Y = t | t est élément des réels";
            }

        }
        else if (matrice.NbLignes == 3 && matrice.NbColonnes == 4)
        {
            if (matrice[2, 2] == 0 && matrice[2, 3] != 0)
            {
                réponse = "Il n'a pas de solution pour ce système";
            }
            else if (matrice[1, 1] != 0 && matrice[1, 2] != 0)
            {
                float z = matrice[2, 3];
                float y = (matrice[1, 3] - matrice[1, 2] * z) / matrice[1, 1];
                float x = (matrice[0, 3] - matrice[0, 2] * z - matrice[0, 1] * y) / matrice[0, 0];
                réponse = "X = " + String.Format("{0:0.00}", x) + ", Y = " + String.Format("{0:0.00}", y) + ", Z = " + String.Format("{0:0.00}", z);
            }
            else
            {
                string z = " t";
                string y = " ("+String.Format("{ 0:0.00} ", matrice[1, 3]) + "-" +
                    String.Format("{0:0.00}", matrice[1, 2]) + z + ")/" +
                    String.Format("{0:0.00}", matrice[1, 1]);
                string x = " (" + String.Format("{ 0:0.00} ", matrice[0, 3]) + "-" +
                    String.Format("{0:0.00}", matrice[0, 1]) + y + "-" +
                    String.Format("{0:0.00}", matrice[0, 2]) + z + ")/" +
                    String.Format("{0:0.00}", matrice[0, 0]);
                réponse = "X = " + x + ", Y = " + y + ", Z = " + z + " | t est élément des réels";
            }
        }
        else
        {
            string z = " t";
            string y = " ("+ String.Format("{0:0.00} ", matrice[1, 3]) + "-" +
                    String.Format("{0:0.00}", matrice[1, 2]) + z + ")/" +
                    String.Format("{0:0.00}", matrice[1, 1]);
            string x = " ("+String.Format("{0:0.00} ", matrice[0, 3]) + "-" +
                    String.Format("{0:0.00}", matrice[0, 1]) + " * " + y + "-" +
                    String.Format("{0:0.00}", matrice[0, 2]) + z + ")/" +
                    String.Format("{0:0.00}", matrice[0, 0]);
            réponse = "X = " + x + ", Y = " + y + ", Z = " + z + " | t est élément des réels";
        }
        return réponse;
    }

    /// <summary>
    /// Résous le SEL par la méthode de la matrice inverse
    /// </summary>
    /// <param name="matrice">La matrice dans l'éditeur</param>
    /// <returns>La matrice qui contient les valeurs des variables</returns>
    static public Matrice InverseSEL(Matrice matrice)
    {
        float[,] matriceCoefficient = new float[0, 0];
        float[,] matriceRésultats = new float[0, 0];
        float[,] matriceTableau = matrice.DataMatrice;
        if (matrice.NbColonnes == 3 && matrice.NbLignes == 2)
        {
            matriceRésultats = new float[2, 1];
            matriceCoefficient = new float[2, 2];
            for (int i = 0; i < matrice.NbLignes; i++)
            {
                for (int j = 0; j < matrice.NbColonnes; j++)
                {
                    if (j == 2)
                    {
                        matriceRésultats[i, 0] = matriceTableau[i, j];
                    }
                    else
                    {
                        matriceCoefficient[i, j] = matriceTableau[i, j];
                    }
                }
            }
        }
        else if (matrice.NbColonnes == 4 && matrice.NbLignes == 3)
        {
            matriceRésultats = new float[3, 1];
            matriceCoefficient = new float[3, 3];
            for (int i = 0; i < matrice.NbLignes; i++)
            {
                for (int j = 0; j < matrice.NbColonnes; j++)
                {
                    if (j == 3)
                    {
                        matriceRésultats[i, 0] = matriceTableau[i, j];
                    }
                    else
                    {
                        matriceCoefficient[i, j] = matriceTableau[i, j];
                    }
                }
            }
        }

        Matrice matriceInverse = MatriceInverse(new Matrice(matriceCoefficient));
        Matrice résultat = Produit(new Matrice(matriceInverse.DataMatrice), new Matrice(matriceRésultats));
        return résultat;
    }

    #endregion

}
