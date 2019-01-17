using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_1
{
    class Program
    {
        public static bool lancerPieceMonnaie()
        {
            Random rnd = new Random();
            string pieceMonnaie;
            int facePiece = 0, facePieceRnd;
            do //Boucle do while pour vérifier la validité de l'entrée utilisateur (Pile ou Face)
            {
                Console.WriteLine("\nVeuillez choisir un côté : Pile ou Face ?");
                pieceMonnaie = Console.ReadLine();
                if (pieceMonnaie != "Pile" && pieceMonnaie != "pile" && pieceMonnaie != "Face" && pieceMonnaie != "face") Console.WriteLine("\nCommande invalide.");
            }
            while (pieceMonnaie != "Pile" && pieceMonnaie != "pile" && pieceMonnaie != "Face" && pieceMonnaie != "face");
            //Si l'entrée utilisateur est Pile renvoie 0 si c'est Face renvoie 1 puis génère un nombre aléatoire entre 0 et 1 et compare les deux. Si pareil ==> Utilisateur joue en premier sinon joue en second.
            if (pieceMonnaie == "Pile" && pieceMonnaie == "pile") facePiece = 0;
            else if (pieceMonnaie == "Face" && pieceMonnaie == "face") facePiece = 1;
            facePieceRnd = rnd.Next(0, 2);
            if (facePiece == facePieceRnd)
            {
                Console.WriteLine("\nVous jouez en premier.");
                return true;
            }
            else
            {
                Console.WriteLine("\nVous jouez en deuxième.");
                return false;
            }

        }
        /// <summary>
        /// Affiche les pièces disponibles
        /// </summary>
        /// <param name="piece"></param>
        /// <returns></returns>
        public static string affichePiece(string piece)
        {
            Console.OutputEncoding = Encoding.GetEncoding("unicode");
            switch (piece.ToUpper()) //Encodage de chaque modèle graphique de pièce
            {
                case "BCGV":
                    piece = ("[[O]]");
                    break;

                case "BCGE":
                    piece = ("[[▄]]");
                    break;
                case "BCPV":
                    piece = (" [O] ");
                    break;
                case "BCPE":
                    piece = (" [▄] ");
                    break;
                case "BRGV":
                    piece = ("((O))");
                    break;
                case "BRGE":
                    piece = ("((▄))");
                    break;
                case "BRPV":
                    piece = (" (O) ");
                    break;
                case "BRPE":
                    piece = (" (▄) ");
                    break;
                case "NCGV":
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    piece = ("[[O]]");
                    break;
                case "NCGE":
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    piece = ("[[▄]]");
                    break;
                case "NCPV":
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    piece = (" [O] ");
                    break;
                case "NCPE":
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    piece = (" [▄] ");
                    break;
                case "NRGV":
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    piece = ("((O))");
                    break;
                case "NRGE":
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    piece = ("((▄))");
                    break;
                case "NRPV":
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    piece = (" (O) ");
                    break;
                case "NRPE":
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    piece = (" (▄) ");
                    break;
            }
                return piece;
        }

        public static void afficheTableau(string[] tableau)
        {
            Console.Write("[ " + tableau[0]);
            for (int i = 1; i < tableau.Length - 1; i++)
            {
                Console.Write(", " + tableau[i]);
            }
            Console.WriteLine(", " + tableau[tableau.Length - 1] + " ]");
        }

        public static string choisirPiece(ref string[] Pieces)
        {
            int ind = 0;
            string pieceChoisie;
            bool isin = false; //Variable permettant de savoir si une pièce est disponible
            do
            {
                Console.WriteLine("\nVeuillez choisir une pièce à donner à l'ordinateur parmi celles restantes :");
                afficheTableau(Pieces);
                pieceChoisie = Console.ReadLine().ToUpper(); //Demande une pièce au joueur
                for (int i = 0; i < Pieces.Length; i++) //Cherche si celle-ci se trouve dans les pièces disponibles
                {
                    if (pieceChoisie.ToUpper() == Pieces[i])
                    {
                        isin = true;
                        ind = i;
                    }
                }
                if (pieceChoisie.ToUpper() == "QUARTO")
                {
                    if (QuartoPos() == true)
                        return "win";
                }
                if (isin == false && pieceChoisie.ToUpper()!="QUARTO")
                    Console.WriteLine("La commande est invalide ou la pièce a déjà été jouée, veuillez recommencer.");
                if (isin == false && pieceChoisie.ToUpper() == "QUARTO")
                    Console.WriteLine("Le QUARTO est invalide, veuillez rééssayer ou choisir une pièce valide.");
            } while (isin == false);
            string[] nvtab = new string[Pieces.Length - 1]; //Retire la pièce des pièces disponibles
            int j = 0;
            for(int i=0;i<nvtab.Length; i++)
            {
                if (i == ind)
                    j++;
                nvtab[i] = Pieces[j];
                j++;
            }
            Pieces = nvtab;
            return pieceChoisie;
        }

        public static void affichePlateau(string[,] Plateau)
        /// <summary>
        /// Affiche le plateau de jeu ainsi que les pièces disposées sur celui-ci
        /// </summary>
        /// <param name="Plateau"></param>
        {
            Console.WriteLine("\n       A       B       C       D");
            for (int i=0; i<4; i++)
            {
                Console.WriteLine("   ---------------------------------");
                Console.Write(" {0} ", (i + 1));
                for (int j = 0; j < 4; j++)
                {
                    Console.Write("| ");
                    if (Plateau[i, j] != null)
                    {
                        Console.Write(affichePiece(Plateau[i, j]));
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                    else
                        Console.Write("     ");
                    Console.Write(" ");
                }
                Console.WriteLine("|");
            }
            Console.WriteLine("   ---------------------------------");
        }

        public static bool placerPiece(string piece, ref string[,] Plateau)
        {
            bool placevalide;
            int i = 0, j = 0;
            do
            {
                placevalide = false;
                Console.WriteLine("\nVeuillez entrer un emplacement de case libre où placer la pièce (sous le format \"LETTREchiffre\") :");
                string place = Console.ReadLine(); //Demande un emplacement
                if (place.ToUpper() == "QUARTO") //On peut entrer QUARTO afin de gagner si la pièce dont on dispose permet de faire un quarto
                    return QuartoJoueur(piece, Plateau);

                if (place.Length == 2) //Vérification préliminaire de la syntaxe
                {
                    if ((place.ToUpper()[0] == 'A' || place.ToUpper()[0] == 'B' || place.ToUpper()[0] == 'C' || place.ToUpper()[0] == 'D') && (place[1] == '1' || place[1] == '2' || place[1] == '3' || place[1] == '4'))
                    {
                        placevalide = true;
                        if ((place.ToUpper()[0] == 'A') && (int)Char.GetNumericValue(place[1]) < 5) //Vérification de la disponibilité de la place sélectionnée
                        {
                            i = 0;
                            j = (int)Char.GetNumericValue(place[1]) - 1;
                            if (Plateau[j, i] is string)
                                placevalide = false;
                        }
                        else if ((place.ToUpper()[0] == 'B') && (int)Char.GetNumericValue(place[1]) < 5)
                        {
                            i = 1;
                            j = (int)Char.GetNumericValue(place[1]) - 1;
                            if (Plateau[j, i] is string)
                                placevalide = false;
                        }
                        else if ((place.ToUpper()[0] == 'C') && (int)Char.GetNumericValue(place[1]) < 5)
                        {
                            i = 2;
                            j = (int)Char.GetNumericValue(place[1]) - 1;
                            if (Plateau[j, i] is string)
                                placevalide = false;
                        }
                        else if ((place.ToUpper()[0] == 'D') && (int)Char.GetNumericValue(place[1]) < 5)
                        {
                            i = 3;
                            j = (int)Char.GetNumericValue(place[1]) - 1;
                            if (Plateau[j, i] is string)
                                placevalide = false;
                        }
                        else
                        {
                            placevalide = false;
                        }
                    }
                }
            if (placevalide == false) Console.WriteLine("\nLa commande est invalide, veuillez rééssayer.");
            } while (placevalide == false);
            Plateau[j, i] = piece; //Une fois la place sélectionnée libre, on attribue à la place la pièce sélectionnée au préalable
            return false;
        }

        public static string IAcommence(ref string[] Pieces)
        {
            Random r = new Random();
            int i = r.Next(0,16);
            int k = 0;
            string piece = Pieces[i];
            string[] Pieces1 = new string[15];
            for (int j = 0; j < 16; j++)
            {
                if (j !=i)
                {
                    Pieces1[k] = Pieces[j];
                    k++;
                }
            }
            Pieces = Pieces1;
            return piece;
        }

        public static string tourIArnd(string piece, ref string[,] Plateau, ref string[] Pieces)
        {
            Random r1 = new Random();
            int i;
            int j;
            bool placevalide;
            do
            {
                placevalide = true; //Vérifie la disponibilité de la place sélectionnée
                i = r1.Next(0, 4); //Variable aléatoire choisissant la ligne
                j = r1.Next(0, 4); //Variable aléatoire choisissant la colonne
                if (Plateau[i, j] is string)
                    placevalide = false;
            } while (placevalide == false);
            Plateau[i, j] = piece; //On attribue à la place choisie (valide) la pièce choisie par le joueur

            bool gagne = QuartoIA(Plateau); //On teste si L'IA voit un QUARTO (s'il y en a, elle la voit à chaque fois)
            if (gagne == true)
                return null; //retourne null si il y a quarto

            if (Pieces.Length != 0)
            {
                Random r = new Random(); //Variable aléatoire choisissant une pièce parmi celles disponibles
                int k = r.Next(0, Pieces.Length);
                string pieceChoisie = Pieces[k];
                string[] nvtab = new string[Pieces.Length - 1]; //On retire la pièce choisie des pièces disponibles
                j = 0;
                for (i = 0; i < nvtab.Length; i++)
                {
                    if (i == k)
                        j++;
                    nvtab[i] = Pieces[j];
                    j++;
                }
                Pieces = nvtab;
                return pieceChoisie;
            }
            else
                return null;
        }
        public static bool QuartoIA(string[,] Plateau)
        {
            int k = 0; //Variable d'incrément servant pour vérifier chaque lettre de chaque pièce alignée
            string Piece1 = null; //Variables prenant pour valeur chaque pièce alignée
            string Piece2 = null;
            string Piece3 = null;
            string Piece4 = null;
            for (int i = 0; i < 4; i++) //test pour chaque ligne
            {
                k = 0;
                Piece1 = Plateau[i, 0];
                Piece2 = Plateau[i, 1];
                Piece3 = Plateau[i, 2];
                Piece4 = Plateau[i, 3];
                if (Piece1 != null && Piece2 != null && Piece3 != null && Piece4 != null) //Si il y a bien 4 pièces sur cette ligne
                {
                    while (k < 4)
                    {
                        if (Piece1[k] == Piece2[k] && Piece2[k] == Piece3[k] && Piece3[k] == Piece4[k]) //Si il y a une lettre commune aux 4 pièces alignées
                        {
                            Console.WriteLine("\nL'IA gagne par aligmement avec la ligne {0}.", (i+1));
                            return true;
                        }
                        else
                            k++;
                    }
                }
            }
            for(int j = 0; j < 4; j++) //Test pour chaque colonne
            {
                k = 0;
                Piece1 = Plateau[0, j];
                Piece2 = Plateau[1, j];
                Piece3 = Plateau[2, j];
                Piece4 = Plateau[3, j];
                if (Piece1 != null && Piece2 != null && Piece3 != null && Piece4 != null) //Si il y a bien 4 pièces sur cette colonne
                {
                    while (k < 4)
                    {
                        if (Piece1[k] == Piece2[k] && Piece2[k] == Piece3[k] && Piece3[k] == Piece4[k]) //Si il y a une lettre commune aux 4 pièces alignées
                        {
                            Console.WriteLine("\nL'IA gagne par aligmement avec la colonne {0}.", (j+1));
                            return true;
                        }
                        else
                            k++;
                    }
                }
            }
            //Test 1e bissectrice
            k = 0;
            Piece1 = Plateau[0, 3];
            Piece2 = Plateau[1, 2];
            Piece3 = Plateau[2, 1];
            Piece4 = Plateau[3, 0];
            if (Piece1 != null && Piece2 != null && Piece3 != null && Piece4 != null) //Si il y a bien 4 pièces sur la 1e bissectrice
            {
                while (k < 4)
                {
                    if (Piece1[k] == Piece2[k] && Piece2[k] == Piece3[k] && Piece3[k] == Piece4[k]) //Si il y a une lettre commune aux 4 pièces alignées
                    {
                        Console.WriteLine("\nL'IA gagne par aligmement avec la 1ère bissectrice.");
                        return true;
                    }
                    else
                        k++;
                }
            }
            //Test 2nd bissectrice
            k = 0;
            Piece1 = Plateau[0, 0];
            Piece2 = Plateau[1, 1];
            Piece3 = Plateau[2, 2];
            Piece4 = Plateau[3, 3];
            if (Piece1 != null && Piece2 != null && Piece3 != null && Piece4 != null) //Si il y a bien 4 pièces sur la 2nd bissectrice
            {
                while (k < 4)
                {
                    if (Piece1[k] == Piece2[k] && Piece2[k] == Piece3[k] && Piece3[k] == Piece4[k]) //Si il y a une lettre commune aux 4 pièces alignées
                    {
                        Console.WriteLine("\nL'IA gagne par aligmement avec la 2nd bissectrice.");
                        return true;
                    }
                    else
                        k++;
                }
            }
            return false; //retourne false s'il n'y a aucun quarto
        }

        public static bool QuartoPos()
        {
            Console.WriteLine("Où voyez vous un Quarto (1,2,3,4,A,B,C,D,E,F) ?"); //On demande d'abord au joueur où il voit un possible Quarto
            string Q = (Console.ReadLine()).ToUpper(); //Variable prenant l'alignement du possible quarto
            bool win = false; // Variable permettant de savoir s'il y a QUARTO ou non
            switch (Q)
            {     
                case "1":
                    string piece1 = Plateau[0, 0];
                    string piece2 = Plateau[0, 1];
                    string piece3 = Plateau[0, 2];
                    string piece4 = Plateau[0, 3];
                    if (piece1 != null && piece2 != null && piece3 != null && piece4 != null)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            if (piece1[i] == piece2[i] && piece2[i] == piece3[i] && piece3[i] == piece4[i])
                                win = true;
                        }
                    }
                    break;

                case "2":
                    piece1 = Plateau[1, 0];
                    piece2 = Plateau[1, 1];
                    piece3 = Plateau[1, 2];
                    piece4 = Plateau[1, 3];
                    if (piece1 != null && piece2 != null && piece3 != null && piece4 != null)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            if (piece1[i] == piece2[i] && piece2[i] == piece3[i] && piece3[i] == piece4[i])
                                win = true;
                        }
                    }
                    break;

                case "3":
                    piece1 = Plateau[2, 0];
                    piece2 = Plateau[2, 1];
                    piece3 = Plateau[2, 2];
                    piece4 = Plateau[2, 3];
                    if (piece1 != null && piece2 != null && piece3 != null && piece4 != null)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            if (piece1[i] == piece2[i] && piece2[i] == piece3[i] && piece3[i] == piece4[i])
                                win = true;
                        }
                    }
                    break;

                case "4":
                    piece1 = Plateau[3, 0];
                    piece2 = Plateau[3, 1];
                    piece3 = Plateau[3, 2];
                    piece4 = Plateau[3, 3];
                    if (piece1 != null && piece2 != null && piece3 != null && piece4 != null)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            if (piece1[i] == piece2[i] && piece2[i] == piece3[i] && piece3[i] == piece4[i])
                                win = true;
                        }
                    }
                    break;

                case "A":
                    piece1 = Plateau[0, 0];
                    piece2 = Plateau[1, 0];
                    piece3 = Plateau[2, 0];
                    piece4 = Plateau[3, 0];
                    if (piece1 != null && piece2 != null && piece3 != null && piece4 != null)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            if (piece1[i] == piece2[i] && piece2[i] == piece3[i] && piece3[i] == piece4[i])
                                win = true;
                        }
                    }
                    break;

                case "B":
                    piece1 = Plateau[0, 1];
                    piece2 = Plateau[1, 1];
                    piece3 = Plateau[2, 1];
                    piece4 = Plateau[3, 1];
                    if (piece1 != null && piece2 != null && piece3 != null && piece4 != null)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            if (piece1[i] == piece2[i] && piece2[i] == piece3[i] && piece3[i] == piece4[i])
                                win = true;
                        }
                    }
                    break;

                case "C":
                    piece1 = Plateau[0, 2];
                    piece2 = Plateau[1, 2];
                    piece3 = Plateau[2, 2];
                    piece4 = Plateau[3, 2];
                    if (piece1 != null && piece2 != null && piece3 != null && piece4 != null)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            if (piece1[i] == piece2[i] && piece2[i] == piece3[i] && piece3[i] == piece4[i])
                                win = true;
                        }
                    }
                    break;

                case "D":
                    piece1 = Plateau[0, 3];
                    piece2 = Plateau[1, 3];
                    piece3 = Plateau[2, 3];
                    piece4 = Plateau[3, 3];
                    if (piece1 != null && piece2 != null && piece3 != null && piece4 != null)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            if (piece1[i] == piece2[i] && piece2[i] == piece3[i] && piece3[i] == piece4[i])
                                win = true;
                        }
                    }
                    break;

                case "E":
                    piece1 = Plateau[0, 0];
                    piece2 = Plateau[1, 1];
                    piece3 = Plateau[2, 2];
                    piece4 = Plateau[3, 3];
                    if (piece1 != null && piece2 != null && piece3 != null && piece4 != null)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            if (piece1[i] == piece2[i] && piece2[i] == piece3[i] && piece3[i] == piece4[i])
                                win = true;
                        }
                    }
                    break;

                case "F":
                    piece1 = Plateau[0, 3];
                    piece2 = Plateau[1, 2];
                    piece3 = Plateau[2, 1];
                    piece4 = Plateau[3, 0];
                    if (piece1 != null && piece2 != null && piece3 != null && piece4 != null)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            if (piece1[i] == piece2[i] && piece2[i] == piece3[i] && piece3[i] == piece4[i])
                                win = true;
                        }
                    }
                    break;
            }
            if (win == true)
            {
                Console.WriteLine("VICTOIRE!!!");
                return true;
            }
            else
                return false;
        }

        public static bool QuartoJoueur(string piece, string[,] Plateau)
        {
            Console.WriteLine("Où voyez vous un QUARTO (1, 2, 3, 4, A, B, C, D, E, F) ?"); //On demande d'abord au joueur où il voit un possible Quarto
            string Q = Console.ReadLine(); //Variable prenant l'alignement du possible quarto
            int k = 0; //variable indexant chaque caractéristique de chaque pièce
            bool win = false; // Variable permettant de savoir s'il y a QUARTO ou non
            switch (Q.ToUpper()) //Test sur chaque alignement
            {
                case "1":
                    int nbnull = 0; //variable compteur du nombre de champs nuls dans une ligne
                    string piece1, piece2, piece3, piece4; //Pièces placés sur cette ligne
                    int ind=0; //place de la place vide sur l'alignement
                    for(int i = 0; i < 4; i++) //Comptage du nombre de champs nuls
                    {
                        if (Plateau[0, i] == null)
                        {
                            ind = i;
                            nbnull++;
                        }
                    }
                    if (nbnull == 1) //Si il y a un seul champ libre (qu'on considère rempli par la pièce que tient le joueur)
                    {
                        if (ind == 0) //Pour chaque possibilité de places vides
                        {
                            piece1 = piece; //On considère la pièce tenue comme posée
                            piece2 = Plateau[0, 1];
                            piece3 = Plateau[0, 2];
                            piece4 = Plateau[0, 3];
                        }
                        else if (ind == 1)
                        {
                            piece1 = Plateau[0, 0];
                            piece2 = piece;
                            piece3 = Plateau[0, 2];
                            piece4 = Plateau[0, 3];
                        }
                        else if (ind == 2)
                        {
                            piece1 = Plateau[0, 0];
                            piece2 = Plateau[0, 1];
                            piece3 = piece;
                            piece4 = Plateau[0, 3];
                        }
                        else
                        {
                            piece1 = Plateau[0, 0];
                            piece2 = Plateau[0, 1];
                            piece3 = Plateau[0, 2];
                            piece4 = piece;
                        }
                        Plateau[0, ind] = piece; //On pose enfin la pièce
                        while (k < 4 && win == false)
                        {
                            if (piece1[k] == piece2[k] && piece2[k] == piece3[k] && piece3[k] == piece4[k]) //Si les pièces ont une caractéristique commune, il y a quarto
                                win = true;
                            k++;
                        }
                    }
                    break;

                case "2": //cf case "1"
                    nbnull = 0;
                    ind = 0;
                    for (int i = 0; i < 4; i++)
                    {
                        if (Plateau[1, i] == null)
                        {
                            ind = i;
                            nbnull++;
                        }
                    }
                    if (nbnull == 1)
                    {
                        if (ind == 0)
                        {
                            piece1 = piece;
                            piece2 = Plateau[1, 1];
                            piece3 = Plateau[1, 2];
                            piece4 = Plateau[1, 3];
                        }
                        else if (ind == 1)
                        {
                            piece1 = Plateau[1, 0];
                            piece2 = piece;
                            piece3 = Plateau[1, 2];
                            piece4 = Plateau[1, 3];
                        }
                        else if (ind == 2)
                        {
                            piece1 = Plateau[1, 0];
                            piece2 = Plateau[1, 1];
                            piece3 = piece;
                            piece4 = Plateau[1, 3];
                        }
                        else
                        {
                            piece1 = Plateau[1, 0];
                            piece2 = Plateau[1, 1];
                            piece3 = Plateau[1, 2];
                            piece4 = piece;
                        }
                        Plateau[1, ind] = piece;
                        while (k < 4 && win == false)
                        {
                            if (piece1[k] == piece2[k] && piece2[k] == piece3[k] && piece3[k] == piece4[k])
                                win = true;
                            k++;
                        }
                    }
                    break;

                case "3": //cf case "1"
                    nbnull = 0;
                    ind = 0;
                    for (int i = 0; i < 4; i++)
                    {
                        if (Plateau[2, i] == null)
                        {
                            ind = i;
                            nbnull++;
                        }
                    }
                    if (nbnull == 1)
                    {
                        if (ind == 0)
                        {
                            piece1 = piece;
                            piece2 = Plateau[2, 1];
                            piece3 = Plateau[2, 2];
                            piece4 = Plateau[2, 3];
                        }
                        else if (ind == 1)
                        {
                            piece1 = Plateau[2, 0];
                            piece2 = piece;
                            piece3 = Plateau[2, 2];
                            piece4 = Plateau[2, 3];
                        }
                        else if (ind == 2)
                        {
                            piece1 = Plateau[2, 0];
                            piece2 = Plateau[2, 1];
                            piece3 = piece;
                            piece4 = Plateau[2, 3];
                        }
                        else
                        {
                            piece1 = Plateau[2, 0];
                            piece2 = Plateau[2, 1];
                            piece3 = Plateau[2, 2];
                            piece4 = piece;
                        }
                        Plateau[2, ind] = piece;
                        while (k < 4 && win == false)
                        {
                            if (piece1[k] == piece2[k] && piece2[k] == piece3[k] && piece3[k] == piece4[k])
                                win = true;
                            k++;
                        }
                    }
                    break;

                case "4": //cf case "1"
                    nbnull = 0;
                    ind = 0;
                    for (int i = 0; i < 4; i++)
                    {
                        if (Plateau[3, i] == null)
                        {
                            ind = i;
                            nbnull++;
                        }
                    }
                    if (nbnull == 1)
                    {
                        if (ind == 0)
                        {
                            piece1 = piece;
                            piece2 = Plateau[3, 1];
                            piece3 = Plateau[3, 2];
                            piece4 = Plateau[3, 3];
                        }
                        else if (ind == 1)
                        {
                            piece1 = Plateau[3, 0];
                            piece2 = piece;
                            piece3 = Plateau[3, 2];
                            piece4 = Plateau[3, 3];
                        }
                        else if (ind == 2)
                        {
                            piece1 = Plateau[3, 0];
                            piece2 = Plateau[3, 1];
                            piece3 = piece;
                            piece4 = Plateau[3, 3];
                        }
                        else
                        {
                            piece1 = Plateau[3, 0];
                            piece2 = Plateau[3, 1];
                            piece3 = Plateau[3, 2];
                            piece4 = piece;
                        }
                        Plateau[3, ind] = piece;
                        while (k < 4 && win == false)
                        {
                            if (piece1[k] == piece2[k] && piece2[k] == piece3[k] && piece3[k] == piece4[k])
                                win = true;
                            k++;
                        }
                    }
                    break;

                case "A": //cf case "1"
                    nbnull = 0;
                    ind = 0;
                    for (int i = 0; i < 4; i++)
                    {
                        if (Plateau[i, 0] == null) 
                        {
                            ind = i;
                            nbnull++;
                        }
                    }
                    if (nbnull == 1)
                    {
                        if (ind == 0)
                        {
                            piece1 = piece;
                            piece2 = Plateau[1, 0];
                            piece3 = Plateau[2, 0];
                            piece4 = Plateau[3, 0];
                        }
                        else if (ind == 1)
                        {
                            piece1 = Plateau[0, 0];
                            piece2 = piece;
                            piece3 = Plateau[2, 0];
                            piece4 = Plateau[3, 0];
                        }
                        else if (ind == 2)
                        {
                            piece1 = Plateau[0, 0];
                            piece2 = Plateau[1, 0];
                            piece3 = piece;
                            piece4 = Plateau[3, 0];
                        }
                        else
                        {
                            piece1 = Plateau[0, 0];
                            piece2 = Plateau[1, 0];
                            piece3 = Plateau[2, 0];
                            piece4 = piece;
                        }
                        Plateau[ind, 0] = piece;
                        while (k < 4 && win == false)
                        {
                            if (piece1[k] == piece2[k] && piece2[k] == piece3[k] && piece3[k] == piece4[k])
                                win = true;
                            k++;
                        }
                    }
                    break;

                case "B": //cf case "1"
                    nbnull = 0;
                    ind = 0;
                    for (int i = 0; i < 4; i++)
                    {
                        if (Plateau[i, 1] == null) 
                        {
                            ind = i;
                            nbnull++;
                        }
                    }
                    if (nbnull == 1)
                    {
                        if (ind == 0)
                        {
                            piece1 = piece;
                            piece2 = Plateau[1, 1];
                            piece3 = Plateau[2, 1];
                            piece4 = Plateau[3, 1];
                        }
                        else if (ind == 1)
                        {
                            piece1 = Plateau[0, 1];
                            piece2 = piece;
                            piece3 = Plateau[2, 1];
                            piece4 = Plateau[3, 1];
                        }
                        else if (ind == 2)
                        {
                            piece1 = Plateau[0, 1];
                            piece2 = Plateau[1, 1];
                            piece3 = piece;
                            piece4 = Plateau[3, 1];
                        }
                        else
                        {
                            piece1 = Plateau[0, 1];
                            piece2 = Plateau[1, 1];
                            piece3 = Plateau[2, 1];
                            piece4 = piece;
                        }
                        Plateau[ind, 1] = piece;
                        while (k < 4 && win == false)
                        {
                            if (piece1[k] == piece2[k] && piece2[k] == piece3[k] && piece3[k] == piece4[k])
                                win = true;
                            k++;
                        }
                    }
                    break;

                case "C": //cf case "1"
                    nbnull = 0;
                    ind = 0;
                    for (int i = 0; i < 4; i++)
                    {
                        if (Plateau[i, 3] == null)
                        {
                            ind = i;
                            nbnull++;
                        }
                    }
                    if (nbnull == 1)
                    {
                        if (ind == 0)
                        {
                            piece1 = piece;
                            piece2 = Plateau[1, 2];
                            piece3 = Plateau[2, 2];
                            piece4 = Plateau[3, 2];
                        }
                        else if (ind == 1)
                        {
                            piece1 = Plateau[0, 2];
                            piece2 = piece;
                            piece3 = Plateau[2, 2];
                            piece4 = Plateau[3, 2];
                        }
                        else if (ind == 2)
                        {
                            piece1 = Plateau[0, 2];
                            piece2 = Plateau[1, 2];
                            piece3 = piece;
                            piece4 = Plateau[3, 2];
                        }
                        else
                        {
                            piece1 = Plateau[0, 2];
                            piece2 = Plateau[1, 2];
                            piece3 = Plateau[2, 2];
                            piece4 = piece;
                        }
                        Plateau[ind, 2] = piece;
                        while (k < 4 && win == false)
                        {
                            if (piece1[k] == piece2[k] && piece2[k] == piece3[k] && piece3[k] == piece4[k])
                                win = true;
                            k++;
                        }
                    }
                    break;

                case "D": //cf case "1"
                    nbnull = 0;
                    ind = 0;
                    for (int i = 0; i < 4; i++)
                    {
                        if (Plateau[i, 3] == null)
                        {
                            ind = i;
                            nbnull++;
                        }
                    }
                    if (nbnull == 1)
                    {
                        if (ind == 0)
                        {
                            piece1 = piece;
                            piece2 = Plateau[1, 3];
                            piece3 = Plateau[2, 3];
                            piece4 = Plateau[3, 3];
                        }
                        else if (ind == 1)
                        {
                            piece1 = Plateau[0, 3];
                            piece2 = piece;
                            piece3 = Plateau[2, 3];
                            piece4 = Plateau[3, 3];
                        }
                        else if (ind == 2)
                        {
                            piece1 = Plateau[0, 3];
                            piece2 = Plateau[1, 3];
                            piece3 = piece;
                            piece4 = Plateau[3, 3];
                        }
                        else
                        {
                            piece1 = Plateau[0, 3];
                            piece2 = Plateau[1, 3];
                            piece3 = Plateau[2, 3];
                            piece4 = piece;
                        }
                        Plateau[ind, 3] = piece;
                        while (k < 4 && win == false)
                        {
                            if (piece1[k] == piece2[k] && piece2[k] == piece3[k] && piece3[k] == piece4[k])
                                win = true;
                            k++;
                        }
                    }
                    break;

                case "E": //cf case "1"
                    nbnull = 0;
                    ind = 0;
                    for (int i = 0; i < 4; i++)
                    {
                        if (Plateau[i, i] == null)
                        {
                            ind = i;
                            nbnull++;
                        }
                    }
                    if (nbnull == 1)
                    {
                        if (ind == 0)
                        {
                            piece1 = piece;
                            piece2 = Plateau[1, 1];
                            piece3 = Plateau[2, 2];
                            piece4 = Plateau[3, 3];
                        }
                        else if (ind == 1)
                        {
                            piece1 = Plateau[0, 0];
                            piece2 = piece;
                            piece3 = Plateau[2, 2];
                            piece4 = Plateau[3, 3];
                        }
                        else if (ind == 2)
                        {
                            piece1 = Plateau[0, 0];
                            piece2 = Plateau[1, 1];
                            piece3 = piece;
                            piece4 = Plateau[3, 3];
                        }
                        else
                        {
                            piece1 = Plateau[0, 0];
                            piece2 = Plateau[1, 1];
                            piece3 = Plateau[2, 2];
                            piece4 = piece;
                        }
                        Plateau[ind, ind] = piece;
                        while (k < 4 && win == false)
                        {
                            if (piece1[k] == piece2[k] && piece2[k] == piece3[k] && piece3[k] == piece4[k])
                                win = true;
                            k++;
                        }
                    }
                    break;

                case "F": //cf case "1"
                    nbnull = 0;
                    ind = 0;
                    for (int i = 0; i < 4; i++)
                    {
                        if (Plateau[i, 3-i] == null)
                        {
                            ind = i;
                            nbnull++;
                        }
                    }
                    if (nbnull == 1)
                    {
                        if (ind == 0)
                        {
                            piece1 = piece;
                            piece2 = Plateau[1, 2];
                            piece3 = Plateau[2, 1];
                            piece4 = Plateau[3, 0];
                        }
                        else if (ind == 1)
                        {
                            piece1 = Plateau[0, 3];
                            piece2 = piece;
                            piece3 = Plateau[2, 1];
                            piece4 = Plateau[3, 0];
                        }
                        else if (ind == 2)
                        {
                            piece1 = Plateau[0, 3];
                            piece2 = Plateau[1, 2];
                            piece3 = piece;
                            piece4 = Plateau[3, 0];
                        }
                        else
                        {
                            piece1 = Plateau[0, 3];
                            piece2 = Plateau[1, 2];
                            piece3 = Plateau[2, 1];
                            piece4 = piece;
                        }
                        Plateau[ind, 3 - ind] = piece;
                        while (k < 4 && win == false)
                        {
                            if (piece1[k] == piece2[k] && piece2[k] == piece3[k] && piece3[k] == piece4[k])
                                win = true;
                            k++;
                        }
                    }
                    break;
            }
            if (win == true) //Si il y a quarto
            {
                Console.WriteLine("Félicitations ! Vous avez gagné !");
                return true;
            }
            else //Sinon
            {
                Console.WriteLine("Le QUARTO est invalide, veuillez rééssayer ou placer votre pièce sur le plateau.");
                return placerPiece(piece, ref Plateau);
            }

        }

        static string[,] Plateau = new string[4, 4]; //On initialise le plateau
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White; //On choisit la couleur blanche pour notre texte
            string[] Pieces = { "BRGV", "BRGE", "BRPV", "BRPE", "BCGV", "BCGE", "BCPV", "BCPE", "NRGV", "NRGE", "NRPV", "NRPE", "NCGV", "NCGE", "NCPV", "NCPE" }; //On initialise le tableau de pièces disponibles
            string regle;
            int ntours = 1; //On commence le décompte de tours
            bool win = false; //Variable permettant de savoir s'il y a eu quarto ou non
            bool commence; //variable permettant de savoir qui commence
            Console.WriteLine("\n================================== QUARTO ======================================");
            Console.WriteLine("Bienvenue dans Quarto, si vous souhaitez afficher les règles veuillez taper R. \nSinon, veuillez appuyer sur Entrée pour commencer à jouer.");
            regle = Console.ReadLine();
            if (regle == "R" || regle == "r") Console.WriteLine("\n================================================================================\nRègle de base : \n\nAlignez quatre pièces ayant une caractéristique commune pour gagner.\n\nComment jouer ?\n\nA chaque tour, un joueur choisi une pièce et la donne à son adversaire qui doit la placer sur le plateau.\n\nPIECES :\nLes pièces ont 4 caractéristiques distinctes : \n- Noir ou Blanc noté N ou B\n- Rond ou Carré noté R ou C\n- Grand ou Petit noté G ou P\n- Vide ou Entier noté V ou E\n\nPour choisir une pièce il faut donc la nommer grâce à ses lettres.\nPar exemple pour prendre la pièce Blanche, Ronde, Petite et Vide on tapera BRPV.\n\nPLATEAU :\nLe plateau est composé de 16 cases notées de A1 à D4.\nUne fois la pièce choisie par le premier joueur, le deuxième doit la placer sur le plateau.\nPour se faire il tape la position à laquelle il souhaite la déposer.\n\nExemple :\nB2\n\nVICTOIRE :\nPour gagner il faut annoncer QUARTO pendant son tour puis placer la pièce donnée et désigner quelle ligne, colonne ou bissectrice permet la victoire. Pour cela, vous devez taper QUARTO puis le numéro ou la lettre de la ligne.\n\nExemple : \nQUARTO\nC\n\nSi le plateau est rempli avant qu'un des deux joueurs ne reussisse un QUARTO! la partie se termine sur une égalité.\n================================================================================");
            Console.WriteLine("\nPour commencer on lance une pièce pour déterminer quel joueur jouera en premier.");
            commence = lancerPieceMonnaie(); //On décide celui qui commence à pile ou face
            if (commence == true) //Si le joueur gagne, il commence
            {
                affichePlateau(Plateau); //On affiche le plateau vide
                while (win == false && ntours<9) //La partie dure jusqu'à ce qu'il y ait quarto ou que le Plateau soit plein
                {
                    string piece = choisirPiece(ref Pieces); //Le joueur choisit une pièce parmis celle disponibles
                    if (piece != "win")
                    {
                        piece = tourIArnd(piece, ref Plateau, ref Pieces); //L'IA joue puis choisit une pièce
                        affichePlateau(Plateau);
                        if (piece == null) //Si la pièce retournée est nulle, l'IA a vu un QUARTO
                            win = true; // Alors L'IA gagne
                        else
                            Console.WriteLine("\nL'IA a choisi la pièce " + piece + "."); //Affiche la pièce choisie par l'IA
                        if (win == false) //Si personne n'a vu de quarto
                            win = placerPiece(piece, ref Plateau); //Le joueur pose une pièce ou peut voir un quarto
                        affichePlateau(Plateau);
                        ntours++; //incrément du nombre de tours
                    }
                    else
                        win = true;
                }
            }
            else //Si le joueur perd au pile ou face, l'IA commence
            {
                string piece = IAcommence(ref Pieces); //L'IA choisit une première pièce
                Console.WriteLine("\nL'IA a choisi la pièce " + piece + "."); //Affiche la pièce choisie par l'IA
                affichePlateau(Plateau); //On affiche le plateau vide
                while (win == false && ntours<9) //La partie dure jusqu'à ce qu'il y ait un quarto ou que le Plateau soit plein
                {
                    win = placerPiece(piece, ref Plateau); //Le joueur peut placer une pièce ou voir un quarto
                    affichePlateau(Plateau);
                    if (win==false)
                    {
                        piece = choisirPiece(ref Pieces); //Le joueur choisit une pièce
                        if (piece != "win")
                        {
                            piece = tourIArnd(piece, ref Plateau, ref Pieces); //L'IA la pose et choisit une pièce à son tour
                            affichePlateau(Plateau);
                        }
                        else
                            win = true;
                    }
                    if (piece == null && win == false && piece!="win") //Si l'IA voit un quarto
                        win = true; //Elle gagne
                    else
                    { 
                        if (win == false)
                            Console.WriteLine("\nL'IA a choisi la pièce " + piece + "."); //Affiche la pièce choisie par l'IA
                    } 
                    ntours++;//Incrément du nombre de tours
                }
            }
            if (ntours == 9) //Si le plateau est plein (à la fin du tour 8)
                Console.WriteLine("Égalité ! Dommage !"); //il y a match nul
            Console.ReadLine(); //Appuyer sur entrée pour quitter
        }
    }
}