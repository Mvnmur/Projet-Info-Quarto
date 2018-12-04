using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Quarto
{
    class Program
    {
        static void Main(string[] args)
        {   //Regles (à finir)
            Console.WriteLine("============ QUARTO! ==========\n\nRègle de base : \n\nAlignez quatre pièces ayant une caractéristique commune pour gagner.\n\nComment jouer ?\nA chaque tour, un joueur choisi une pièce et la donne à son adversaire qui doit la placer sur le plateau.\\nnPIECES :\nLes pièces ont 4 caractéristiques distinctes : \n- Noir ou Blanc noté N ou B\n- Rond ou Carré noté R ou C\n- Grand ou Petit noté G ou P\n- Vide ou Entier noté V ou E\n\nPour choisir une pièce il faut donc la nommer grâce à ces lettres.\nPar exemple pour prendre la pièce Blanche, Ronde, Petite et Vide on tapera BRPV.\n\nPLATEAU :\nLe plateau est composé de 16 cases notées de A1 à D4.\nUne fois la pièce choisie par le premier joueur, le deuxième doit la placer sur le plateau.\nPour se faire il tape la position à laquelle il souhaite la déposer.\nExemple : B2\n\nVICTOIRE :\nPour gagner, lors de son tour il faut annoncer QUARTO puis placer la pièce donnée et désigner quelle ligne permet la victoire. Pour celà vous devez taper QUARTO puis l'emplacement de la pièce et ensuite les emplacement de début et de fin de ligne gagnante.\nExemple : \nQUARTO\nA4\nA1A4\n\n");
            Console.ReadLine();
        }
    }
}
