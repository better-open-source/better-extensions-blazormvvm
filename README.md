<p align="center">
    <img width="100px" src="https://github.com/itkerry/better-extensions-blazormvvm/raw/main/icon.png" align="center" />
    <h1 align="center">BetterExtensions | Blazor MVVM</h1>
    <p align="center">Implementation of MVVM pattern for Blazor.</p>
</p>
<p align="center">
    <a href="https://github.com/itkerry/better-extensions-blazormvvm/blob/master/LICENSE">
        <img alt="license" src="https://img.shields.io/github/license/mashape/apistatus.svg" />
    </a>
    <a href="https://www.nuget.org/packages/BetterExtensions.BlazorMVVM/">
        <img alt="nuget-version" src="https://img.shields.io/nuget/v/BetterExtensions.BlazorMVVM.svg" />
    </a>
    <a href="https://www.nuget.org/packages/BetterExtensions.BlazorMVVM/">
        <img alt="nuget-downloads" src="https://img.shields.io/nuget/dt/BetterExtensions.BlazorMVVM.svg" />
    </a>
</p>

## Status
| Branch | Build & Test |
|---|:---|
|**master**|[![Build & Test][build-master-badge]][build]| 
|**develop**|[![Build & Test][build-develop-badge]][build]|

[build-master-badge]: https://dev.azure.com/better-open-source/better-extensions/_apis/build/status/BetterExtensions.BlazorMVVM?branchName=main
[build-develop-badge]: https://dev.azure.com/better-open-source/better-extensions/_apis/build/status/BetterExtensions.BlazorMVVM?branchName=develop
[build]: https://dev.azure.com/better-open-source/better-extensions/_build?definitionId=7

## Installation
Latest version in here:  [![NuGet](https://img.shields.io/nuget/v/BetterExtensions.BlazorMVVM.svg)](https://www.nuget.org/packages/BetterExtensions.BlazorMVVM/)

To Install 

```
Install-Package BetterExtensions.BlazorMVVM
```
or 
```
dotnet add package BetterExtensions.BlazorMVVM
```

## Usage

Step 1: Create ViewModel

```csharp
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
```

Step 2: Inject ViewModel to your IoC. ```IServiceCollection``` is the default one

>It is your decision of how to register ViewModels. It can be Transcient, Scoped or Singleton and depends on your needs.

```csharp
services
    .AddTransient<CounterViewModel>()
    .AddTransient<FetchDataViewModel>();
```

Step 3: Inherit page from ```BasePage``` and mention ViewModel as data context

```html
@page "/counter"
@inherits BetterExtensions.BlazorMVVM.BasePage<CounterViewModel>
```

Step 4: Add rest of HTML. ViewModel props and commands are available through ```@ViewModel``` parameter

```html
<h1>Counter</h1>

<p>Current count: @ViewModel.CurrentCount</p>

<button class="btn btn-primary" @onclick="ViewModel.IncrementCounter">Click me</button>
```

Also ViewModel provide ```State``` parameter that indicate page state. Possible values is:
* Clean
* Loading
* Normal
* NoData
* Error

Example of ```PageState``` usage:

```html
@page "/fetchdata"
@inherits BetterExtensions.BlazorMVVM.BasePage<FetchDataViewModel>

<h1>Weather forecast</h1>

<p>This component demonstrates fetching data from the server.</p>

@if (ViewModel.State is PageState.Loading)
{
    <p><em>Loading...</em></p>
}

@if (ViewModel.State is PageState.NoData)
{
    <p><em>No forecasts for now...</em></p>
}

@if (ViewModel.State is PageState.Normal)
{
    <table class="table">
        <thead>
            <tr>
                <th>Date</th>
                <th>Temp. (C)</th>
                <th>Temp. (F)</th>
                <th>Summary</th>
            </tr>
        </thead>
        <tbody>
        @foreach (var forecast in ViewModel.Forecasts)
        {
            <tr>
                <td>@forecast.Date.ToShortDateString()</td>
                <td>@forecast.TemperatureC</td>
                <td>@forecast.TemperatureF</td>
                <td>@forecast.Summary</td>
            </tr>
        }
        </tbody>
    </table>
}
```
