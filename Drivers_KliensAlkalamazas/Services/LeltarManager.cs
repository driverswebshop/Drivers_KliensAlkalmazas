using Castle.Components.DictionaryAdapter;
using Drivers_KliensAlkalamazas.Abstractions;
using Drivers_KliensAlkalamazas.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drivers_KliensAlkalamazas.Services
{
    public class LeltarManager : ILeltarManager
    {
        public BindingList<Leltar> leltar { get; } = new BindingList<Leltar>();

        public Leltar CreateLeltar(Leltar leltar)
        {
            this.leltar.Add(leltar);
            return leltar;
        }
    }
}
