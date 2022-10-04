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
    public interface IModelRepository
    {
        Task<List<IModel>> GetAllModelAsync(Sorting sort, Paging page, Filtering filter);
        Task<IModel> GetModelByIdAsync(int id);
        Task RemoveModelAsync(int id);
        Task SaveNewModelAsync(IModel vehiclesModel);
        Task UpdateModelAsync(int id, string name, string abrv);
    }
}
