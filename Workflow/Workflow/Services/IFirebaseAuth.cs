using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;

namespace Workflow.Services
{
    public interface IFirebaseAuth
    {
        Task<string> Authenticate();
    }
}
