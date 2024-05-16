namespace AACN.API.Model
{
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;


    public class AacnAssessmentLineAssessmentHeaderAacnA
    {
        [JsonProperty("_aacn_assessment_header_value")]
        public string _aacn_assessment_header_value { get; set; }

        [JsonProperty("aacn_assessment_line_control")]
        public int? aacn_assessment_line_control { get; set; }

        [JsonProperty("aacn_assessment_line_number")]
        public string aacn_assessment_line_number { get; set; }

        [JsonProperty("aacn_assessment_line_order")]
        public object assessment_Line_Order { get; set; }

        [JsonProperty("aacn_assessment_line_orientation")]
        public int aacn_assessment_line_orientation { get; set; }

        [JsonProperty("_aacn_assessment_line_question_value")]
        public object _aacn_assessment_line_question_value { get; set; }

        [JsonProperty("aacn_assessment_line_question_text")]
        public object aacn_assessment_line_question_text { get; set; }

        [JsonProperty("_aacn_assessment_line_reference_value")]
        public object _aacn_assessment_line_reference_value { get; set; }

        [JsonProperty("aacn_assessment_line_required")]
        public bool aacn_assessment_line_required { get; set; }

        [JsonProperty("aacn_assessment_line_title")]
        public string aacn_assessment_line_title { get; set; }

        [JsonProperty("aacn_assessment_line_type")]
        public int aacn_assessment_line_type { get; set; }

        [JsonProperty("aacn_assessment_lineid")]
        public string aacn_assessment_lineid { get; set; }

        [JsonProperty("aacn_assessment_max_responses")]
        public int aacn_assessment_max_responses { get; set; }

        [JsonProperty("_createdby_value")]
        public string _createdby_value { get; set; }

        [JsonProperty("createdon")]
        public DateTime createdon { get; set; }

        [JsonProperty("statecode@OData.Community.Display.V1.FormattedValue")]
        public string status { get; set; }

        [JsonProperty("aacn_assessment_line_option_assessment_line_aa")]
        public List<AacnAssessmentLineOptionAssessmentLineAa> options { get; set; }

        [JsonProperty("aacn_assessment_line_assessment_line_reference")]
        public List<AacnAssessmentLineAssessmentLineReference> referenceLines { get; set; }
    }

    public class AacnAssessmentLineAssessmentLineReference
    {
        [JsonProperty("_aacn_assessment_header_value")]
        public object _aacn_assessment_header_value { get; set; }

        [JsonProperty("aacn_assessment_line_control")]
        public int? aacn_assessment_line_control { get; set; }

        [JsonProperty("aacn_assessment_line_number")]
        public string aacn_assessment_line_number { get; set; }

        [JsonProperty("aacn_assessment_line_order")]
        public object assessment_Line_Order { get; set; }

        [JsonProperty("aacn_assessment_line_orientation")]
        public int? aacn_assessment_line_orientation { get; set; }

        [JsonProperty("_aacn_assessment_line_question_value")]
        public string _aacn_assessment_line_question_value { get; set; }

        [JsonProperty("aacn_assessment_line_question_text")]
        public string aacn_assessment_line_question_text { get; set; }

        [JsonProperty("_aacn_assessment_line_reference_value")]
        public string _aacn_assessment_line_reference_value { get; set; }

        [JsonProperty("aacn_assessment_line_required")]
        public bool aacn_assessment_line_required { get; set; }

        [JsonProperty("aacn_assessment_line_title")]
        public string aacn_assessment_line_title { get; set; }

        [JsonProperty("aacn_assessment_line_type")]
        public int aacn_assessment_line_type { get; set; }

        [JsonProperty("aacn_assessment_lineid")]
        public string aacn_assessment_lineid { get; set; }

        [JsonProperty("aacn_assessment_max_responses")]
        public int? aacn_assessment_max_responses { get; set; }

        [JsonProperty("_createdby_value")]
        public string _createdby_value { get; set; }

        [JsonProperty("createdon")]
        public DateTime createdon { get; set; }

        [JsonProperty("statecode@OData.Community.Display.V1.FormattedValue")]
        public string status { get; set; }

        [JsonProperty("_createdonbehalfby_value")]
        public object _createdonbehalfby_value { get; set; }

        [JsonProperty("aacn_assessment_line_option_assessment_line_aa")]
        public List<AacnAssessmentLineOptionAssessmentLineAa> optionLines { get; set; }

        [JsonProperty("aacn_assessment_line_assessment_line_reference")]
        public List<AacnAssessmentLineAssessmentLineReference> childLines { get; set; }

        [JsonProperty("overriddencreatedon")]
        public object overriddencreatedon { get; set; }
        
         
    }

    public class AacnAssessmentLineOptionAssessmentLineAa
    {
        [JsonProperty("_aacn_assessment_line_value")]
        public string _aacn_assessment_line_value { get; set; }

        [JsonProperty("aacn_assessment_line_option_number")]
        public string aacn_assessment_line_option_number { get; set; }

        [JsonProperty("aacn_assessment_line_option_required")]
        public bool aacn_assessment_line_option_required { get; set; }

        [JsonProperty("aacn_assessment_line_option_score")]
        public int? aacn_assessment_line_option_score { get; set; }

        [JsonProperty("aacn_assessment_line_option_sort_order")]
        public int? aacn_assessment_line_option_sort_order { get; set; }

        [JsonProperty("aacn_assessment_line_optionid")]
        public string aacn_assessment_line_optionid { get; set; }

        [JsonProperty("_aacn_assessment_line_question_value")]
        public string _aacn_assessment_line_question_value { get; set; }

        [JsonProperty("_aacn_assessment_line_question_option_value")]
        public string _aacn_assessment_line_question_option_value { get; set; }

        [JsonProperty("aacn_assessment_line_question_option_text")]
        public string aacn_assessment_line_question_option_text { get; set; }

        [JsonProperty("aacn_assessment_line_question_text")]
        public string aacn_assessment_line_question_text { get; set; }

        [JsonProperty("_createdby_value")]
        public string _createdby_value { get; set; }

        [JsonProperty("createdon")]
        public DateTime createdon { get; set; }

        [JsonProperty("modifiedon")]
        public DateTime modifiedon { get; set; }

        [JsonProperty("statecode@OData.Community.Display.V1.FormattedValue")]
        public string status { get; set; }
    }

    public class AssessmentRootNew
    {
        [JsonProperty("@odata.context")]
        public string odatacontext { get; set; }

        [JsonProperty("value")]
        public List<Assessments> value { get; set; }
    }

    public class Assessments
    {
        [JsonProperty("@odata.etag")]
        public string odataetag { get; set; }

        [JsonProperty("aacn_assessment_description")]
        public string aacn_assessment_description { get; set; }

        [JsonProperty("aacn_assessment_numer")]
        public string aacn_assessment_numer { get; set; }

        [JsonProperty("aacn_assessment_title")]
        public string aacn_assessment_title { get; set; }

        [JsonProperty("aacn_assessmentid")]
        public string aacn_assessmentid { get; set; }

        [JsonProperty("aacn_passing_score")]
        public int aacn_passing_score { get; set; }

        [JsonProperty("_aacn_survey_provider_value")]
        public string _aacn_survey_provider_value { get; set; }
        
        [JsonProperty("_aacn_survey_provider_value@OData.Community.Display.V1.FormattedValue")]
        public string survey_Provider_Name { get; set; }

        [JsonProperty("_createdby_value")]
        public string _createdby_value { get; set; }

        [JsonProperty("modifiedon")]
        public DateTime modifiedon { get; set; }

        [JsonProperty("overriddencreatedon")]
        public object overriddencreatedon { get; set; }

        [JsonProperty("statecode@OData.Community.Display.V1.FormattedValue")]
        public string status { get; set; }

        [JsonProperty("aacn_assessment_line_assessment_header_aacn_as")]
        public List<AacnAssessmentLineAssessmentHeaderAacnA> AssessmentsLines { get; set; }
    }


}
