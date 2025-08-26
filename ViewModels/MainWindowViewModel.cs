using System.ComponentModel;

namespace SqlHelper.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public MainWindowViewModel()
        {
            OnLoaded();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnLoaded()
        {
            // aqui quero carregar os dados iniciais
        }
    }
}
