using Castle.Components.DictionaryAdapter;
using Drivers_KliensAlkalamazas.Abstractions;
using Drivers_KliensAlkalamazas.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Drivers_KliensAlkalamazas.Services
{
    public class SzuresManager : ISzuresManager
    {
        public BindingList<Szures> szures { get; } = new BindingList<Szures>();

        public Szures CreateSzures(Szures szures)
        {
            this.szures.Add(szures);
            return szures;
        }


    }
}
