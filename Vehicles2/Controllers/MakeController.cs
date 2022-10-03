using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Vehicles.Model.Common;
using Vehicles.Common;
using Vehicles.Service.Common;
using Vehicles.Service;
using AutoMapper;
using Vehicles.Models;
using Vehicles.Model;

namespace Vehicles.Controllers
{
    public class MakeController : ApiController
    {
        private readonly IMapper mapper;
        protected IMakeService makeService { get; private set; }
        public MakeController(IMakeService iMakeService, IMapper imapper)
        {
            makeService = iMakeService;
            mapper = imapper;
        }

        public Sorting sort = new Sorting();
        public Paging page = new Paging();
        public Filtering filter = new Filtering();

        [HttpGet]
        public async Task<HttpResponseMessage> GetAllMakeAsync(string sortBy = "", string orderBy = "", int itemsPerPage = 0, int pageNumber = 0, string Name = "")
        {
            if (sortBy != "" || orderBy != "")
            {
                sort = new Sorting();
                sort.OrderBy = orderBy;
                sort.SortOrder = sortBy;
            }


            if (pageNumber != 0 || itemsPerPage != 0)
            {
                page = new Paging();
                page.ItemsPerPage = itemsPerPage;
                page.PageNumber = pageNumber;
            }


            if (Name != "")
            {
                filter.MakeName = Name;
            }
            List<IMake> MakeList = await makeService.GetAllMakeAsync(sort, page, filter);
            if (MakeList != null)
            {
                List<MakeViewModel> MakeViewModellist = new List<MakeViewModel>();
                foreach (IMake vehicle in MakeList)
                {
                    var vehicleViewModel = mapper.Map<MakeViewModel>(vehicle);
                    MakeViewModellist.Add(vehicleViewModel);
                }
                return Request.CreateResponse(HttpStatusCode.OK, MakeViewModellist);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No VehicleMakes found");
            }
        }
        [HttpGet]
        public async Task<HttpResponseMessage> GetMakeByIdAsync(int id)
        {
            IMake vehicle = await makeService.GetMakeByIdAsync(id);
            if (vehicle.Name != null)
            {
                var vehicleViewModel = mapper.Map<MakeViewModel>(vehicle);
                return Request.CreateResponse(HttpStatusCode.OK, vehicleViewModel);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No vehicle make with id=" + id);
            }
        }
        [HttpPut]
        public async Task<HttpResponseMessage> UpdateMakeAsync(int id, string name, string abrv)
        {
            IMake vehicle = await makeService.GetMakeByIdAsync(id);
            if (vehicle.Name == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No vehicle make with id=" + id);
            }
            else
            {
                await makeService.UpdateMakeAsync(id, name, abrv);
                return Request.CreateResponse(HttpStatusCode.OK, "Vehicle make updated");
            }
        }
        [HttpPost]
        public async Task<HttpResponseMessage> SaveNewMakeAsync([FromBody] MakeViewModel MakeViewModel)
        {
            IMake vehicle = new Make();
            vehicle = mapper.Map<IMake>(MakeViewModel);
            await makeService.SaveNewMakeAsync(vehicle);
            return Request.CreateResponse(HttpStatusCode.OK, "Vehicle make added");
        }
    }
}
