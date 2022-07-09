using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterApp.DataService.Services.Abstractions
{
    public interface IReportService
    {
        Task ReadMessages();
    }

}
