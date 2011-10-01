using Buzz.Specs.Discovery.Setup.Ninject;

namespace Buzz.Specs.Discovery.Setup
{
    public static class Using
    {
         public static ISetup Ninject()
         {
             return new Configuration();
         }
    }
}