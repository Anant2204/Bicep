namespace AACN.API.Model
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.Json.Serialization;
    using System.Threading.Tasks;

    public class SpeakerModel
    {
        [JsonProperty("@odata.context")]
        public string odatacontext { get; set; }

        [JsonProperty("@Microsoft.Dynamics.CRM.totalrecordcount")]
        public int MicrosoftDynamicsCRMtotalrecordcount { get; set; }

        [JsonProperty("@Microsoft.Dynamics.CRM.totalrecordcountlimitexceeded")]
        public bool MicrosoftDynamicsCRMtotalrecordcountlimitexceeded { get; set; }

        [JsonProperty("@Microsoft.Dynamics.CRM.globalmetadataversion")]
        public string MicrosoftDynamicsCRMglobalmetadataversion { get; set; }
        public List<speakerData> value { get; set; }
    }
    public class speakerData
    {
        [JsonProperty("aacn_speaker_name")]
        public string speaker_Name { get; set; }

        [JsonProperty("statecode")]
        public int statecode { get; set; }

        [JsonProperty("statuscode")]
        public int statuscode { get; set; }

        [JsonProperty("_createdby_value")]
        public string createdby_Value { get; set; }

        [JsonProperty("_ownerid_value")]
        public string ownerid_Value { get; set; }

        [JsonProperty("modifiedon")]
        public DateTime modifiedon { get; set; }

        [JsonProperty("_aacn_speaker_value")]
        public object speaker_Value { get; set; }

        [JsonProperty("_owninguser_value")]
        public string owninguser_value { get; set; }

        [JsonProperty("_modifiedby_value")]
        public string modifiedBy_Value { get; set; }

        [JsonProperty("aacn_speaker_number")]
        public string speaker_Number { get; set; }

        [JsonProperty("createdon")]
        public DateTime createdOn { get; set; }

        [JsonProperty("aacn_speakerid")]
        public string speakerId { get; set; }

        [JsonProperty("_aacn_session_value")]
        public object session_Value { get; set; }

        [JsonProperty("_createdonbehalfby_value")]
        public object createdonbehalfby_value { get; set; }

        [JsonProperty("_modifiedonbehalfby_value")]
        public object modifiedonbehalfby_value { get; set; }

        [JsonProperty("_owningteam_value")]
        public object owningteam_Value { get; set; }

    }

    public class PostSpeaker
    {
        [JsonProperty("aacn_speakerid", NullValueHandling = NullValueHandling.Ignore)]
        public string speakerId { get; set; }

        [JsonProperty("aacn_speaker_number")]
        public string speaker_Number { get; set; }

        [JsonProperty("aacn_speaker_name")]
        public string speaker_Name { get; set; }

        [Newtonsoft.Json.JsonIgnore] // Ignore this property during serialization
        public Guid speaker_Id { get; set; }

        [JsonProperty("aacn_speaker@odata.bind")] // Map to this property during serialization/deserialization
        private string speakerRef
        {
            get => $"/contacts({speaker_Id})";
            set
            {
                if (Guid.TryParse(value, out Guid guid))
                {
                    speaker_Id = guid;
                }
                else
                {
                    throw new ArgumentException("Invalid speaker_Id format.");
                }
            }
        }

        [Newtonsoft.Json.JsonIgnore] // Ignore this property during serialization
        public Guid session_Id { get; set; }

        [JsonProperty("aacn_session@odata.bind")] // Map to this property during serialization/deserialization
        private string sessionId
        {
            get => $"/aacn_sessions({session_Id})";
            set
            {
                if (Guid.TryParse(value, out Guid guid))
                {
                    session_Id = guid;
                }
                else
                {
                    throw new ArgumentException("Invalid session_Id format.");
                }
            }
        }
    }


}
