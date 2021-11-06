using Ninject;
using System.Configuration;
using System.Data.Entity;
using Es.Udc.DotNet.Photogram.Model.ImageDao;
using Es.Udc.DotNet.Photogram.Model.CategoryDao;
using Es.Udc.DotNet.Photogram.Model.ImageService;


namespace Es.Udc.DotNet.Photogram.Test
{
    public class TestManager
    {
        /// <summary>
        /// Configures and populates the Ninject kernel
        /// </summary>
        /// <returns>The NInject kernel</returns>
        public static IKernel ConfigureNInjectKernel(){

            #region configuration via sourcecode

            IKernel kernel = new StandardKernel();
            
            kernel.Bind<IImageService>().To<ImageService>();

            kernel.Bind<IImageDao>().To<ImageDaoEntityFramework>();

            kernel.Bind<ICategoryDao>().To<CategoryDaoEntityFramework>();

            string connectionString =
                ConfigurationManager.ConnectionStrings["PhotogramEntities"].ConnectionString;

            kernel.Bind<DbContext>().
                ToSelf().
                InSingletonScope().
                WithConstructorArgument("nameOrConnectionString", connectionString);

            #endregion configuration via sourcecode

            return kernel;
        }

        /// <summary>
        /// Configures the Ninject kernel from an external module file.
        /// </summary>
        /// <param name="moduleFilename">The module filename.</param>
        /// <returns>The NInject kernel</returns>
        public static IKernel ConfigureNInjectKernel(string moduleFilename)
        {
            IKernel kernel = new StandardKernel();
            kernel.Load(moduleFilename);

            return kernel;
        }

        public static void ClearNInjectKernel(IKernel kernel)
        {
            kernel.Dispose();
        }
    }
}
