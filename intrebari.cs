using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistech
{

    public class Chestionar
    {
        public string Titlu { get; set; }
        public List<string> Intrebari { get; set; }

        public Chestionar(string titlu)
        {
            Titlu = titlu;
            Intrebari = new List<string>();
        }
    }
    public class Intrebare
    {
        private int id;
        private string text;

        public Intrebare(int id, string text)
        {
            this.id = id;
            this.text = text;
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        public override string ToString()
        {
            return $"ID Întrebare: {id}, Text: {text}";
        }
    }

    public class Raspuns
    {
        private int idIntrebare;
        private string idAngajat;
        private string raspunsText;

        public Raspuns(int idIntrebare, string idAngajat, string raspunsText)
        {
            this.idIntrebare = idIntrebare;
            this.idAngajat = idAngajat;
            this.raspunsText = raspunsText;
        }

        public int IdIntrebare
        {
            get { return idIntrebare; }
            set { idIntrebare = value; }
        }

        public string IdAngajat
        {
            get { return idAngajat; }
            set { idAngajat = value; }
        }

        public string RaspunsText
        {
            get { return raspunsText; }
            set { raspunsText = value; }
        }

        public override string ToString()
        {
            return $"ID Întrebare: {idIntrebare}, ID Angajat: {idAngajat}, Răspuns: {raspunsText}";
        }
    }

    public class Evaluare
    {
        private string titlu;
        private DateTime dataCrearii;
        private string creatDe;

        public Evaluare(string titlu, string creatDe)
        {
            this.titlu = titlu;
            this.dataCrearii = DateTime.Now;
            this.creatDe = creatDe;
        }

        public string Titlu
        {
            get { return titlu; }
            set { titlu = value; }
        }

        public DateTime DataCrearii
        {
            get { return dataCrearii; }
        }

        public string CreatDe
        {
            get { return creatDe; }
        }

        public override string ToString()
        {
            return $"Titlu: {titlu}, Creat de: {creatDe}, Data creării: {dataCrearii}";
        }
    }
}
