using Newtonsoft.Json;

namespace CadContato.WebApi.Models
{
    public class GenericErrorDTO
    {
        public int StatusCode { get; set; }

        public string Message { get; set; }

        public bool Success { get { return false; } }

        public GenericErrorDTO()
        {
        }

        public GenericErrorDTO(int statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
