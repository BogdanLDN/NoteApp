using System.Windows;
using System.Windows.Input;
using NoteApp.ViewModels;
using ShopingSelfService.UI.Helpers;

namespace NoteApp.ViewModels
{
    public class MainViewVM : BaseViewModel
    {
        public ICommand CloseWindowCommand
        {
            get
            {
                return closeWindowCommand ??
                  (closeWindowCommand = new RelayCommand(obj =>
                  {
                      Application.Current.Shutdown();
                  }));
            }
        }
        private RelayCommand closeWindowCommand;
        public ICommand DragMoveWindowCommand
        {
            get
            {
                return dragMoveWindowCommand ??
                  (dragMoveWindowCommand = new RelayCommand(obj =>
                  {
                      (obj as Window).DragMove();
                  }));
            }
        }
        private RelayCommand dragMoveWindowCommand;

    }


}
