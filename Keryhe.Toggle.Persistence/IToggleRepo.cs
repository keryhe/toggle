using Keryhe.Toggle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Keryhe.Toggle.Persistence
{
    public interface IToggleRepo
    {
        int SaveApp(AppModel app);
        int SaveFeature(FeatureModel feature);
        void AssignFeatureToApp(int featureId, int appId);
        void UnassignFeatureFromApp(int featureId, int appId);
        IEnumerable<AppModel> GetApps();
        IEnumerable<FeatureModel> GetFeatures();
        IEnumerable<AppModel> GetApps(int featureId);
        IEnumerable<FeatureModel> GetFeatures(int appId);
        AppModel GetApp(string name);
        FeatureModel GetFeature(string name);
    }
}
