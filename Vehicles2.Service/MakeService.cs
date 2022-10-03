using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicles.Model.Common;
using Vehicles.Common;
using Vehicles.Repository.Common;
using Vehicles.Service.Common;

namespace Vehicles.Service
{
    public class MakeService : IMakeService
    {
        protected IMakeRepository MakeRepository { get; private set; }
        public MakeService(IMakeRepository repository) { MakeRepository = repository; }
        public async Task<List<IMake>> GetAllMakeAsync(Sorting sort, Paging page, Filtering filter)
        {
            return await MakeRepository.GetAllMakeAsync(sort, page, filter);
        }

        public async Task<IMake> GetMakeByIdAsync(int id)
        {
            return await MakeRepository.GetMakeByIdAsync(id);
        }

        public async Task RemoveMakeAsync(int id)
        {
             await MakeRepository.RemoveMakeAsync(id);
        }

        public async Task SaveNewMakeAsync(IMake vehicle2make)
        {
            await MakeRepository.SaveNewMakeAsync(vehicle2make);

        }

        public async Task UpdateMakeAsync(int id, string name, string abrv)
        {
            await MakeRepository.UpdateMakeAsync(id, name, abrv);
        }
    }
}
