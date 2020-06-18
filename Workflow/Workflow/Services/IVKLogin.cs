using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Workflow.Models;

namespace Workflow.Services
{
    public interface IVKLogin
    {
        Task<UserAuthModel> LoginViaVK();
    }
}
