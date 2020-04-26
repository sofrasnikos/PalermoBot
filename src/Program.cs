using System.Threading.Tasks;

namespace PalermoBot
{
    class Program
    {
        public static Task Main(string[] args)
            => Startup.RunAsync(args);
    }
}
