using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocoDelight
{
    internal class Choco
    {
        public string Nev { get; set; }
        public string CsokiTipus { get; set; }
        public int Ar { get; set; }
        public string TermekTipusa { get; set; }
        public int KakaoTartalom { get; set; }
        public int Tomeg { get; set; }

        public Choco(string sor) 
        {
            var s = sor.Split(";");
            this.Nev = s[0];
            this.CsokiTipus = s[1];
            this.Ar = int.Parse(s[2]);
            this.TermekTipusa = s[3];
            this.KakaoTartalom = int.Parse(s[4]);
            this.Tomeg = int.Parse(s[5]);
        }
    }
}
