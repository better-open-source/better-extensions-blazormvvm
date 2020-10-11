using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using BetterExtensions.BlazorMVVM.Demo.Models;

namespace BetterExtensions.BlazorMVVM.Demo.ViewModels
{
    public class FetchDataViewModel : BaseViewModel
    {
        private readonly HttpClient _httpClient;

        public FetchDataViewModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public ObservableCollection<WeatherForecast> Forecasts
        {
            get => Get<ObservableCollection<WeatherForecast>>();
            private set => Set(value);
        }

        protected override async Task LoadDataAsync()
        {
            State = PageState.Loading;

            var forecasts = await FetchForecastsData();

            IsLoadDataStarted = false;

            Forecasts = new ObservableCollection<WeatherForecast>(forecasts);

            State = Forecasts.Any()
                ? PageState.Normal
                : PageState.NoData;
        }

        private async Task<WeatherForecast[]> FetchForecastsData()
        {
            const string requestUri = "sample-data/weather.json";
            var result = await _httpClient.GetFromJsonAsync<WeatherForecast[]>(requestUri);
            return result;
        }
    }
}