using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Migrations;
using System.Text;
using System.Threading.Tasks;

namespace _2NET_PROJECT
{
    /**
     * Classe pour le projet RandomGift
     * @author Yahia
     */
    public class UsersRandomGift
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Wishlist { get; set; }

        public List<string> AllWishes = new List<string> { };

        /**
         * Fonction permettant de définir le menu
         */
        public void Menu()
        {   
            bool openMenu = true;
            int UserId;
            char continueProgram;

            while (openMenu)
            {
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Bienvenue dans le programme RandomGift !\n");
                Console.ResetColor();

                Console.WriteLine("Que voulez vous faire ?\n");
                Console.WriteLine("1 - Ajouter un utilisateur");
                Console.WriteLine("2 - Chercher un utilistateur par Id");
                Console.WriteLine("3 - Afficher la liste de tous les utilisateurs");
                Console.WriteLine("4 - Mettre à jour un utilisateur");
                Console.WriteLine("5 - Supprimer un utilisateur");
                Console.WriteLine("6 - Tirer un gagnant au hasard\n");

                Console.Write("Faites un choix : ");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        AddNewUser();
                        break;
                    case 2:
                        Console.Clear();
                        Console.Write("\nVeuillez entrez l'Id de l'utilisateur à rechercher : ");
                        UserId = int.Parse(Console.ReadLine());
                        SearchUser(UserId);
                        break;
                    case 3:
                        DisplayAllUsers();
                        break;
                    case 4:
                        Console.Clear();
                        Console.Write("\nVeuillez entrez l'Id de l'utilisateur à mettre à jour : ");
                        UserId = int.Parse(Console.ReadLine());
                        UpdateUser(UserId);
                        break;
                    case 5:
                        Console.Clear();
                        Console.Write("\nVeuillez entrez l'Id de l'utilisateur à supprimer : ");
                        UserId = int.Parse(Console.ReadLine());
                        DeleteUser(UserId);
                        break;
                    case 6:
                        PickWinner();
                        break;
                    default:
                        Console.WriteLine("Choix impossible");
                        break;
                }
                
                Console.Write("\nVoulez vous continuer sur le Menu ? O/N : ");
                continueProgram = char.Parse(Console.ReadLine().ToUpper());
                if (continueProgram != 'O')
                    openMenu = false;
            }
        }

        /**
         * Fonction permettant d'ajouter un nouvel utilisateur
         */
        public void AddNewUser()
        {
            string name, phoneNumber, email, wish1, wish2, wish3;

            using (var db = new RandomGiftModel())
            {
                Console.Clear();
                Console.WriteLine("Ajout d'un nouvel utilisateur\n");

                Console.Write("Nom et prénom : ");
                name = Console.ReadLine();

                Console.Write("Numéro de téléphone : ");
                phoneNumber = Console.ReadLine();

                Console.Write("Email : ");
                email = Console.ReadLine();

                Console.WriteLine("Entrez votre premier souhait : ");
                wish1 = Console.ReadLine();
                Console.WriteLine("Entrez votre second souhait : ");
                wish2 = Console.ReadLine();
                Console.WriteLine("Entrez votre troisième souhait : ");
                wish3 = Console.ReadLine();

                string[] allWishes = { wish1, wish2, wish3 };
                AllWishes.AddRange(allWishes);
                string wishlist = AllWishes[0] + " " + AllWishes[1] + " " + AllWishes[2];

                UsersRandomGift User = new UsersRandomGift()
                {
                    FullName = name,
                    Phone = phoneNumber,
                    Email = email,
                    Wishlist = wishlist
                };
                db.Users.Add(User);
                db.SaveChanges();
                AllWishes.Clear();

                Console.WriteLine("\nUtilisateur enregistré !");
            }
        }

        /**
         * Fonction permettant de rechercher un utilisateur par Id
         */
        public void SearchUser(int id)
        {
            using (var db = new RandomGiftModel())
            {
                var cts = db.Users.Find(id);
                if (cts != null)
                {
                    Console.WriteLine("Utilisateur n° : " + cts.Id + "\n");
                    Console.WriteLine("Nom complet : " + cts.FullName);
                    Console.WriteLine("Numéro de téléphone - " + cts.Phone);
                    Console.WriteLine("Email - " + cts.Email);
                    Console.WriteLine("Liste de souhaits : ");
                    Console.WriteLine("Souhaits : ");
                    Console.WriteLine("\tn° 1 : " + cts.Wishlist.Split()[0]);
                    Console.WriteLine("\tn° 2 : " + cts.Wishlist.Split()[1]);
                    Console.WriteLine("\tn° 3 : " + cts.Wishlist.Split()[2]);
                }
                else
                {
                    Console.WriteLine("\nAucun utilisateur n'a été trouvé avec cet Id");
                }
            }
        }

        /**
         * Fonction permettant d'afficher tous les utilisateurs
         */
        public void DisplayAllUsers()
        {
            using (var db = new RandomGiftModel())
            {
                Console.Clear();
                if (db.Users.Count() > 0)
                {
                    Console.WriteLine("Il y a " + db.Users.Count() + " utilisateurs actuellement au total");
                    foreach (var cts in db.Users)
                    {
                        Console.WriteLine("--------------------------------");
                        Console.WriteLine("Utilisateur n° : " + cts.Id);
                        Console.WriteLine("Nom complet : " + cts.FullName);
                        Console.WriteLine("Numéro de téléphone - " + cts.Phone);
                        Console.WriteLine("Email - " + cts.Email);
                        Console.WriteLine("Liste de souhaits : ");
                        Console.WriteLine("Souhaits : ");
                        Console.WriteLine("\tn° 1 : " + cts.Wishlist.Split()[0]);
                        Console.WriteLine("\tn° 2 : " + cts.Wishlist.Split()[1]);
                        Console.WriteLine("\tn° 3 : " + cts.Wishlist.Split()[2]);
                    }
                }
                else
                {
                    Console.WriteLine("La base de donnée est vide");
                }
            }
        }

        /**
         * Fonction permettant de mettre à jour un utilisateur
         */
        public void UpdateUser(int id)
        {
            string name, phoneNumber, email, wish1, wish2, wish3;
            using (var db = new RandomGiftModel())
            {
                if (db.Users.Count() > 0)
                {
                    var cts = db.Users.Find(id);
                    if (cts != null)
                    {
                        Console.WriteLine("Vous êtes sur le point de modifier : " + cts.FullName + "\n");

                        Console.Write("Nouveau nom complet : ");
                        name = Console.ReadLine();

                        Console.Write("Nouveau numéro de téléphone : ");
                        phoneNumber = Console.ReadLine();

                        Console.Write("Nouvel email : ");
                        email = Console.ReadLine();

                        Console.WriteLine("Nouveau souhait 1 : ");
                        wish1 = Console.ReadLine();
                        Console.WriteLine("Nouveau souhait 2 : ");
                        wish2 = Console.ReadLine();
                        Console.WriteLine("Nouveau souhait 3 : ");
                        wish3 = Console.ReadLine();

                        string[] allWishes = { wish1, wish2, wish3 };
                        AllWishes.AddRange(allWishes);
                        string wishlist = AllWishes[0] + " " + AllWishes[1] + " " + AllWishes[2];

                        UsersRandomGift User = new UsersRandomGift
                        {
                            Id = id,
                            FullName = name,
                            Phone = phoneNumber,
                            Email = email,
                            Wishlist = wishlist
                        };
                        db.Users.AddOrUpdate(User);
                        db.SaveChanges();
                        AllWishes.Clear();

                        Console.WriteLine("Utilisateur modifié avec succes");
                    }
                    else
                    {
                        Console.WriteLine("Utilisateur introuvable");
                    }
                }
                else
                {
                    Console.WriteLine("La base de donnée est vide");
                }
            }
        }

        /**
         * Fonction permettant de supprimer un utilisateur
         */
        public void DeleteUser(int UserIdToDelete)
        {
            try
            {
                using (var db = new RandomGiftModel())
                {
                    if (db.Users.Count() > 0)
                    {
                        UsersRandomGift User = new UsersRandomGift() { Id = UserIdToDelete };
                        db.Users.Attach(User);
                        db.Users.Remove(User);
                        db.SaveChanges();

                        Console.WriteLine("\nUtilisateur supprimé avec succes");
                    }
                    else
                    {
                        Console.WriteLine("La base de donnée est vide");
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Utilisateur introuvable");
            }
        }

        /**
         * Fonction permettant de choisir au hasard un utilisateur de la base de donnée ainsi qu'un choix de sa liste de souhait
         */
        public void PickWinner()
        {
            using (var db = new RandomGiftModel())
            {
                Console.Clear();

                Random pick = new Random();

                if (db.Users.Count() > 0)
                {
                    int randomUser = pick.Next(0, db.Users.Count());
                    var user = db.Users.ToList()[randomUser];

                    int randomWish = pick.Next(0, user.Wishlist.Split().Count());
                    var wish = user.Wishlist.Split()[randomWish];

                    Console.WriteLine("\nLa personne tiré au hasard est ... " + user.FullName + " !");
                    Console.WriteLine("Son souhait tiré au hasard est ... " + wish + " !!");
                    Console.WriteLine("Félicitations à lui !");
                }
                else
                {
                    Console.WriteLine("La base de donnée est vide");
                }
            }
        }
    }
}
