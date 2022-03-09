using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MfeConfigurations.Application.Create
{
    public sealed class MfeConfigurationCreator
    {
        public void Execute (MfeConfigurationRequest configuration)
        {
            Console.WriteLine($"Execute repository or coordinate repositories");
            Console.WriteLine($"Configurations to add", configuration.Configurations);
        }
    }
}
