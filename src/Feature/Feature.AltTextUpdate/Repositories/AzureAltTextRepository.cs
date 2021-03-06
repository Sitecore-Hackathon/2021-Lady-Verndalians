using Feature.AltTextUpdate.Models;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;


namespace Feature.AltTextUpdate.Repositories
{
    public class AzureAltTextRepository : IAzureAltTextRepository
    {
        public AltTextResult GetImageDescription(Stream image, string descriptionLanguage)
        {
            byte[] bytes = new byte[image.Length];
            image.Read(bytes, 0, bytes.Length);

            try
            {
                var result = new AltTextResult();

                var analysis = MakeDescriptiveAnalysisRequestToAzure(bytes, descriptionLanguage);

                if (analysis == null)
                {
                    // we had some sort of comm failure, but oddly, no exceptions.
                    result.Status = AltTextResult.ResultStatus.NoResponse;
                    return result;
                }                

                result.Descriptions = GetCaptionsAboveThreshold(analysis);
                result.Description = result.Descriptions.FirstOrDefault();

                return result;
            }
            catch (Exception ex)
            {
                return new AltTextResult { Status = AltTextResult.ResultStatus.Error};
            }
        }

        private ImageAnalysis MakeDescriptiveAnalysisRequestToAzure(byte[] imageByteArray, string language)
        {
            ImageAnalysis output = null;
            HttpWebResponse response = null;

            try
            {
                // https://hackathon2021.cognitiveservices.azure.com/vision/v2.0/analyze
                var url = $"{ConfigurationManager.AppSettings["cognitiveServicesUri"]}vision/v2.0/analyze";
                var request = WebRequest.CreateHttp(new Uri(url));
                request.ContentType = "application/octet-stream";
                var subscriptionKey = ConfigurationManager.AppSettings["subscriptionKey"];
                request.Headers.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
                request.Method = "POST";
                request.ContentLength = imageByteArray.Length;
                var requestStream = request.GetRequestStream();
                requestStream.Write(imageByteArray, 0, imageByteArray.Length);
                requestStream.Close();


                response = (HttpWebResponse)request.GetResponse();


                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new HttpRequestException("did not return success");
                }

                // ReSharper disable once AssignNullToNotNullAttribute
                var reader = new StreamReader(response.GetResponseStream());
                output = JsonConvert.DeserializeObject<ImageAnalysis>(reader.ReadToEnd());
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(ex);
                throw;
            }
            
            return output;
        }

        private IEnumerable<string> GetCaptionsAboveThreshold(ImageAnalysis analysis)
        {
            var confidenceThreshold = 0.75;

            var captions = new SortedList<double, string>();

            foreach (var caption in analysis.Description.Captions)
            {
                if (caption.Confidence >= confidenceThreshold)
                {
                    captions.Add(caption.Confidence, caption.Text);
                }
            }

            return captions.Values.Reverse();
        }
    }
}
