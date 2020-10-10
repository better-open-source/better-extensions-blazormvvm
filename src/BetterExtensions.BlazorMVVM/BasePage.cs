using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace BetterExtensions.BlazorMVVM
{
    public class BasePage : ComponentBase, IDisposable
    {
        private BaseViewModel _baseViewModel;
        protected BaseViewModel BaseViewModel
        {
            get => _baseViewModel ??= new BaseViewModel();
            set => _baseViewModel = value;
        }

        public void Dispose()
        {
            BaseViewModel?.Dispose();
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            if (BaseViewModel is null) 
                return;

            BaseViewModel.PropertyChanged += (obj, args) => StateHasChanged();

            await BaseViewModel.StartLoadDataAsync();
        }
    }

    public class BasePage<T> : BasePage 
        where T : BaseViewModel
    {
        [Inject]
        public T ViewModel
        {
            get => BaseViewModel as T;
            set => BaseViewModel = value;
        }
    }
}
