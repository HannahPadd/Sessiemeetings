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

        public SessieAanmelding(string sessieid, string userid, string opmerking)
        {
            this.SessieId = sessieid;
            this.UserId = userid;
            this.Opmerking = opmerking;
        }
    }
}
