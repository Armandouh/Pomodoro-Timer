using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace PomodoroTimer.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty] private int _minutes = 25;
    [ObservableProperty] private int _seconds = 0;
    [ObservableProperty] private string _timeToDisplay = "25:00";

    [RelayCommand]
    private void Start()
    {
        Console.WriteLine("Timer started!");
    }
    
    [RelayCommand]
    private void Pause()
    {
        Console.WriteLine("Timer paused!");
    }

    [RelayCommand]
    private void Skip()
    {
        Console.WriteLine("Timer skipped!");
    }
}