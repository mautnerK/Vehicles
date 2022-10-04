
using Vehicles.Model.Common;

namespace Vehicles.Model
{
    public class Model : IModel
    {
        public int ID { get; set; }
        public int MakeID { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }
    }
}
