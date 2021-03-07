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
            byte[] mediaBytes = new byte[image.Length];
            image.Read(mediaBytes, 0, mediaBytes.Length);

            try
            {
                var result = new AltTextResult();

                var analyzedImage = RequestImageAnalysisToComputerVisionAPI(mediaBytes, descriptionLanguage);

                if (analyzedImage == null)
                {                    
                    result.Status = AltTextResult.ResultStatus.NoResponse;
                    return result;
                }                

                result.Descriptions = GetCaptionsFromImageAnalysis(analyzedImage);
                result.Description = result.Descriptions.FirstOrDefault();

                return result;
            }
            catch (Exception ex)
            {
                return new AltTextResult { Status = AltTextResult.ResultStatus.Error};
            }
        }

        private ImageAnalysis RequestImageAnalysisToComputerVisionAPI(byte[] imageByteArray, string language)
        {
            ImageAnalysis analyzedImage = null;
            
            try
            {
                // https://hackathon2021.cognitiveservices.azure.com/vision/v2.1/analyze?visualFeatures=Description,Tags
                var url = $"{ConfigurationManager.AppSettings["cognitiveServicesUri"]}{ConfigurationManager.AppSettings["cognitiveComputerVisionUrlPart"]}{ConfigurationManager.AppSettings["visualFeaturesParams"]}";
                var request = WebRequest.CreateHttp(new Uri(url));
                request.ContentType = "application/octet-stream";
                var subscriptionKey = ConfigurationManager.AppSettings["subscriptionKey"];
                request.Headers.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
                request.Method = "POST";
                request.ContentLength = imageByteArray.Length;
                var requestStream = request.GetRequestStream();
                requestStream.Write(imageByteArray, 0, imageByteArray.Length);
                requestStream.Close();

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();               
                                
                var reader = new StreamReader(response.GetResponseStream());
                analyzedImage = JsonConvert.DeserializeObject<ImageAnalysis>(reader.ReadToEnd());
            }
            catch (HttpRequestException ex)
            {
                throw;
            }
            
            return analyzedImage;
        }

        private IEnumerable<string> GetCaptionsFromImageAnalysis(ImageAnalysis analysis)
        {
            var confidenceThreshold = Convert.ToDouble(ConfigurationManager.AppSettings["confidenceThreshold"]);

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
