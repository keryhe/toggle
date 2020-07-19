using System;
using System.Collections.Generic;
using System.Text;

namespace Keryhe.Toggle
{
    internal sealed class FeatureCache
    {
        private Dictionary<string, bool> _features;

        private FeatureCache()
        {
            _features = new Dictionary<string, bool>();
        }

        public static FeatureCache Instance { get; } = new FeatureCache();

        public void Add(Dictionary<string, bool> features)
        {
            _features = features;
        }

        public void Add(string name, bool value)
        {
            if (_features.ContainsKey(name))
            {
                _features.Remove(name);
            }

            _features.Add(name, value);
        }

        public void Remove(string name)
        {
            _features.Remove(name);
        }

        public bool Get(string name)
        {
            return _features[name];
        }
    }
}
