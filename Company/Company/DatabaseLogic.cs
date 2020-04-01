using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Company
{
    class DatabaseLogic
    {
        private const string CONNECTION_STRING = "Server=DESKTOP-986JRQ9;Database=Company;Trusted_Connection=True;";

        public void insertBug(Bug bug)
        {
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = "INSERT INTO [dbo].[Product]([ID], [ProductID],[Description], [Status], [Priority], [FoundByID], [AssignedToID])  " +
                        "Values (@ID,@ProductID, @Description,  @Status, @Priority, @FoundByID, @AssignedToID)";
                    command.Parameters.AddWithValue("@ID", bug.ID);
                    command.Parameters.AddWithValue("@ProductID", bug.ProductID);
                    command.Parameters.AddWithValue("@Description", bug.Description);
                    command.Parameters.AddWithValue("@Status", bug.Status);
                    command.Parameters.AddWithValue("@Priority", bug.Priority);
                    command.Parameters.AddWithValue("@FoundByID", bug.FoundByID);
                    command.Parameters.AddWithValue("@AssignedToID", bug.AssignedToID);

                    command.ExecuteNonQuery();
                }
            }
        }

        public Bug readBygByID(int id)
        {

            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = "SELECT *FROM [dbo].[Product] Where [ID]=@ID";
                    command.Parameters.AddWithValue("@ID", id);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();
                        int ID = (int)reader["ID"];
                        int ProductID = (int)reader["ProductID"];
                        string Description = (string)reader["Description"];
                        string Status = (string)reader["Status"];
                        string Priority = (string)reader["Priority"];
                        int FoundByID = (int)reader["FoundByID"];
                        int AssignedToID = (int)reader["AssignedToID"];

                        return new Bug(ID, ProductID, Description, Status, Priority, FoundByID, AssignedToID);
                    }

                }
            }

            return null;
        }

    }
}
