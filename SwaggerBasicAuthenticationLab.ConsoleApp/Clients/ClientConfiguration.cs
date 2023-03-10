using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwaggerBasicAuthenticationLab.ConsoleApp.Clients
{
    public class ClientConfiguration
    {
    }

    public class BusyControl
    {
        private bool isBusy;

        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                Console.WriteLine(value);
                isBusy = value;
            }
        }

    }
}
