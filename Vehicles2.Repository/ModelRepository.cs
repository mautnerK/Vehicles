using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicles.Model.Common;
using Vehicles.Model;
using Vehicles.Repository.Common;
using Vehicles.Common;

namespace Vehicles.Repository
{
    public class ModelRepository : IModelRepository
    {
        SqlConnection connection = new SqlConnection("Server = .; Database = Vehicles; Trusted_Connection = True;");
        SqlCommand cmd = null;
        StringBuilder commandString = new StringBuilder("Select * from VehicleModel WHERE 1=1");



        public async Task<List<IModel>> GetAllModelAsync(Sorting sort, Paging page, Filtering filter)
        {

            List<IModel> ModelList = new List<IModel>();
            
             await AddFilter(filter, commandString);
            await AddSorting(sort, commandString);
            await AddPager(sort, page, commandString);
             
            cmd = new SqlCommand(commandString.ToString(), connection);
            connection.Open();
            using (SqlDataReader oReader = await cmd.ExecuteReaderAsync())
            {
                while (oReader.Read())
                {
                    IModel Model = new Model.Model();
                    Model.ID = int.Parse(oReader["Id"].ToString());
                    Model.Name = oReader["Name"].ToString();
                    Model.Abbreviation = oReader["Abrv"].ToString();
                    ModelList.Add(Model);
                }
            }
            connection.Close();
            return ModelList;
        }
        public async Task<IModel> GetModelByIdAsync(int id)
        {

            cmd = new SqlCommand("Select * from VehicleModel WHERE ID=" + id, connection);
            IModel Model = new Model.Model();
            connection.Open();
            using (SqlDataReader oReader = await cmd.ExecuteReaderAsync())
            {
                while (oReader.Read())
                {
                    Model.ID = int.Parse(oReader["Id"].ToString());
                    Model.Name = oReader["Name"].ToString();
                    Model.Abbreviation = oReader["Abrv"].ToString();
                }
            }
            connection.Close();
            return Model;
        }

        public async Task RemoveModelAsync(int id)
        {
            connection.Open();
            cmd = new SqlCommand("DELETE FROM VehicleModel WHERE ID=" + id + ";", connection);
            await cmd.ExecuteNonQueryAsync();
            connection.Close();
        }
        public async Task SaveNewModelAsync(IModel vehicle2Model)
        {
            string commandtext = @"INSERT INTO VehicleModel (Name, Abrv) VALUES(@Name, @Abbreviation)";
            using (connection)
            {
                cmd = new SqlCommand(commandtext, connection);
                cmd.Parameters.AddWithValue("@Name", vehicle2Model.Name);
                cmd.Parameters.AddWithValue("@Abbreviation", vehicle2Model.Abbreviation);
                connection.Open();
                await cmd.ExecuteNonQueryAsync();
                connection.Close();
            }
        }
        public async Task UpdateModelAsync(int id, int madeId, string name, string abrv)
        {
            if (name != "")
            {
                cmd = new SqlCommand("UPDATE VehicleModel Set Name='" + name + "' WHERE ID='" + id + "'", connection);
                await cmd.ExecuteNonQueryAsync();
            }
            if (abrv != "")
            {
                cmd = new SqlCommand("UPDATE VehicleModel Set Abbreviation='" + abrv + "' WHERE ID='" + id + "'", connection);
                await cmd.ExecuteNonQueryAsync();
            }
            connection.Close();
        }
        private async Task<StringBuilder> AddFilter(Filtering filter, StringBuilder commandString)
        {

            if (!string.IsNullOrWhiteSpace(filter.MakeName))
            {
                commandString.Append($" AND (Name LIKE '%{filter.MakeName}%')");
            }
            return await Task.FromResult(commandString);
        }
        private async Task<StringBuilder> AddSorting(Sorting sorting, StringBuilder queryString)
        {
            if (!string.IsNullOrWhiteSpace(sorting.OrderBy))
            {
                queryString.Append($" ORDER BY '{sorting.OrderBy}' {sorting.SortOrder}");
            }
            return await Task.FromResult(queryString);
        }
        private async Task<StringBuilder> AddPager(Sorting sorting, Paging pager, StringBuilder queryString)
        {
            if (!string.IsNullOrWhiteSpace(sorting.OrderBy) && pager.PageNumber != 0 && pager.ItemsPerPage != 0)
            {
                queryString.Append($" OFFSET {(pager.PageNumber - 1) * pager.ItemsPerPage} ROWS FETCH NEXT {pager.ItemsPerPage} ROWS ONLY;");
            }
            return await Task.FromResult(queryString);
        }
    }
}
