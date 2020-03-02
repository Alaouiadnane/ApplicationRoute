using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Threading;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {


            /*string cs = @"URI=file:C:\Users\DELL\Documents\Villes.db";

            var con = new SQLiteConnection(cs);
            con.Open();

            var cmd = new SQLiteCommand(con);

            cmd.CommandText = "DROP TABLE IF EXISTS Villes";
            cmd.ExecuteNonQuery();

            cmd.CommandText = @"CREATE TABLE Villes(id INTEGER PRIMARY KEY,
                    name TEXT, x REAL, y REAL)";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "INSERT INTO Villes(name, x,y) VALUES('CASABLANCA',-7.5898434,33.5731104)";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "INSERT INTO Villes(name, x, y) VALUES('RABAT',-6.8325500,34.0132500)";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "INSERT INTO Villes(name, x, y) VALUES('MARRAKECH',-8.008889,31.630000)";
            cmd.ExecuteNonQuery();


            Console.WriteLine("Table Villes created");
            Console.ReadLine();
            /*/
            //connection string

            //hadi diyal te3maar table villes hhhh
            string cs = @"URI=file:C:\Users\DELL\Documents\Villes.db";
            var con = new SQLiteConnection(cs);
            con.Open();
            var cmd = new SQLiteCommand(con);

            /*
            cmd.CommandText = "INSERT INTO Villes(name,x,y, xmin ,xmax, ymin,ymax) VALUES('Marrakech',273,127,270,280,120,130)";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "INSERT INTO Villes(name,x,y, xmin ,xmax, ymin,ymax) VALUES('Rabat',300,62,300,310,60,70)";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "INSERT INTO Villes(name,x,y, xmin ,xmax, ymin,ymax) VALUES('Casablanca',282,72,280,290,70,80)";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "INSERT INTO Villes(name,x,y, xmin ,xmax, ymin,ymax) VALUES('Agadir',324,158,230,240,150,160)";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "INSERT INTO Villes(name,x,y, xmin ,xmax, ymin,ymax) VALUES('Tanger',328,14,325,335,13,23)";
            cmd.ExecuteNonQuery();
            /*/

            /////////////////////////////////////////////////////////////////////

            cmd.CommandText = "INSERT INTO Villes(name,x,y, xmin ,xmax, ymin,ymax) VALUES('DAKHLA',73,340,70,80,332,345)";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "INSERT INTO Villes(name,x,y, xmin ,xmax, ymin,ymax) VALUES('BENI MELLAL',312,108,310,320,100,110)";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "INSERT INTO Villes(name,x,y, xmin ,xmax, ymin,ymax) VALUES('Tan Tan',197,209,190,200,205,215)";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "INSERT INTO Villes(name,x,y, xmin ,xmax, ymin,ymax) VALUES('Essaouira',232,128,230,240,120,130)";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "INSERT INTO Villes(name,x,y, xmin ,xmax, ymin,ymax) VALUES('Oujda',414,43,405,420,40,50)";
            cmd.ExecuteNonQuery();
            Console.ReadLine();

            // hadi ma 3ert diyalch
            /*string cs = @"URI=file:C:\Users\DELL\Documents\Villes.db";

            var con = new SQLiteConnection(cs);
            con.Open();

            string stm = "INsert into Villes(name,x,y) VALUES('', '',''); ";

            var cmd = new SQLiteCommand(stm, con);
            SQLiteDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {

                var items = new List<Ville>();
                Console.WriteLine(rdr.GetString(1));
                Console.WriteLine(rdr.GetFloat(2));
                Console.WriteLine(rdr.GetFloat(3));

                // ... Assign ItemsSource of DataGrid.
            }
            Console.ReadLine();

            //Ville ville1 = new Ville("Nice", 642, 863);
            //Ville ville2 = new Ville("Saint-laurent", 765, 254);
            /*Ville ville3 = new Ville("Cagnes-sur-mer", 206, 475);
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


            Generateur g1 = new Generateur();
            //Generer 10 chemins
            List<Chemin> chemins = g1.GenererChemins(10, villes);
            Console.WriteLine("***** Chemins générés***** ");
            Console.WriteLine(String.Join("\n \n", chemins));

            //Mutation
            List<Chemin> cheminsModifies = g1.Echanger(chemins, 2);
            Console.WriteLine("\n \n *****liste des chemins modifiés*****");
            Console.WriteLine(String.Join("\n", cheminsModifies));

            //xover
            List<Chemin> cheminsXover = g1.GenererXOver(chemins, 2, 2);
            Console.WriteLine("\n \n *****Chemins générés par le xover*****");
            Console.WriteLine(String.Join("\n", cheminsXover));

    /*/
            /*
                    int nbChemins = 8;
                    int xoverCoefficient = 7;
                    int xoverPivot = 2;
                    int echangeCoefficient = 8;
                    int eliteCoefficient = 3;

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
                    Console.WriteLine("***** Chemins générés***** ");
                    Console.WriteLine(String.Join("\n \n", chemins));

                    //Mutation
                    List<Chemin> cheminsModifies = generateur.Echanger(chemins, echangeCoefficient);
                    Thread.Sleep(2000);
                    Console.WriteLine("\n \n *****liste des chemins modifiés*****");
                    Console.WriteLine(String.Join("\n", cheminsModifies));
                    totale.AddRange(cheminsModifies);
                    //Thread.Sleep(3000);

                    //xover
                    List<Chemin> cheminsXover = generateur.GenererXOver(chemins, xoverPivot, xoverCoefficient);
                    Thread.Sleep(3000);
                    Console.WriteLine("\n \n *****Chemins générés par le xover*****");
                    Console.WriteLine(String.Join("\n", cheminsXover));
                    totale.AddRange(cheminsXover);
                    //Thread.Sleep(4000);

                    //elite
                        List<Chemin> cheminsElite = generateur.Elite(chemins, eliteCoefficient);
                    //Thread.Sleep(4000);
                    Console.WriteLine("\n \n *****Chemins générés par le Elite");
                    Console.WriteLine(String.Join("\n", cheminsElite));
                    totale.AddRange(cheminsElite);
                    //Thread.Sleep(4000);

                    //resultat
                    resultat = generateur.Elite(totale, nbChemins);
                    //Thread.Sleep(4000);
                    Console.WriteLine("\n \n *****Resultat");
                    Console.WriteLine(String.Join("\n", resultat));

                    Console.ReadLine();/*/
        }
    }
}