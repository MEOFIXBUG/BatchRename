using ProjectBatchName.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProjectBatchName.Model
{
    public abstract class StringOperation : BaseViewModel
    {
        public StringOperation()
        {

        }
        public IStringArgs Args { get; set; }
        public abstract string Name { get; }

        public abstract string Operate(string origin);
        public abstract StringOperation Clone();
    }

    class ReplaceOpertion : StringOperation
    {
        public override string Name
        {
            get { return "Replace"; }
        }

        public override StringOperation Clone()
        {
            var args = Args as ReplaceArgs;
            var res = new ReplaceOpertion()
            {
                Args = new ReplaceArgs()
                {
                    From = args.From,
                    To = args.To
                }
            };

            return res;
        }

        public override string Operate(string origin)
        {
            var args = Args as ReplaceArgs;
            return origin.Replace(args.From, args.To);
        }
    }

    class NewCaseOperation : StringOperation
    {
        public override string Name => "New Case";


        public override StringOperation Clone()
        {
            var args = Args as NewCaseArgs;
            var res = new NewCaseOperation()
            {
                Args = new NewCaseArgs()
                {
                    Mode = args.Mode
                }
            };

            return res;
        }

        public override string Operate(string origin)
        {
            var args = Args as NewCaseArgs;
            switch (args.Mode)
            {
                case 1:
                    return origin.ToUpper();
                case 2:
                    return origin.ToLower();
                default:
                    var s = Regex.Replace(origin, @"(^\w)|(\s\w)", m => m.Value.ToUpper());
                    return s;
            }
        }
    }
    //By Vu Pham Duc Thang
    class NewFullnameNormalize : StringOperation
    {
        public override string Name => "FullnameNormalize";

        public override StringOperation Clone()
        {
            return new NewFullnameNormalize() { Args = new NewFullNameNormalizeArgs() };
        }

        public override string Operate(string origin)
        {
            var exts = Path.GetExtension(origin);
            var path = Path.GetFileNameWithoutExtension(origin);
            string modified = String.Join(" ", path.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries));
            modified = modified.ToLower();
            modified = Regex.Replace(modified, @"(^\w)|(\s\w)", m => m.Value.ToUpper());
            return modified + exts;
        }
    }

    //By Vu Pham Duc Thang
    class Move : StringOperation
    {
        public override string Name => "Move";


        public override StringOperation Clone()
        {
            var args = Args as MoveArgs;
            return new Move()
            {
                Args = new MoveArgs()
                {
                    Mode = args.Mode,
                    Length = args.Length
                }
            };
        }

        public override string Operate(string origin)
        {
            var args = Args as MoveArgs;
          //  string pattern = @"((978[\--– ])?[0-9][0-9\--– ]{10}[\--– ][0-9xX])|((978)?[0-9]{9}[0-9Xx])";
          //  Regex myRegex = new Regex(pattern);
            string ISBN = "";
            //foreach (Match match in myRegex.Matches(origin))
            //{
            //    ISBN = match.ToString();
            //    break;
            //}

            //Not found ISBN in string
            //if (ISBN == "") return origin;
            var exts = Path.GetExtension(origin);
            var path = Path.GetFileNameWithoutExtension(origin);
            if (args.Length > path.Length || args.Length == 0) return origin;
            switch (args.Mode)
            {
                //Case 1: move to head of string
                case 1:
                    ISBN = path.Substring(path.Length - args.Length, args.Length);
                    return ISBN + " " + path.Replace(ISBN, "") + exts;

                //Case 2: move to tail of string
                default:
                    ISBN = path.Substring(0, args.Length);
                    return path.Replace(ISBN, "") + " " + ISBN + exts;
            }
        }
    }

    class UniqueName : StringOperation
    {
        public override string Name => "Unique Name";
        
        public override StringOperation Clone()
        {
            return new UniqueName() { Args = new UniqueNameArgs() };
        }

        public override string Operate(string origin)
        {
            var exts = Path.GetExtension(origin);
            return Guid.NewGuid().ToString() + exts;
        }
    }
}
