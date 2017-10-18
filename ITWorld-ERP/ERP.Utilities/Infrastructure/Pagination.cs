using System.Collections.Generic;

namespace ERP.Utilities.Infrastructure
{
    public class Pagination<T>
    {
        public long total { get; set; }
        public List<T> rows { get; set; }
    }
}
