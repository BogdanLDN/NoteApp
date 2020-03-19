using Microsoft.EntityFrameworkCore;
using NoteApp.Data;
using ShopingSelfService.UI.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NoteApp.ViewModels
{
    public class NoteViewVM : BaseViewModel
    {
        public NoteViewVM()
        {
            notes = new ObservableCollection<NoteEntity>(Task.Run(async () => await GetNotes()).Result);
            Notes = notes;
        }

        public async Task<List<NoteEntity>> GetNotes()
        {
            List<NoteEntity> list = new List<NoteEntity>();
            using (var context = new NoteContext())
            {
                list = await context.Notes.ToListAsync();
            }
            return list;
        }
        public NoteEntity SelectedNote
        {
            get { return selectedNote; }
            set
            {
                selectedNote = value;
                OnPropertyChanged(nameof(SelectedNote));
            }
        }
        private NoteEntity selectedNote;

        public ObservableCollection<NoteEntity> Notes { get; set; }

        ObservableCollection<NoteEntity> notes;

        public ICommand RemoveCommand
        {
            get
            {
                return removeCommand ??
                  (removeCommand = new RelayCommand(obj =>
                  {
                      NoteEntity note = obj as NoteEntity;
                      if (note != null)
                      {
                          Notes.Remove(note);
                          Task.Run(async () => await RemoveEntityAsync(note));
                      }
                  },
                 (obj) => Notes.Count > 0));
            }
        }
        private ICommand removeCommand;

        public ICommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand(obj =>
                  {
                      NoteEntity note = new NoteEntity();
                      Notes.Insert(0, note);
                      SelectedNote = note;
                      Task.Run(async () => await AddEntityAsync(note));
                  }));
            }
        }
        private ICommand addCommand;
        public ICommand SaveChangesCommand
        {
            get
            {
                return saveChangesCommand ??
                  (saveChangesCommand = new RelayCommand(obj =>
                  {
                      NoteEntity note = obj as NoteEntity;
                      if (note == null)
                      {
                          Task.Run(() => MessageBox.Show("Редагування доступне для існуючих записів, додайте новий запис."));
                      }
                      SelectedNote = note;
                      Task.Run(async () => await SaveChangesAsync(note));
                  }));
            }
        }

        private ICommand saveChangesCommand;
        private async Task RemoveEntityAsync(NoteEntity note)
        {
            using (var context = new NoteContext())
            {
                context.Notes.Remove(note);
                await context.SaveChangesAsync();
            }
        }
        private async Task AddEntityAsync(NoteEntity note)
        {
            using (var context = new NoteContext())
            {
                context.Notes.Add(note);
                await context.SaveChangesAsync();
            }
        }
        private async Task SaveChangesAsync(NoteEntity note)
        {
            using (var context = new NoteContext())
            {
                context.Notes.Update(selectedNote);
                await context.SaveChangesAsync();
            }
        }

    }
}
