namespace MOTUS
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool replay;
            do
            {
                Console.WriteLine("Bienvenue dans MOTUS !");
                string motAleatoire = ChargerMots();

                if (motAleatoire == null)
                {
                    Console.WriteLine("Impossible de charger un mot. Fin du programme.");
                    return;
                }

                AfficherPremierMot(motAleatoire);
                Console.WriteLine($"\n{motAleatoire}");

                int tentativesRestantes = motAleatoire.Length;
                bool motTrouve = false;

                for (int i = 1; i < motAleatoire.Length && !motTrouve; i++)
                {
                    tentativesRestantes--;
                    Console.WriteLine($"Il vous reste {tentativesRestantes} coups à jouer");

                    string MotUser = GetMot(motAleatoire.Length);
                    Comparaison(motAleatoire, MotUser);

                    if (motAleatoire == MotUser)
                    {
                        motTrouve = true;
                    }
                }

                if (!motTrouve)
                {
                    Console.WriteLine($"\nDommage, vous avez perdu ! Le mot caché était {motAleatoire}");
                }

                Console.WriteLine("Voulez-vous rejouer ? (Y/N)");

                string reponse;
                do
                {
                    reponse = Console.ReadLine();
                    replay = reponse.Equals("y", StringComparison.OrdinalIgnoreCase) || reponse.Equals("yes", StringComparison.OrdinalIgnoreCase);

                    if (!replay && !reponse.Equals("n", StringComparison.OrdinalIgnoreCase) && !reponse.Equals("no", StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine("Veuillez entrer une réponse valide : Y pour Oui ou N pour Non.");
                    }

                } while (!reponse.Equals("y", StringComparison.OrdinalIgnoreCase) &&
                         !reponse.Equals("n", StringComparison.OrdinalIgnoreCase) &&
                         !reponse.Equals("yes", StringComparison.OrdinalIgnoreCase) &&
                         !reponse.Equals("no", StringComparison.OrdinalIgnoreCase));
                Console.WriteLine("");

            } while (replay);
        }


        // ------------------------------------------------------------------------------------ 
        public static string GetMot(int lenMot)
        {
            string MotUser="";
            do
            {
                Console.Write($"Entrer votre mot de {lenMot} charactere :");
                MotUser = Console.ReadLine();
            } while (MotUser.Length != lenMot);
            return MotUser;
        }

        // ------------------------------------------------------------------------------------ 
        public static bool Comparaison(string motAleatoire, string UserMot)
        {
            bool motTrouve = motAleatoire == UserMot;

            for (int i = 0; i < motAleatoire.Length; i++)
            {
                bool yellowtesteur = false;
                bool redtesteur = false;

                if (motAleatoire[i] == UserMot[i])
                {
                    redtesteur = true;
                    AfficherCouleur(UserMot[i].ToString(), ConsoleColor.Red);
                }
                else
                {
                    for (int j = 0; j < motAleatoire.Length; j++)
                    {
                        if (motAleatoire[j] == UserMot[i] && j != i)
                        {
                            yellowtesteur = true;
                            AfficherCouleur(UserMot[i].ToString(), ConsoleColor.Yellow);
                            break;
                        }
                    }
                }

                if (!yellowtesteur && !redtesteur)
                {
                    Console.Write(UserMot[i]);
                }
            }

            Console.WriteLine();

            if (motTrouve)
            {
                Console.WriteLine($"\nBravo, vous avez gagné ! Le mot caché était {motAleatoire}");
            }

            return motTrouve;
        }


        // ------------------------------------------------------------------------------------ 
        public static string ChargerMots()
        {
            string cheminFichier = "mot.txt";
            try
            {
                string[] mots = File.ReadAllLines(cheminFichier);

                if (mots.Length > 0)
                {
                    Random random = new Random();
                    int indexAleatoire;
                    string motAleatoire;

                    do
                    {
                        indexAleatoire = random.Next(0, mots.Length);
                        motAleatoire = mots[indexAleatoire];

                    } while (motAleatoire.Length < 6 || motAleatoire.Length > 8);

                    return motAleatoire;
                }
                else
                {
                    Console.WriteLine("Le fichier est vide.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la lecture du fichier : {ex.Message}");
            }

            return null;
        }

        // ------------------------------------------------------------------------------------ 

        public static void AfficherPremierMot(string motAleatoire)
        {
            motAleatoire = motAleatoire.ToUpper();
            int compteur = motAleatoire.Length;
            Console.Write($"Le mot caché contient {compteur} characteres et il commence par la lettre ");
            AfficherCouleur(motAleatoire[0].ToString(), ConsoleColor.Red);
            Console.WriteLine();
            

        }

        // ------------------------------------------------------------------------------------ 

        public static void AfficherCouleur(String texte, ConsoleColor couleur)
        {
            Console.ForegroundColor = couleur; 
            Console.Write(texte);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
