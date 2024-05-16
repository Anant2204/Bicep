namespace AACN.API.Model
{
    using AACN.Services;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.Json.Serialization;
    using System.Threading.Tasks;

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class AssessmentModel
    {
        [JsonProperty("@odata.context")]
        public string odatacontext { get; set; }

        [JsonProperty("@Microsoft.Dynamics.CRM.totalrecordcount")]
        public int MicrosoftDynamicsCRMtotalrecordcount { get; set; }

        [JsonProperty("@Microsoft.Dynamics.CRM.totalrecordcountlimitexceeded")]
        public bool MicrosoftDynamicsCRMtotalrecordcountlimitexceeded { get; set; }

        [JsonProperty("@Microsoft.Dynamics.CRM.globalmetadataversion")]
        public string MicrosoftDynamicsCRMglobalmetadataversion { get; set; }
        public List<Assessmentdata> value { get; set; }
    }

    public class Assessmentdata
    {

        [JsonProperty("_owningbusinessunit_value")]
        public string Owningbusinessunit_value { get; set; }

        [JsonProperty("statecodeODataCommunityDisplayV1FormattedValue")]
        public string statecode { get; set; }

        [JsonProperty("statuscodeODataCommunityDisplayV1FormattedValue")]
        public string statuscode { get; set; }
        [JsonProperty("_createdby_value")]
        public string Createdby_value { get; set; }

        [JsonProperty("aacn_assessment_numer")]
        public string Assessment_Numer { get; set; }

        [JsonProperty("aacn_assessmentid")]
        public string AssessmentId { get; set; }
        [JsonProperty("_ownerid_value")]
        public string OwnerId_value { get; set; }

        [JsonProperty("modifiedon")]
        public DateTime Modifiedon { get; set; }

        [JsonProperty("_modifiedby_value")]
        public string Modifiedby_value { get; set; }


        [JsonProperty("_owninguser_value")]
        public string Owninguser_value { get; set; }

        [JsonProperty("createdon")]
        public DateTime Createdon { get; set; }

        [JsonProperty("aacn_assessment_title")]
        public string Assessment_Title { get; set; }

        [JsonProperty("overriddencreatedon")]
        public object overriddencreatedon { get; set; }

        [JsonProperty("aacn_passing_score")]
        public int Passing_Score { get; set; }

        [JsonProperty("aacn_assessment_description")]
        public string Assessment_Description { get; set; }
    }
    public class PostAssessment
    {

        [JsonProperty("aacn_assessmentid", NullValueHandling = NullValueHandling.Ignore)]
        public string assessment_Id { get; set; }

        [JsonProperty("aacn_assessment_number")]
        public string assessment_Number { get; set; }

        [JsonProperty("aacn_assessment_title")]
        public string assessment_Title { get; set; }

        [JsonProperty("aacn_passing_score")]
        public int passing_Score { get; set; }

        [JsonProperty("aacn_assessment_description")]
        public string assessment_Description { get; set; }

        [JsonProperty("aacn_survey_provider@odata.bind")]
        public string survey_Provider_Name { get; set; }


    }



}
