using Castle.Components.DictionaryAdapter;
using Drivers_KliensAlkalamazas.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drivers_KliensAlkalamazas.Abstractions
{
    public interface ISzuresManager
    {
        BindingList<Szures> szures { get; }

        Szures CreateSzures(Szures szures);
    }
}
