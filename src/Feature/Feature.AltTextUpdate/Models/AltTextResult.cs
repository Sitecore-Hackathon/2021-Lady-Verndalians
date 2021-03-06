using System.Collections.Generic;


namespace Feature.AltTextUpdate.Models
{
    public class AltTextResult
    {
        public enum ResultStatus
        {
            Success,
            NoResponse,
            Error
        }

        public IEnumerable<string> Descriptions { get; set; }

        public string Description { get; set; }

        public ResultStatus Status { get; set; }

        public AltTextResult()
        {
            Descriptions = new List<string>();
            Status = ResultStatus.Success;
        }
    }
}
