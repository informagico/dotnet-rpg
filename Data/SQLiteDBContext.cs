using dotnet_rpg.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Data
{
	public class SQLiteDBContext : DbContext
	{
		// protected override void OnConfiguring(DbContextOptionsBuilder options)
		// 	=> options.UseSqlite("Data Source=sqlitedemo.db");

		public SQLiteDBContext(DbContextOptions<SQLiteDBContext> options) : base(options) { }

		public DbSet<Character> Characters { get; set; }
	}
}
