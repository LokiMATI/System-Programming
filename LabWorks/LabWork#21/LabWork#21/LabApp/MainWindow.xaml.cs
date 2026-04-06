using Microsoft.Win32;
using System.Management;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows;

namespace LabApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        GetInformation();
    }

    private void GetInformation()
    {
        LoadProcessInformation();
        LoadVideoCardInformation();
        LoadMotherBoardInformation();
        LoadDiskDriveInformation();
        LoadOsInformation();
        LoadNetworkInformation();
        LoadProgrammsInformation();
    }

    private void LoadProcessInformation()
    {
        ManagementObjectSearcher management = new("root\\cimv2", "SELECT * FROM Win32_Processor");

        StringBuilder builder = new();

        foreach (var managmentObject in management.Get())
        {
            builder.AppendLine($"Proccess name: {managmentObject["Name"].ToString()}");
            builder.AppendLine($"Number of cores: {managmentObject["NumberOfCores"].ToString()}");
            builder.AppendLine($"Thread count: {managmentObject["ThreadCount"].ToString()}\n");
        }

        ProcessesTextBlock.Text = builder.ToString();
    }

    private void LoadVideoCardInformation()
    {
        ManagementObjectSearcher management = new("root\\cimv2", "SELECT * FROM Win32_VideoController");

        StringBuilder builder = new();

        foreach (var managmentObject in management.Get())
        {
            builder.AppendLine($"Video name: {managmentObject["Name"].ToString()}");
            builder.AppendLine($"Driver version: {managmentObject["DriverVersion"].ToString()}");
            builder.AppendLine($"Description: {managmentObject["Description"].ToString()}\n");
        }

        VideoCardsTextBlock.Text = builder.ToString();
    }

    private void LoadMotherBoardInformation()
    {
        ManagementObjectSearcher management = new("root\\cimv2", "SELECT * FROM Win32_MotherboardDevice");

        StringBuilder builder = new();

        foreach (var managmentObject in management.Get())
        {
            builder.AppendLine($"Motcher board name: {managmentObject["Name"].ToString()}");
            builder.AppendLine($"Device ID: {managmentObject["DeviceID"].ToString()}");
            builder.AppendLine($"Status: {managmentObject["Status"].ToString()}\n");
        }

        MotherBoardsTextBlock.Text = builder.ToString();
    }

    private void LoadDiskDriveInformation()
    {
        ManagementObjectSearcher management = new("root\\cimv2", "SELECT * FROM Win32_DiskDrive");

        StringBuilder builder = new();

        foreach (var managmentObject in management.Get())
        {
            builder.AppendLine($"Video name: {managmentObject["Name"].ToString()}");
            builder.AppendLine($"Model: {managmentObject["Model"].ToString()}");
            builder.AppendLine($"System name: {managmentObject["SystemName"].ToString()}\n");
        }

        HardDrivesTextBlock.Text = builder.ToString();
    }

    private void LoadOsInformation()
    {
        ManagementObjectSearcher management = new("root\\cimv2", "SELECT * FROM Win32_OperatingSystem");

        StringBuilder builder = new();

        foreach (var managmentObject in management.Get())
        {
            builder.AppendLine($"OS name: {managmentObject["Name"].ToString()}");
            builder.AppendLine($"OS language: {managmentObject["OSLanguage"].ToString()}");
            builder.AppendLine($"Number of users: {managmentObject["NumberOfUsers"].ToString()}\n");
        }

        OperationsSystemsTextBlock.Text = builder.ToString();
    }

    private void LoadNetworkInformation()
    {
        var adapters = NetworkInterface.GetAllNetworkInterfaces();

        StringBuilder builder = new();

        foreach (var adapter in adapters)
        {
            builder.AppendLine($"ID: {adapter.Id}");
            builder.AppendLine($"Name: {adapter.Name}");
            builder.AppendLine($"Type: {adapter.NetworkInterfaceType}\n");
        }

        NetworkTextBlock.Text = builder.ToString();
    }

    private void LoadProgrammsInformation()
    {
        StringBuilder builder = new();

        builder.AppendLine("Программы текущего пользователя: ");
        var registry = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Uninstall");
        foreach (var programm in registry.GetSubKeyNames())
        {
            registry = Registry.CurrentUser.OpenSubKey(@$"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\{programm}");

            var programmName = (string)registry.GetValue("DisplayName");
            if (!string.IsNullOrWhiteSpace(programmName))
                builder.AppendLine(programmName);
            registry.Close();
        }

        builder.AppendLine("\nПрограммы на локальной машине (все): ");
        registry = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
        foreach (var programm in registry.GetSubKeyNames())
        {
            registry = Registry.LocalMachine.OpenSubKey(@$"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\{programm}");
            var programmName = (string)registry.GetValue("DisplayName");
            if (!string.IsNullOrWhiteSpace(programmName))
                builder.AppendLine(programmName);
            registry.Close();
        }

        ProgramsTextBlock.Text = builder.ToString();
    }
}