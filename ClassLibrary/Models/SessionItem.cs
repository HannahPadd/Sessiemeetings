using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace ClassLibrary.Models
{
    public class SessionItem
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public bool IsDone { get; set; }
        public string Address { get; set; }
    }
}
