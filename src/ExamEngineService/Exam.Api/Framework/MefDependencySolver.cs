using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Web;
using System.Web.Http.Dependencies;

namespace Exam.Api.Framework
{
    /// <summary>
    ///     MEF IOC
    /// </summary>
    public class MefDependencySolver : IDependencyResolver
    {
        private const string MefContainerKey = "MefContainerKey";
        private readonly ComposablePartCatalog _catalog;

        public MefDependencySolver(ComposablePartCatalog catalog)
        {
            _catalog = catalog;
        }

        public CompositionContainer Container
        {
            get
            {
                if (!HttpContext.Current.Items.Contains(MefContainerKey))
                {
                    HttpContext.Current.Items.Add(MefContainerKey, new CompositionContainer(_catalog));
                }
                var container = (CompositionContainer) HttpContext.Current.Items[MefContainerKey];
                HttpContext.Current.Application["Container"] = container;
                return container;
            }
        }

        public object GetService(Type serviceType)
        {
            string contractName = AttributedModelServices.GetContractName(serviceType);
            return Container.GetExportedValueOrDefault<object>(contractName);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return Container.GetExportedValues<object>(serviceType.FullName);
        }

        public IDependencyScope BeginScope()
        {
            return new MefDependencySolver(Container.Catalog);
        }

        public void Dispose()
        {
            Container.Dispose();
        }
    }
}