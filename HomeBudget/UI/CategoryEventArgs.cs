using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HomeBudget.UI
{
    internal class CategoryEventArgs : EventArgs
    {
        public string CategoryName { get; set; }
        public string CategoryPrefix { get; set; }
    }
}
