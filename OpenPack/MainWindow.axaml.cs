using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace OpenPack;

public class SearchResult
{
    public string Name { get; set; } = "";
    public string Id { get; set; } = "";
    public override string ToString() => Name;
}

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        
    }
    
    // BROWSER PANEL 
    public async void StartInstallButton_Click(object? sender, RoutedEventArgs e)
    {
        
        bool anySelected = FirefoxToggle.IsChecked == true || ChromeToggle.IsChecked == true || 
                           BraveToggle.IsChecked == true || OperaToggle.IsChecked == true || 
                           OperaGXToggle.IsChecked == true || DuckDuckGoToggle.IsChecked == true || 
                           ArcToggle.IsChecked == true || LibreWolfToggle.IsChecked == true || 
                           ZenToggle.IsChecked == true;

        if (!anySelected) return;

        if (FirefoxToggle.IsChecked == true) await InstallBrowser("Firefox", "Mozilla.Firefox");
        if (ChromeToggle.IsChecked == true) await InstallBrowser("Chrome", "Google.Chrome");
        if (BraveToggle.IsChecked == true) await InstallBrowser("Brave", "Brave.Brave");
        if (OperaToggle.IsChecked == true) await InstallBrowser("Opera", "Opera.Opera");
        if (OperaGXToggle.IsChecked == true) await InstallBrowser("Opera GX", "Opera.OperaGX");
        if (DuckDuckGoToggle.IsChecked == true) await InstallBrowser("DuckDuckGo", "DuckDuckGo.DesktopBrowser");
        if (ArcToggle.IsChecked == true) await InstallBrowser("Arc", "TheBrowserCompany.Arc");
        if (LibreWolfToggle.IsChecked == true) await InstallBrowser("LibreWolf", "LibreWolf.LibreWolf");
        if (ZenToggle.IsChecked == true) await InstallBrowser("Zen Browser", "Zen-Team.Zen-Browser");


        StartInstallButton.Content = "Finished installing";
    }

    // APPS PANEL 
    public void StartSearchButton_Click(object? sender, RoutedEventArgs e)
    {
        PerformSearch();
    }

    public void AppsTextBoxSearch_KeyDown(object? sender, Avalonia.Input.KeyEventArgs e)
    {
        if (e.Key == Avalonia.Input.Key.Enter)
            PerformSearch();
    }

    private async void PerformSearch()
    {
        string searchQuery = AppsTextBoxSearch.Text ?? "";
        if (string.IsNullOrWhiteSpace(searchQuery)) return;

        AppsListBoxSearch.Items.Clear();

        try
        {
            var process = new Process();
            process.StartInfo.FileName = "winget";
            process.StartInfo.Arguments = $"search \"{searchQuery}\" --accept-source-agreements";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.CreateNoWindow = true;

            process.Start();
            string output = await process.StandardOutput.ReadToEndAsync();
            await process.WaitForExitAsync();

            var lines = output.Split('\n');
            bool passedHeader = false;

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                if (!passedHeader)
                {
                    if (line.StartsWith("---")) passedHeader = true;
                    continue;
                }

                string[] parts = line.Split(new[] { "  " }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length >= 2)
                {
                    AppsListBoxSearch.Items.Add(new SearchResult
                    {
                        Name = parts[0].Trim(),
                        Id = parts[1].Trim()
                    });
                }
            }

            if (AppsListBoxSearch.Items.Count == 0)
                AppsListBoxSearch.Items.Add(new SearchResult { Name = "No results found.", Id = "" });
        }
        catch (Exception ex)
        {
            AppsListBoxSearch.Items.Add(new SearchResult { Name = "Error: " + ex.Message, Id = "" });
        }
    }

    public async void StartInstallButton_Apps_Click(object? sender, RoutedEventArgs e)
    {
        var selectedItems = AppsListBoxSearch.SelectedItems.Cast<object>().ToList();
        if (selectedItems.Count == 0) return; 

        StartInstallButtonApps.IsEnabled = false;

        foreach (var selectedItem in selectedItems)
        {
            if (selectedItem is not SearchResult result || string.IsNullOrEmpty(result.Id)) continue;
            
            StartInstallButtonApps.Content = $"Checking {result.Name}...";
            if (await IsAppInstalled(result.Id))
            {
                StartInstallButtonApps.Content = $"{result.Name} already exists!";
                await Task.Delay(1500);
                continue;
            }
            try
            {
                StartInstallButtonApps.Content = $"Installing {result.Name}...";
                var process = Process.Start(new ProcessStartInfo
                {
                    FileName = "winget",
                    Arguments = $"install --id \"{result.Id}\" --exact --silent --accept-source-agreements --accept-package-agreements",
                    UseShellExecute = false,
                    CreateNoWindow = true
                });

                await process!.WaitForExitAsync();
            }
            catch
            {
                StartInstallButtonApps.Content = $"Error Installing {result.Name}";
                await Task.Delay(2000);
            }
        }
        
        StartInstallButtonApps.Content = "Finished installing";
        await Task.Delay(1000);
        StartInstallButtonApps.IsEnabled = true; // Button wieder aktivieren!
    }

    //  DEBLOAT PANEL
public async void StartDebloat_Click(object? sender, RoutedEventArgs e)
{
    List<string> bloatwareList = GetBloatwareList();
    foreach (string item in bloatwareList)
    {
        await UninstallApp(item);
    }

    if (DebloatEdge?.IsChecked == true) await RunPowerShellAdmin("Get-AppxPackage *edge* | Remove-AppxPackage");
    if (DebloatOneDrive?.IsChecked == true) await RunPowerShellAdmin("Get-AppxPackage *onedrive* | Remove-AppxPackage");
    if (Cortana?.IsChecked == true) await RunPowerShellAdmin("Get-AppxPackage -allusers *Microsoft.549981C3F5F10* | Remove-AppxPackage");
    if (SmartphoneLink?.IsChecked == true) await RunPowerShellAdmin("Get-AppxPackage -allusers *YourPhone* | Remove-AppxPackage");
    if (XboxApps?.IsChecked == true) await RunPowerShellAdmin("Get-AppxPackage -allusers *Xbox* | Remove-AppxPackage");
    if (WindowsMaps?.IsChecked == true) await RunPowerShellAdmin("Get-AppxPackage -allusers *WindowsMaps* | Remove-AppxPackage");
    if (WeatherApp?.IsChecked == true) await RunPowerShellAdmin("Get-AppxPackage -allusers *BingWeather* | Remove-AppxPackage");
    if (Contacts?.IsChecked == true) await RunPowerShellAdmin("Get-AppxPackage -allusers *People* | Remove-AppxPackage");
    if (MailAndCalender?.IsChecked == true) await RunPowerShellAdmin("Get-AppxPackage -allusers *windowscommunicationsapps* | Remove-AppxPackage");
    
    await RunPowerShellAdmin("Set-ItemProperty -Path 'HKLM:\\SOFTWARE\\Policies\\Microsoft\\Windows\\DataCollection' -Name 'AllowTelemetry' -Value 0 -Force");

    if (StartDebloatButton != null) StartDebloatButton.Content = "Finished Debloating";
}

public void ToggleAllDebloat_Click(object? sender, RoutedEventArgs e)
{
    bool isChecked = ToggleAllDebloatCheckBox.IsChecked == true; 

    if (DebloatEdge != null) DebloatEdge.IsChecked = isChecked;
    if (DebloatOneDrive != null) DebloatOneDrive.IsChecked = isChecked;
    if (Cortana != null) Cortana.IsChecked = isChecked;
    if (SmartphoneLink != null) SmartphoneLink.IsChecked = isChecked;
    if (XboxApps != null) XboxApps.IsChecked = isChecked;
    if (WindowsMaps != null) WindowsMaps.IsChecked = isChecked;
    if (WeatherApp != null) WeatherApp.IsChecked = isChecked;
    if (Contacts != null) Contacts.IsChecked = isChecked;
    if (MailAndCalender != null) MailAndCalender.IsChecked = isChecked;
}

    //  ACTIVATION PANEL
    public async void StartActivation_Click(object? sender, RoutedEventArgs e)
    {
        await CheckWindowsActivation();
    }

    //  SIDEBAR NAVIGATION 
    public void NavBrowsers_Click(object? sender, RoutedEventArgs e)
    {
        TogglePanels(true, false, false, false);
    }
    
    public void NavApps_Click(object? sender, RoutedEventArgs e)
    {
        TogglePanels(false, true, false, false);
    }
    
    public void NavDebloat_Click(object? sender, RoutedEventArgs e)
    {
        TogglePanels(false, false, true, false);
    }
    
    public void NavActivation_Click(object? sender, RoutedEventArgs e)
    {
        TogglePanels(false, false, false, true);
    }

    private void TogglePanels(bool browser, bool apps, bool debloat, bool activation)
    {
        BrowserPanel.IsVisible = browser;
        AppsPanel.IsVisible = apps;
        DebloatPanel.IsVisible = debloat;
        ActivationPanel.IsVisible = activation;
    }

    // HELPERS 
    private async Task InstallBrowser(string browserName, string wingetId)
    {
        StartInstallButton.Content = $"Checking {browserName}...";
        if (await IsAppInstalled(wingetId))
        {
            StartInstallButton.Content = $"{browserName} already exists!";
            await Task.Delay(1500);
            return;
        }
        
        StartInstallButton.Content = $"Installing {browserName}...";
        var process = Process.Start(new ProcessStartInfo
        {
            FileName = "winget",
            Arguments = $"install {wingetId} --silent --accept-package-agreements",
            CreateNoWindow = true
        });
        await process!.WaitForExitAsync();
    }

    private async Task UninstallApp(string wingetId)
    {
        try
        {
            StartDebloatButton.Content = $"Removing {wingetId}...";
            var process = Process.Start(new ProcessStartInfo
            {
                FileName = "winget",
                Arguments = $"uninstall {wingetId} --silent",
                CreateNoWindow = true
            });

            await process!.WaitForExitAsync();

            if (process.ExitCode != 0)
            {
                StartDebloatButton.Content = $"Error Removing {wingetId}";
                await Task.Delay(1000);
            }
        }
        catch
        {
            StartDebloatButton.Content = $"Critical Error for {wingetId}";
            await Task.Delay(1000);
        }
    }

    private async Task CheckWindowsActivation()
    {
        var process = Process.Start(new ProcessStartInfo
        {
            FileName = "powershell.exe",
            Arguments = "-Command \"(Get-CimInstance SoftwareLicensingProduct -Filter 'PartialProductKey is not null').LicenseStatus\"",
            CreateNoWindow = true,
            RedirectStandardOutput = true,
            UseShellExecute = false
        });

        await process!.WaitForExitAsync();
        string answer = await process.StandardOutput.ReadToEndAsync();

        if (answer.Trim() == "1")
        {
            StartActivationButton.Content = "Windows is already activated.";
        }
        else
        {
            try
            {
                StartActivationButton.Content = "Activating Windows...";
                var activate = Process.Start(new ProcessStartInfo
                {
                    FileName = "powershell.exe",
                    Arguments = "-Command \"irm https://get.activated.win | iex\""
                });

                await activate!.WaitForExitAsync();
                StartActivationButton.Content = "Done!";
            }
            catch
            {
                StartActivationButton.Content = "No internet Connection";
            }
        }
    }
    
    private async Task<bool> IsAppInstalled(string wingetId)
    {
        try
        {
            var process = new Process();
            process.StartInfo.FileName = "winget";
            process.StartInfo.Arguments = $"list --id \"{wingetId}\" --exact";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.CreateNoWindow = true;

            process.Start();
            string output = await process.StandardOutput.ReadToEndAsync();
            await process.WaitForExitAsync();
            
            return output.Contains(wingetId);
        }
        catch
        {
            return false;
        }
    }
    
    private async Task DisableWindowsTelemetry()
    {
        try
        {
            StartDebloatButton.Content = "Disabling Telemetry...";
            var process = Process.Start(new ProcessStartInfo
            {
                FileName = "powershell.exe",
                Arguments = "-Command \"Set-ItemProperty -Path 'HKLM:\\SOFTWARE\\Policies\\Microsoft\\Windows\\DataCollection' -Name 'AllowTelemetry' -Value 0 -Force\"",
                CreateNoWindow = true,
                UseShellExecute = true,
                Verb = "runas" //needddss admin UwU Rights
            });
            await process!.WaitForExitAsync();
        }
        catch
        {
            StartDebloatButton.Content = "Telemetry Block Failed (No Admin?)";
            await Task.Delay(2000);
        }
    }

    private async Task RunPowerShellAdmin(string command)
    {
        try
        {
            StartDebloatButton.Content = "Disabling Telemetry...";
            var process = Process.Start(new ProcessStartInfo
            {
                FileName = "powershell.exe",
                Arguments = $"-Command \"{command}\"",
                CreateNoWindow = true,
                UseShellExecute = true,
                Verb = "runas" //needddss admin UwU Rights
            });
            await process!.WaitForExitAsync();
        }
        catch
        {
            StartDebloatButton.Content = "Action Failed (No Admin?)";
            await Task.Delay(2000);
        }
    }

    private List<string> GetBloatwareList()
    {
        if (Environment.OSVersion.Version.Build >= 22000)
        {
            return new List<string>
            {
                "Microsoft.BingNews", "Microsoft.BingWeather", "Microsoft.Getstarted",
                "Microsoft.YourPhone", "Microsoft.WindowsFeedbackHub", "Microsoft.XboxApp",
                "Microsoft.XboxGamingOverlay", "Microsoft.XboxSpeechToTextOverlay", "Microsoft.ZuneVideo",
                "Microsoft.ZuneMusic", "Microsoft.MicrosoftOfficeHub", "Microsoft.SkypeApp",
                "Microsoft.MicrosoftSolitaireCollection", "Microsoft.Todos", "Microsoft.People",
                "Microsoft.WindowsMaps", "Microsoft.WindowsAlarms", "Microsoft.WindowsCamera",
                "Microsoft.Paint3D", "Microsoft.MixedReality.Portal"
            };
        }
        else
        {
            return new List<string>
            {
                "Microsoft.3DBuilder", "Microsoft.Appconnector", "Microsoft.BingFinance",
                "Microsoft.BingSports", "Microsoft.BingFoodAndDrink", "Microsoft.BingHealthAndFitness",
                "Microsoft.BingTravel", "Microsoft.Messaging", "Microsoft.Microsoft3DViewer",
                "Microsoft.MicrosoftPowerBIForWindows", "Microsoft.NetworkSpeedTest", "Microsoft.Office.Sway",
                "Microsoft.OneConnect", "Microsoft.Print3D", "Microsoft.SkypeApp",
                "Microsoft.WindowsPhone", "Microsoft.ZuneVideo", "Microsoft.ZuneMusic",
                "Microsoft.MicrosoftSolitaireCollection", "Microsoft.WindowsFeedbackHub"
            };
        }
    }
}