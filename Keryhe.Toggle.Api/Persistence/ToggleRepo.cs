using Keryhe.Toggle.Api.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Keryhe.Persistence;

namespace Keryhe.Toggle.Api.Persistence
{
    public class ToggleRepo : IToggleRepo
    {
        private readonly IConnectionFactory _factory;

        public ToggleRepo(IConnectionFactory factory)
        {
            _factory = factory;
        }

        public App GetApp(int id)
        {
            using(IDbConnection conn = _factory.CreateConnection())
            {
                App result = null;
                var apps = conn.ExecuteQuery("SELECT * FROM App where Id = " + id.ToString());

                if(apps.Count == 1)
                {
                    result = new App(apps[0]);
                }

                return result;
            }
        }

        public IEnumerable<App> GetApps()
        {
            using (IDbConnection conn = _factory.CreateConnection())
            {
                List<App> results = new List<App>();

                var apps = conn.ExecuteQuery("SELECT * FROM App");

                foreach(Dictionary<string, object> app in apps)
                {
                    results.Add(new App(app));
                }

                return results;
            }
        }

        public IEnumerable<App> GetApps(int featureId)
        {
            using (IDbConnection conn = _factory.CreateConnection())
            {
                List<App> results = new List<App>();

                var apps = conn.ExecuteQuery("SELECT * FROM App a INNER JOIN AppFeatureLink af ON a.Id = af.AppId WHERE af.FeatureId = " + featureId.ToString());

                foreach (Dictionary<string, object> app in apps)
                {
                    results.Add(new App(app));
                }

                return results;
            }
        }

        public Feature GetFeature(int id)
        {
            using (IDbConnection conn = _factory.CreateConnection())
            {
                Feature result = null;
                var features = conn.ExecuteQuery("SELECT * FROM Feature where Id = " + id.ToString());

                if (features.Count == 1)
                {
                    result = new Feature(features[0]);
                }

                return result;
            }
        }

        public IEnumerable<Feature> GetFeatures()
        {
            using (IDbConnection conn = _factory.CreateConnection())
            {
                List<Feature> results = new List<Feature>();

                var features = conn.ExecuteQuery("SELECT * FROM Features");

                foreach (Dictionary<string, object> feature in features)
                {
                    results.Add(new Feature(feature));
                }

                return results;
            }
        }

        public IEnumerable<Feature> GetFeatures(int appId)
        {
            using (IDbConnection conn = _factory.CreateConnection())
            {
                List<Feature> results = new List<Feature>();

                var features = conn.ExecuteQuery("SELECT * FROM Feature f INNER JOIN AppFeatureLink af ON f.Id = af.AppId WHERE af.AppId = " + appId.ToString());

                foreach (Dictionary<string, object> feature in features)
                {
                    results.Add(new Feature(feature));
                }

                return results;
            }
        }
    }
}
