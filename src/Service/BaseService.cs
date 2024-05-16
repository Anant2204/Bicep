
namespace AACN.Services
{
    using AACN.API.Model;
    using AACN.API.Service;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Data;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml;
    public class BaseService : Connection
    {
        bool IsTokenValid = false;

        public HttpResponseMessage GetRecords(string query)
        {
            HttpResponseMessage response;
            try
            {
                Task<string> task = AccessTokenGenerator();
                CrmBearerToken = task.Result;
                //CrmBearerToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6IjJaUXBKM1VwYmpBWVhZR2FYRUpsOGxWMFRPSSIsImtpZCI6IjJaUXBKM1VwYmpBWVhZR2FYRUpsOGxWMFRPSSJ9.eyJhdWQiOiJodHRwczovL29wZXJhdGlvbnMtdzM2NXVhdC5hcGkuY3JtLmR5bmFtaWNzLmNvbSIsImlzcyI6Imh0dHBzOi8vc3RzLndpbmRvd3MubmV0Lzc4M2NkNDY1LTc1OTYtNDMxZS04YTIyLTNlYTAzM2U4MmVmYy8iLCJpYXQiOjE2NzA0Nzk2NDEsIm5iZiI6MTY3MDQ3OTY0MSwiZXhwIjoxNjcwNDgzNTQxLCJhaW8iOiJFMlpnWUlqNzl1cWZ3M2syVnhsaDgzMkxCWFlvQXdBPSIsImFwcGlkIjoiNDAwZmEzZjMtYTYzYy00ODE4LWEzNTMtMTQxZGM4OTYwNzI5IiwiYXBwaWRhY3IiOiIxIiwiaWRwIjoiaHR0cHM6Ly9zdHMud2luZG93cy5uZXQvNzgzY2Q0NjUtNzU5Ni00MzFlLThhMjItM2VhMDMzZTgyZWZjLyIsIm9pZCI6IjExOTViYzdiLWUyYWEtNDMyOC1iYmRiLTI0MjM3Y2M5Yzk1NCIsInJoIjoiMC5BU2NBWmRROGVKWjFIa09LSWo2Z00tZ3VfQWNBQUFBQUFBQUF3QUFBQUFBQUFBQW5BQUEuIiwic3ViIjoiMTE5NWJjN2ItZTJhYS00MzI4LWJiZGItMjQyMzdjYzljOTU0IiwidGlkIjoiNzgzY2Q0NjUtNzU5Ni00MzFlLThhMjItM2VhMDMzZTgyZWZjIiwidXRpIjoiUUlwcXlKS1h1VUc2bk51OVZMZFBBQSIsInZlciI6IjEuMCJ9.d7LKwCEY50J1OE5GEhkZWYAuhgSiEZeF-d-8KmtKmyKwSpV-FzaMnFC9Y8LG3bvhrts5aYKwwNh2zuvo_foh04l21X61Cu-qzr4C3RCXRckGP-FBtS5gw3lB4guBetDeVGX4FU6Q2sDj0rLvLB5lEPiQ45GfsenIGoTefn1r14EMrH42tEitApnA9i4-t2whGsNPMhC3-wxPG9NyaAuw7GkXuOIOx2tAAwoh4snVjnLfPx31-8c59kjir96Hjp5UCdDpNJoxG28HLB1bixl5cc__rU_sXslppeE9xv8zylALNCn4ZsyyeV2-nZm_GWaYC7ehN1zCtW75snigSnJ0QQ";
                //if (CrmBearerToken == null)
                //{
                //    Task<string> task = AccessTokenGenerator();
                //    CrmBearerToken = task.Result;
                //}
                string crmRestQuery = CrmApiUrl + query;

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, crmRestQuery);
                //add header parameters
                request.Headers.Add("Prefer", "odata.include-annotations=\"*\"");//for formatted values
                request.Headers.Add("Authorization", "Bearer " + CrmBearerToken);
                HttpClient httpClient = new HttpClient();
                //send request
                response = httpClient.SendAsync(request).Result;

                TokenValidate(response);

                if (CrmBearerToken == null)
                {
                    GetRecords(query);
                }

                string responseString = response.Content.ReadAsStringAsync().Result;
            }
            catch (WebException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

        public HttpResponseMessage CreateRecord(string query, object entity)
        {
            HttpResponseMessage response = null;

            try
            {
                Task<string> task = AccessTokenGenerator();
                CrmBearerToken = task.Result;
                //if (CrmBearerToken == null)
                //{
                //    Task<string> task = AccessTokenGenerator();
                //    CrmBearerToken = task.Result;
                //}
                string crmRestQuery = CrmApiUrl + query;
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, crmRestQuery);
                //add header parameters
                request.Headers.Add("Authorization", "Bearer " + CrmBearerToken);
                request.Content = new StringContent(entity.ToString(), Encoding.UTF8, "application/json");
                request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json; charset=utf-8");
                HttpClient httpClient = new HttpClient();
                response = httpClient.SendAsync(request).Result;

                TokenValidate(response);
                if (CrmBearerToken == null)
                {
                    CreateRecord(query, entity);
                }
            }
            catch (WebException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

        public HttpResponseMessage UpdateRequest(string query, object entity)
        {
            HttpResponseMessage response = null;
            try
            {

                //
                Task<string> task = AccessTokenGenerator();
                CrmBearerToken = task.Result;
                //if (CrmBearerToken == null)
                //{

                //}
                string crmRestQuery = CrmApiUrl + query;
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Patch, crmRestQuery);
                //add header parameters
                request.Headers.Add("Authorization", "Bearer " + CrmBearerToken);
                request.Content = new StringContent(entity.ToString());
                request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json; charset=utf-8");
                HttpClient httpClient = new HttpClient();
                response = httpClient.SendAsync(request).Result;

                TokenValidate(response);
                if (CrmBearerToken == null)
                {
                    UpdateRequest(query, entity);
                }
            }
            catch (WebException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;

        }
        public void TokenValidate(HttpResponseMessage response)
        {
            if (IsTokenValid == false)
            {
                if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NoContent)
                {
                    IsTokenValid = true;
                    // CrmBearerToken = null;
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    CrmBearerToken = null;
                }
            }

        }
        public string getSurveryProviderByName(string Name)
        {
            string surveyProviderId = "";
            SurveyProviderModel surveyProviderModel = new SurveyProviderModel();
            string query = string.Format(Utility.getSurveyProviderByName, Name);
            HttpResponseMessage answerResponse = GetRecords(query);
            if (answerResponse.IsSuccessStatusCode == true)
            {
                if (!string.IsNullOrEmpty(answerResponse.Content.ReadAsStringAsync().Result))
                {
                    surveyProviderModel = JsonConvert.DeserializeObject<SurveyProviderModel>(Utility.RemoveJsonNulls(answerResponse.Content.ReadAsStringAsync().Result));
                    if (surveyProviderModel.value != null && surveyProviderModel.value.Count > 0)
                    {
                        surveyProviderId = surveyProviderModel.value[0].aacn_survey_providerid;
                    }
                }
                else
                {

                }


            }
            return surveyProviderId;

        }
    }
}
