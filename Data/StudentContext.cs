using Microsoft.EntityFrameworkCore;
using WebApplication1.Models.Entities;

namespace WebApplication1.Data
{
    public class StudentContext: DbContext
    {
        public StudentContext(DbContextOptions<StudentContext> options): base(options)
        {
        }

        public DbSet<BStudent> BSutends { get; set; }
    }
}
