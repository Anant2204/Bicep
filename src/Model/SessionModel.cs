namespace AACN.API.Model
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.Json.Serialization;
    using System.Threading.Tasks;

    public class SessionModel
    {
        [JsonProperty("@odata.context")]
        public string odatacontext { get; set; }

        [JsonProperty("@Microsoft.Dynamics.CRM.totalrecordcount")]
        public int MicrosoftDynamicsCRMtotalrecordcount { get; set; }

        [JsonProperty("@Microsoft.Dynamics.CRM.totalrecordcountlimitexceeded")]
        public bool MicrosoftDynamicsCRMtotalrecordcountlimitexceeded { get; set; }

        [JsonProperty("@Microsoft.Dynamics.CRM.globalmetadataversion")]
        public string MicrosoftDynamicsCRMglobalmetadataversion { get; set; }
        public List<SessionData> value { get; set; }
    }

    public class SessionData
    {
        [JsonProperty("_aacn_event_id_value")]
        public string eventId_value { get; set; }
        [JsonProperty("_aacn_assessment_code_value")]
        public string assessment_Code_Value { get; set; }

        [JsonProperty("statecode")]
        public int statecode { get; set; }

        [JsonProperty("statuscode")]
        public int statuscode { get; set; }

        [JsonProperty("_createdby_value")]
        public string createdBy_Value { get; set; }

        [JsonProperty("aacn_sessionid")]
        public string sessionid { get; set; }

        [JsonProperty("_ownerid_value")]
        public string ownerid_Value { get; set; }

        [JsonProperty("aacn_session_id")]
        public string session_Id { get; set; }

        [JsonProperty("modifiedon")]
        public DateTime modifiedon { get; set; }

        [JsonProperty("_owninguser_value")]
        public string owninguser_value { get; set; }

        [JsonProperty("_modifiedby_value")]
        public string modifiedBy_Value { get; set; }


        [JsonProperty("createdon")]
        public DateTime createdOn { get; set; }

        [JsonProperty("aacn_session_name")]
        public string session_Name { get; set; }

        [JsonProperty("aacn_end_time")]
        public object end_Time { get; set; }

        [JsonProperty("overriddencreatedon")]
        public object Overriddencreatedon { get; set; }

        [JsonProperty("aacn_start_date")]
        public DateTime start_Date { get; set; }

        [JsonProperty("aacn_start_time")]
        public object start_Time { get; set; }

        [JsonProperty("aacn_end_date")]
        public DateTime end_Date { get; set; }

        [JsonProperty("_createdonbehalfby_value")]
        public object createdonbehalfby_Value { get; set; }

        [JsonProperty("_owningteam_value")]
        public object owningteam_Value { get; set; }


    }

    public class PostSession
    {
        [JsonProperty("aacn_sessionid", NullValueHandling = NullValueHandling.Ignore)]
        public string sessionId { get; set; }

        [JsonProperty("aacn_session_id")]
        public string session_Number { get; set; }

        [JsonProperty("aacn_session_name")]
        public string Session_Name { get; set; }

        [JsonProperty("aacn_start_date")]
        public DateTime Start_Date { get; set; }

        [JsonProperty("aacn_end_date")]
        public DateTime End_Date { get; set; }

        [Newtonsoft.Json.JsonIgnore] // Ignore this property during serialization
        public Guid event_Id { get; set; }

        [JsonProperty("aacn_event_id@odata.bind")] // Map to this property during serialization/deserialization
        private string eventId
        {
            get => $"/aacn_events({event_Id})";
            set
            {
                if (Guid.TryParse(value, out Guid guid))
                {
                    event_Id = guid;
                }
                else
                {
                    throw new ArgumentException("Invalid event_Id format.");
                }
            }
        }

        [Newtonsoft.Json.JsonIgnore] // Ignore this property during serialization
        public Guid assessment_Code_Id { get; set; }

        [JsonProperty("aacn_assessment_code@odata.bind")] // Map to this property during serialization/deserialization
        private string assessmentCode_Id
        {
            get => $"/aacn_assessments({assessment_Code_Id})";
            set
            {
                if (Guid.TryParse(value, out Guid guid))
                {
                    assessment_Code_Id = guid;
                }
                else
                {
                    throw new ArgumentException("Invalid assessment_Code_Id format.");
                }
            }
        }
        [JsonProperty("aacn_survey_provider@odata.bind")]
        public string survey_Provider_Name { get; set; }
    }


}
