using System;

namespace Activity4
{
    // Interface
    interface ProgramInterface
    {
        string Name { get; set; }
        bool NeedHelp { get; set; }
        int Attempts { get; set; }
        delegate void GetTutoringEventHandler(object source, EventArgs args);
        void TakeTest(int testGrade);
    }
    public class Student : ProgramInterface
    {
        // Indexer
        class PastGrades<T>
        {
            private T[] arr = new T[5];

            public T this[int i]
            {
                get { return arr[i]; }
                set { arr[i] = value; }
            }
        }

        // Fields
        private string name;
        private bool needHelp;
        private int attempts;
        private PastGrades<int> grades = new PastGrades<int>();

        // Properties
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public bool NeedHelp
        {
            get { return needHelp; }
            set { needHelp = value; }
        }
        public int Attempts
        {
            get { return attempts; }
            set { attempts = value; }
        }

        //define a delegate
        public delegate void GetTutoringEventHandler(object source, EventArgs args);

        //declare the event
        public event GetTutoringEventHandler RequestTutor;

        // Instance method
        public void TakeTest(int testGrade)
        {
            Console.WriteLine($"This is the {attempts+1} time student {name} is taking the test");
            Console.WriteLine($"\tTheir score was {testGrade}");
            needHelp = (testGrade < 65);
            grades[attempts] = testGrade;
            attempts++;
            if (needHelp)
            {
                OnFailedTest();
            } else
            {
                Console.WriteLine($"\tStudent {name} have passed the test on their {attempts+1} attempt");
            }
        }

        protected virtual void OnFailedTest()
        {
            if (RequestTutor != null)
            {
                RequestTutor(this, null);
                Console.WriteLine($"\tStudent {name} got brushed up on their skills");
            }
        }
    }
    class TestCenter
    {
        public void OnFailedTest(object source, EventArgs args)
        {
            Console.WriteLine("\tRequesting tutoring for them ...");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var Anna = new Student();
            var Midterm = new TestCenter();
            Anna.Name = "Anna";
            Anna.Attempts = 0;
            Anna.RequestTutor += Midterm.OnFailedTest;
            Anna.TakeTest(60);
            Anna.TakeTest(70);
            Console.WriteLine("This concludes the testing season");
        }
    }
}

