using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.DesktopApplication.Views.Components
{

    public interface IComponent

    {
        void ForceSetValue();
        bool HasError { get; set; }
    }
}
