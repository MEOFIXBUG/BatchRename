﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProjectBatchName.Model
{
    abstract class StringOperation
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

    class NewFullnameNormalize : StringOperation
    {
        public override string Name => "FullnameNormalize";

        public override string Description => "the quick brown fox jump over the lazy dog";

        public override string Operate(string origin)
        {
            return "the quick brown fox jump over the lazy dog";
            //do something later            
        }
    }

    class Move : StringOperation
    {
        public override string Name => "Move";

        public override string Description => "Move What? How? Implemment later";

        public override string Operate(string origin)
        {
            return "";
            //do it later.

        }
    }

    class UniqueName : StringOperation
    {
        public override string Name => "Unique Name";

        public override string Description => "Use GUID to set an unique name";

        public override string Operate(string origin)
        {
            return "Nguyen Hoang Phuc";
            //do it later.
        }
    }
}
