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

    public class AssessmentLineService : BaseService
    {
        public AssessmentLineModel getAssessmentlinebyId(string AssessmentId)
        {
            AssessmentLineModel assessmentLineModel = new AssessmentLineModel();
            string query = string.Format(Utility.getAssessmentlinebyId, AssessmentId);
            HttpResponseMessage assessmentLineResponse = GetRecords(query);
            if (assessmentLineResponse.IsSuccessStatusCode == true)
            {
                if (!string.IsNullOrEmpty(assessmentLineResponse.Content.ReadAsStringAsync().Result))
                {
                    assessmentLineModel = JsonConvert.DeserializeObject<AssessmentLineModel>(Utility.RemoveJsonNulls(assessmentLineResponse.Content.ReadAsStringAsync().Result));
                }
                else
                {

                }


            }
            return assessmentLineModel;
        }

        public HttpResponseMessage CreateAssesmentLine(PostAssessMentLine assessmentLine)
        {
            HttpResponseMessage answerResponse = null;
            try
            {
                string odataQuery = "aacn_assessment_lines";
                answerResponse = CreateRecord(odataQuery, JsonConvert.SerializeObject(assessmentLine));
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

        public HttpResponseMessage updateAssessmentLine(PostAssessMentLine assessMentLine, string assessmentLineId)
        {
            HttpResponseMessage answerResponse = null;
            try
            {
                if (assessmentLineId != null)
                {
                    string odataQuery = "aacn_assessment_lines(" + assessmentLineId + ")";
                    answerResponse = UpdateRequest(odataQuery, JsonConvert.SerializeObject(assessMentLine));
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

        public APIResponse UpsertAssessmentLine(PostAssessMentLine assessMentLine, string assessmentLineId)
        {
            HttpResponseMessage answerResponse = null;
            APIResponse apiResponse = new APIResponse();
            try
            {
                bool recordAvailable = false;
                if (!string.IsNullOrEmpty(assessmentLineId))
                {
                    AssessmentLineModel assessmentLineModel = getAssessmentlinebyId(assessmentLineId);
                    if (assessmentLineModel.value != null && assessmentLineModel.value.Count != 0)
                    {
                        recordAvailable = true;
                        answerResponse = updateAssessmentLine(assessMentLine, assessmentLineId);
                    }

                }
                if (recordAvailable == false)
                {
                    answerResponse = CreateAssesmentLine(assessMentLine);
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
