namespace AACN.API.Model
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.Json.Serialization;
    using System.Threading.Tasks;

    public class QuestionModel
    {
        [JsonProperty("@odata.context")]
        public string odatacontext { get; set; }

        [JsonProperty("@Microsoft.Dynamics.CRM.totalrecordcount")]
        public int MicrosoftDynamicsCRMtotalrecordcount { get; set; }

        [JsonProperty("@Microsoft.Dynamics.CRM.totalrecordcountlimitexceeded")]
        public bool MicrosoftDynamicsCRMtotalrecordcountlimitexceeded { get; set; }

        [JsonProperty("@Microsoft.Dynamics.CRM.globalmetadataversion")]
        public string MicrosoftDynamicsCRMglobalmetadataversion { get; set; }
        public List<QuestionData> value { get; set; }
    }

    public class QuestionData
    {
        [JsonProperty("aacn_question")]
        public string question { get; set; }

        [JsonProperty("statecode")]
        public int statecode { get; set; }

        [JsonProperty("statuscode")]
        public int statuscode { get; set; }

        [JsonProperty("_createdby_value")]
        public string createdby_value { get; set; }

        [JsonProperty("aacn_questionid")]
        public string questionid { get; set; }

        [JsonProperty("aacn_question_id")]
        public string question_id { get; set; }

        [JsonProperty("createdon")]
        public DateTime createdon { get; set; }

        [JsonProperty("modifiedon")]
        public DateTime modifiedon { get; set; }

        [JsonProperty("_modifiedby_value")]
        public string modifiedby_value { get; set; }

        [JsonProperty("_ownerid_value")]
        public string ownerid_value { get; set; }

        [JsonProperty("overriddencreatedon")]
        public object overriddencreatedon { get; set; }

        [JsonProperty("_aacn_group_value")]
        public object group_Value { get; set; }

        [JsonProperty("importsequencenumber")]
        public object importsequencenumber { get; set; }

        [JsonProperty("_createdonbehalfby_value")]
        public object createdonbehalf_By_Value { get; set; }


    }
    public class PostQuestion
    {
        [JsonProperty("aacn_questionid",NullValueHandling = NullValueHandling.Ignore)]
        public string question_Id { get; set; }
        
        [JsonProperty("aacn_question_id")]
        public string question_Number { get; set; }

        [JsonProperty("aacn_question")]
        public string question { get; set; }
    }



}
