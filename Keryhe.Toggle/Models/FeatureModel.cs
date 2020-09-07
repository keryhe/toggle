using System;
using System.Collections.Generic;
using System.Text;

namespace Keryhe.Toggle.Models
{
    public class FeatureModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool Toggle { get; set; }
    }
}
