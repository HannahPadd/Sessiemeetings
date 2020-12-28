using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary.Models
{
    public class Appointment
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Text { get; set; }



        public Appointment(DateTime Start, DateTime End, string Text)
        {
            this.Start = Start;
            this.End = End;
            this.Text = Text;

        }
    }
}
