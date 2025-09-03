using Microsoft.Data.SqlClient;
using mvcrud.Models;

namespace mvcrud.Services
{
    public class StudentRepository
    {
        private readonly string _cs;

        public StudentRepository(IConfiguration config)
        {
            _cs = config.GetConnectionString("DefaultConnection")!;
        }

        public async Task<List<Student>> GetAllAsync()
        {
            var list = new List<Student>();
            using var conn = new SqlConnection(_cs);
            using var cmd = new SqlCommand("SELECT Id, Name, Age, Grade FROM Students", conn);
            await conn.OpenAsync();
            using var r = await cmd.ExecuteReaderAsync();
            while (await r.ReadAsync())
            {
                list.Add(new Student
                {
                    Id = r.GetInt32(0),
                    Name = r.GetString(1),
                    Age = r.GetInt32(2),
                    Grade = r.IsDBNull(3) ? null : r.GetString(3)
                });
            }
            return list;
        }

        public async Task<Student?> GetByIdAsync(int id)
        {
            using var conn = new SqlConnection(_cs);
            using var cmd = new SqlCommand("SELECT Id, Name, Age, Grade FROM Students WHERE Id=@Id", conn);
            cmd.Parameters.AddWithValue("@Id", id);
            await conn.OpenAsync();
            using var r = await cmd.ExecuteReaderAsync();
            if (await r.ReadAsync())
            {
                return new Student
                {
                    Id = r.GetInt32(0),
                    Name = r.GetString(1),
                    Age = r.GetInt32(2),
                    Grade = r.IsDBNull(3) ? null : r.GetString(3)
                };
            }
            return null;
        }

        public async Task<int> CreateAsync(Student s)
        {
            using var conn = new SqlConnection(_cs);
            using var cmd = new SqlCommand(
                "INSERT INTO Students (Name, Age, Grade) VALUES (@Name, @Age, @Grade); SELECT SCOPE_IDENTITY();",
                conn);
            cmd.Parameters.AddWithValue("@Name", s.Name);
            cmd.Parameters.AddWithValue("@Age", s.Age);
            cmd.Parameters.AddWithValue("@Grade", (object?)s.Grade ?? DBNull.Value);
            await conn.OpenAsync();
            var result = await cmd.ExecuteScalarAsync();
            return Convert.ToInt32(result);
        }

        public async Task UpdateAsync(Student s)
        {
            using var conn = new SqlConnection(_cs);
            using var cmd = new SqlCommand(
                "UPDATE Students SET Name=@Name, Age=@Age, Grade=@Grade WHERE Id=@Id", conn);
            cmd.Parameters.AddWithValue("@Id", s.Id);
            cmd.Parameters.AddWithValue("@Name", s.Name);
            cmd.Parameters.AddWithValue("@Age", s.Age);
            cmd.Parameters.AddWithValue("@Grade", (object?)s.Grade ?? DBNull.Value);
            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync(int id)
        {
            using var conn = new SqlConnection(_cs);
            using var cmd = new SqlCommand("DELETE FROM Students WHERE Id=@Id", conn);
            cmd.Parameters.AddWithValue("@Id", id);
            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }
    }
}
