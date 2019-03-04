using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ProxyExample
{
    public interface IResourceAccessor
    {
        int AccessResource();
    }

    public class RealResourceAccessor : IResourceAccessor
    {
        public int AccessResource() => 42;
    }

    public class ResourceConsumer
    {
        private readonly IResourceAccessor resourceAccessor;

        public ResourceConsumer(IResourceAccessor resourceAccessor) => this.resourceAccessor = resourceAccessor;

        public void DoSomethingAwesome()
        {
            int resource = resourceAccessor.AccessResource();

            for (int i = 0; i < resource; i++)
            {
                Console.WriteLine("Awesome");
            }
        }
    }

    // proxy object
    public class RestrictedResourceAccessor : IResourceAccessor
    {
        private readonly IResourceAccessor _resourceAccessor;

        public RestrictedResourceAccessor(IResourceAccessor resourceAccessor)
        {
            _resourceAccessor = resourceAccessor;
        }

        public int AccessResource()
        {
            Console.WriteLine("Pw: ");
            string pw = Console.ReadLine();
            if (pw == "123")
            {
                return _resourceAccessor.AccessResource();
            }
            else
            {
                throw new UnauthorizedAccessException();
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var rc = new ResourceConsumer(new RestrictedResourceAccessor(new RealResourceAccessor()));
            rc.DoSomethingAwesome();
            Console.ReadKey();
        }
    }
}
