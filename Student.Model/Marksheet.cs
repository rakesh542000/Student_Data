using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Student.Enums;

namespace Student.Model
{
    public class Marksheet
    {
        public int MarksheetId { get; set; }
        public int StudentId { get; set; }
        public Subjects Subject { get; set; }
        public double TotalMark { get; set; }
        public double MarksObtained { get; set; }

        public override string ToString() => "<" + MarksheetId + "," + StudentId + "," + Subject + ">";

    }

    public class Subjects
    {
    }
}