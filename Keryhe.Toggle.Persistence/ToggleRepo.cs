using Keryhe.Toggle.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Keryhe.Persistence;

namespace Keryhe.Toggle.Persistence
{
    public class ToggleRepo : IToggleRepo
    {
        private readonly IConnectionFactory _factory;

        public ToggleRepo(IConnectionFactory factory)
        {
            _factory = factory;
        }

        public int SaveApp(AppModel app)
        {
            using (IDbConnection conn = _factory.CreateConnection())
            {
                conn.Open();

                Dictionary<string, object> parameters = new Dictionary<string, object>();

                if (app.Id > 0)
                {
                    parameters.Add("@name", app.Name);
                    parameters.Add("@description", app.Description);
                    conn.ExecuteNonQuery("INSERT INTO App (Name, Description) VALUES (@name, @description)", CommandType.Text, parameters);
                }
                else
                {
                    parameters.Add("@id", app.Id);
                    parameters.Add("@name", app.Name);
                    parameters.Add("@description", app.Description);
                    conn.ExecuteNonQuery("UPDATE App SET Name = @name, @description = Description WHERE Id = @id", CommandType.Text, parameters);
                }

                return 0;
            }
        }

        public int SaveFeature(FeatureModel feature)
        {
            using (IDbConnection conn = _factory.CreateConnection())
            {
                conn.Open();

                Dictionary<string, object> parameters = new Dictionary<string, object>();

                if (feature.Id > 0)
                {
                    parameters.Add("@name", feature.Name);
                    parameters.Add("@description", feature.Description);
                    parameters.Add("@toggle", feature.Toggle);
                    conn.ExecuteNonQuery("INSERT INTO Feature (Name, Description, Toggle) VALUES (@name, @description, @toggle)", CommandType.Text, parameters);
                }
                else
                {
                    parameters.Add("@id", feature.Id);
                    parameters.Add("@name", feature.Name);
                    parameters.Add("@description", feature.Description);
                    parameters.Add("@toggle", feature.Toggle);
                    conn.ExecuteNonQuery("UPDATE Feature SET Name = @name, @description = Description Toggle = @toogle WHERE Id = @id", CommandType.Text, parameters);
                }

                return 0;
            }
        }
        public void AssignFeatureToApp(int featureId, int appId)
        {
            using (IDbConnection conn = _factory.CreateConnection())
            {
                conn.Open();

                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("@featureId", featureId);
                parameters.Add("@appId", appId);

                conn.ExecuteNonQuery("INSERT INTO AppFeatureLink (FeatureId, AppId) VALUES (@featureId, @AppId)", CommandType.Text, parameters);
            }
        }

        public void UnassignFeatureFromApp(int featureId, int appId)
        {
            using (IDbConnection conn = _factory.CreateConnection())
            {
                conn.Open();

                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("@featureId", featureId);
                parameters.Add("@appId", appId);

                conn.ExecuteNonQuery("DELETE FROM AppFeatureLink WHERE FeatureId = @featureId AND AppId = @appId", CommandType.Text, parameters);
            }
        }

        public AppModel GetApp(string name)
        {
            using(IDbConnection conn = _factory.CreateConnection())
            {
                conn.Open();
                AppModel result = null;

                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@name", name }
                };

                var apps = conn.ExecuteQuery("SELECT * FROM App where Id = @name", CommandType.Text, parameters);

                if(apps.Count == 1)
                {
                    result = new AppModel(apps[0]);
                }

                return result;
            }
        }

        public IEnumerable<AppModel> GetApps()
        {
            using (IDbConnection conn = _factory.CreateConnection())
            {
                conn.Open();
                List<AppModel> results = new List<AppModel>();

                var apps = conn.ExecuteQuery("SELECT * FROM App");

                foreach(Dictionary<string, object> app in apps)
                {
                    results.Add(new AppModel(app));
                }

                return results;
            }
        }

        public IEnumerable<AppModel> GetApps(int featureId)
        {
            using (IDbConnection conn = _factory.CreateConnection())
            {
                conn.Open();
                List<AppModel> results = new List<AppModel>();

                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@featureId", featureId }
                };

                var apps = conn.ExecuteQuery("SELECT * FROM App a INNER JOIN AppFeatureLink af ON a.Id = af.AppId WHERE af.FeatureId = @featureId", CommandType.Text, parameters);

                foreach (Dictionary<string, object> app in apps)
                {
                    results.Add(new AppModel(app));
                }

                return results;
            }
        }

        public FeatureModel GetFeature(string name)
        {
            using (IDbConnection conn = _factory.CreateConnection())
            {
                conn.Open();
                FeatureModel result = null;

                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@name", name }
                };

                var features = conn.ExecuteQuery("SELECT * FROM Feature where Name = @name", CommandType.Text, parameters);

                if (features.Count == 1)
                {
                    result = new FeatureModel(features[0]);
                }

                return result;
            }
        }

        public IEnumerable<FeatureModel> GetFeatures()
        {
            using (IDbConnection conn = _factory.CreateConnection())
            {
                conn.Open();
                List<FeatureModel> results = new List<FeatureModel>();

                var features = conn.ExecuteQuery("SELECT * FROM Features");

                foreach (Dictionary<string, object> feature in features)
                {
                    results.Add(new FeatureModel(feature));
                }

                return results;
            }
        }

        public IEnumerable<FeatureModel> GetFeatures(int appId)
        {
            using (IDbConnection conn = _factory.CreateConnection())
            {
                conn.Open();
                List<FeatureModel> results = new List<FeatureModel>();

                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@appId", appId }
                };

                var features = conn.ExecuteQuery("SELECT * FROM Feature f INNER JOIN AppFeatureLink af ON f.Id = af.AppId WHERE af.AppId = @appId", CommandType.Text, parameters);

                foreach (Dictionary<string, object> feature in features)
                {
                    results.Add(new FeatureModel(feature));
                }

                return results;
            }
        }
    }
}
