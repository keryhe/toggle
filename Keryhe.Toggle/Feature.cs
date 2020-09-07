using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Keryhe.Toggle
{
    public class Feature
    {
        public readonly IToggleClient _toggleClient;
        public Feature(IToggleClient toggleClient)
        {
            _toggleClient = toggleClient;
        }

        public bool IsOn(string name)
        {
            var feature = _toggleClient.GetFeatureAsync(name).GetAwaiter().GetResult();
            return feature.Toggle;
        }

        public bool IsOff(string name)
        {
            var feature = _toggleClient.GetFeatureAsync(name).GetAwaiter().GetResult();
            return !feature.Toggle;
        }

        public async Task<bool> IsOnAsync(string name)
        {
            var feature = await _toggleClient.GetFeatureAsync(name);
            return feature.Toggle;
        }

        public async Task<bool> IsOffAsync(string name)
        {
            var feature = await _toggleClient.GetFeatureAsync(name);
            return !feature.Toggle;
        }
    }
}
