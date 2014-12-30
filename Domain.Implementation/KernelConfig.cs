using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using Infrastructure;
using Infrastructure.DtoFetchers;
using Infrastructure.Entities;
using Ninject;

namespace Domain.Implementation
{
    public static class KernelConfig
    {
        public static IKernel AddBindings(this IKernel kernel)
        {
            kernel.Bind<ITreeDao>().To<TreeDao>();

            kernel.Bind<ITreeRepository>().To<TreeRepository>();
            kernel.Bind<IDomainTreeService>().To<DomainTreeService>();
            //kernel.Bind<IDaoFetcher<TreeDto, TreeDao>>().To<TreeDaoFetcher>();
            kernel.Bind<IDtoFetcher<TreeDao, TreeDto>>().To<TreeDtoFetcher>();
            return kernel;
        }
    }
}
