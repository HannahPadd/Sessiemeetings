using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary.Models
{
    public class Appointment
    {


        public string Text { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public Appointment(string text, DateTime start, DateTime end)
        {
            this.Text = text;
            this.Start = start;
            this.End = end;
        }
        public Appointment()
        {

        }
        
    }
}
