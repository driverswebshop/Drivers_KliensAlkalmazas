using Drivers_KliensAlkalamazas.Abstractions;
using Drivers_KliensAlkalamazas.Entities;
using Drivers_KliensAlkalamazas.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Schema;
using System.Activities;

namespace Drivers_KliensAlkalamazas.Controllers
{
    public class SzuresController
    {
        public ISzuresManager SzuresManager { get; set; }

        public SzuresController()
        {
            SzuresManager = new SzuresManager();
        }

        public string Validalas(string szures)
        {
            if (!SzuresValidator(szures))
            {
                throw new ValidationException("A megadott SKU nem megfelelő\nA megfelelő kód G/T-vel kezdődik és 7 szám követi.");
            }

            return szures;
        }

        public bool SzuresValidator(string szures)
        {
            return Regex.IsMatch(szures, @"^(?:[GT]\d{7})?$");
        }
    }
}
