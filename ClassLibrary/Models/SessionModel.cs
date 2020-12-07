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
        public string TodayDate { get; set; }
        public string SessionAdress { get; set; }
    

        public SessionModel(string Id, string name, string location, string description, string date, string todayDate) {
            this.SessionId = Id;
            this.SessionName = name;
            this.SessionDesc = description;
            this.SessionAdress = location;
            this.SessionDate = date;
            this.TodayDate = todayDate;
        }
    }
}
