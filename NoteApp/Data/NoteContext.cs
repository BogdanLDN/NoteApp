using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Data
{
   public class NoteContext : DbContext
    {
        public NoteContext()
        {
            Database.EnsureCreated();
        }
        public DbSet<NoteEntity> Notes { get; set; }
      
        protected override void OnConfiguring(DbContextOptionsBuilder options)
          => options.UseSqlite("Data Source=NoteDb.db");
    }
}
