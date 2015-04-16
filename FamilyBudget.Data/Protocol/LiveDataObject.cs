using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;

namespace FamilyBudget.Data.Protocol
{
    public class LiveDataObject
    {
        public object dataSource { get; set; }
        public ObjectContext objectContext { get; set; }
    }
}
