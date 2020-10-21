using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary.Models {
    class SessionsModel {
        public string Name;
        public string Description;
        public string Location;
        public DateTime Date;
        public DateTime StartingTime;

        public SessionsModel(string name, string description, string location, DateTime date, DateTime startingTime) {
            this.Name = name;
            this.Description = description;
            this.Location = location;
            this.Date = date;
            this.StartingTime = startingTime;
        }
    }
}
