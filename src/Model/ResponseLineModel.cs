namespace AACN.API.Model
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.Json.Serialization;
    using System.Threading.Tasks;

    public class ResponseLineModel
    {
        [JsonProperty("@odata.context")]
        public string odatacontext { get; set; }

        [JsonProperty("@Microsoft.Dynamics.CRM.totalrecordcount")]
        public int MicrosoftDynamicsCRMtotalrecordcount { get; set; }

        [JsonProperty("@Microsoft.Dynamics.CRM.totalrecordcountlimitexceeded")]
        public bool MicrosoftDynamicsCRMtotalrecordcountlimitexceeded { get; set; }

        [JsonProperty("@Microsoft.Dynamics.CRM.globalmetadataversion")]
        public string MicrosoftDynamicsCRMglobalmetadataversion { get; set; }
        public List<ResponeLineData> value { get; set; }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class ResponeLineData
    {
        [JsonProperty("statecodeODataCommunityDisplayV1FormattedValue")]
        public string statecode { get; set; }

        [JsonProperty("statuscodeODataCommunityDisplayV1FormattedValue")]
        public string statuscode { get; set; }

        [JsonProperty("createdon")]
        public DateTime createdon { get; set; }

        [JsonProperty("aacn_response_line_number")]
        public string response_Line_Number { get; set; }

        [JsonProperty("aacn_response_lineid")]
        public string response_LineId { get; set; }

        [JsonProperty("_ownerid_value")]
        public string ownerid_Value { get; set; }

        [JsonProperty("_aacn_assessment_line_option_valueODataCommunityDisplayV1FormattedValue")]
        public string assessment_Line_Option { get; set; }
        [JsonProperty("_aacn_assessment_line_option_value")]
        public string assessment_Line_Option_value { get; set; }

        [JsonProperty("modifiedon")]
        public DateTime modifiedon { get; set; }

        [JsonProperty("_aacn_answer_valueODataCommunityDisplayV1FormattedValue")]
        public string answer { get; set; }

        [JsonProperty("_aacn_answer_value")]
        public string answer_Value { get; set; }

        [JsonProperty("aacn_response_text")]
        public string response_Text { get; set; }

        [JsonProperty("_modifiedby_value")]
        public string modifiedby_value { get; set; }

        [JsonProperty("aacn_question_text")]
        public string question_Text { get; set; }

        [JsonProperty("_aacn_response_valueODataCommunityDisplayV1FormattedValue")]
        public string response { get; set; }

        [JsonProperty("_aacn_response_value")]
        public string response_Value { get; set; }

        [JsonProperty("_aacn_question_valueODataCommunityDisplayV1FormattedValue")]
        public string question { get; set; }

        [JsonProperty("_aacn_question_value")]
        public string question_Value { get; set; }

        [JsonProperty("_createdby_value")]
        public string createdby_Value { get; set; }

        [JsonProperty("_aacn_assessment_line_valueODataCommunityDisplayV1FormattedValue")]
        public string assessment_Line { get; set; }

        [JsonProperty("_aacn_assessment_line_value")]
        public string assessment_Line_Value { get; set; }

        [JsonProperty("_owninguser_valueMicrosoftDynamicsCRMlookuplogicalname")]
        public string _owninguser_valueMicrosoftDynamicsCRMlookuplogicalname { get; set; }

        [JsonProperty("_owninguser_value")]
        public string owninguser_Value { get; set; }

        [JsonProperty("overriddencreatedon")]
        public object overriddenCreatedon { get; set; }

        [JsonProperty("_createdonbehalfby_value")]
        public object createdonbehalfby_Value { get; set; }

        [JsonProperty("_owningteam_value")]
        public object owningteam_Value { get; set; }


    }

    public class PostResponseLine
    {
        [JsonProperty("aacn_response_lineid", NullValueHandling = NullValueHandling.Ignore)]
        public string response_LineId { get; set; }

        [JsonProperty("aacn_response_line_number")]
        public string response_Line_Number { get; set; }

        [JsonProperty("aacn_response_text")]
        public string response_Text { get; set; }

        [JsonProperty("aacn_question_text")]
        public string question_Text { get; set; }

        [Newtonsoft.Json.JsonIgnore] // Ignore this property during serialization
        public string assessment_Line_Option_Id { get; set; }

        [JsonProperty("aacn_assessment_line_option@odata.bind")] // Map to this property during serialization/deserialization
        private string assessment_Line_Option
        {
            get => $"/aacn_assessment_line_options({assessment_Line_Option_Id})";
            set
            {
                if (Guid.TryParse(value, out Guid guid))
                {
                    assessment_Line_Option_Id = assessment_Line_Option_Id;
                }
                else
                {
                    throw new ArgumentException("Invalid assessment_Line_Option_Id format.");
                }
            }
        }

        [Newtonsoft.Json.JsonIgnore] // Ignore this property during serialization
        public Guid answer_Id { get; set; }
        [JsonProperty("aacn_answer@odata.bind")] // Map to this property during serialization/deserialization
        private string answerId
        {
            get => $"/aacn_answers({answer_Id})";
            set
            {
                if (Guid.TryParse(value, out Guid guid))
                {
                    answer_Id = guid;
                }
                else
                {
                    throw new ArgumentException("Invalid aanswer_Id format.");
                }
            }
        }

        [Newtonsoft.Json.JsonIgnore] // Ignore this property during serialization
        public Guid response_Id { get; set; }

        [JsonProperty("aacn_response@odata.bind")] // Map to this property during serialization/deserialization
        private string responseId
        {
            get => $"/aacn_responses({response_Id})";
            set
            {
                if (Guid.TryParse(value, out Guid guid))
                {
                    response_Id = guid;
                }
                else
                {
                    throw new ArgumentException("Invalid response_Id format.");
                }
            }
        }

        [Newtonsoft.Json.JsonIgnore] // Ignore this property during serialization
        public Guid question_Id { get; set; }

        [JsonProperty("aacn_question@odata.bind")] // Map to this property during serialization/deserialization
        private string questionId
        {
            get => $"/aacn_questions({question_Id})";
            set
            {
                if (Guid.TryParse(value, out Guid guid))
                {
                    question_Id = guid;
                }
                else
                {
                    throw new ArgumentException("Invalid question_Id format.");
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
