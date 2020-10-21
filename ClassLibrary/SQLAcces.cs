using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Dapper;


namespace ClassLibrary {
    public class SQLAcces {

        private string connectionString;
        public SQLAcces() {
            connectionString = "host=localhost port=5432 dbname=mydb connect_timeout=10";
            
        }
  
        public void StartSQL() {

        }
        

        public bool saveToDB() {
            return true;
        }
    }
}
