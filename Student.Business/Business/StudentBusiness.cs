using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Student.Model;
using Student.Enum;
using System.Runtime.InteropServices;

namespace Student.Business.Business
{
    public static class StudentBusiness
    {
        private static List<studentModel> Students;
        private static List<Marksheet> Marksheets;
        private static int s_id = 6;
        private static int m_id = 10;
        static StudentBusiness()
        {
           
            Students = new List<studentModel>
            {
                new studentModel{StudentId=1, Name="Rakesh", JoinDate=new DateTime(2023, 04, 29), Standard=12},
                new studentModel{StudentId=2, Name="Rajesh", JoinDate=new DateTime(2023, 05, 28), Standard=11},
                new studentModel{StudentId=3, Name="Pintu", JoinDate=new DateTime(2022, 03, 11), Standard=11},
                new studentModel{StudentId=4, Name="Ramesh", JoinDate=new DateTime(2024, 06, 15), Standard=12},
                new studentModel{StudentId=5, Name="Litu", JoinDate=new DateTime(2024, 10, 9), Standard=10}
            };

            Marksheets = new List<Marksheet>
            {
                new Marksheet{MarksheetId=1, StudentId=1, Subject=Subjects.PHYSICS, TotalMark=100, MarksObtained=34},
                new Marksheet{MarksheetId=2, StudentId=1, Subject=Subjects.CHEMISTRY, TotalMark=100, MarksObtained=35},
                new Marksheet{MarksheetId=3, StudentId=2, Subject=Subjects.ENGLISH, TotalMark=100, MarksObtained=78},
                new Marksheet{MarksheetId=4, StudentId=3, Subject=Subjects.BIOLOGY, TotalMark=100, MarksObtained=86},
                new Marksheet{MarksheetId=5, StudentId=5, Subject=Subjects.COMPUTER, TotalMark=100, MarksObtained=94},
                new Marksheet{MarksheetId=6, StudentId=2, Subject=Subjects.PHYSICS, TotalMark=100, MarksObtained=33},
                new Marksheet{MarksheetId=7, StudentId=4, Subject=Subjects.PHYSICS, TotalMark=100, MarksObtained=96},
                new Marksheet{MarksheetId=8, StudentId=4, Subject=Subjects.MATHS, TotalMark=100, MarksObtained=98},
                new Marksheet{MarksheetId=9, StudentId=3, Subject=Subjects.CHEMISTRY, TotalMark=100, MarksObtained=87},
            };
        }

        public static void GetList()
        {
            foreach (Marksheet mark in Marksheets)
            {
                Console.WriteLine(mark.ToString());
            }
        }

       
        public static List<studentModel> GetAllStudents() => Students;
        public static List<Marksheet> GetMarksheets() => Marksheets;

        public static double GetTotalMarkObtained(int id)
        {
            List<Marksheet> marksheet = Marksheets.FindAll(m => m.StudentId == id);
            if (marksheet.Count == 0) return -1;

            double result = 0.0;
            foreach (Marksheets marks in marksheet)
            {
                result += marks.MarksObtained;
            }
            return result;
        }
        public static List<SubjectAndMarks>? GetAllMarksById(int id)
        {
            List<Marksheet> marks = Marksheets.FindAll(e => e.StudentId == id);
            if (marks.Count == 0) return null;
            List<SubjectAndMarks> result = new List<SubjectAndMarks>();
            foreach (Marksheet mark in marks)
            {
                result.Add(new() { Subject = mark.Subject, Marks = mark.MarksObtained });
            }
            return result;
        }

        public static SubjectPercentage? GetTotalPercentageObtained(int id)
        {
            List<Marksheet> marks = Marksheets.FindAll(e => e.StudentId == id);
            if (marks.Count == 0) return null;
            Console.WriteLine(marks);
            SubjectPercentage spm = new SubjectPercentage();
            foreach (Marksheet mrk in marks)
            {
                double percentage = (mrk.MarksObtained / mrk.TotalMark) * 100;
                if (mrk.Subject == Subjects.MATHS)
                {
                    spm.MathsPercentage = percentage;
                }
                if (mrk.Subject == Subjects.PHYSICS)
                {
                    spm.PhysicsPercentage = percentage;
                }
                if (mrk.Subject == Subjects.CHEMISTRY)
                {
                    spm.ChemistryPercentage = percentage;
                }
                if (mrk.Subject == Subjects.BIOLOGY)
                {
                    spm.BiologyPercentage = percentage;
                }
                if (mrk.Subject == Subjects.ENGLISH)
                {
                    spm.EnglishPercentage = percentage;
                }
                if (mrk.Subject == Subjects.COMPUTER)
                {
                    spm.ComputerPercentage = percentage;
                }
            }
            return spm;
        }

        public static List<StudentUltimateModel> GetStudentList()
        {
            List<StudentUltimateModel> result = new List<StudentUltimateModel>();
            foreach (studentModel student in Students)
            {
                List<Marksheet> mrks = Marksheets.FindAll(m => m.StudentId == student.StudentId);
                if (mrks.Count == 0)
                {
                    result.Add(new() { StudentId = student.StudentId, Name = student.Name });
                    continue;
                }
                double TotalMarks = 0, TotalMarksObtained = 0, TotalPercentage = 0;
                foreach (Marksheet mrk in mrks)
                {
                    TotalMarks += mrk.TotalMark;
                    TotalMarksObtained += mrk.MarksObtained;

                }
                TotalPercentage = (TotalMarksObtained / TotalMarks) * 100;
                result.Add(new() { StudentId = student.StudentId, Name = student.Name, TotalMark = TotalMarks, TotalMarkObtained = TotalMarksObtained, TotalPercentage = TotalPercentage });
            }
            return result;
        }

       
        public static bool AddMarks(Marksheet m)
        {
            if (Students.Find(p => p.StudentId == m.StudentId) == null) return false;  
            if (Marksheets.Find(p => (p.StudentId == m.StudentId && p.Subject == m.Subject)) != null) return false; 

            m.MarksheetId = m_id++;
            Marksheets.Add(m);
            return true;
        }

        
        public static bool UpdateMarks(Marksheet m)
        {
            var i = Marksheets.FindIndex(p => p.StudentId == m.StudentId && p.Subject == m.Subject);
            if (i == -1) return false;
            //else case
            Marksheets[i] = m;
            return true;
        }
    }
}
