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
    public class MakeRepository : IMakeRepository
    {
        SqlConnection connection = new SqlConnection("Server = .; Database = Vehicles; Trusted_Connection = True;");
        SqlCommand cmd = null;
        StringBuilder commandString = new StringBuilder("Select * from VehicleMake WHERE 1=1");



        public async Task<List<IMake>> GetAllMakeAsync(Sorting sort, Paging page, Filtering filter) {

            List<IMake> MakeList = new List<IMake>();
            
            await AddFilter(filter, commandString);
            await AddSorting(sort, commandString);
            await AddPager(sort, page, commandString);
             
            cmd = new SqlCommand(commandString.ToString(), connection);
            connection.Open();
            using (SqlDataReader oReader = await cmd.ExecuteReaderAsync())
            {
                while (oReader.Read())
                {
                    IMake Make = new Make(); 
                    Make.ID = int.Parse(oReader["Id"].ToString());
                    Make.Name = oReader["Name"].ToString();
                    Make.Abbreviation = oReader["Abrv"].ToString();
                    MakeList.Add(Make);
                }
            }
            connection.Close();
            return MakeList;
        }
        public async Task<IMake> GetMakeByIdAsync(int id) {

            cmd = new SqlCommand("Select * from VehicleMake WHERE ID=" + id, connection);
            IMake Make = new Make();
            connection.Open();
            using (SqlDataReader oReader = await cmd.ExecuteReaderAsync())
            {
                while (oReader.Read())
                {
                    Make.ID = int.Parse(oReader["Id"].ToString());
                    Make.Name = oReader["Name"].ToString();
                    Make.Abbreviation = oReader["Abrv"].ToString();
                }
            }
            connection.Close();
            return Make;
        }

        public async Task RemoveMakeAsync(int id) {
            connection.Open();
            cmd = new SqlCommand("DELETE FROM VehicleMake WHERE ID=" + id + ";", connection);
            await cmd.ExecuteNonQueryAsync();
            connection.Close();
    }
        public async Task SaveNewMakeAsync(IMake vehicle2Make)
        {
            string commandtext = @"INSERT INTO VehicleMake (Name, Abrv) VALUES(@Name, @Abbreviation)";
            using (connection)
            {
                cmd = new SqlCommand(commandtext, connection);
                cmd.Parameters.AddWithValue("@Name", vehicle2Make.Name);
                cmd.Parameters.AddWithValue("@Abbreviation", vehicle2Make.Abbreviation);
                connection.Open();
                await cmd.ExecuteNonQueryAsync();
                connection.Close();
            }
        }
        public async Task UpdateMakeAsync(int id, string name, string abrv) {
            if (name != "") {
                cmd = new SqlCommand("UPDATE VehicleMake Set Name='" + name + "' WHERE ID='" + id + "'", connection);
                await cmd.ExecuteNonQueryAsync();
            }
            if (abrv != "")
            {
                cmd = new SqlCommand("UPDATE VehicleMake Set Abbreviation='" + abrv + "' WHERE ID='" + id + "'", connection);
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
