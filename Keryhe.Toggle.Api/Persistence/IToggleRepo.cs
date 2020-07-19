using Keryhe.Toggle.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Keryhe.Toggle.Api.Persistence
{
    public interface IToggleRepo
    {
        IEnumerable<App> GetApps();
        IEnumerable<Feature> GetFeatures();
        IEnumerable<App> GetApps(int featureId);
        IEnumerable<Feature> GetFeatures(int appId);
        App GetApp(int id);
        Feature GetFeature(int id);
    }
}
