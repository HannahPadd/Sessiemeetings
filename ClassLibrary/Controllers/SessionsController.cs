using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ClassLibrary.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;

namespace ClassLibrary.Controllers {
    class SessionsController {
        private string connectionString;

        public SessionsController() {
            connectionString = "host=localhost port=5432 dbname=mydb connect_timeout=10";
        }
        
        //Creates the Database table for session if it doesn't exist already
        public bool CreateDBTableSessions() {
            try
            {
                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["host=localhost port=5432 dbname=mydb connect_timeout=10"].ConnectionString))
                {
                    db.Query<SessionsModel>(@"CREATE TABLE if not exists KennisSessie (
                                            SessieID int,
                                            Datum Date,
                                            Naam varchar(255),
                                            Locatie varchar(255),
                                            StartTijd time,
                                            AantalDeelnemers int,
                                            PRIMARY KEY(SessieID)
                                            ); 
                                        ");
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
        
        //Returns all the data that is in the Database for sessions
        public void ShowAllSessions() {
            List<SessionsModel> sessionsList = new List<SessionsModel>();
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["host=localhost port=5432 dbname=mydb connect_timeout=10"].ConnectionString)) {
                sessionsList = db.Query<SessionsModel>("Select * from sessions").ToList();
            }    
        }
        
        //Adds a session to the session Database
        public void AddSession() {

        }
    }
}
