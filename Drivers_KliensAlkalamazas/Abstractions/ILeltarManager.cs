using Castle.Components.DictionaryAdapter;
using Drivers_KliensAlkalamazas.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drivers_KliensAlkalamazas.Abstractions
{
    public interface ILeltarManager
    {
        BindingList<Leltar> leltar { get; }

        Leltar CreateLeltar(Leltar leltar);
    }
}
