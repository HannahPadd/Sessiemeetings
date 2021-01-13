using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary.Models
{
    public class Appointment
    {

        public Appointment() { }

        public String Text { get; set; }
        public string Location { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public Appointment(string text, string location, DateTime start, DateTime end)
        {
            this.Text = text;
            this.Location = location;
            this.Start = start;
            this.End = end;
        }
        
    }
}
