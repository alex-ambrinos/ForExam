using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bilet1
{
    [Serializable]
    public class Interviu:ICalculPunctaj, ICloneable
    {
        private string nume;
        private int varsta;
        private int nrIntrebariCorecte;
        private float nota;
        private DateTime data;

        public Interviu(string nume, int varsta, int nrIntrebariCorecte, DateTime data)
        {
            this.nume = nume;
            this.varsta = varsta;
            this.nrIntrebariCorecte = nrIntrebariCorecte;
            this.nota = this.CalculareNota(nrIntrebariCorecte);
            this.data = data;
        }
        public string Nume { get => nume; set => nume = value; }
        public int Varsta { get => varsta; set => varsta = value; }
        public int NrIntrebari { get => nrIntrebariCorecte; set => nrIntrebariCorecte = value; }
        public float Nota { get => nota; set => nota = value; }
        public DateTime Data { get => data; set => data = value; }

        public float CalculareNota(int nrIntrebari)
        {
            float rez = nrIntrebari * 10 / 20;
            return rez;
        }

        public object Clone()
        {
            Interviu clone = new Interviu(nume, varsta, nrIntrebariCorecte, data);
            return clone;
        }

        public override string ToString()
        {
            return $"Nume: {nume}, Varsta: {varsta}, Raspunsuri: {nrIntrebariCorecte}, Nota: {nota}, Data: {data.Date}";
        }
    }
}
