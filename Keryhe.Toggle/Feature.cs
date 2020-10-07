using Keryhe.Toggle.Models;
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
        private readonly HttpClient _httpClient;
        private readonly IDistributedCache _cache;
        private readonly ILogger<Feature> _logger;

        public Feature(HttpClient httpClient, IDistributedCache cache, ILogger<Feature> logger)
        {
            _httpClient = httpClient;
            _cache = cache;
            _logger = logger;
        }

        public bool IsOn(string name)
        {
            var feature = GetFeatureAsync(name).GetAwaiter().GetResult();
            return feature.Toggle;
        }

        public bool IsOff(string name)
        {
            var feature = GetFeatureAsync(name).GetAwaiter().GetResult();
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

        private async Task<FeatureModel> GetFeatureAsync(string name)
        {
            FeatureModel feature = await RetrieveFromCache(name);

            if (feature == null)
            {
                _logger.LogDebug("Retrieve feature {0} from API", name);
                HttpResponseMessage response = await _httpClient.GetAsync("api/features/" + name);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string responseBody = await TryReadResponseBody(response);
                    feature = JsonSerializer.Deserialize<FeatureModel>(responseBody);
                    
                    await AddToCache(feature);

                    return feature;
                }
                else
                {
                    _logger.LogError("Failed to retrieve feature {0} from API, status code {1}", name, response.StatusCode);
                    return new FeatureModel()
                    {
                        Id = 0,
                        Name = name,
                        Toggle = false
                    };
                }
            }

            return feature;
        }

        private async Task<string> TryReadResponseBody(HttpResponseMessage response)
        {
            try
            {
                return await response.Content.ReadAsStringAsync();
            }
            catch
            {
                return "";
            }
        }

        private async Task AddToCache(FeatureModel feature)
        {
            var featureJson = JsonSerializer.Serialize<FeatureModel>(feature);
            var encodedFeature = Encoding.UTF8.GetBytes(featureJson);
            await _cache.SetAsync(feature.Name, encodedFeature);
        }

        private async Task<FeatureModel> RetrieveFromCache(string name)
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
    }
}
