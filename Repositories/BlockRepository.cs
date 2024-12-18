using Microsoft.Data.SqlClient;
using StudentManagementSystem.Helper;
using StudentManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.Repositories
{
    public interface IBlockRepository
    {
        IAsyncEnumerable<Block> GetBlocksHandledByUser();
        IAsyncEnumerable<Block> GetAllBlocks();
    }

    public class BlockRepository : DatabaseConnection, IBlockRepository
    {
        public async IAsyncEnumerable<Block> GetBlocksHandledByUser()
        {
            using (var conn = SqlConn())
            {
                conn.Open();
                string query = "select distinct b.name, s.block_id from studentGrades as sg" +
                    "\n join students as s on s.id = sg.student_id" +
                    "\n join blocks as b on b.id = s.block_id" +
                    "\n where sg.user_id = @id;";
                using(var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("id", Auth.CurrentUser.Id);

                    using(var reader = await cmd.ExecuteReaderAsync(System.Data.CommandBehavior.CloseConnection))
                    {
                        while(await reader.ReadAsync())
                        {
                            yield return new Block
                            {
                                Name = reader.GetString(0),
                                Id = reader.GetInt32(1)
                            };
                        }
                    }
                }
            }
        }

        public async IAsyncEnumerable<Block> GetAllBlocks()
        {
            using (var conn = SqlConn())
            {
                conn.Open();
                string query = "select name, id from blocks;";
                using (var cmd = new SqlCommand(query, conn))
                {
                    using (var reader = await cmd.ExecuteReaderAsync(System.Data.CommandBehavior.CloseConnection))
                    {
                        while(await reader.ReadAsync())
                        {
                            yield return new Block
                            {
                                Name = reader.GetString(0),
                                Id = reader.GetInt32(1)
                            };
                        }
                    }
                }
            }
        }
    }
}
