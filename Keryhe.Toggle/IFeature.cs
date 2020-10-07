using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Keryhe.Toggle
{
    public interface IFeature
    {
        bool IsOn(string name);
        bool IsOff(string name);

        Task<bool> IsOnAsync(string name);

        Task<bool> IsOffAsync(string name);
    }
}
