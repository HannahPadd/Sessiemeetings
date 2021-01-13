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
        public void InsertSession(string Naam, string Locatie, string Onderwerp, string Tijd, string Datum)
        {
            connection.Open();
            string sessionId = GenerateId();
            string query = $"INSERT INTO SessionsTable (SessieID, Naam, Locatie, Onderwerp, Tijd, Datum) VALUES ('{sessionId}', '{Naam}', '{Locatie}', '{Onderwerp}', '{Tijd}', '{Datum}')";
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

            List<string>[] list = new List<string>[6];
            list[0] = new List<string>();
            list[1] = new List<string>();
            list[2] = new List<string>();
            list[3] = new List<string>();
            list[4] = new List<string>();
            list[5] = new List<string>();

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


        //Gets the sessions back from the current user
        public List<string>[] GetSessionsOfUser(string currentUser) 
        {
            string query = $"SELECT * from SessieAanmeldingen WHERE UserId='{currentUser}'";
            List<string> list = new List<string>();

            List<string>[] sessionsList = new List<string>[6];
            sessionsList[0] = new List<string>();
            sessionsList[1] = new List<string>();
            sessionsList[2] = new List<string>();
            sessionsList[3] = new List<string>();
            sessionsList[4] = new List<string>();
            sessionsList[5] = new List<string>();

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    list.Add(dataReader["SessieId"] + "");
                }
                dataReader.Close();
                for (int i = 0; i < list.Count; i++) {
                    string query2 = $"SELECT * from SessionsTale WHERE SessieID='{list[i]}'";

                    MySqlCommand cmd2 = new MySqlCommand(query2, connection);
                    MySqlDataReader dataReader2 = cmd.ExecuteReader();

                    while (dataReader2.Read())
                    {
                       sessionsList[0].Add(dataReader["SessieId"] + "");
                       sessionsList[1].Add(dataReader["Naam"] + "");
                       sessionsList[2].Add(dataReader["Locatie"] + "");
                       sessionsList[3].Add(dataReader["Onderwerp"] + "");
                       sessionsList[4].Add(dataReader["Tijd"] + "");
                       sessionsList[5].Add(dataReader["Datum"] + "");
                    }
                 }
                return sessionsList;
            }
                
            else
            {
                return sessionsList;
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
