using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Data
{
  public static class DataLoader 
    {

        public static async Task RemoveEntityAsync(NoteEntity note)
        {
            using (var context = new NoteContext())
            {
                context.Notes.Remove(note);
                await context.SaveChangesAsync();
            }
        }
        public static async Task AddEntityAsync(NoteEntity note)
        {
            using (var context = new NoteContext())
            {
                context.Notes.Add(note);
                await context.SaveChangesAsync();
            }
        }
        public static async Task SaveChangesAsync(NoteEntity selectedNote)
        {
            using (var context = new NoteContext())
            {
                context.Notes.Update(selectedNote);
                await context.SaveChangesAsync();
            }
        }
    }
}
