namespace AACN.API.Model
{
    using AACN.Services;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Core.Objects.DataClasses;
    using System.Linq;
    using System.Text.Json.Serialization;
    using System.Threading.Tasks;

    public class EventModel
    {
        [JsonProperty("@odata.context")]
        public string odatacontext { get; set; }

        [JsonProperty("@Microsoft.Dynamics.CRM.totalrecordcount")]
        public int MicrosoftDynamicsCRMtotalrecordcount { get; set; }

        [JsonProperty("@Microsoft.Dynamics.CRM.totalrecordcountlimitexceeded")]
        public bool MicrosoftDynamicsCRMtotalrecordcountlimitexceeded { get; set; }

        [JsonProperty("@Microsoft.Dynamics.CRM.globalmetadataversion")]
        public string MicrosoftDynamicsCRMglobalmetadataversion { get; set; }
        public List<EventData> value { get; set; }
    }
    public class EventData
    {

        [JsonProperty("aacn_event_id")]
        public string event_id { get; set; }

        [JsonProperty("aacn_eventid")]
        public string eventid { get; set; }

        [JsonProperty("_aacn_assessment_code_value")]
        public string assessment_Code_value { get; set; }

        [JsonProperty("statecodeODataCommunityDisplayV1FormattedValue")]
        public string statecode { get; set; }



        [JsonProperty("statuscodeODataCommunityDisplayV1FormattedValue")]
        public string statuscode { get; set; }

        [JsonProperty("aacn_event_name")]
        public string event_Name { get; set; }

        [JsonProperty("_createdby_value")]
        public string createdby_value { get; set; }


        [JsonProperty("_ownerid_value")]
        public string ownerid_value { get; set; }

        [JsonProperty("modifiedon")]
        public DateTime modifiedon { get; set; }

        [JsonProperty("_modifiedby_value")]
        public string modifiedby_value { get; set; }

        [JsonProperty("createdon")]
        public DateTime createdon { get; set; }

        [JsonProperty("_aacn_parent_event_value")]
        public string parent_Event_Value { get; set; }

        [JsonProperty("overriddencreatedon")]
        public object overriddencreatedon { get; set; }

        [JsonProperty("aacn_start_date")]
        public DateTime start_Date { get; set; }

        [JsonProperty("_aacn_member_id_value")]
        public string member_Id_Value { get; set; }
        [JsonProperty("aacn_end_date")]
        public DateTime end_Date { get; set; }

    }

    public class PostEvent
    {

        [JsonProperty("aacn_eventid", NullValueHandling = NullValueHandling.Ignore)]
        public string eventId { get; set; }

        [JsonProperty("aacn_event_id")]
        public string event_Id { get; set; }

        [JsonProperty("aacn_event_name")]
        public string event_Name { get; set; }

        //[JsonProperty("aacn_assessment_code@odata.bind")]
        //public string Assessment_Code { get; set; }

        //[JsonProperty("aacn_parent_event@odata.bind")]
        //public string Parent_EventId { get; set; }

        [JsonProperty("aacn_start_date")]
        public DateTime start_Date { get; set; }

        [JsonProperty("aacn_end_date")]
        public DateTime end_Date { get; set; }


        [Newtonsoft.Json.JsonIgnore] // Ignore this property during serialization
        public Guid assessment_Code_Id { get; set; }

        [JsonProperty("aacn_assessment_code@odata.bind")]
        private string assessment_Code
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
                    throw new ArgumentException("Invalid assessment_Line_Question_Id format.");
                }
            }
        }

        [Newtonsoft.Json.JsonIgnore] // Ignore this property during serialization
        public Guid parent_Event_Id { get; set; }

        [JsonProperty("aacn_parent_event@odata.bind")]
        private string aacn_parent_event
        {
            get => $"/aacn_events({parent_Event_Id})";
            set
            {
                if (Guid.TryParse(value, out Guid guid))
                {
                    parent_Event_Id = guid;
                }
                else
                {
                    throw new ArgumentException("Invalid parent_Event_Id format.");
                }
            }
        }

        [JsonProperty("aacn_survey_provider@odata.bind")]
        public string survey_Provider_Name { get; set; }


    }






}
