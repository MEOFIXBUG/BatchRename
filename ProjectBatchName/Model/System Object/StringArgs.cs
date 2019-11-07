using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBatchName.Model
{
    public interface IStringArgs
    {
    }
    
    class ReplaceArgs : IStringArgs
    {
        string from;
        string to;

        public string From { get => from; set => from = value; }
        public string To { get => to; set => to = value; }
    }

    class NewCaseArgs: IStringArgs
    {
        int mode;

        public int Mode { get => mode; set => mode = value; }
    }

    class MoveArgs: IStringArgs
    {
        int mode;

        public int Mode { get => mode; set => mode = value; }
    }

 
}
