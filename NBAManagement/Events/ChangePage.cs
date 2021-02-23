using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace NBAManagement.Events
{
    class ChangePage : IEvent
    {
        public Page NextPage { get; set; }
        public ChangePage(Page page)
        {
            NextPage = page;
        }
    }
}
