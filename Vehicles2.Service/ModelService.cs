using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicles.Model.Common;
using Vehicles.Model;
using Vehicles.Repository.Common;
using Vehicles.Service.Common;
using Vehicles.Common;

namespace Vehicles.Service
{
    public class ModelService : IModelService
    {
        protected IModelRepository ModelRepository { get; private set; }
        public ModelService(IModelRepository repository) { ModelRepository = repository; }
        public async Task<List<IModel>> GetAllModelAsync(Sorting sort, Paging page, Filtering filter)
        {
            return await ModelRepository.GetAllModelAsync(sort,  page,  filter);
        }

        public async Task<IModel> GetModelByIdAsync(int id)
        {
            return await ModelRepository.GetModelByIdAsync(id);
        }

        public async Task RemoveModelAsync(int id)
        {
            await ModelRepository.RemoveModelAsync(id);
        }

        public async Task SaveNewModelAsync(IModel vehicle2model)
        {
            await ModelRepository.SaveNewModelAsync(vehicle2model);

        }

        public async Task UpdateModelAsync(int id, string name, string abrv)
        {
            await ModelRepository.UpdateModelAsync(id, name, abrv);
        }
    }
}
