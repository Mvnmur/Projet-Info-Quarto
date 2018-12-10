using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Quarto
{
    class Program
    {
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
        }
    }
}
