using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicles.Model.Common;
using Vehicles.Common;
using Vehicles.Model;

namespace Vehicles.Service.Common
{
    public interface IMakeService
    {
        Task<List<IMake>> GetAllMakeAsync(Sorting sort, Paging page, Filtering filter);
        Task<IMake> GetMakeByIdAsync(int id);
        Task RemoveMakeAsync(int id);
        Task SaveNewMakeAsync(IMake Make);
        Task UpdateMakeAsync(int id, string name, string abrv);
    }
}
