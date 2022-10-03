
using Vehicles.Model.Common;

namespace Vehicles.Model
{
    public class Model : IModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public int VehicleMadeID { get; set; }
    }
}
