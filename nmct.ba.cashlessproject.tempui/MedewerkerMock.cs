using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.cashlessproject.tempui
{
    public class MedewerkerMock
    {
        private string _naam;

        public string Naam
        {
            get { return _naam; }
            set { _naam = value; }
        }

        private string _email;

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public static List<MedewerkerMock> getSampleList()
        {
            List<MedewerkerMock> res = new List<MedewerkerMock>();
            res.Add(new MedewerkerMock{Naam = "Thomas", Email = "aoeu@dev.null"});
            res.Add(new MedewerkerMock{Naam = "Test", Email = "test@gmail.com"});
            res.Add(new MedewerkerMock{Naam = "Mister X", Email = "me@mister.x"});
            return res;
        }
        
    }
}
