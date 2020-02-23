using dotnet_rpg.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Data
{
	public class SQLServerDBContext : DbContext
	{
		public SQLServerDBContext(DbContextOptions<SQLServerDBContext> options) : base(options) { }
		public DbSet<Character> Characters { get; set; }
	}
}
