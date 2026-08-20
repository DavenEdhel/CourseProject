using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CourseProject.DataLayer.Models;
using CourseProject.DataLayer.Repositories;

namespace CourseProject.UI.ViewModels;

public partial class CounterViewModel : ObservableObject
{
    private readonly ICounterRepository _counterRepository;
    private readonly Counter _counter;

    [ObservableProperty]
    private int value;

    public CounterViewModel(ICounterRepository counterRepository)
    {
        _counterRepository = counterRepository;
        _counter = _counterRepository.Get();
        Value = _counter.Value;
    }

    [RelayCommand]
    private void Increment()
    {
        Value++;
        _counter.Value = Value;
        _counterRepository.Save(_counter);
    }
}
