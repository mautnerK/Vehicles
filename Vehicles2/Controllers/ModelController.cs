using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Vehicles.Common;
using Vehicles.Model.Common;
using Vehicles.Models;
using Vehicles.Service.Common;

namespace Vehicles.Controllers
{
    public class ModelController : ApiController
    {
        private readonly IMapper mapper;
        protected IModelService modelService { get; private set; }
        public ModelController(IModelService iModelService, IMapper imapper)
        {
            modelService = iModelService;
            mapper = imapper;
        }

        public Sorting sort = new Sorting();
        public Paging page = new Paging();
        public Filtering filter = new Filtering();

        [HttpGet]
        public async Task<HttpResponseMessage> GetAllModelAsync(string sortBy = "", string orderBy = "", int itemsPerPage = 0, int pageNumber = 0, string Name = "")
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
            List<IModel> modelList = await modelService.GetAllModelAsync(sort, page, filter);
            if (modelList != null)
            {
                List<ModelViewModel> modelViewModellist = new List<ModelViewModel>();
                foreach (IMake vehicle in modelList)
                {
                    var vehicleViewModel = mapper.Map<ModelViewModel>(vehicle);
                    modelViewModellist.Add(vehicleViewModel);
                }
                return Request.CreateResponse(HttpStatusCode.OK, modelViewModellist);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No VehicleMakes found");
            }
        }
        [HttpGet]
        public async Task<HttpResponseMessage> GetModelByIdAsync(int id)
        {
            IModel vehicle = await modelService.GetModelByIdAsync(id);
            if (vehicle.Name != null)
            {
                var vehicleViewModel = mapper.Map<ModelViewModel>(vehicle);
                return Request.CreateResponse(HttpStatusCode.OK, vehicleViewModel);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No vehicle make with id=" + id);
            }
        }
        [HttpPut]
        public async Task<HttpResponseMessage> UpdateModelAsync(int id, int madeId, string name, string abrv)
        {
            IModel vehicle = await modelService.GetModelByIdAsync(id);
            if (vehicle.Name == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No vehicle make with id=" + id);
            }
            else
            {
                await modelService.UpdateModelAsync(id, madeId, name, abrv);
                return Request.CreateResponse(HttpStatusCode.OK, "Vehicle make updated");
            }
        }
        [HttpPost]
        public async Task<HttpResponseMessage> SaveNewModelAsync([FromBody] ModelViewModel vehicle2MakeViewModel)
        {
            IModel vehicle = new Model.Model();
            vehicle = mapper.Map<IModel>(vehicle2MakeViewModel);
            await modelService.SaveNewModelAsync(vehicle);
            return Request.CreateResponse(HttpStatusCode.OK, "Vehicle model added");
        }
    }
}
