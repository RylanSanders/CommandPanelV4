using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace CommandPanelV4.Util
{
    public class SvcUtil
    {
        public static bool IsServiceInstalled(string serviceName)
        {
            try
            {
                using (ServiceController serviceController = new ServiceController(serviceName))
                {
                    // Attempt to access the Status property to validate the service existence
                    ServiceControllerStatus status = serviceController.Status;
                    return true;
                }
            }
            catch (InvalidOperationException)
            {
                // This exception is thrown if the service is not found
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }
    }
}
