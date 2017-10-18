using AutoMapper;
using Ninject;

namespace ERP.BLL
{
    /// <summary>
    /// Initialises all the AutoMapper profiles to create various mappings
    /// </summary>
    public static class AutoMapperBootstrapper
    {
        /// <summary>
        /// Configure the model mappings including .Ignore() for fields that explicity not mapped
        /// Include ignores here for fields that you know don't need to be mapped else the unit tests will fail
        /// The ninject kernel is passed in here to allow AutoMapper to construct objects 
        /// whilst injecting neccesary dependencies
        /// </summary>
        public static void Initialize(IKernel kernel)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.ConstructServicesUsing(type => kernel.Get(type));                
            });

        }
    }
}
