using System.Data;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace LabApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public List<Process> Processes { get; private set; } = new();
    
    public IEnumerable<Process> Applications { get; private set; } = new List<Process>();

    private DispatcherTimer _timer = new()
    {
        Interval = TimeSpan.FromSeconds(3)
    };

    public MainWindow()
    {
        InitializeComponent();
        UpdateProcessesListView();
        UpdateApplicationsListView();

        _timer.Tick += Timer_Tick;
        _timer.Start();
    }
    private void Timer_Tick(object? sender, EventArgs e)
    {
        if (ProcessesTabItem.IsSelected)
            UpdateProcessesListView();
        else if (ApplicationsTabItem.IsSelected)
            UpdateApplicationsListView();

    }

    public void UpdateProcessesListView()
    {
        ProcessesListView.ItemsSource = null;
        Processes.Clear();

        UpdateProcesses();

        ProcessesListView.ItemsSource = Processes;

        StartedProcessCountLabel.Content = $"Колчичество запущенных процессов: {Processes.Count}";
    }

    private void UpdateApplicationsListView()
    {
        ApplicationsListView.ItemsSource = null;

        UpdateApplications();

        ApplicationsListView.ItemsSource = Applications;
    }

    private void UpdateProcesses()
    {
        Processes = Process.GetProcesses().ToList();
    }

    private void UpdateApplications()
    {
        Applications = Process.GetProcesses().ToList()
            .Where(p => !string.IsNullOrWhiteSpace(p.MainWindowTitle));
    }

    private void ExitProcess_Click(object sender, RoutedEventArgs e)
    {
        var selected = ProcessesListView.SelectedItem;

        if (selected is not Process process)
            return;

        process.Kill();
        process.WaitForExit();

        UpdateProcessesListView();
    }

    private void StartNewProcessMenuItem_Click(object sender, RoutedEventArgs e)
    {
        StartProcessWindow widnow = new(this);

        widnow.Show();
    }
}