using System.Collections.Generic;

namespace SampleApi.Models
{
    public class GenericPagedList<T>
    {
        public int PageIndex { set; get; }
        public int PageSize { set; get; }
        public int TotalCount { set; get; }
        public int TotalPages { set; get; }
        public IEnumerable<T> Items { get; set; }
    }
}