using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Keryhe.Toggle.Api.Models;
using Keryhe.Toggle.Api.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Keryhe.Toggle.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeaturesController : ControllerBase
    {
        private readonly IToggleRepo _repo;
        private readonly ILogger<FeaturesController> _logger;

        public FeaturesController(IToggleRepo repo, ILogger<FeaturesController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        // GET: api/features
        [HttpGet]
        public IEnumerable<Feature> Get()
        {
            IEnumerable<Feature> features = _repo.GetFeatures();
            return features;
        }

        // GET api/features/5
        [HttpGet("{id}")]
        public Feature Get(int id)
        {
            Feature feature = _repo.GetFeature(id);
            return feature;
        }

        // GET api/features/5/apps
        [HttpGet("{id}/apps")]
        public IEnumerable<App> GetApps(int id)
        {
            IEnumerable<App> apps = _repo.GetApps(id);
            return apps;
        }

        // POST api/features
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/features/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/features/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
