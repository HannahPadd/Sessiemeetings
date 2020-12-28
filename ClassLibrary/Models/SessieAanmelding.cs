using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary.Models
{
    public class SessieAanmelding
    {
        public string SessieId { get; set; }
        public string UserId { get; set; }
        public string Opmerking { get; set; }
        public SessieAanmelding(string sessieId, string userId, string opmerking)
        {
            this.SessieId = sessieId;
            this.UserId = userId;
            this.Opmerking = opmerking;
        }
    }
}
