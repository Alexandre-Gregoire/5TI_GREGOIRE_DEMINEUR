using System;

namespace UAA9_Demineur_Alexandre_2023
{
    class Program
    {
        public static ConsoleColor ForegroundColor { get; set; }
        static void Main(string[] args)
        {
            /*
            affichage : 
            -1 = non decouvert
            -2 = drapeaux
            mine = 9
            0 = 0
            1 = 1
            2 = 2
            etc
             
             
            
            
            
             */




            bool aymeric = true;
            int nbLigne = 10;
            int nbColonne = 10;
            int nbMine = 10;
            int entreeLi = 5;
            int entreeCol = 5;
            int[,] MineT;
            int[,] affichage = new int[nbLigne, nbColonne];
            int[,] GrilleAvecFleche = new int[nbLigne, nbColonne];
            bool drapeau = false;
            bool GameOver = false;
            for (int i = 0; i < nbLigne; i++)
            {
                for (int y = 0; y < nbColonne; y++)
                {
                    affichage[i, y] = -1;
                    GrilleAvecFleche[i, y] = 0;
                }
            }
            GrilleAvecFleche[5, 5] = 1;
            genererGrille(nbLigne, nbColonne, nbMine, out MineT);
            while (!GameOver)
            {
                while (aymeric)
                {
                    deplacement(MineT, out entreeLi, out entreeCol, ref GrilleAvecFleche, nbLigne, nbColonne, out aymeric, ref affichage, out drapeau);

                }

                if (drapeau == true)
                {
                    while (true)
                    {
                        Console.WriteLine("caca");
                    }
                    affichage[entreeLi, entreeCol] = -2;
                }
                else if (MineT[entreeLi, entreeCol] == 1 || MineT[entreeLi, entreeCol] == 2 || MineT[entreeLi, entreeCol] == 3 || MineT[entreeLi, entreeCol] == 4 || MineT[entreeLi, entreeCol] == 5 || MineT[entreeLi, entreeCol] == 6 || MineT[entreeLi, entreeCol] == 7 || MineT[entreeLi, entreeCol] == 8)
                {
                    affichage[entreeLi, entreeCol] = MineT[entreeLi, entreeCol];
                }
                else if (MineT[entreeLi, entreeCol] == 9)
                {
                    affichage[entreeLi, entreeCol] = 9;
                    Console.WriteLine("perdu");
                    GameOver = true;
                }
                else if (MineT[entreeLi, entreeCol] == 9)
                {
                    affichage[entreeLi, entreeCol] = MineT[entreeLi, entreeCol];
                }
                else if (affichage[entreeLi, entreeCol] == -1 && MineT[entreeLi, entreeCol] == 0)
                {
                    RevelerGrille(entreeLi, entreeCol, nbLigne, nbColonne, MineT, ref affichage);
                }
                drapeau = false;
                aymeric = true;

            }

            static void affichageMatrices(int[,] Matrices)
            {
                for (int i = 0; i < Matrices.GetLength(0); i++)
                {
                    for (int y = 0; y < Matrices.GetLength(1); y++)
                    {
                        Console.Write(Matrices[i, y] + "|");
                    }
                    Console.WriteLine();
                }
            }



            static void ecritureGrille(int[,] affichage, int[,] deplacement, int nbLigne, int nbColonne)
            {
                ConsoleColor[] couleursChiffre = new ConsoleColor[11];
                couleursChiffre[1] = ConsoleColor.Blue;
                couleursChiffre[2] = ConsoleColor.Green;
                couleursChiffre[3] = ConsoleColor.Red;
                couleursChiffre[4] = ConsoleColor.DarkBlue;
                couleursChiffre[5] = ConsoleColor.DarkRed;
                couleursChiffre[6] = ConsoleColor.Cyan;
                couleursChiffre[7] = ConsoleColor.Black;
                couleursChiffre[8] = ConsoleColor.Gray;
                couleursChiffre[10] = ConsoleColor.Red; //Drapeau


                for (int y = 0; y < nbLigne; y++)
                {


                    for (int i = 0; i < nbColonne; i++)
                    {
                        //----------Haut de la case----------
                        //Couleur de la case
                        Console.ForegroundColor = ConsoleColor.Red;
                        if (deplacement[y, i] != 1)
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                        }


                        if (y != 0)
                        {
                            if (deplacement[y - 1, i] == 1)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                            }
                        }
                        if (i != 0 && y != 0)
                        {
                            if (deplacement[y, i - 1] == 1 || deplacement[y - 1, i - 1] == 1)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                            }
                        }
                        Console.Write("+");
                        if (i != 0 && y != 0)
                        {
                            if (deplacement[y, i - 1] == 1 || deplacement[y - 1, i - 1] == 1)
                            {
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                        }
                        Console.Write("---");

                        if (i == nbColonne - 1)
                        {
                            if (i != 0 && y != 0)
                            {
                                if (deplacement[y, i] == 1)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                }

                            }

                            Console.Write("+");
                        }
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Console.WriteLine("");
                    for (int i = 0; i < affichage.GetLength(1); i++)
                    {
                        //----------Millieux de la case----------
                        //-Couleur de la case : 
                        if (deplacement[y, i] == 1)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }
                        if (i != 0)
                        {
                            if (deplacement[y, i - 1] == 1)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                            }
                        }

                        Console.Write("|");
                        //-Couleur du chiffre
                        Console.ForegroundColor = ConsoleColor.White;
                        if (affichage[y, i] > 0)
                        {
                            Console.ForegroundColor = couleursChiffre[affichage[y, i]];
                        }
                        if (affichage[y, i] == -2)
                        {
                            Console.Write(" F ");
                        }
                        else if (affichage[y, i] == 0)
                        {
                            Console.Write("   ");
                        }
                        else
                        {
                            Console.Write(String.Format("{0,3}", affichage[y, i] + " "));
                        }

                        //Couleur de la case

                        if (deplacement[y, i] == 1)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }

                        if (i == nbLigne - 1)
                        {
                            if (i != 0 && y != 0)
                            {
                                if (deplacement[y, i] == 1)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                }

                            }

                            Console.Write("|");
                        }
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Console.WriteLine("");
                    if (y == nbLigne - 1)
                    {
                        for (int i = 0; i < nbColonne; i++)
                        {
                            if (i != 0 && y != 0)
                            {
                                if (deplacement[y, i] == 1 || deplacement[y, i - 1] == 1)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;

                                }
                            }
                            Console.Write("+");
                            if (i != 0 && y != 0)
                            {
                                if (deplacement[y, i - 1] == 1 || deplacement[y, i - 1] == 1)
                                {
                                    Console.ForegroundColor = ConsoleColor.White;
                                }
                            }
                            Console.Write("---");
                            Console.ForegroundColor = ConsoleColor.White;
                            if (i == nbColonne - 1)
                            {

                                if (deplacement[y, i] == 1)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                }



                            }
                        }

                        Console.WriteLine("+");
                        Console.ForegroundColor = ConsoleColor.White;
                    }


                }



            }

            static void genererGrille(int nbLigne, int nbColonne, int nbMine, out int[,] MineT)
            {
                Random alea = new Random();
                MineT = new int[nbLigne, nbColonne];
                int MineCol;
                int MineLi;
                int departCol = -1;
                int departLi = -1;
                int finCol = 2;
                int finLi = 2;
                for (int i = 0; i < nbMine; i++)
                {
                    do
                    {
                        MineCol = alea.Next(0, nbColonne);
                        MineLi = alea.Next(0, nbLigne);
                    } while (MineT[MineLi, MineCol] == 9);
                    MineT[MineLi, MineCol] = 9;
                }
                for (int ligne = 0; ligne < nbLigne; ligne++)
                {
                    for (int colonne = 0; colonne < nbColonne; colonne++)
                    {

                        if (ligne == 0)
                        {
                            departLi = 0;
                        }
                        if (ligne == nbLigne - 1)
                        {
                            finLi = 1;
                        }
                        if (colonne == 0)
                        {
                            departCol = 0;
                        }
                        if (colonne == nbColonne - 1)
                        {
                            finCol = 1;
                        }
                        if (MineT[ligne, colonne] == 9)
                        {

                            for (int ligneB = departLi; ligneB < finLi; ligneB++)
                            {
                                for (int colonneB = departCol; colonneB < finCol; colonneB++)
                                {
                                    if ((ligneB != 0 || colonneB != 0) && MineT[ligne + ligneB, colonne + colonneB] != 9)
                                    {
                                        MineT[ligne + ligneB, colonne + colonneB] += 1;
                                    }
                                }
                            }
                        }
                        departCol = -1;
                        departLi = -1;
                        finCol = 2;
                        finLi = 2;

                    }
                }


            }
            static void RevelerGrille(int entreeLi, int entreeCol, int nbLigne, int nbColonne, int[,] MineT, ref int[,] affichage)
            {

                int departCol = -1;
                int departLi = -1;
                int finCol = 2;
                int finLi = 2;
                if (entreeLi == 0)
                {
                    departLi = 0;
                }
                if (entreeLi == nbLigne - 1)
                {
                    finLi = 1;
                }
                if (entreeCol == 0)
                {
                    departCol = 0;
                }
                if (entreeCol == nbColonne - 1)
                {
                    finCol = 1;
                }
                for (int ligne = departLi; ligne < finLi; ligne++)
                {
                    for (int colonne = departCol; colonne < finCol; colonne++)
                    {
                        if ((ligne != 0 || colonne != 0) && MineT[entreeLi + ligne, entreeCol + colonne] == 0 && affichage[entreeLi + ligne, entreeCol + colonne] == -1)
                        {
                            affichage[entreeLi + ligne, entreeCol + colonne] = MineT[entreeLi + ligne, entreeCol + colonne];
                            RevelerGrille(entreeLi + ligne, entreeCol + colonne, nbLigne, nbColonne, MineT, ref affichage);

                        }
                    }
                }
                for (int ligne = departLi; ligne < finLi; ligne++)
                {
                    for (int colonne = departCol; colonne < finCol; colonne++)
                    {
                        if ((ligne != 0 || colonne != 0) && affichage[entreeLi + ligne, entreeCol + colonne] == -1)
                        {
                            affichage[entreeLi + ligne, entreeCol + colonne] = MineT[entreeLi + ligne, entreeCol + colonne];
                        }
                    }
                }

            }
            static void deplacement(int[,] MineT, out int entreeLi, out int entreeCol, ref int[,] GrilleAvecFleche, int nbLi, int nbCol, out bool aymeric, ref int[,] affichage, out bool drapeau)
            {
                drapeau = false;
                entreeLi = 0;
                entreeCol = 0;
                ConsoleKeyInfo cki;
                int testi = 0;
                int testy = 0;
                aymeric = true;
                bool testSiTouchePresse = true;

                Console.Clear();
                ecritureGrille(affichage, GrilleAvecFleche, nbLi, nbCol);

                cki = Console.ReadKey(true);

                while (testSiTouchePresse)
                {
                    if (cki.Key == ConsoleKey.Enter)
                    {
                        for (int i = 0; i < nbLi; i++)
                        {
                            for (int y = 0; y < nbCol; y++)
                            {
                                if (GrilleAvecFleche[i, y] == 1 && i != 0)
                                {
                                    entreeLi = i;
                                    entreeCol = y;
                                    aymeric = false;
                                    testSiTouchePresse = false;
                                }
                            }
                        }

                    }

                    if (cki.Key == ConsoleKey.UpArrow)
                    {
                        for (int i = 0; i < nbLi; i++)
                        {
                            for (int y = 0; y < nbCol; y++)
                            {
                                if (GrilleAvecFleche[i, y] == 1 && i != 0)
                                {
                                    GrilleAvecFleche[i - 1, y] = 1;
                                    GrilleAvecFleche[i, y] = 0;
                                    testSiTouchePresse = false;
                                }
                            }
                        }

                    }

                    if (cki.Key == ConsoleKey.DownArrow)
                    {
                        for (int i = 0; i < nbLi; i++)
                        {
                            for (int y = 0; y < nbCol; y++)
                            {
                                if (GrilleAvecFleche[i, y] == 1 && i != 19)
                                {

                                    GrilleAvecFleche[i, y] = 0;
                                    testi = i;
                                    testy = y;
                                    testSiTouchePresse = false;
                                }
                            }

                        }
                        GrilleAvecFleche[testi + 1, testy] = 1;
                    }
                    if (cki.Key == ConsoleKey.RightArrow)
                    {
                        for (int i = 0; i < nbLi; i++)
                        {
                            for (int y = 0; y < nbCol; y++)
                            {
                                if (GrilleAvecFleche[i, y] == 1 && y != 19)
                                {

                                    GrilleAvecFleche[i, y] = 0;
                                    testi = i;
                                    testy = y;
                                    testSiTouchePresse = false;
                                }
                            }

                        }
                        GrilleAvecFleche[testi, testy + 1] = 1;
                    }
                    if (cki.Key == ConsoleKey.LeftArrow)
                    {
                        for (int i = 0; i < nbLi; i++)
                        {
                            for (int y = 0; y < nbCol; y++)
                            {
                                if (GrilleAvecFleche[i, y] == 1 && y != 0)
                                {
                                    GrilleAvecFleche[i, y - 1] = 1;
                                    GrilleAvecFleche[i, y] = 0;
                                    testSiTouchePresse = false;
                                }
                            }
                        }

                    }
                    if (cki.Key == ConsoleKey.F)
                    {
                        drapeau = true;

                        testSiTouchePresse = false;

                    }
                }







            }

        }

    }
}
