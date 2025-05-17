using Microsoft.EntityFrameworkCore;

namespace Hospital_Management.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions options): base(options)
        {

        }

    }
}
