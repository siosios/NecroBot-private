using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace PoGo.NecroBot.Logic.Model.Settings
{
    [JsonObject(Title = "API Config", Description = "Config your prefered API to use", ItemRequired = Required.DisallowNull)]
    public class APIConfig 
    {

        [DefaultValue(true)]
        [JsonProperty(Required = Required.DisallowNull, DefaultValueHandling = DefaultValueHandling.Populate, Order = 1)]
        public bool UsePogoDevAPI = true;

        [DefaultValue("")]
        [MinLength(0)]
        [MaxLength(100)]
        [JsonProperty(Required = Required.DisallowNull, DefaultValueHandling = DefaultValueHandling.Populate, Order = 9)]
        public string AuthAPIKey = "";

        [DefaultValue(false)]
        [JsonProperty(Required = Required.Default, DefaultValueHandling = DefaultValueHandling.Populate, Order = 15)]
        public bool UseLegacyAPI;
    }
}