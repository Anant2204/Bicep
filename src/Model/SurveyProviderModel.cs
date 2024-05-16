namespace AACN.API.Model
{
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class SurveyProviderModel
    {
        [JsonProperty("@odata.context")]
        public string odatacontext { get; set; }
        public List<SurveyProviderData> value { get; set; }
    }

    public class SurveyProviderData
    {
        [JsonProperty("@odata.etag")]
        public string odataetag { get; set; }
        public string aacn_survey_providerid { get; set; }
    }
}

