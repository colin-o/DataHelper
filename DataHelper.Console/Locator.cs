using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;
using Ninject.Modules;
using DataHelper.Core;

namespace DataHelper.Console
{
    public class Locator
    {
        private class Services : NinjectModule
        {
            public override void Load()
            {
                Bind<IFileManager>().To<StandardFileManager>();
                Bind<IOutput>().To<StandardOutput>();
                Bind<ICommandInterpreter>().To<StandardCommandInterpreter>();

                Bind<IDataManager>().To<MsSqlDataManager>();
                Bind<ISchemaManager>().To<MsSqlSchemaManager>();
            }
        }

        private static IKernel _kernel;

        public static IKernel Kernel
        {
            get
            {
                if (_kernel == null)
                    _kernel = new StandardKernel(new INinjectModule[] { new Services() });
                return _kernel;
            }
        }

        public static void Inject(object instance)
        {
            Kernel.Inject(instance);
        }

        public static TObject Instance<TObject>() where TObject : class
        {
            return Kernel.GetService(typeof(TObject)) as TObject;
        }
    }
}
