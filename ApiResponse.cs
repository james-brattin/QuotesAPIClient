using System;
using System.Collections.Generic;
using System.Text;

namespace QuotesAPIClient
{
    public class ApiResponse
    {
        public Dictionary<string, List<Quotes>> contents { get; set; }
    }
    public class Quotes
    {
        public string quote { get; set; }
        public string author { get; set; }
        public Uri permalink { get; set; }
    }
}
