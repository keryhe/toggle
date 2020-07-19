using System;
using System.Collections.Generic;
using System.Text;

namespace Keryhe.Toggle
{
    public class Feature
    {
        public bool IsOn(string name)
        {
            return FeatureCache.Instance.Get(name);
        }

        public bool IsOff(string name)
        {
            return !FeatureCache.Instance.Get(name);
        }
    }
}
