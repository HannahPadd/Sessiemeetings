using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sessiemeetings.Data
{
    public class EvaluationForm
    {
        public string DataID { get; set; }
        public string FormID { get; set; }
        public string formName { get; set; }
        public List<Field> fields = new List<Field>();
        public EvaluationForm() { }
        public EvaluationForm(string DataID, string FormID, string formName, List<Field> fields)
        {
            this.DataID = DataID;
            this.FormID = FormID;
            this.formName = formName;
            this.fields = fields;
        }

    }
    public class Field
    {
        public string title { get; set; }
        public string type { get; set; }
        public string data { get; set; }

        public List<string> types = new List<string>();
        
    }

}
