using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        StartInstallButton.Content = "Finished installing";
        BrowserPanel.IsVisible = false;
        AppsPanel.IsVisible = true;
    }
    
    //APPS PANEL
    public async void StartInstallButton_Apps_Click(object? sender, RoutedEventArgs e)
    {
        if (DiscordToggle.IsChecked == true)
        {
            await InstallApps("Discord", "Discord.Discord");
        }

        if (ProtonPassToggle.IsChecked == true)
        {
            await InstallApps("ProtonPass", "Proton.ProtonPass");
        }
        
        if (ProtonVPNToggle.IsChecked == true)
        {
            await InstallApps("ProtonVpn", "Proton.ProtonVPN");
        }
        
        if (ProtonDriveToggle.IsChecked == true)
        {
            await InstallApps("ProtonDrive", "Proton.ProtonDrive");
        }
        
        if (SteamToggle.IsChecked == true)
        {
            await InstallApps("Steam", "Valve.Steam");
        }
        
        if (MalwarebytesToggle.IsChecked == true)
        {
            await InstallApps("Malwarebytes", "Malwarebytes.Malwarebytes");
        }
        
        if (SpotifyToggle.IsChecked == true)
        {
            await InstallApps("Spotify", "Spotify.Spotify");
        }
        StartInstallButtonApps.Content = "Finished installing";
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
        StartDebloatButton.Content =  "Finished installing";
        DebloatPanel.IsVisible = false;
        Idkwhatcomesnext.IsVisible = true;
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
}
