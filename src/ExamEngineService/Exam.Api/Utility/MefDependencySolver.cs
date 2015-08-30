// 源文件头信息：
// 文 件 名：MefDependencySolver.cs
// 类    名：MefDependencySolver
// 所属工程：Exam.Api
// 最后修改：游凯
// 最后修改：2013-09-11 05:08:10


using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Web;
using System.Web.Mvc;

namespace Exam.Api.Utility
{
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
    }
}