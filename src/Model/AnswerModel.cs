namespace AACN.API.Model
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class AnswerModel
    {
        [JsonProperty("@odata.context")]
        public string odatacontext { get; set; }

        [JsonProperty("@Microsoft.Dynamics.CRM.totalrecordcount")]
        public int MicrosoftDynamicsCRMtotalrecordcount { get; set; }

        [JsonProperty("@Microsoft.Dynamics.CRM.totalrecordcountlimitexceeded")]
        public bool MicrosoftDynamicsCRMtotalrecordcountlimitexceeded { get; set; }

        [JsonProperty("@Microsoft.Dynamics.CRM.globalmetadataversion")]
        public string MicrosoftDynamicsCRMglobalmetadataversion { get; set; }
        public List<AnswerData> value { get; set; }
    }

    public class AnswerData
    {

        [JsonProperty("odataetag", NullValueHandling = NullValueHandling.Ignore)]
        public string odataetag { get; set; }

        [JsonProperty("aacn_answer", NullValueHandling = NullValueHandling.Ignore)]
        public string Answer { get; set; }

        [JsonProperty("statuscodeODataCommunityDisplayV1FormattedValue", NullValueHandling = NullValueHandling.Ignore)]
        public string StatusCode { get; set; }

        [JsonProperty("_createdby_valueODataCommunityDisplayV1FormattedValue", NullValueHandling = NullValueHandling.Ignore)]
        public string CreatedBy { get; set; }
        [JsonProperty("aacn_answerid", NullValueHandling = NullValueHandling.Ignore)]
        public string AnswerId { get; set; }

        [JsonProperty("aacn_answer_id", NullValueHandling = NullValueHandling.Ignore)]
        public string Answer_Id { get; set; }

        [JsonProperty("_ownerid_valueODataCommunityDisplayV1FormattedValue", NullValueHandling = NullValueHandling.Ignore)]
        public string _ownerid_valueODataCommunityDisplayV1FormattedValue { get; set; }

        [JsonProperty("_ownerid_valueMicrosoftDynamicsCRMassociatednavigationproperty", NullValueHandling = NullValueHandling.Ignore)]
        public string _ownerid_valueMicrosoftDynamicsCRMassociatednavigationproperty { get; set; }

        [JsonProperty("_ownerid_valueMicrosoftDynamicsCRMlookuplogicalname", NullValueHandling = NullValueHandling.Ignore)]
        public string _ownerid_valueMicrosoftDynamicsCRMlookuplogicalname { get; set; }

        [JsonProperty("_ownerid_value", NullValueHandling = NullValueHandling.Ignore)]
        public string _ownerid_value { get; set; }

        [JsonProperty("modifiedonODataCommunityDisplayV1FormattedValue", NullValueHandling = NullValueHandling.Ignore)]
        public string ModifiedonFormattedValue { get; set; }

        [JsonProperty("modifiedon", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime Modifiedon { get; set; }


        [JsonProperty("_modifiedby_valueODataCommunityDisplayV1FormattedValue", NullValueHandling = NullValueHandling.Ignore)]
        public string ModifiedBy { get; set; }


        [JsonProperty("createdonODataCommunityDisplayV1FormattedValue", NullValueHandling = NullValueHandling.Ignore)]
        public string CreatedonFormattedValue { get; set; }

        [JsonProperty("createdon", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime Createdon { get; set; }

        [JsonProperty("_createdonbehalfby_value", NullValueHandling = NullValueHandling.Ignore)]
        public object CreatedOnBehalfby { get; set; }

        [JsonProperty("_modifiedonbehalfby_value", NullValueHandling = NullValueHandling.Ignore)]
        public object ModifiedonBehalfby { get; set; }

        [JsonProperty("_owningteam_value", NullValueHandling = NullValueHandling.Ignore)]
        public object OwningTeam { get; set; }
    }

    public class PostAnswer
    {

        [JsonProperty("aacn_answerid", NullValueHandling = NullValueHandling.Ignore)]
        public string answerId { get; set; }
        
        [JsonProperty("aacn_answer_id")]
        public string answer_Number { get; set; }

        [JsonProperty("aacn_answer")]
        public string answer { get; set; }

    }

}
