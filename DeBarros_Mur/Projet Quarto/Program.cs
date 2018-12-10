using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Quarto
{
    class Program
    {
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
            bool isin = false;
            do
            {
                Console.WriteLine("Veuillez choisir une pièce parmi celles restantes");
                afficheTableau(Pieces);
                pieceChoisie = Console.ReadLine();
                for (int i = 0; i < Pieces.Length; i++)
                {
                    if (pieceChoisie == Pieces[i])
                    {
                        isin = true;
                        ind = i;
                    }
                }
            } while (isin == false);
            string[] nvtab = new string[Pieces.Length - 1];
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
        {
            Console.WriteLine("     A      B      C      D");
            for (int i=0; i<4; i++)
            {
                Console.WriteLine("  -----------------------------");
                Console.Write("{0} ", (i + 1));
                for (int j = 0; j < 4; j++)
                {
                    Console.Write("| ");
                    if (Plateau[i, j] != null)
                        Console.Write(Plateau[i, j]);
                    else
                        Console.Write("    ");
                    Console.Write(" ");
                }
                Console.WriteLine("|");
            }
            Console.WriteLine("  -----------------------------");
        }

        public static void placerPiece(string piece, ref string[,] Plateau)
        {
            bool placevalide;
            int i = 0, j = 0;
            do
            {
                placevalide = true;
                Console.WriteLine("Entrez une case libre où placer la pièce (sous le format \"LETTREchiffre\") :");
                string place = Console.ReadLine();
                if (place == "QUARTO")
                    QuartoJoueur(piece, Plateau);
                if (place.Length == 2)
                {
                    if (place[0] == 'A' && (int)Char.GetNumericValue(place[1]) < 5)
                    {
                        i = 0;
                        j = (int)Char.GetNumericValue(place[1])-1;
                        if (Plateau[j, i] is string)
                            placevalide = false;
                    }
                    else if (place[0] == 'B' && (int)Char.GetNumericValue(place[1]) < 5)
                    {
                        i = 1;
                        j = (int)Char.GetNumericValue(place[1])-1;
                        if (Plateau[j, i] is string)
                            placevalide = false;
                    }
                    else if (place[0] == 'C' && (int)Char.GetNumericValue(place[1]) < 5)
                    {
                        i = 2;
                        j = (int)Char.GetNumericValue(place[1])-1;
                        if (Plateau[j, i] is string)
                            placevalide = false;
                    }
                    else if (place[0] == 'D' && (int)Char.GetNumericValue(place[1]) < 5)
                    {
                        i = 3;
                        j = (int)Char.GetNumericValue(place[1])-1;
                        if (Plateau[j, i] is string)
                            placevalide = false;
                    }
                    else
                    {
                        Console.WriteLine("NOPE");
                        placevalide = false;
                    }

                }

            } while (placevalide == false);
            Plateau[j, i] = piece;
        }

        public static string tourIArnd(string piece, ref string[,] Plateau, ref string[] Pieces)
        {
            Random r1 = new Random();
            int i;
            int j;
            bool placevalide;
            do
            {
                placevalide = true;
                i = r1.Next(0, 4);
                j = r1.Next(0, 4);
                if (Plateau[i, j] is string)
                    placevalide = false;
            } while (placevalide == false);
            Plateau[i, j] = piece;

            QuartoIA(i, j, Plateau);

            if (Pieces.Length != 0)
            {
                Random r = new Random();
                int k = r.Next(0, Pieces.Length);
                string pieceChoisie = Pieces[k];
                string[] nvtab = new string[Pieces.Length - 1];
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
        public static void QuartoIA(int i, int j, string[,] Plateau)
        {
            bool win = false;

            int k = 0;
            string piece1 = Plateau[i, 0];
            string piece2 = Plateau[i, 1];
            string piece3 = Plateau[i, 2];
            string piece4 = Plateau[i, 3];
            if (piece1 != null && piece2 != null && piece3 != null && piece4 != null) { 
                while (k < 4 && win == false)
                {
                    if (piece1[k] == piece2[k] && piece2[k] == piece3[k] && piece3[k] == piece4[k])
                        win = true;
                    k++;
                }
                if (win == true)
                    Console.WriteLine("Victoire de l'IA : alignement ligne {0} !", (i + 1));
                else
                {
                    k = 0;
                    piece1 = Plateau[0, j];
                    piece2 = Plateau[1, j];
                    piece3 = Plateau[2, j];
                    piece4 = Plateau[3, j];
                    if (piece1 != null && piece2 != null && piece3 != null && piece4 != null)
                    {
                        while (k < 4 && win == false)
                        {
                            if (piece1[k] == piece2[k] && piece2[k] == piece3[k] && piece3[k] == piece4[k])
                                win = true;
                            k++;
                        }
                        char[] col = { 'A', 'B', 'C', 'D' };
                        if (win == true)
                            Console.WriteLine("Victoire de l'IA : alignement colonne {0} !", col[j]);
                        else
                        {
                            k = 0;
                            piece1 = Plateau[0, 0];
                            piece2 = Plateau[1, 1];
                            piece3 = Plateau[2, 2];
                            piece4 = Plateau[3, 3];
                            if (piece1 != null && piece2 != null && piece3 != null && piece4 != null)
                            {
                                while (k < 4 && win == false)
                                {
                                    if (piece1[k] == piece2[k] && piece2[k] == piece3[k] && piece3[k] == piece4[k])
                                        win = true;
                                    k++;
                                }
                                if (win == true)
                                    Console.WriteLine("Victoire de l'IA : alignement 1e bissectrice !");
                                else
                                {
                                    k = 0;
                                    piece1 = Plateau[0, 3];
                                    piece2 = Plateau[1, 2];
                                    piece3 = Plateau[2, 1];
                                    piece4 = Plateau[3, 0];
                                    if (piece1 != null && piece2 != null && piece3 != null && piece4 != null)
                                    {
                                        while (k < 4 && win == false)
                                        {
                                            if (piece1[k] == piece2[k] && piece2[k] == piece3[k] && piece3[k] == piece4[k])
                                                win = true;
                                            k++;
                                        }
                                        if (win == true)
                                            Console.WriteLine("Victoire de l'IA : alignement 2nd bissectrice !");
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        
        public static void QuartoJoueur(string piece, string[,] Plateau)
        {
            Console.WriteLine("Où voyez vous un Quarto (1,2,3,4,A,B,C,D,E,F) ?");
            string Q = Console.ReadLine();
            int k = 0;
            bool win = false;
            switch (Q)
            {
                case "1":
                    int nbnull = 0;
                    string piece1, piece2, piece3, piece4;
                    int ind=0;
                    for(int i = 0; i < 4; i++)
                    {
                        if (Plateau[0, i] == null)
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
                        while (k < 4 && win == false)
                        {
                            if (piece1[k] == piece2[k] && piece2[k] == piece3[k] && piece3[k] == piece4[k])
                                win = true;
                            k++;
                        }
                        if (win == true)
                            Console.WriteLine("VICTOIRE !!!!!");
                    }
                    break;
            }


        }

//Timer qui arrête le tour du joueur au bout de 60 secondes.
        public static void Timer()
        {
            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new System.Timers.ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = 60000;
            aTimer.Enabled = true;

            Console.WriteLine("Le Timer est activé, appuyez sur \'q\' pour le désactiver.");
            while (Console.Read() != 'q') ;
        }

        // Quand le timer est fini, affiche "Fin de tour."
        private static void OnTimedEvent(object source, System.Timers.ElapsedEventArgs e)
        {
            Console.WriteLine("Fin de tour.");
        }
        public static string lancerPieceMonnaie()
        {
            Random rnd = new Random();
            string pieceMonnaie;
            int facePiece = 0, facePieceRnd;
            do //Boucle do while pour vérifier la validité de l'entrée utilisateur (Pile ou Face)
            {
                Console.WriteLine("Choisissez : Pile ou Face ?");
                pieceMonnaie = Console.ReadLine();
            }
            while (pieceMonnaie != "Pile" && pieceMonnaie != "Face");
            //Si l'entrée utilisateur est Pile renvoie 0 si c'est Face renvoie 1 puis génére un nombre aléatoire entre 0 et 1 et compare les deux. Si pareil ==> Utilisateur joue en premier sinon joue en second.
                if (pieceMonnaie == "Pile") facePiece = 0;
                else if(pieceMonnaie == "Face") facePiece = 1;
            facePieceRnd = rnd.Next(0, 2);
                if (facePiece == facePieceRnd) return "\nVous jouez en premier.";
                else return "\nVous jouez en deuxième.";
        }
        static void Main(string[] args)
        {
            char choixCompet;

            //Regles de jeu
            Console.WriteLine("============ QUARTO! ==========\n\nRègle de base : \n\nAlignez quatre pièces ayant une caractéristique commune pour gagner.\n\nComment jouer ?\nA chaque tour, un joueur choisi une pièce et la donne à son adversaire qui doit la placer sur le plateau.\\nnPIECES :\nLes pièces ont 4 caractéristiques distinctes : \n- Noir ou Blanc noté N ou B\n- Rond ou Carré noté R ou C\n- Grand ou Petit noté G ou P\n- Vide ou Entier noté V ou E\n\nPour choisir une pièce il faut donc la nommer grâce à ces lettres.\nPar exemple pour prendre la pièce Blanche, Ronde, Petite et Vide on tapera BRPV.\n\nPLATEAU :\nLe plateau est composé de 16 cases notées de A1 à D4.\nUne fois la pièce choisie par le premier joueur, le deuxième doit la placer sur le plateau.\nPour se faire il tape la position à laquelle il souhaite la déposer.\nExemple : B2\n\nVICTOIRE :\nPour gagner, lors de son tour il faut annoncer QUARTO puis placer la pièce donnée et désigner quelle ligne permet la victoire. Pour celà vous devez taper QUARTO puis l'emplacement de la pièce et ensuite les emplacement de début et de fin de ligne gagnante.\n\nExemple : \nQUARTO\nA4\nA1A4\n\nSi le plateau est rempli avant qu'un des deux joueurs ne reussisse un QUARTO! la partie se termine sur une égalité.");

            //Choix du mode compétitif (1 min de temps par tour et sinon placement aléatoire.)
      /*      Console.WriteLine("Voulez-vous activer le mode compétitif (1 min max par coups) ?");
            choixCompet = char.Parse(Console.ReadLine());
            do
            {
                if (choixCompet == 'O') Timer();
            }
            while (choixCompet != 'O' && choixCompet != 'N');*/

            //Choix du premier joueur à jouer
            Console.WriteLine("Pour commencer lancez une pièce pour déterminer le premier joueur à jouer.");
            Console.WriteLine(lancerPieceMonnaie());
            Console.ReadLine();
            
            string[] Pieces = { "BRGV", "BRGE", "BRPV", "BRPE", "BCGV", "BCGE", "BCPV", "BCPE", "NRGV", "NRGE", "NRPV", "NRPE", "NCGV", "NCGE", "NCPV", "NCPE" };
            string[,] Plateau = new string[4, 4];
            affichePlateau(Plateau);
            int i = 0;
            while (i < 200)
            {
                string piece = choisirPiece(ref Pieces);
                placerPiece(piece, ref Plateau);
                affichePlateau(Plateau);
                i++;

            }
            
            Console.ReadLine();
        }
    }
}
