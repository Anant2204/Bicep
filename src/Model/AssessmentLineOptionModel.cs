namespace AACN.API.Model
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.Json.Serialization;
    using System.Threading.Tasks;

    public class AssessmentLineOptionModel
    {
        [JsonProperty("@odata.context")]
        public string odatacontext { get; set; }

        [JsonProperty("@Microsoft.Dynamics.CRM.totalrecordcount")]
        public int MicrosoftDynamicsCRMtotalrecordcount { get; set; }

        [JsonProperty("@Microsoft.Dynamics.CRM.totalrecordcountlimitexceeded")]
        public bool MicrosoftDynamicsCRMtotalrecordcountlimitexceeded { get; set; }

        [JsonProperty("@Microsoft.Dynamics.CRM.globalmetadataversion")]
        public string MicrosoftDynamicsCRMglobalmetadataversion { get; set; }
        public List<AssessmentLineOptionModelData> value { get; set; }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class AssessmentLineOptionModelData
    {
        [JsonProperty("_owningbusinessunit_value")]
        public string owningbusinessunit_value { get; set; }

        [JsonProperty("aacn_assessment_line_optionid")]
        public string assessment_Line_Optionid { get; set; }

        [JsonProperty("statecode")]
        public int statecode { get; set; }

        [JsonProperty("statuscode")]
        public int statuscode { get; set; }

        [JsonProperty("aacn_assessment_line_question_option_text")]
        public string assessment_Line_Question_Option_Text { get; set; }

        [JsonProperty("_createdby_value")]
        public string createdby_value { get; set; }

        [JsonProperty("aacn_assessment_line_option_required")]
        public bool assessment_Line_Option_Required { get; set; }

        [JsonProperty("_ownerid_value")]
        public string ownerid_value { get; set; }

        [JsonProperty("modifiedon")]
        public DateTime modifiedon { get; set; }

        [JsonProperty("_modifiedby_value")]
        public string modifiedby_value { get; set; }

        [JsonProperty("_owninguser_value")]
        public string owninguser_value { get; set; }

        [JsonProperty("createdon")]
        public DateTime createdon { get; set; }

        [JsonProperty("aacn_assessment_line_option_number")]
        public string assessment_Line_Option_Number { get; set; }

        [JsonProperty("_aacn_assessment_line_question_value")]
        public string assessment_Line_Question_Value { get; set; }

        [JsonProperty("overriddencreatedon")]
        public object overriddencreatedon { get; set; }

        [JsonProperty("aacn_assessment_line_option_sort_order")]
        public int assessment_Line_Option_Sort_Order { get; set; }

        [JsonProperty("_modifiedonbehalfby_value")]
        public object modifiedonbehalfby_value { get; set; }

        [JsonProperty("_aacn_assessment_line_question_option_value")]
        public string assessment_Line_Question_Option_value { get; set; }

        [JsonProperty("_aacn_assessment_line_value")]
        public string assessment_Line_Value { get; set; }

        [JsonProperty("_createdonbehalfby_value")]
        public object createdonbehalfby_value { get; set; }

        [JsonProperty("aacn_assessment_line_option_score")]
        public int assessment_Line_Option_Score { get; set; }

        [JsonProperty("_owningteam_value")]
        public object owningteam_value { get; set; }

        [JsonProperty("timezoneruleversionnumber")]
        public object timezoneruleversionnumber { get; set; }
    }
    public class PostAssessMentLineOption
    {
        [JsonProperty("aacn_assessment_line_optionid", NullValueHandling = NullValueHandling.Ignore)]
        public string assessment_Line_OptionId { get; set; }
        [JsonProperty("aacn_assessment_line_option_number")]
        public string assessment_Line_Option_Number { get; set; }

        [JsonProperty("aacn_assessment_line_question_option_text")]
        public string Assessment_Line_Question_Option_Text { get; set; }

        [JsonProperty("aacn_assessment_line_option_required")]
        public bool Assessment_Line_Option_Required { get; set; }

        [JsonProperty("aacn_assessment_line_option_sort_order")]
        public int Assessment_Line_Option_Sort_Order { get; set; }

        [JsonProperty("aacn_assessment_line_option_score")]
        public int Assessment_Line_Option_Score { get; set; }

        [Newtonsoft.Json.JsonIgnore] // Ignore this property during serialization
        public Guid assessment_Line_Id { get; set; }

        [JsonProperty("aacn_assessment_line@odata.bind")] // Map to this property during serialization/deserialization
        private string Assessment_Line
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
                    throw new ArgumentException("Invalid Assessment_Line format.");
                }
            }
        }

        [Newtonsoft.Json.JsonIgnore] // Ignore this property during serialization
        public Guid assessment_Line_Question_Id { get; set; }

        [JsonProperty("aacn_assessment_line_question@odata.bind")] // Map to this property during serialization/deserialization
        private string assessment_Line_Question
        {
            get => $"/aacn_assessment_lines({assessment_Line_Question_Id})";
            set
            {
                if (Guid.TryParse(value, out Guid guid))
                {
                    assessment_Line_Question_Id = guid;
                }
                else
                {
                    throw new ArgumentException("Invalid assessment_Line_Question_Id format.");
                }
            }
        }


        [Newtonsoft.Json.JsonIgnore] // Ignore this property during serialization
        public Guid assessment_Line_Question_Option_Id { get; set; }

        [JsonProperty("aacn_assessment_line_question_option@odata.bind")] // Map to this property during serialization/deserialization
        private string assessment_Line_Question_Option
        {
            get => $"/aacn_assessment_lines({assessment_Line_Question_Option_Id})";
            set
            {
                if (Guid.TryParse(value, out Guid guid))
                {
                    assessment_Line_Id = guid;
                }
                else
                {
                    throw new ArgumentException("Invalid assessment_Line_Question_Option_Id format.");
                }
            }
        }


    }



}
