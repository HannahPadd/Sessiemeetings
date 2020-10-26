using SessionMeetingsClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SessionMeetingsClassLibrary.Controllers {
    class SessionsController {
        
        public void CreateDB()
        {
            using (var db = new SessionsContext())
            {
                var name = "test";
                var newSession = new SessionsModel() { name = name };
                db.Session.Add(newSession);
                db.SaveChanges();
            }
        } 
    }
}
