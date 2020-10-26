using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace SessionMeetingsClassLibrary.Models {
    public class SessionsModel {
        public string name;
        public string description;
        public string location;
        public DateTime dateTime;

    }

    public class SessionsContext : DbContext {
        public DbSet<SessionsModel> Session { get; set; }
    }
}
