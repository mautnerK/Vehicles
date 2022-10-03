using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicles.Model.Common;
using Vehicles.Model;
using Vehicles.Common;

namespace Vehicles.Repository.Common
{
    public interface IMakeRepository
    {
        Task<List<IMake>> GetAllMakeAsync(Sorting sort, Paging page, Filtering filter);
        Task<IMake> GetMakeByIdAsync(int id);
        Task RemoveMakeAsync(int id);
        Task SaveNewMakeAsync(IMake vehicle2Make);
        Task UpdateMakeAsync(int id, string name, string abrv);
    }
}
