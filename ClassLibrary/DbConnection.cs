using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using System.Text.Json;


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
        public void InsertSession(string Naam, string Locatie, string Onderwerp, string Tijd, string Datum, int maxDeelnemers, string FormID)
        {
            connection.Open();
            string sessionId = GenerateId();
            string query = $"INSERT INTO SessionsTable (SessieID, Naam, Locatie, Onderwerp, Tijd, Datum, MaxDeelnemers, BeschikbarePlekken, FormID) VALUES ('{sessionId}', '{Naam}', '{Locatie}', '{Onderwerp}', '{Tijd}', '{Datum}', '{maxDeelnemers}', '{maxDeelnemers}', '{FormID}')";
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
            string query = $"INSERT INTO SessieAanmeldingen (UserId, SessieId, Opmerking, IsAanwezig) VALUES ('{UserId}', '{SessionId}', '{Opmerking}', 'False')";
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

        public void InsertForm(string Name, string Fields)
        {
            connection.Open();
            string formID = GenerateId();
            string query = $"INSERT INTO FormsTable (FormID, Name, Fields) VALUES ('{formID}', '{Name}', '{Fields}')";
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

        public void InsertFormsData( string user, string formName, string Fields)
        {
            connection.Open();
            string dataID = GenerateId();
            string query = $"INSERT INTO FormsDataTable (DataID, UserID, FormName, FieldsData) VALUES ('{dataID}','{user}', '{formName}', '{Fields}')";
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

            List<string>[] list = new List<string>[9];
            list[0] = new List<string>();
            list[1] = new List<string>();
            list[2] = new List<string>();
            list[3] = new List<string>();
            list[4] = new List<string>();
            list[5] = new List<string>();
            list[6] = new List<string>();
            list[7] = new List<string>();
            list[8] = new List<string>();

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
                    list[8].Add(dataReader["FormID"] + "");
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


        public List<string>[] GetFormsList()
        {
            string query = "SELECT * FROM FormsTable";

            List<string>[] list = new List<string>[3];
            list[0] = new List<string>();
            list[1] = new List<string>();
            list[2] = new List<string>();

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Puts the sessiontable into a list for display into the application
                while (dataReader.Read())
                {
                    list[0].Add(dataReader["FormID"] + "");
                    list[1].Add(dataReader["Name"] + "");
                    list[2].Add(dataReader["Fields"] + "");
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

        public List<string>[] GetFormsDataList(string formName)
        {
            string query = "SELECT * FROM FormsDataTable WHERE FormName LIKE" + "'" + formName + "'";

            List<string>[] list = new List<string>[4];
            list[0] = new List<string>();
            list[1] = new List<string>();
            list[2] = new List<string>();
            list[3] = new List<string>();

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Puts the sessiontable into a list for display into the application
                while (dataReader.Read())
                {
                    list[0].Add(dataReader["DataID"] + "");
                    list[1].Add(dataReader["UserID"] + "");
                    list[2].Add(dataReader["FormName"] + "");
                    list[3].Add(dataReader["FieldsData"] + "");
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

        public List<string> GetFormData(string DataID)
        {
            string query = "SELECT * FROM FormsDataTable WHERE DataID LIKE" + "'" + DataID + "'";
            List<string> list = new List<string>(4);

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Puts the sessiontable into a list for display into the application
                while (dataReader.Read())
                {
                    list.Add(dataReader["DataID"] + "");
                    list.Add(dataReader["UserID"] + "");
                    list.Add(dataReader["FormName"] + "");
                    list.Add(dataReader["FieldsData"] + "");
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




        //Select users from the Database and returns them
        public List<string>[] GetUsersList()
        {
            string query = "SELECT * from AspNetUsers";

            List<string>[] list = new List<string>[6];
            list[0] = new List<string>();
            list[1] = new List<string>();
            list[2] = new List<string>();

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    list[0].Add(dataReader["Id"] + "");
                    list[1].Add(dataReader["UserName"] + "");
                    list[2].Add(dataReader["Email"] + "");
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

        public List<string>[] GetAanmeldingen()
        {
            string query = "SELECT * from SessieAanmeldingen";

            List<string>[] list = new List<string>[6];
            list[0] = new List<string>();
            list[1] = new List<string>();
            list[2] = new List<string>();
            list[3] = new List<string>();

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    list[0].Add(dataReader["UserId"] + "");
                    list[1].Add(dataReader["SessieId"] + "");
                    list[2].Add(dataReader["Opmerking"] + "");
                    list[3].Add(dataReader["IsAanwezig"] + "");
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

        public void DeelnemerAanwezig(string UserId, string SessionId)
        {
            connection.Open();
            string query = $"UPDATE SessieAanmeldingen SET IsAanwezig = 'True' WHERE UserId = '{UserId}' AND SessieId = '{SessionId}'";
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

        public void DeelnemerAfwezig(string UserId, string SessionId)
        {
            connection.Open();
            string query = $"UPDATE SessieAanmeldingen SET IsAanwezig = 'False' WHERE UserId = '{UserId}' AND SessieId = '{SessionId}'";
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
    }
}
