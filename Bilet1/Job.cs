using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bilet1
{
    [Serializable]
    public class Job
    {
        private string denumire;
        List<Interviu> jobs;

        public Job(string denumire)
        {
            this.denumire = denumire;
            jobs = new List<Interviu>();
        }
        public string Denumire { get => denumire; set => denumire = value; }
        public List<Interviu> Jobs { get => jobs; set => jobs = value; }

        public override string ToString()
        {
            String mesaj = $"Jobul {denumire}:";
            foreach(Interviu i in jobs)
            {
                mesaj += $"\n{i.ToString()}";
            }
            return mesaj;
        }

        public static Job operator+(Job j, Interviu i)
        {
            j.jobs.Add(i);
            return j;
        }

        public Interviu this[int index]
        {
            get => jobs[index];
            set => jobs[index] = value;
        }

    }
}
