using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary.Models {
    public class SessionModel
    {
        public string SessionId { get; set; }
        public string SessionName { get; set; }
        public string SessionDate { get; set; }
        public string SessionDesc { get; set; }
        public string StartingTime { get; set; }
        public string SessionAddress { get; set; }
        public string MaxAttendees { get; set; }
        public string AvailableSpots { get; set; }
        public string FormID { get; set; }


        public SessionModel(string Id, string name, string location, string description, string date, string startingTime, string maxattendees, string availablespots, string formID) {
            this.SessionId = Id;
            this.SessionName = name;
            this.SessionDesc = description;
            this.SessionAddress = location;
            this.SessionDate = date;
            this.StartingTime = startingTime;
            this.MaxAttendees = maxattendees;
            this.AvailableSpots = availablespots;
            this.FormID = formID;
        }
    }
}
