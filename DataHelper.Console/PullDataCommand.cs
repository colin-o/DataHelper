using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataHelper.Console
{
    public class PullDataCommand : CommandBase
    {
        public override void Execute()
        {
            throw new NotImplementedException();
        }

        public override void ExplainUsage()
        {
            Out.Usage("pulldata <server name> <database name>");
        }
    }
}
