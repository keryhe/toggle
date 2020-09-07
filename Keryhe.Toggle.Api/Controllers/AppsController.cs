using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Keryhe.Toggle.Api.Models;
using Keryhe.Toggle.Api.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Keryhe.Toggle.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppsController : ControllerBase
    {
        private readonly IToggleRepo _repo;
        private readonly ILogger<FeaturesController> _logger;

        public AppsController(IToggleRepo repo, ILogger<FeaturesController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        // GET: api/apps
        [HttpGet]
        public IEnumerable<App> Get()
        {
            IEnumerable<App> apps = _repo.GetApps();
            return apps;
        }

        // GET api/apps/5
        [HttpGet("{name}")]
        public App Get(string name)
        {
            App app = _repo.GetApp(name);
            return app;
        }

        // GET api/apps/5/features
        [HttpGet("{appId}/features")]
        public IEnumerable<Feature> GetFeatures(int appId)
        {
            IEnumerable<Feature> features = _repo.GetFeatures(appId);
            return features;
        }

        // POST api/apps
        [HttpPost]
        public void Post([FromBody] App value)
        {
        }

        // PUT api/apps/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] App value)
        {
        }

        // DELETE api/apps/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
