using Domain.Enums;

namespace Application.Common.Dtos
{
    public class ResponseIEnumerableDto<T>
    {
        public IEnumerable<T> Value { get; set; }
        public int Count { get; set; }
        public bool IsError { get; set; }
        public ResponseCode ResponseCode { get; set; }
        public string JsonResponded { get; set; }
        public string Description { get; set; }
    }

}

