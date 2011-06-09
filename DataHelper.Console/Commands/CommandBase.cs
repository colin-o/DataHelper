using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;

namespace DataHelper.Console
{
    public abstract class CommandBase : ICommand
    {
        public string ServerName;

        public string DatabaseName;

        [Inject]
        public IOutput Out { get; set; }

        public virtual void SetArguments(string[] args)
        {
            this.ServerName = args[0];
            this.DatabaseName = args[1];
        }

        public abstract void Execute();

        public abstract void ExplainUsage();

    }
}
