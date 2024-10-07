using Microsoft.EntityFrameworkCore;

namespace MoodSensingServices.Infrastructure.Context
{
    public partial class MSAContext : DbContext
    {
        public MSAContext()
        {
        }

        public MSAContext(DbContextOptions<MSAContext> options) : base(options)
        {
        }
    }
}
