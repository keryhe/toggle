using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Keryhe.Toggle.Api.Models
{
    public class Feature
    {
        public Feature()
        {

        }

        public Feature(Dictionary<string, object> properties)
        {
            foreach (string key in properties.Keys)
            {
                switch (key)
                {
                    case "Id":
                        Id = (int)properties[key];
                        break;
                    case "Name":
                        Name = properties[key].ToString();
                        break;
                    case "Description":
                        Description = properties[key].ToString();
                        break;
                    case "Toggle":
                        Toggle = (bool)properties[key];
                        break;
                }
            }
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool Toggle { get; set; }
    }
}
