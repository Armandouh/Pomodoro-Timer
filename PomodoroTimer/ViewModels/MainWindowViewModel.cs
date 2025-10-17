using System;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace PomodoroTimer.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty] private int _minutes = 0;
    [ObservableProperty] private int _seconds = 8;
    [ObservableProperty] private string _timeToDisplay = "25:00";

    // Timer internals
    private readonly DispatcherTimer? _timer; // Fires on UI thread
    private DateTimeOffset _endTime; // Wall-clock target
    private bool _isRunning; // Simple state flag

    public MainWindowViewModel()
    {
        _timer = new DispatcherTimer()
        {
            Interval = TimeSpan.FromSeconds(1)
        };
        _timer.Tick += OnTick;
    }

    [RelayCommand]
    private void Start()
    {
        if (_isRunning)
            return;

        var duration = new TimeSpan(0, Minutes, Seconds);
        if (duration <= TimeSpan.Zero)
            duration = TimeSpan.FromSeconds(1);

        _endTime = DateTimeOffset.UtcNow + duration;
        _isRunning = true;
        _timer?.Start();
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

    private void OnTick(object? sender, EventArgs e)
    {
        var remaining = _endTime - DateTimeOffset.UtcNow;
        if (remaining <= TimeSpan.Zero)
        {
            TimeToDisplay = "00:00";
            _timer.Stop();
            _isRunning = false;
            return;
        }

        TimeToDisplay = $"{(int)remaining.TotalMinutes:00}:{remaining.Seconds:00}";
    }
}