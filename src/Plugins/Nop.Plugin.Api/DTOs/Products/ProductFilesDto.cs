using Newtonsoft.Json;

namespace Nop.Plugin.Api.DTOs.Products
{
    [JsonObject(Title = "product_files")]
    public class ProductFilesDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("specification")]
        public string Specification { get; set; }

        [JsonProperty("specification_file_name")]
        public string SpecificationFileName { get; set; }

        [JsonProperty("accessories")]
        public string Accessories { get; set; }

        [JsonProperty("accessories_file_name")]
        public string AccessoriesFileName { get; set; }

    }
}
