using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
//SQLite
using System.Data.SQLite;
using ConsoleApp;
using System.Data;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Threading;

namespace ApplicationRoute
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Ville> Villes = new List<Ville>();
        //compteur pour les noms des villes non existantes
        int cpt = 0;

        //compteur pour suppression ellipse avec ville
        int cpt2 = 0;
        //liste pour suppresion des ellipses
        List<UIElement> itemstoremove = new List<UIElement>();

        //liste d'affichage des chemins resultats
        public ObservableCollection<Chemin> ListeChemin = new ObservableCollection<Chemin>();

        //liste des villes ajoutees de SQLite
        public ObservableCollection<Ville>ListeVillesSQLite = new ObservableCollection<Ville>();

        //liste des chemins resultat
        public ObservableCollection<Chemin> ListeCheminsReslutats = new ObservableCollection<Chemin>();

        //Liste des villes 
        public ObservableCollection<Ville> ListeVilles = new ObservableCollection<Ville>();

        // dictionnaire pour mettre ville et ellipse correspondant ( pour supprimer ellipse apres suppression ville)
        private IDictionary<Ville, Ellipse> Dictionnaire_ville_ellipse = new Dictionary<Ville, Ellipse>();

        public MainWindow()
        {
            InitializeComponent();

            //initialisation liste des villes recuperes de SQLite
            grid_seconde.ItemsSource = ListeVillesSQLite;
            grid_first.ItemsSource = ListeVilles;
            g.ItemsSource = ListeChemin;
        }

        //methode lors du click sur boutton sur carte pour aller chercher une ville sur SQLite
        public void RechercheVilleSQLite(object sender, RoutedEventArgs e)
        {
            tab_global.SelectedIndex = 1;
            ListeVillesSQLite.Clear();
        }
        //api key = AIzaSyBEQyhy6PIAvFhfEI-g4ocRoxd-B36wpfcc

        public void RunProgramme(object sender, RoutedEventArgs e)
        {
            if(ListeVilles==null)
            {
                MessageBox.Show(" Liste des villes vide ! ");
            }
            else
            {
                if (txt_nbrchemin.Text !=null && txt_mutation!=null && txt_elite!=null && txt_crossover!=null)
                {

                    int nbChemins = int.Parse(txt_nbrchemin.Text);
                    int xoverCoefficient = int.Parse(txt_crossover.Text);
                    int xoverPivot = 2;
                    int echangeCoefficient = int.Parse(txt_mutation.Text);
                    int eliteCoefficient = int.Parse(txt_nbrchemin.Text);

                    List<Chemin> totale = new List<Chemin>();
                    List<Chemin> resultat = new List<Chemin>();

                    Ville ville1 = new Ville("Nice", 642, 863);
                    Ville ville2 = new Ville("Saint-laurent", 765, 254);
                    Ville ville3 = new Ville("Cagnes-sur-mer", 206, 475);
                    Ville ville4 = new Ville("Biot", 874, 452);
                    Ville ville5 = new Ville("Antibes", 345, 345);
                    Ville ville6 = new Ville("Mougins", 453, 543);
                    Ville ville7 = new Ville("Grasse", 437, 938);
                    Ville ville8 = new Ville("Cannes", 65, 243);
                    Ville ville9 = new Ville("Valbonne", 234, 976);
                    Ville ville10 = new Ville("Menton", 432, 635);


                    //create a list
                    List<Ville> villes = new List<Ville>();
                    // Add items using Add method   
                    villes.Add(ville1);
                    villes.Add(ville2);
                    villes.Add(ville3);
                    villes.Add(ville4);
                    villes.Add(ville5);
                    villes.Add(ville6);
                    villes.Add(ville7);
                    villes.Add(ville8);
                    villes.Add(ville9);
                    villes.Add(ville10);


                    Generateur generateur = new Generateur();
                    //Generer 10 chemins
                    List<Chemin> chemins = generateur.GenererChemins(nbChemins, villes);
                    //Thread.Sleep(3000);

                    //Mutation
                    List<Chemin> cheminsModifies = generateur.Echanger(chemins, echangeCoefficient);
                    // Thread.Sleep(2000);
                    foreach (Chemin item in cheminsModifies)
                    {
                        totale.Add(item);
                    }
                    //Thread.Sleep(1000);
                    //xover
                    List<Chemin> cheminsXover = generateur.GenererXOver(chemins, xoverPivot, xoverCoefficient);
                    Thread.Sleep(3000);
                    foreach (Chemin item in cheminsXover)
                    {
                        totale.Add(item);
                    }
                    //Thread.Sleep(1000);

                    //elite
                    List<Chemin> cheminsElite = generateur.Elite(chemins, eliteCoefficient);
                    //Thread.Sleep(2000);
                    foreach (Chemin item in cheminsElite)
                    {
                        totale.Add(item);
                    }
                    //Thread.Sleep(1000);

                    //resultat
                    resultat = generateur.Elite(totale, nbChemins);
                    //Thread.Sleep(2000);

                    foreach (Chemin item in resultat)
                    {
                        ListeChemin.Add(item);
                    }
                }
                else
                {
                    MessageBox.Show(" Un des parametres est vide !! ");
                }
            }



        }
        //methode pour revenir a la carte apres ajout des villes recherches
        public void RetourCarte(object sender, RoutedEventArgs e)
        {
            tab_global.SelectedIndex = 0;
        }
        //ajouter une ville a la liste principale depuis SQLite et la dessiner
        public void Ajouter_ville_liste(object sender, RoutedEventArgs e)
         {
            if (ListeVillesSQLite != null)
            {
                if (ListeVilles.Contains((Ville)grid_seconde.SelectedItem))
                {
                    ListeVillesSQLite.RemoveAt(grid_seconde.SelectedIndex);
                    MessageBox.Show("Ville deja presente dans la liste");
                }
                else
                {
                    ListeVilles.Add((Ville)grid_seconde.SelectedItem);
                    //dessiner la ville

                    dessiner_ville((Ville)grid_seconde.SelectedItem);

                    ListeVillesSQLite.RemoveAt(grid_seconde.SelectedIndex);
                    MessageBox.Show("Ville Ajouté à la liste");

                        
                    
                }
                
            }
            
        }
        public void Ajouter_ville(Ville v)
        {
            ListeVilles.Add(v);
        }
        public void getPointedVille(object sender, MouseButtonEventArgs e)
        {
            Point p = Mouse.GetPosition(image_canvas);
            double x = p.X;
            double y = p.Y;

            string name_ville="";
            
            //get ville name from coordonnees
            string cs = @"URI=file:C:\Users\DELL\Documents\Villes.db";

            var con = new SQLiteConnection(cs);
            con.Open();

            string sx =x.ToString().Replace(',', '.');
            string sy = y.ToString().Replace(',', '.');

            string stm = "SELECT name FROM Villes where "+sx+" between xmin and xmax and " + sy + " between ymin and ymax;";

            var cmd = new SQLiteCommand(stm, con);
            SQLiteDataReader rdr = cmd.ExecuteReader();

            if (rdr.Read())
            {
                name_ville = rdr.GetString(0);

            }
            else
            {
                name_ville = "Ville_" + cpt;
                cpt++;
            }
            // faut faire peut être apres la verification si le nom de la ville existe déjà
            Ville v = new Ville(name_ville, (float)x, (float)y);

            
            dessiner_ville(v);
            Ajouter_ville(v);
        }
        public void dessiner_ville(Ville v)
        {
            Ellipse ellipse = new Ellipse() ;
            cpt2++;
            SolidColorBrush mySolidColorBrush = new SolidColorBrush();
            mySolidColorBrush.Color = Colors.Black;
            ellipse.Fill = mySolidColorBrush;
            ellipse.StrokeThickness = 2;
            ellipse.Stroke = Brushes.Aqua;
            ellipse.Width = 8;
            ellipse.Height = 8;

            Canvas.SetTop(ellipse, v.Y -5);
            Canvas.SetLeft(ellipse, v.X -5);
            image_canvas.Children.Add(ellipse);
            Dictionnaire_ville_ellipse.Add(v, ellipse);

        }
        public void Recherche(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(name_ville.Text))
            {
                MessageBox.Show("Nom de ville Manquant !! Toutes les villes vont etre affichees !!");
                var items = new List<Ville>();
                //connection string
                string cs = @"URI=file:C:\Users\DELL\DocumentsVilles.db";

                var con = new SQLiteConnection(cs);
                con.Open();

                string stm = "SELECT * FROM Villes ;";

                var cmd = new SQLiteCommand(stm, con);
                SQLiteDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    var xname = rdr.GetString(1);
                    var xd = rdr.GetFloat(2);
                    var yd = rdr.GetFloat(3);
                    if (ListeVillesSQLite.Contains(new Ville(xname, xd, yd)))
                    {

                    }
                    else
                    {
                        ListeVillesSQLite.Add(new Ville(xname, xd, yd));
                    }

                }
            }
            else
            {
                var items = new List<Ville>();
                //connection string
                string cs = @"URI=file:C:\Users\DELL\Documents\Villes.db";

                var con = new SQLiteConnection(cs);
                con.Open();

                string stm = "SELECT * FROM Villes WHERE name LIKE '%" + name_ville.Text +"%';";

                var cmd = new SQLiteCommand(stm, con);
                SQLiteDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    var xname = rdr.GetString(1);
                    var xd = rdr.GetFloat(2);
                    var yd = rdr.GetFloat(3);
                    if (ListeVillesSQLite.Contains(new Ville(xname, xd, yd)))
                    {

                    }
                    else
                    {
                        ListeVillesSQLite.Add(new Ville(xname, xd, yd));
                    }
                    
                }
                if (ListeVillesSQLite.Count== 0)
                  {
                    MessageBox.Show("Aucune Ville Correspondante au nom donné !");
                }
            } 
        }
        
        private void Reset(object sender, RoutedEventArgs e)
        {
            foreach (Ville item in ListeVilles)
            {
                Ellipse ellipse = Dictionnaire_ville_ellipse[item];
                image_canvas.Children.Remove(ellipse);
            }
            ListeVilles.Clear();
            ListeVillesSQLite.Clear();
            ListeChemin.Clear();
        }
       private void Supprimer_Ville(object sender, MouseButtonEventArgs e)
        {
            //test si on a clické sur le header
            Ville a = (e.Source as ListView).SelectedItem as Ville;

            if (a!=null)
            {
                ListeVilles.Remove(a);

                //image_canvas.Children.RemoveAt();

                Ellipse ellipse = Dictionnaire_ville_ellipse[a];
                image_canvas.Children.Remove(ellipse);
            }
        }
        private void Exit(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
            
    }


}
