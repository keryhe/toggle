using Keryhe.Toggle.Models;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Keryhe.Toggle
{
    public interface IToggleClient
    {
        Task<FeatureModel> GetFeatureAsync(string name);
    }

    public class ToggleClient
    {
        private readonly HttpClient _httpClient;
        private readonly IDistributedCache _cache;

        public ToggleClient(HttpClient httpClient, IDistributedCache cache)
        {
            _httpClient = httpClient;
            _cache = cache;
        }

        public async Task<FeatureModel> GetFeatureAsync(string name)
        {
            FeatureModel feature = await RetrieveFromCache(name);
            
            if (feature == null)
            {
                HttpResponseMessage response = await _httpClient.GetAsync("api/features/" + name);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await TryReadResponseBody(response);
                    feature = JsonSerializer.Deserialize<FeatureModel>(responseBody);

                    await AddToCache(feature);

                    return feature;
                }
                else
                {
                    throw new Exception("Get Feature failed with status code: " + (int)response.StatusCode);
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
            if(encodedFeature != null)
            {
                var featureJson = Encoding.UTF8.GetString(encodedFeature);
                feature = JsonSerializer.Deserialize<FeatureModel>(featureJson);
            }

            return feature;
        }
    }
}
