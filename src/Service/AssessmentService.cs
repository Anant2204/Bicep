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

    public class AssessmentService : BaseService
    {
        public AssessmentModel getAssessmentbyId(string AssessmentId)
        {
            AssessmentModel assessmentModel = new AssessmentModel();
            string query = string.Format(Utility.getAssessmentbyId, AssessmentId);
            HttpResponseMessage assessmentResponse = GetRecords(query);
            if (assessmentResponse.IsSuccessStatusCode == true)
            {
                if (!string.IsNullOrEmpty(assessmentResponse.Content.ReadAsStringAsync().Result))
                {
                    assessmentModel = JsonConvert.DeserializeObject<AssessmentModel>(Utility.RemoveJsonNulls(assessmentResponse.Content.ReadAsStringAsync().Result));
                }
                else
                {

                }


            }
            return assessmentModel;
        }

        public HttpResponseMessage createAssessment(PostAssessment postAssessment)
        {
            HttpResponseMessage answerResponse = new HttpResponseMessage();
            try
            {
                string odataQuery = "aacn_assessments";
                if (!string.IsNullOrEmpty(postAssessment.survey_Provider_Name))
                {
                    string survey_Provider_ID = getSurveryProviderByName(postAssessment.survey_Provider_Name);
                    if (!string.IsNullOrEmpty(survey_Provider_ID))
                    {
                        postAssessment.survey_Provider_Name = $"/aacn_survey_providers({survey_Provider_ID})";
                    }
                    else
                    {
                        answerResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                        answerResponse.Content = new StringContent("Survey Provider does not exists with name - " + postAssessment.survey_Provider_Name);
                        return answerResponse;
                    }
                }
                answerResponse = CreateRecord(odataQuery, JsonConvert.SerializeObject(postAssessment));
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

        public HttpResponseMessage updateAssessment(PostAssessment postAssessment, string answerId)
        {
            HttpResponseMessage answerResponse = new HttpResponseMessage();
            try
            {
                if (answerId != null)
                {
                    if (!string.IsNullOrEmpty(postAssessment.survey_Provider_Name))
                    {
                        string survey_Provider_ID = getSurveryProviderByName(postAssessment.survey_Provider_Name);
                        if (!string.IsNullOrEmpty(survey_Provider_ID))
                        {
                            postAssessment.survey_Provider_Name = $"/aacn_survey_providers({survey_Provider_ID})";
                        }
                        else
                        {
                            answerResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                            answerResponse.Content = new StringContent("Survey Provider does not exists with name - " + postAssessment.survey_Provider_Name);
                            return answerResponse;
                        }
                    }
                    string odataQuery = "aacn_assessments(" + answerId + ")";
                    answerResponse = UpdateRequest(odataQuery, JsonConvert.SerializeObject(postAssessment));
                    if (answerResponse.StatusCode == HttpStatusCode.NoContent)//204
                    {
                        string _recordUrl = answerResponse.Headers.GetValues("OData-EntityId").FirstOrDefault();
                        string[] splitRetrievedData = _recordUrl.Split('[', '(', ')', ']');
                        answerResponse.StatusCode = System.Net.HttpStatusCode.NoContent;
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
        public APIResponse UpsertAssessment(PostAssessment postAssessment, string assessmentId)
        {
            HttpResponseMessage answerResponse = null;
            APIResponse apiResponse = new APIResponse();
            try
            {
                bool recordAvailable = false;
                if (!string.IsNullOrEmpty(postAssessment.survey_Provider_Name))
                {
                    string survey_Provider_ID = getSurveryProviderByName(postAssessment.survey_Provider_Name);
                    if (!string.IsNullOrEmpty(survey_Provider_ID))
                    {
                        postAssessment.survey_Provider_Name = $"/aacn_survey_providers({survey_Provider_ID})";
                    }
                    else
                    {
                        answerResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                        answerResponse.Content = new StringContent("Survey Provider does not exists with name -" + postAssessment.survey_Provider_Name);
                        //return answerResponse;
                    }
                }
                if (!string.IsNullOrEmpty(assessmentId))
                {
                    AssessmentModel assessmentModel = getAssessmentbyId(assessmentId);
                    if (assessmentModel.value != null && assessmentModel.value.Count != 0)
                    {
                        recordAvailable = true;
                        answerResponse = updateAssessment(postAssessment, assessmentId);
                    }

                }
                if (recordAvailable == false)
                {
                    answerResponse = createAssessment(postAssessment);
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

        public AssessmentRootNew getAssessmentDetailsByNumber(string assessmentNumber)
        {
            AssessmentRootNew assessmentRoot = new AssessmentRootNew();
            string query = string.Format(Utility.getAssessmentByNumberNew2, assessmentNumber);
            HttpResponseMessage assessmentResponse = GetRecords(query);
            if (assessmentResponse.IsSuccessStatusCode == true)
            {
                if (!string.IsNullOrEmpty(assessmentResponse.Content.ReadAsStringAsync().Result))
                {
                    assessmentRoot = JsonConvert.DeserializeObject<AssessmentRootNew>(Utility.RemoveJsonNulls(assessmentResponse.Content.ReadAsStringAsync().Result));
                }
                else
                {

                }


            }
            return assessmentRoot;
        }
    }
}
