using System;
using System.Threading;
using System.Threading.Tasks;

namespace BetterExtensions.BlazorMVVM
{
    public class BaseViewModel : Bindable, IDisposable
    {
        private readonly CancellationTokenSource _networkTokenSource = new CancellationTokenSource();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~BaseViewModel()
        {
            Dispose(false);
        }

        public CancellationToken CancellationToken => 
            _networkTokenSource?.Token ?? CancellationToken.None;

        public PageState State
        {
            get => Get(PageState.Clean);
            set => Set(value);
        }

        public bool IsLoadDataStarted
        {
            get => Get<bool>();
            protected internal set => Set(value);
        }

        public async Task StartLoadDataAsync()
        {
            if (IsLoadDataStarted) return;
            IsLoadDataStarted = true;

            await LoadDataAsync(CancellationToken);
        }

        //override this method for load data
        protected virtual Task LoadDataAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }

        protected virtual void Dispose(bool disposing)
        {
            CancelNetworkRequests();
        }

        public void CancelNetworkRequests()
        {
            _networkTokenSource.Cancel();
        }
    }
}
