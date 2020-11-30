using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace ClassLibrary
{
    public class DbConnection
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;


        public DbConnection()
        {
            Initialize();
        }

        private void Initialize()
        {
            server = "";
            database = "0993055";
            uid = "0993055";
            password = "ihuqueis";
            string connectionString;
            connectionString = $"Server=sql.hosted.hr.nl;user id={uid};password={password};database={database}";
            connection = new MySqlConnection(connectionString);
            OpenConnection();

        }
        //Opens the connection to the Database
        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                Console.WriteLine("Succesfully connected to the database");
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        Console.WriteLine("Cannot connect to server.  Contact administrator");
                        break;
                   
                    case 1045:
                        Console.WriteLine("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }
        //Closes the connection to the Database
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        //Inserts into the Database
        public void InsertSession(string Naam, string Locatie, string Onderwerp, string Tijd, string Datum)
        {
            string query = $"INSERT INTO SessionsTable (Naam, Locatie, Onderwerp, Tijd, Datum) VALUES ('{Naam}', '{Locatie}', '{Onderwerp}', 'Time', 'Datum')";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        //Updates the Database
        public void Update()
        {

        }
        //Deletes in the Database
        public void Delete()
        {

        }
        //Selects from the Database
    //  public List <string> [] Select()
    //  {
    //      string query = "SELECT * FROM SessionsTable";
    //      using MySqlCommand cmd = new MySqlCommand(query, connection);
    //      using MySqlDataReader result = cmd.ExecuteReader();
    //
    //      while (result.Read())
    //      {
    //
    //      }
    //      
    //  }
        //Count statement
        public int Count()
        {
            return 0;
        }
        //Makes a backup of the Database
        public void Backup()
        {

        }
        //Restores the backup
        public void Restore()
        {

        }
    }
}
