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

        public App GetApp(string name)
        {
            using(IDbConnection conn = _factory.CreateConnection())
            {
                App result = null;

                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@name", name }
                };

                var apps = conn.ExecuteQuery("SELECT * FROM App where Id = @name", CommandType.Text, parameters);

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

                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@featureId", featureId }
                };

                var apps = conn.ExecuteQuery("SELECT * FROM App a INNER JOIN AppFeatureLink af ON a.Id = af.AppId WHERE af.FeatureId = @featureId", CommandType.Text, parameters);

                foreach (Dictionary<string, object> app in apps)
                {
                    results.Add(new App(app));
                }

                return results;
            }
        }

        public Feature GetFeature(string name)
        {
            using (IDbConnection conn = _factory.CreateConnection())
            {
                Feature result = null;

                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@name", name }
                };

                var features = conn.ExecuteQuery("SELECT * FROM Feature where Name = @name", CommandType.Text, parameters);

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

                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@appId", appId }
                };

                var features = conn.ExecuteQuery("SELECT * FROM Feature f INNER JOIN AppFeatureLink af ON f.Id = af.AppId WHERE af.AppId = @appId", CommandType.Text, parameters);

                foreach (Dictionary<string, object> feature in features)
                {
                    results.Add(new Feature(feature));
                }

                return results;
            }
        }
    }
}
