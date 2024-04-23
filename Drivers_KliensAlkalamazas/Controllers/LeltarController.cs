using Drivers_KliensAlkalamazas.Abstractions;
using Drivers_KliensAlkalamazas.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Activities;

namespace Drivers_KliensAlkalamazas.Controllers
{
    public class LeltarController
    {
        public ILeltarManager LeltarManager { get; set; }

        public LeltarController()
        {
            LeltarManager = new LeltarManager();
        }

        public int Validalas(string szam)
        {
            if (!SzamValidator(szam))
            {
                throw new ValidationException("Nem számot adtál meg, vagy 4 számjegynél több!");
            }
            return Convert.ToInt32(szam);
        }

        public bool SzamValidator(string szam)
        {
            return Regex.IsMatch(szam, @"^\d{0,4}$");
        }
    }
}
