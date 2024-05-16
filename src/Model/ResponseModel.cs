namespace AACN.API.Model
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.Json.Serialization;
    using System.Threading.Tasks;

    public class ResponseModel
    {
        [JsonProperty("@odata.context")]
        public string odatacontext { get; set; }

        [JsonProperty("@Microsoft.Dynamics.CRM.totalrecordcount")]
        public int MicrosoftDynamicsCRMtotalrecordcount { get; set; }

        [JsonProperty("@Microsoft.Dynamics.CRM.totalrecordcountlimitexceeded")]
        public bool MicrosoftDynamicsCRMtotalrecordcountlimitexceeded { get; set; }

        [JsonProperty("@Microsoft.Dynamics.CRM.globalmetadataversion")]
        public string MicrosoftDynamicsCRMglobalmetadataversion { get; set; }
        public List<ResponseData> value { get; set; }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class ResponseData
    {

        [JsonProperty("statecode")]
        public int statecode { get; set; }

        [JsonProperty("statuscode")]
        public int statuscode { get; set; }

        [JsonProperty("_aacn_assessment_value")]
        public string assessment_Value { get; set; }

        [JsonProperty("aacn_responseid")]
        public string responseId { get; set; }

        [JsonProperty("_aacn_event_value")]
        public string event_Value { get; set; }

        [JsonProperty("modifiedon")]
        public DateTime modifiedOn { get; set; }

        [JsonProperty("aacn_response_number")]
        public string responseNumber { get; set; }

        [JsonProperty("_modifiedby_value")]
        public string modifiedBy_Value { get; set; }

        [JsonProperty("_aacn_member_value")]
        public string member_Value { get; set; }

        [JsonProperty("createdon")]
        public DateTime createdOn { get; set; }

        [JsonProperty("_createdby_value")]
        public string createdBy { get; set; }

        [JsonProperty("_aacn_session_value")]
        public string session_Value { get; set; }

        [JsonProperty("overriddencreatedon")]
        public object overriddencreatedon { get; set; }

    }

    public class PostResponse
    {

        [JsonProperty("aacn_responseid", NullValueHandling = NullValueHandling.Ignore)]
        public string responseId { get; set; }

        [JsonProperty("aacn_response_number")]
        public string response_Number { get; set; }

        [Newtonsoft.Json.JsonIgnore] // Ignore this property during serialization
        public Guid assessment_Id { get; set; }

        [JsonProperty("aacn_assessment@odata.bind")] // Map to this property during serialization/deserialization
        public string assessmentId
        {
            get => $"/aacn_assessments({assessment_Id})";
            set
            {
                if (Guid.TryParse(value, out Guid guid))
                {
                    assessment_Id = guid;
                }
                else
                {
                    throw new ArgumentException("Invalid assessment_Id format.");
                }
            }
        }

        [Newtonsoft.Json.JsonIgnore] // Ignore this property during serialization
        public Guid event_Id { get; set; }

        [JsonProperty("aacn_event@odata.bind")] // Map to this property during serialization/deserialization
        public string eventId
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
        public Guid member_Id { get; set; }

        [JsonProperty("aacn_member@odata.bind")] // Map to this property during serialization/deserialization
        public string memberId
        {
            get => $"/contacts({member_Id})";
            set
            {
                if (Guid.TryParse(value, out Guid guid))
                {
                    member_Id = guid;
                }
                else
                {
                    throw new ArgumentException("Invalid member_Id format.");
                }
            }
        }

        [Newtonsoft.Json.JsonIgnore] // Ignore this property during serialization
        public Guid session_Id { get; set; }

        [JsonProperty("aacn_session@odata.bind")] // Map to this property during serialization/deserialization
        public string sessionId
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

        [JsonProperty("aacn_survey_provider@odata.bind")]
        public string survey_Provider_Name { get; set; }

    }


    public class PostResponseData
    {
        [JsonProperty("aacn_response_number")]
        public string response_Number { get; set; }

        [Newtonsoft.Json.JsonIgnore] // Ignore this property during serialization
        public Guid assessment_Id { get; set; }

        [JsonProperty("aacn_assessment@odata.bind")] // Map to this property during serialization/deserialization
        public string assessmentId
        {
            get => $"/aacn_assessments({assessment_Id})";
            set
            {
                if (Guid.TryParse(value, out Guid guid))
                {
                    assessment_Id = guid;
                }
                else
                {
                    throw new ArgumentException("Invalid assessment_Id format.");
                }
            }
        }

        [Newtonsoft.Json.JsonIgnore] // Ignore this property during serialization
        public Guid event_Id { get; set; }

        [JsonProperty("aacn_event@odata.bind")] // Map to this property during serialization/deserialization
        public string eventId
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
        public Guid member_Id { get; set; }

        [JsonProperty("aacn_member@odata.bind")] // Map to this property during serialization/deserialization
        public string memberId
        {
            get => $"/contacts({member_Id})";
            set
            {
                if (Guid.TryParse(value, out Guid guid))
                {
                    member_Id = guid;
                }
                else
                {
                    throw new ArgumentException("Invalid member_Id format.");
                }
            }
        }

        [Newtonsoft.Json.JsonIgnore] // Ignore this property during serialization
        public Guid session_Id { get; set; }

        [JsonProperty("aacn_session@odata.bind")] // Map to this property during serialization/deserialization
        public string sessionId
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

        [JsonProperty("aacn_survey_provider@odata.bind")]
        public string survey_Provider_Name { get; set; }

        [JsonProperty("aacn_response_line_response_aacn_response")]
        public List<PostResponseLines> response_Line { get; set; }
    }

    public class PostResponseLines
    {
        [JsonProperty("aacn_response_line_number")]
        public string response_Line_Number { get; set; }

        [JsonProperty("aacn_response_line_text_area", NullValueHandling = NullValueHandling.Ignore)]
        public string response_Text { get; set; }

        [JsonProperty("aacn_question_text", NullValueHandling = NullValueHandling.Ignore)]
        public string question_Text { get; set; }

        [Newtonsoft.Json.JsonIgnore] // Ignore this property during serialization
        [JsonProperty("assessment_Line_Option_Id", NullValueHandling = NullValueHandling.Ignore)]
        public string assessment_Line_Option_Id { get; set; }

        [JsonProperty("aacn_assessment_line_option@odata.bind", NullValueHandling = NullValueHandling.Ignore)] // Map to this property during serialization/deserialization
        private string assessment_Line_Option
        {
            get
            {
                if (!string.IsNullOrEmpty(assessment_Line_Option_Id))
                {
                    return $"/aacn_assessment_line_options({assessment_Line_Option_Id})";
                }
                return null;
            }

            set
            {
                if (Guid.TryParse(value, out Guid guid))
                {
                    assessment_Line_Option_Id = guid.ToString();
                }

            }
        }

        [Newtonsoft.Json.JsonIgnore] // Ignore this property during serialization
        [JsonProperty("answer_Id", NullValueHandling = NullValueHandling.Ignore)]
        public string answer_Id { get; set; }
        [JsonProperty("aacn_answer@odata.bind", NullValueHandling = NullValueHandling.Ignore)] // Map to this property during serialization/deserialization
        private string answerId
        {
            get
            {
                if (!string.IsNullOrEmpty(answer_Id))
                {
                    return $"/aacn_answers({answer_Id})";
                }
                return null;
            }
            set
            {
                if (Guid.TryParse(value, out Guid guid))
                {
                    answer_Id = guid.ToString();
                }

            }
        }

        [Newtonsoft.Json.JsonIgnore] // Ignore this property during serialization
        [JsonProperty("question_Id", NullValueHandling = NullValueHandling.Ignore)] // Map to this property during serialization/deserialization

        public string question_Id { get; set; }

        [JsonProperty("aacn_question@odata.bind", NullValueHandling = NullValueHandling.Ignore)] // Map to this property during serialization/deserialization
        private string questionId
        {
            get
            {

                if (!string.IsNullOrEmpty(question_Id))
                {
                    return $"/aacn_questions({question_Id})"; ;
                }
                return null;
            }
            set
            {
                if (Guid.TryParse(value, out Guid guid))
                {
                    question_Id = guid.ToString();
                }

            }
        }

        [Newtonsoft.Json.JsonIgnore] // Ignore this property during serialization
        public Guid assessment_Line_Id { get; set; }

        [JsonProperty("aacn_assesment_line@odata.bind")] // Map to this property during serialization/deserialization
        private string assessment_LineId
        {
            get => $"/aacn_assessment_lines({assessment_Line_Id})";
            set
            {
                if (Guid.TryParse(value, out Guid guid))
                {
                    assessment_Line_Id = guid;
                }
                else
                {
                    throw new ArgumentException("Invalid assessment_Line_Id format.");
                }
            }
        }

    }
}
