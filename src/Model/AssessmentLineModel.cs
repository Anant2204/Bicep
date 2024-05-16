namespace AACN.API.Model
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.Json.Serialization;
    using System.Threading.Tasks;

    public class AssessmentLineModel
    {
        [JsonProperty("@odata.context")]
        public string odatacontext { get; set; }

        [JsonProperty("@Microsoft.Dynamics.CRM.totalrecordcount")]
        public int MicrosoftDynamicsCRMtotalrecordcount { get; set; }

        [JsonProperty("@Microsoft.Dynamics.CRM.totalrecordcountlimitexceeded")]
        public bool MicrosoftDynamicsCRMtotalrecordcountlimitexceeded { get; set; }

        [JsonProperty("@Microsoft.Dynamics.CRM.globalmetadataversion")]
        public string MicrosoftDynamicsCRMglobalmetadataversion { get; set; }
        public List<AssessmentLineModelData> value { get; set; }
    }
    public class AssessmentLineModelData
    {
        [JsonProperty("aacn_assessment_line_title")]
        public string assessment_Line_Title { get; set; }

        [JsonProperty("aacn_assessment_line_question_text")]
        public string assessment_Line_Question_Text { get; set; }


        [JsonProperty("aacn_assessment_line_type")]
        public int assessment_Line_Type { get; set; }


        [JsonProperty("_aacn_assessment_header_value")]
        public string assessment_Header_Value { get; set; }

        [JsonProperty("modifiedon")]
        public DateTime modifiedon { get; set; }

        [JsonProperty("aacn_sssessment_lineid")]
        public string assessment_lineid { get; set; }



        [JsonProperty("_aacn_assessment_line_reference_value")]
        public string assessment_Line_Reference_value { get; set; }

        [JsonProperty("aacn_assessment_line_number")]
        public string assessment_Line_Number { get; set; }

        [JsonProperty("createdon")]
        public DateTime createdon { get; set; }

        [JsonProperty("_aacn_assessment_line_question_value")]
        public string assessment_Line_Question_Value { get; set; }



        [JsonProperty("aacn_assessment_max_responses")]
        public int assessment_Max_Responses { get; set; }

        [JsonProperty("aacn_assessment_line_orientation")]
        public int assessment_Line_Orientation { get; set; }

        [JsonProperty("_createdonbehalfby_value")]
        public object createdonbehalfby_value { get; set; }

        [JsonProperty("aacn_assessment_line_control")]
        public int assessment_Line_Control { get; set; }

        [JsonProperty("aacn_assessment_line_required")]
        public bool assessment_Line_Required { get; set; }

        [JsonProperty("_owningteam_value")]
        public object owningteam_value { get; set; }

        [JsonProperty("timezoneruleversionnumber")]
        public object timezoneruleversionnumber { get; set; }

        [JsonProperty("aacn_assessment_line_order")]
        public int assessment_Line_Order { get; set; }
    }

    public class PostAssessMentLine
    {

        [JsonProperty("aacn_assessment_lineid", NullValueHandling = NullValueHandling.Ignore)]
        public string assessment_LineId { get; set; }

        [JsonProperty("aacn_assessment_line_number")]
        public string assessment_Line_Number { get; set; }
        [JsonProperty("aacn_assessment_line_title")]
        public string assessment_Line_Title { get; set; }

        [JsonProperty("aacn_assessment_line_question_text")]
        public string assessment_Line_Question_Text { get; set; }

        [JsonProperty("aacn_assessment_line_type")]
        public int assessment_Line_Type { get; set; }


        [JsonProperty("aacn_assessment_max_responses")]
        public int assessment_Max_Responses { get; set; }

        [JsonProperty("aacn_assessment_line_orientation")]
        public int assessment_Line_Orientation { get; set; }

        [JsonProperty("aacn_assessment_line_control")]
        public int assessment_Line_Control { get; set; }

        [JsonProperty("aacn_assessment_line_required")]
        public bool assessment_Line_Required { get; set; }

        [JsonProperty("aacn_assessment_line_order")]
        public string assessment_Line_Order { get; set; }


        [Newtonsoft.Json.JsonIgnore] // Ignore this property during serialization
        public Guid assessment_Header_Id { get; set; } // Store the GUID

        [JsonProperty("aacn_assessment_header@odata.bind")] // Map to this property during serialization/deserialization
        private string assessment_Header
        {
            get => $"/aacn_assessments({assessment_Header_Id})";
            set
            {
                if (Guid.TryParse(value, out Guid guid))
                {
                    assessment_Header_Id = guid;
                }
                else
                {
                    throw new ArgumentException("Invalid assessment header format.");
                }
            }
        }

        [Newtonsoft.Json.JsonIgnore] // Ignore this property during serialization
        public Guid assessment_Line_Question_Id { get; set; } // Store the GUID

        [JsonProperty("aacn_assessment_line_question@odata.bind")] // Map to this property during serialization/deserialization
        public string assessment_Line_Question
        {
            get => $"/aacn_questions({assessment_Line_Question_Id})";
            set
            {
                if (Guid.TryParse(value, out Guid guid))
                {
                    assessment_Line_Question_Id = guid;
                }
                else
                {
                    throw new ArgumentException("Invalid assessment_Line_Question format.");
                }
            }
        }

        [Newtonsoft.Json.JsonIgnore] // Ignore this property during serialization
        public Guid assessment_Line_Parent_Id { get; set; } // Store the GUID

        [JsonProperty("aacn_assessment_line_reference@odata.bind")] // Map to this property during serialization/deserialization
        private string assessment_Line_Reference
        {
            get => $"/aacn_assessment_lines({assessment_Line_Parent_Id})";
            set
            {
                if (Guid.TryParse(value, out Guid guid))
                {
                    assessment_Line_Parent_Id = guid;
                }
                else
                {
                    throw new ArgumentException("Invalid aacn_assessment_line_reference format.");
                }
            }
        }


    }

}

