using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace FamilyBudget.Data.Protocol
{
    public class APIResponseObject
    {
        public String status { get; set; }
        public String reason { get; set; }
        public List<Object> data { get; set; }
    }
}
