using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace OpenPack;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    //BROOWSER PANEL
    public async void StartInstallButton_Click(object? sender, RoutedEventArgs e)
    {
        if (FirefoxToggle.IsChecked == true)
        {
            await InstallBrowser("FireFox", "Mozilla.Firefox");
        }

        if (ChromeToggle.IsChecked == true)
        {
            await InstallBrowser("Chrome", "Google.Chrome");
        }

        if (BraveToggle.IsChecked == true)
        {
            await InstallBrowser("Brave", "Brave.Brave");
        }

        if (OperaToggle.IsChecked == true)
        {
            await InstallBrowser("Opera", "Opera.Opera");
        }

        if (OperaGXToggle.IsChecked == true)
        {
            await InstallBrowser("OperaGX", "Opera.OperaGX");
        }

        if (DuckDuckGoToggle.IsChecked == true)
        {
            await InstallBrowser("DuckDuckGo", "DuckDuckGo.DesktopBrowser");
        }

        if (ArcToggle.IsChecked == true)
        {
            await InstallBrowser("Arc", "TheBrowserCompany.Arc");
        }
        
        if (LibreWolfToggle.IsChecked == true)
        {
            await InstallBrowser("LibreWolf", "LibreWolf.LibreWolf");
        }
        
        if (ZenToggle.IsChecked == true)
        {
            await InstallBrowser("Zen Browser", "Zen-Team.Zen-Browser");
        }

        StartInstallButton.Content = "Finished installing";
        BrowserPanel.IsVisible = false;
        AppsPanel.IsVisible = true;
    }

    //APPS PANEL
    public void StartSearchButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        PerformSearch();
    }

    public void AppsTextBoxSearch_KeyDown(object? sender, Avalonia.Input.KeyEventArgs e)
    {
        if (e.Key == Avalonia.Input.Key.Enter)
        {
            PerformSearch();
        }
    }

    private async void PerformSearch()
    {
        string searchQuery = AppsTextBoxSearch.Text ?? "";
    
        if (string.IsNullOrWhiteSpace(searchQuery)) 
        {
            return; 
        }
        AppsListBoxSearch.Items.Clear(); 

        try
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
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
                    if (line.StartsWith("---"))
                    {
                        passedHeader = true;
                    }
                    continue;
                }
                
                string[] parts = line.Split(new[] { "  " }, System.StringSplitOptions.RemoveEmptyEntries);
    
                if (parts.Length > 0)
                {
                    AppsListBoxSearch.Items.Add(parts[0].Trim());
                }
            }
        }
        catch (System.Exception ex)
        {
            AppsListBoxSearch.Items.Add("Fehler: " + ex.Message);
        }
    }
    
    public async void StartInstallButton_Apps_Click(object? sender, RoutedEventArgs e)
    {
        foreach (var selectedItem in AppsListBoxSearch.SelectedItems)
        {
            string? appName = selectedItem?.ToString();
            if (!string.IsNullOrEmpty(appName))
            {
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo.FileName = "winget";
                process.StartInfo.Arguments = $"install \"{appName}\" --accept-source-agreements --accept-package-agreements --silent";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;

                process.Start();
                await process.WaitForExitAsync();
            }
        }
        
        AppsPanel.IsVisible = false;
        DebloatPanel.IsVisible = true;
    }

    public async void StartDebloat_Click(object? sender, RoutedEventArgs e)
    {
        if (Environment.OSVersion.Version.Build >= 22000)
        {
            List<string> win11Bloat = new List<string>
            {
                "Microsoft.BingNews", "Microsoft.BingWeather", "Microsoft.Getstarted",
                "Microsoft.YourPhone", "Microsoft.WindowsFeedbackHub", "Microsoft.XboxApp",
                "Microsoft.XboxGamingOverlay", "Microsoft.XboxSpeechToTextOverlay", "Microsoft.ZuneVideo",
                "Microsoft.ZuneMusic", "Microsoft.MicrosoftOfficeHub", "Microsoft.SkypeApp",
                "Microsoft.MicrosoftSolitaireCollection", "Microsoft.Todos", "Microsoft.People",
                "Microsoft.WindowsMaps", "Microsoft.WindowsAlarms", "Microsoft.WindowsCamera",
                "Microsoft.Paint3D", "Microsoft.MixedReality.Portal"
            };
            foreach (string item in win11Bloat)
            {
                await UninstallApp(item, item);
            }
        }
        else
        {
            List<string> win10Bloat = new List<string>
            {
                "Microsoft.3DBuilder", "Microsoft.Appconnector", "Microsoft.BingFinance",
                "Microsoft.BingSports", "Microsoft.BingFoodAndDrink", "Microsoft.BingHealthAndFitness",
                "Microsoft.BingTravel", "Microsoft.Messaging", "Microsoft.Microsoft3DViewer",
                "Microsoft.MicrosoftPowerBIForWindows", "Microsoft.NetworkSpeedTest", "Microsoft.Office.Sway",
                "Microsoft.OneConnect", "Microsoft.Print3D", "Microsoft.SkypeApp",
                "Microsoft.WindowsPhone", "Microsoft.ZuneVideo", "Microsoft.ZuneMusic",
                "Microsoft.MicrosoftSolitaireCollection", "Microsoft.WindowsFeedbackHub"
            };
            foreach (string item in win10Bloat)
            {
                await UninstallApp(item, item);
            }
        }

        StartDebloatButton.Content = "Finished Debloating";
        DebloatPanel.IsVisible = false;
        ActivationPanel.IsVisible = true;
    }

    public async void StartActivation_Click(object? sender, RoutedEventArgs e)
    {
        await CheckWindowsActivation();
    }



    private async Task InstallBrowser(string browserName, string wingetId)
    {
        StartInstallButton.Content = $"Installing {browserName}...";

        var settings = new ProcessStartInfo
        {
            FileName = "winget",
            Arguments = $"install {wingetId} --silent --accept-package-agreements",
            CreateNoWindow = true
        };

        var setup = Process.Start(settings);
        await setup.WaitForExitAsync();
    }

    private async Task InstallApps(string appName, string wingetId)
    {
        StartInstallButtonApps.Content = $"Installing {appName}...";

        var settings = new ProcessStartInfo
        {
            FileName = "winget",
            Arguments = $"install {wingetId} --silent --accept-package-agreements",
            CreateNoWindow = true
        };

        var setup = Process.Start(settings);
        await setup.WaitForExitAsync();
    }

    private async Task UninstallApp(string appName, string wingetId)
    {
        StartDebloatButton.Content = $"Removing {appName}...";

        var settings = new ProcessStartInfo
        {
            FileName = "winget",
            Arguments = $"uninstall {wingetId} --silent --accept-package-agreements",
            CreateNoWindow = true
        };

        var setup = Process.Start(settings);
        await setup.WaitForExitAsync();
    }

    private async Task CheckWindowsActivation()
    {
        var settings = new ProcessStartInfo
        {
            FileName = "powershell.exe",
            Arguments =
                "-Command \"(Get-CimInstance SoftwareLicensingProduct -Filter 'PartialProductKey is not null').LicenseStatus\"",
            CreateNoWindow = true,
            RedirectStandardOutput = true,
            UseShellExecute = false,
        };
        var setup = Process.Start(settings);
        await setup.WaitForExitAsync();
        string answer = await setup.StandardOutput.ReadToEndAsync();

        if (answer.Trim() == "1")
        {
            StartActivationButton.Content = "Windows is Already Activated";
        }
        else
        {
            StartActivationButton.Content = "Windows is yet not Activated one powershell command is being execuded";

            var activateSettings = new ProcessStartInfo
            {
                FileName = "powershell.exe",
                Arguments = "-Command \"irm https://get.activated.win | iex\"",
            };
            var setupactivation = Process.Start(activateSettings);
            await setupactivation.WaitForExitAsync();
        }
    }
}

//Task: Create back buttons 