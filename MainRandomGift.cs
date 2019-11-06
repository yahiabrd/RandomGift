using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2NET_PROJECT
{
    /**
     * Classe pour le projet RandomGift
     * @author Yahia
     */
    class MainRandomGift
    {
        /**
         * Fonction permettant de lancer le programme
         */
        static void Main(string[] args)
        {
            try
            {
                Console.Title = "RandomGift by Yahia";
                UsersRandomGift User = new UsersRandomGift();
                User.Menu();
            }
            catch (Exception e)
            {
                Console.WriteLine("Une erreur est survenue, veuillez relancer le programme\n");
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }
        }
    }
}
