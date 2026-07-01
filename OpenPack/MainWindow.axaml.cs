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
        StartInstallButton.Content = "Finished installing";
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
}
