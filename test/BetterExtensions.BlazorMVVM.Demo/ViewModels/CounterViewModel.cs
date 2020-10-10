namespace BetterExtensions.BlazorMVVM.Demo.ViewModels
{
    public class CounterViewModel : BaseViewModel
    {
        public int CurrentCount
        {
            get => Get<int>();
            set => Set(value);
        }

        public void IncrementCounter()
        {
            CurrentCount += 1;
        }
    }
}