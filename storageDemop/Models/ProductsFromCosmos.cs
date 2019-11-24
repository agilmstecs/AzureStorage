using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace storageDemop.Models
{
    public class ProductsFromCosmos
    {
        [Key]
        [JsonProperty(PropertyName = "Product Id")]
        public string ProductId { get; set; }

        [JsonProperty(PropertyName = "Name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "Product Model")]
        public string ProductModel { get; set; }

        [JsonProperty(PropertyName = "Category")]
        public string Category { get; set; }

        [JsonProperty(PropertyName = "Description")]
        public string Description { get; set; }
    }
}