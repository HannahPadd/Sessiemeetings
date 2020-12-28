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
        bool isConnectionopen = false;


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
            if (isConnectionopen)
            {
                bool isConnectionOpen = OpenConnection();
            }
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
                        System.Diagnostics.Debug.WriteLine("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        System.Diagnostics.Debug.WriteLine("Invalid username/password, please try again");
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
        public void InsertSession(string Naam, string Locatie, string Onderwerp, string Tijd, string Datum, int maxDeelnemers)
        {
            connection.Open();
            string sessionId = GenerateId();
            string query = $"INSERT INTO SessionsTable (SessieID, Naam, Locatie, Onderwerp, Tijd, Datum, MaxDeelnemers, BeschikbarePlekken) VALUES ('{sessionId}', '{Naam}', '{Locatie}', '{Onderwerp}', '{Tijd}', '{Datum}', '{maxDeelnemers}', '{maxDeelnemers}')";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        public void InsertAanmelding(string SessionId, string UserId, string Opmerking)
        {
            connection.Open();
            string query = $"INSERT INTO SessieAanmeldingen (UserId, SessieId, Opmerking) VALUES ('{UserId}', '{SessionId}', '{Opmerking}')";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        public void updateAvailableSpots(string SessionId)
        {
            string query = $"UPDATE SessionsTable SET BeschikbarePlekken = BeschikbarePlekken - 1 WHERE SessieID = '{SessionId}'";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
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
         public List <string> [] GetSessionsList()
         {
            string query = "SELECT * FROM SessionsTable";

            List<string>[] list = new List<string>[8];
            list[0] = new List<string>();
            list[1] = new List<string>();
            list[2] = new List<string>();
            list[3] = new List<string>();
            list[4] = new List<string>();
            list[5] = new List<string>();
            list[6] = new List<string>();
            list[7] = new List<string>();

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Puts the sessiontable into a list for display into the application
                while (dataReader.Read())
                {
                    list[0].Add(dataReader["SessieId"] + "");
                    list[1].Add(dataReader["Naam"] + "");
                    list[2].Add(dataReader["Locatie"] + "");
                    list[3].Add(dataReader["Onderwerp"] + "");
                    list[4].Add(dataReader["Tijd"] + "");
                    list[5].Add(dataReader["Datum"] + "");
                    list[6].Add(dataReader["MaxDeelnemers"] + "");
                    list[7].Add(dataReader["BeschikbarePlekken"] + "");
                }
                dataReader.Close();

                this.CloseConnection();

                return list;
            }
            else
            {
                return list;
            }
             
         }
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


        public string GenerateId()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}
