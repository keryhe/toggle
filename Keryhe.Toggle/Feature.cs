using Keryhe.Toggle.Models;
using Keryhe.Toggle.Persistence;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Keryhe.Toggle
{
    public class Feature: IFeature
    {
        private readonly IToggleRepo _repo;
        private readonly IDistributedCache _cache;
        private readonly ILogger<Feature> _logger;

        public Feature(IToggleRepo repo, IDistributedCache cache, ILogger<Feature> logger)
        {
            _repo = repo;
            _cache = cache;
            _logger = logger;
        }

        public bool IsOn(string name)
        {
            var feature = GetFeature(name);
            return feature.Toggle;
        }

        public bool IsOff(string name)
        {
            var feature = GetFeature(name);
            return !feature.Toggle;
        }

        public async Task<bool> IsOnAsync(string name)
        {
            var feature = await GetFeatureAsync(name);
            return feature.Toggle;
        }

        public async Task<bool> IsOffAsync(string name)
        {
            var feature = await GetFeatureAsync(name);
            return !feature.Toggle;
        }

        #region Sync
        private FeatureModel GetFeature(string name)
        {
            FeatureModel feature = RetrieveFromCache(name);

            if (feature == null)
            {
                _logger.LogDebug("Retrieve feature {0} from DB", name);
                feature = _repo.GetFeature(name);
                AddToCache(feature);
            }

            return feature;
        }

        private void AddToCache(FeatureModel feature)
        {
            var featureJson = JsonSerializer.Serialize<FeatureModel>(feature);
            var encodedFeature = Encoding.UTF8.GetBytes(featureJson);
            _cache.Set(feature.Name, encodedFeature);
        }

        private FeatureModel RetrieveFromCache(string name)
        {
            FeatureModel feature = null;

            var encodedFeature = _cache.Get(name);
            if (encodedFeature != null)
            {
                var featureJson = Encoding.UTF8.GetString(encodedFeature);
                feature = JsonSerializer.Deserialize<FeatureModel>(featureJson);
            }

            return feature;
        }
        #endregion

        #region Async
        private async Task<FeatureModel> GetFeatureAsync(string name)
        {
            FeatureModel feature = await RetrieveFromCacheAsync(name);

            if (feature == null)
            {
                _logger.LogDebug("Retrieve feature {0} from DB", name);
                feature = _repo.GetFeature(name);
                await AddToCacheAsync(feature);
            }

            return feature;
        }

        

        private async Task AddToCacheAsync(FeatureModel feature)
        {
            var featureJson = JsonSerializer.Serialize<FeatureModel>(feature);
            var encodedFeature = Encoding.UTF8.GetBytes(featureJson);
            await _cache.SetAsync(feature.Name, encodedFeature);
        }

        private async Task<FeatureModel> RetrieveFromCacheAsync(string name)
        {
            FeatureModel feature = null;

            var encodedFeature = await _cache.GetAsync(name);
            if (encodedFeature != null)
            {
                var featureJson = Encoding.UTF8.GetString(encodedFeature);
                feature = JsonSerializer.Deserialize<FeatureModel>(featureJson);
            }

            return feature;
        }
        #endregion
    }
}
