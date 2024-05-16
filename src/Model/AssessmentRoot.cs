namespace AACN.API.Model
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class AssessmentLines
    {
        [JsonProperty("_aacn_assessment_header_value")]
        public string _aacn_assessment_header_value { get; set; }

        [JsonProperty("aacn_assessment_line_control")]
        public int? aacn_assessment_line_control { get; set; }

        [JsonProperty("aacn_assessment_line_number")]
        public string aacn_assessment_line_number { get; set; }

        [JsonProperty("aacn_assessment_line_order")]
        public object aacn_assessment_line_order { get; set; }

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

        [JsonProperty("aacn_assessment_line_option_assessment_line_aa")]
        public List<AssessmentLineOptionData> assessment_Line_Options { get; set; }

        [JsonProperty("aacn_assessment_line_assessment_line_reference")]
        public List<AssessmentLineRefData> child_Assessment_Lines { get; set; }
    }

    public class AssessmentLineRefData
    {
        [JsonProperty("_aacn_assessment_header_value")]
        public object _aacn_assessment_header_value { get; set; }

        [JsonProperty("aacn_assessment_line_control")]
        public int? aacn_assessment_line_control { get; set; }

        [JsonProperty("aacn_assessment_line_number")]
        public string aacn_assessment_line_number { get; set; }

        [JsonProperty("aacn_assessment_line_order")]
        public object aacn_assessment_line_order { get; set; }

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

        [JsonProperty("_createdonbehalfby_value")]
        public object _createdonbehalfby_value { get; set; }

        [JsonProperty("aacn_assessment_line_option_assessment_line_aa")]
        public List<AssessmentLineOptionData> assessmentOptionsData { get; set; }
    }

    public class AssessmentLineOptionData
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
    }

    public class Events
    {
        [JsonProperty("_aacn_assessment_code_value")]
        public string _aacn_assessment_code_value { get; set; }

        [JsonProperty("aacn_end_date")]
        public DateTime aacn_end_date { get; set; }

        [JsonProperty("aacn_event_id")]
        public string aacn_event_id { get; set; }

        [JsonProperty("aacn_event_name")]
        public string aacn_event_name { get; set; }

        [JsonProperty("aacn_eventid")]
        public string aacn_eventid { get; set; }

        [JsonProperty("_aacn_parent_event_value")]
        public object _aacn_parent_event_value { get; set; }

        [JsonProperty("aacn_start_date")]
        public DateTime aacn_start_date { get; set; }

        [JsonProperty("_createdby_value")]
        public string _createdby_value { get; set; }

        [JsonProperty("createdon")]
        public DateTime createdon { get; set; }

        [JsonProperty("aacn_event_parent_event_aacn_event")]
        public List<ParentEventData> childEvents { get; set; }
    }

    public class ParentEventData
    {
        [JsonProperty("_aacn_assessment_code_value")]
        public object _aacn_assessment_code_value { get; set; }

        [JsonProperty("aacn_end_date")]
        public DateTime aacn_end_date { get; set; }

        [JsonProperty("aacn_event_id")]
        public string aacn_event_id { get; set; }

        [JsonProperty("aacn_event_name")]
        public string aacn_event_name { get; set; }

        [JsonProperty("aacn_eventid")]
        public string aacn_eventid { get; set; }

        [JsonProperty("_aacn_parent_event_value")]
        public string _aacn_parent_event_value { get; set; }

        [JsonProperty("aacn_start_date")]
        public DateTime aacn_start_date { get; set; }

        [JsonProperty("_aacn_survey_provider_value")]
        public object _aacn_survey_provider_value { get; set; }

        [JsonProperty("modifiedon")]
        public DateTime modifiedon { get; set; }
    }

    public class Responses
    {
        [JsonProperty("_aacn_assessment_value")]
        public string _aacn_assessment_value { get; set; }

        [JsonProperty("_aacn_event_value")]
        public string _aacn_event_value { get; set; }

        [JsonProperty("_aacn_member_value")]
        public string _aacn_member_value { get; set; }

        [JsonProperty("aacn_response_number")]
        public string aacn_response_number { get; set; }

        [JsonProperty("aacn_responseid")]
        public string aacn_responseid { get; set; }

        [JsonProperty("_aacn_session_value")]
        public string _aacn_session_value { get; set; }

        [JsonProperty("_aacn_survey_provider_value")]
        public string _aacn_survey_provider_value { get; set; }

        [JsonProperty("_createdby_value")]
        public string _createdby_value { get; set; }

        [JsonProperty("createdon")]
        public DateTime createdon { get; set; }

        [JsonProperty("aacn_response_line_response_aacn_response")]
        public List<ResponseLineData> responseLineData { get; set; }
    }

    public class ResponseLineData
    {
        [JsonProperty("@odata.etag")]
        public string odataetag { get; set; }

        [JsonProperty("_aacn_answer_value")]
        public string _aacn_answer_value { get; set; }

        [JsonProperty("_aacn_assesment_line_value")]
        public object _aacn_assesment_line_value { get; set; }

        [JsonProperty("_aacn_assessment_line_option_value")]
        public string _aacn_assessment_line_option_value { get; set; }

        [JsonProperty("_aacn_question_value")]
        public string _aacn_question_value { get; set; }

        [JsonProperty("aacn_question_text")]
        public string aacn_question_text { get; set; }

        [JsonProperty("_aacn_response_value")]
        public string _aacn_response_value { get; set; }

        [JsonProperty("aacn_response_line_number")]
        public string aacn_response_line_number { get; set; }

        [JsonProperty("aacn_response_lineid")]
        public string aacn_response_lineid { get; set; }

        [JsonProperty("aacn_response_text")]
        public string aacn_response_text { get; set; }

        [JsonProperty("utcconversiontimezonecode")]
        public object utcconversiontimezonecode { get; set; }

        [JsonProperty("versionnumber")]
        public int versionnumber { get; set; }
    }

    public class Sessions
    {
        [JsonProperty("_aacn_assessment_code_value")]
        public string _aacn_assessment_code_value { get; set; }

        [JsonProperty("aacn_end_date")]
        public DateTime aacn_end_date { get; set; }

        [JsonProperty("aacn_end_time")]
        public object aacn_end_time { get; set; }

        [JsonProperty("_aacn_event_id_value")]
        public string _aacn_event_id_value { get; set; }

        [JsonProperty("aacn_session_id")]
        public string aacn_session_id { get; set; }

        [JsonProperty("aacn_session_name")]
        public string aacn_session_name { get; set; }

        [JsonProperty("aacn_sessionid")]
        public string aacn_sessionid { get; set; }

        [JsonProperty("aacn_start_date")]
        public DateTime aacn_start_date { get; set; }

        [JsonProperty("aacn_start_time")]
        public object aacn_start_time { get; set; }

        [JsonProperty("_aacn_survey_provider_value")]
        public string _aacn_survey_provider_value { get; set; }

        [JsonProperty("_createdby_value")]
        public string _createdby_value { get; set; }

        [JsonProperty("createdon")]
        public DateTime createdon { get; set; }

        [JsonProperty("statecode")]
        public int statecode { get; set; }

        [JsonProperty("statuscode")]
        public int statuscode { get; set; }
    }

    public class AssessmentRoot
    {
        [JsonProperty("@odata.context")]
        public string odatacontext { get; set; }
        [JsonProperty("value")]
        public List<AssessmentData> value { get; set; }
    }

    public class AssessmentData
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

        [JsonProperty("_createdby_value")]
        public string _createdby_value { get; set; }

        [JsonProperty("modifiedon")]
        public DateTime modifiedon { get; set; }

        [JsonProperty("overriddencreatedon")]
        public object overriddencreatedon { get; set; }

        [JsonProperty("aacn_assessment_line_assessment_header_aacn_as")]
        public List<AssessmentLines> assessment_Lines { get; set; }

        [JsonProperty("aacn_event_assessment_code_aacn_assessment")]
        public List<Events> eventsData { get; set; }

        [JsonProperty("aacn_response_assessment_aacn_assessment")]
        public List<Responses> responseData { get; set; }

        [JsonProperty("aacn_session_assessment_code_aacn_assessment")]
        public List<Sessions> sessionsData { get; set; }
    }


}
