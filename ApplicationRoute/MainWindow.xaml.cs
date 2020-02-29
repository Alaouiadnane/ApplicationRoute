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

        //liste des villes ajoutees de SQLite
        public ObservableCollection<Ville>ListeVillesSQLite = new ObservableCollection<Ville>();


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
        }

        //methode lors du click sur boutton sur carte pour aller chercher une ville sur SQLite
        public void RechercheVilleSQLite(object sender, RoutedEventArgs e)
        {
            tab_global.SelectedIndex = 1;
            ListeVillesSQLite.Clear();
        }
        //api key = AIzaSyBEQyhy6PIAvFhfEI-g4ocRoxd-B36wpfcc
        public void Choix_ville(object sender, MouseButtonEventArgs e)
        {
            // faut faire peut être apres la verification si le nom de la ville existe déjà

            // ajout de la ville dans dictionnaire afin de supprimer le ellipse a la suppression de la ville

        }

        public void RunProgramme(object sender, RoutedEventArgs e)
        {

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
                string cs = @"URI=file:C:\Users\DELL\Documents\Villes.db";

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
            
    }


}
