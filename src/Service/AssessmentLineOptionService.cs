namespace AACN.API.Service
{
    using AACN.API.Model;
    using AACN.Services;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Net;
    using System.Threading.Tasks;

    public class AssessmentLineOptionService : BaseService
    {
        public AssessmentLineOptionModel getAssessmentLineOptionbyId(string AssessmentLineOptionId)
        {
            AssessmentLineOptionModel assessmentLineOptionModel = new AssessmentLineOptionModel();
            string query = string.Format(Utility.getAssessmentLineOptionId, AssessmentLineOptionId);
            HttpResponseMessage answerResponse = GetRecords(query);
            if (answerResponse.IsSuccessStatusCode == true)
            {
                if (!string.IsNullOrEmpty(answerResponse.Content.ReadAsStringAsync().Result))
                {
                    assessmentLineOptionModel = JsonConvert.DeserializeObject<AssessmentLineOptionModel>(Utility.RemoveJsonNulls(answerResponse.Content.ReadAsStringAsync().Result));
                }
                else
                {

                }


            }
            return assessmentLineOptionModel;
        }

        public HttpResponseMessage createAssessmentLineOption(PostAssessMentLineOption postAssessMentLineOption)
        {
            HttpResponseMessage answerResponse = null;
            try
            {
                string odataQuery = "aacn_assessment_line_options";
                answerResponse = CreateRecord(odataQuery, JsonConvert.SerializeObject(postAssessMentLineOption));
                if (answerResponse.StatusCode == HttpStatusCode.NoContent)//204
                {
                    string _recordUrl = answerResponse.Headers.GetValues("OData-EntityId").FirstOrDefault();
                    string[] splitRetrievedData = _recordUrl.Split('[', '(', ')', ']');
                    answerResponse.StatusCode = System.Net.HttpStatusCode.NoContent; ;
                }
                else
                {
                    answerResponse.Content = new StringContent(answerResponse.Content.ReadAsStringAsync().Result);
                    answerResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                }

                return answerResponse;
            }
            catch (Exception ex)
            {
                answerResponse.Content = new StringContent(ex.Message);
                return null;
            }
        }

        public HttpResponseMessage updateAssessmentLineOption(PostAssessMentLineOption postAssessMentLineOption, string AssessmentLineOptionId)
        {
            HttpResponseMessage answerResponse = null;
            try
            {
                if (AssessmentLineOptionId != null)
                {
                    string odataQuery = "aacn_assessment_line_options(" + AssessmentLineOptionId + ")";
                    answerResponse = UpdateRequest(odataQuery, JsonConvert.SerializeObject(postAssessMentLineOption));
                    if (answerResponse.StatusCode == HttpStatusCode.NoContent)//204
                    {
                        string _recordUrl = answerResponse.Headers.GetValues("OData-EntityId").FirstOrDefault();
                        string[] splitRetrievedData = _recordUrl.Split('[', '(', ')', ']');
                        answerResponse.StatusCode = System.Net.HttpStatusCode.NoContent; ;
                    }
                    else
                    {
                        answerResponse.Content = new StringContent(answerResponse.Content.ReadAsStringAsync().Result);
                        answerResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    }
                }

                return answerResponse;
            }
            catch (Exception ex)
            {
                answerResponse.Content = new StringContent(ex.Message);
                return null;
            }
        }

        public APIResponse UpsertAssessmentLineOption(PostAssessMentLineOption postAssessMentLineOption, string AssessmentLineOptionId)
        {
            HttpResponseMessage answerResponse = null;
            APIResponse apiResponse = new APIResponse();
            try
            {
                bool recordAvailable = false;
                if (!string.IsNullOrEmpty(AssessmentLineOptionId))
                {
                    AssessmentLineOptionModel assessmentLineOptionModel = getAssessmentLineOptionbyId(AssessmentLineOptionId);
                    if (assessmentLineOptionModel.value != null && assessmentLineOptionModel.value.Count != 0)
                    {
                        recordAvailable = true;
                        answerResponse = updateAssessmentLineOption(postAssessMentLineOption, AssessmentLineOptionId);
                    }

                }
                if (recordAvailable == false)
                {
                    answerResponse = createAssessmentLineOption(postAssessMentLineOption);
                }
                if (answerResponse != null)
                {
                    if (answerResponse.StatusCode == HttpStatusCode.NoContent)
                    {
                        if (recordAvailable == false)
                        {
                            apiResponse.Status = "Created";
                        }
                        else
                        {
                            apiResponse.Status = "Updated";
                        }
                        string _recordUrl = answerResponse.Headers.GetValues("OData-EntityId").FirstOrDefault();
                        string[] splitRetrievedData = _recordUrl.Split('[', '(', ')', ']');
                        apiResponse.RecordId = new Guid(splitRetrievedData[1]);
                        apiResponse.StatusCode = (int)answerResponse.StatusCode;
                        return apiResponse;
                    }
                    else
                    {
                        apiResponse.RecordId = Guid.Empty;
                        apiResponse.Status = answerResponse.Content.ReadAsStringAsync().Result;
                        apiResponse.StatusCode = (int)answerResponse.StatusCode;
                    }

                }

                return apiResponse;
            }
            catch (Exception ex)
            {
                apiResponse.Status = ex.Message;
                apiResponse.RecordId = Guid.Empty;
                apiResponse.StatusCode = (int)HttpStatusCode.BadRequest;
                return apiResponse;
            }
        }
    }
}
