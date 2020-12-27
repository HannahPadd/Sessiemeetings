using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sessiemeetings.Data
{
    public class EvaluationForm
    {
        public string formName { get; set; }
        public List<Field> fields = new List<Field>();
        public EvaluationForm() { }
        public EvaluationForm(String formName, List<Field> fields)
        {
            this.formName = formName;
            this.fields = fields;
        }

    }
    public class Field
    {
        public string title { get; set; }
        public string type { get; set; }

        public List<string> types = new List<string>();
        
    }

}
