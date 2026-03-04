using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Entities;
using Microsoft.EntityFrameworkCore;
using static System.Environment;

namespace Api.Database
{
    public class SqliteContext : DbContext
    {
        public string DbPath { get; }

        public SqliteContext(DbContextOptions<SqliteContext> options)
        : base(options)
        {
            SpecialFolder folder = Environment.SpecialFolder.LocalApplicationData;
            string path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "database.sqlite");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");

        public DbSet<BookEntity> Books => Set<BookEntity>();
    }
}