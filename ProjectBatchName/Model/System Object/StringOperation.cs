using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProjectBatchName.Model
{
    public abstract class StringOperation
    {
        public StringOperation()
        {

        }
        public IStringArgs Args { get; set; }
        public abstract string Name { get; }
        public abstract string Description { get; }

        public abstract string Operate(string origin);
    }

    class ReplaceOpertion : StringOperation
    {
        public override string Name
        {
            get { return "Replace"; }
        }

        public override string Description
        {
            get
            {
                var args = Args as ReplaceArgs;
                return $"Replace from {args.From} to {args.To}";
            }
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

        public override string Description
        {
            get
            {
                var args = Args as NewCaseArgs;
                switch (args.Mode)
                {
                    case 1:
                        return "Convert to lowercase";
                    case 2:
                        return "Convert to upercase";
                    default:
                        return "Convert to capitalize case";
                }
            }
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

        public override string Description => "Normalize Full Name";

        public override string Operate(string origin)
        {
            string modified = String.Join(" ", origin.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries));
            modified = modified.ToLower();
            modified = Regex.Replace(modified, @"(^\w)|(\s\w)", m => m.Value.ToUpper());
            return modified;
        }
    }
    
    //By Vu Pham Duc Thang
    class Move : StringOperation
    {
        public override string Name => "Move";

        public override string Description
        {
            get
            {
                var args = Args as MoveArgs;
                switch (args.Mode)
                {
                    case 1:
                        return "Move ISBN to Start";
                    default:
                        return "Move ISBN to End";
                }
            }
        }

        public override string Operate(string origin)
        {
            var args = Args as MoveArgs;
            string pattern = @"((978[\--– ])?[0-9][0-9\--– ]{10}[\--– ][0-9xX])|((978)?[0-9]{9}[0-9Xx])";
            Regex myRegex = new Regex(pattern);
            string ISBN = "";
            foreach (Match match in myRegex.Matches(origin))
            {
                ISBN = match.ToString();
                break;
            }

            //Not found ISBN in string
            if (ISBN == "") return origin;
            switch (args.Mode)
            {
                //Case 1: move to head of string
                case 1:
                    return ISBN + " " + origin.Replace(ISBN, "");

                //Case 2: move to tail of string
                default:
                    return origin.Replace(ISBN, "") + " " + ISBN;
            }
        }
    }

    class UniqueName : StringOperation
    {
        public override string Name => "Unique Name";

        public override string Description => "Use GUID to set an unique name";

        public override string Operate(string origin)
        {
            return Guid.NewGuid().ToString();
        }
    }
}
