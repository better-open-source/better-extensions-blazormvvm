using System;
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

        protected override void OnInitialized()
        {
            base.OnInitialized();

            if (BaseViewModel is null)
                return;

            BaseViewModel.PropertyChanged += (obj, args) => StateHasChanged();
            BaseViewModel.StartLoadData();
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
