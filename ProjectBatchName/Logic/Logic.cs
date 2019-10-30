using ProjectBatchName.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBatchName.Logic
{
    public class Logic : ILogic
    {
        public static Logic Instance { get; } = new Logic();
    }
}
