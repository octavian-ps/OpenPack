using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace OpenPack;

// Holds both the display name and the winget ID for each search result
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
        if (FirefoxToggle.IsChecked == true)
            await InstallBrowser("Firefox", "Mozilla.Firefox");

        if (ChromeToggle.IsChecked == true)
            await InstallBrowser("Chrome", "Google.Chrome");

        if (BraveToggle.IsChecked == true)
            await InstallBrowser("Brave", "Brave.Brave");

        if (OperaToggle.IsChecked == true)
            await InstallBrowser("Opera", "Opera.Opera");

        if (OperaGXToggle.IsChecked == true)
            await InstallBrowser("Opera GX", "Opera.OperaGX");

        if (DuckDuckGoToggle.IsChecked == true)
            await InstallBrowser("DuckDuckGo", "DuckDuckGo.DesktopBrowser");

        if (ArcToggle.IsChecked == true)
            await InstallBrowser("Arc", "TheBrowserCompany.Arc");

        if (LibreWolfToggle.IsChecked == true)
            await InstallBrowser("LibreWolf", "LibreWolf.LibreWolf");

        if (ZenToggle.IsChecked == true)
            await InstallBrowser("Zen Browser", "Zen-Team.Zen-Browser");

        StartInstallButton.Content = "Finished installing";
        BrowserPanel.IsVisible = false;
        AppsPanel.IsVisible = true;
    }

    // BACK BUTTONS
    public void BackToBrowsers_Click(object? sender, RoutedEventArgs e)
    {
        AppsPanel.IsVisible = false;
        BrowserPanel.IsVisible = true;
    }

    public void BackToApps_Click(object? sender, RoutedEventArgs e)
    {
        DebloatPanel.IsVisible = false;
        AppsPanel.IsVisible = true;
    }

    public void BackToDebloat_Click(object? sender, RoutedEventArgs e)
    {
        ActivationPanel.IsVisible = false;
        DebloatPanel.IsVisible = true;
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

        if (string.IsNullOrWhiteSpace(searchQuery))
            return;

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
                    if (line.StartsWith("---"))
                        passedHeader = true;
                    continue;
                }

                // winget output columns are separated by 2+ spaces
                // Format: Name   Id   Version   Source
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

        foreach (var selectedItem in selectedItems)
        {
            if (selectedItem is not SearchResult result)
                continue;

            if (string.IsNullOrEmpty(result.Id))
                continue;

            StartInstallButtonApps.Content = $"Installing {result.Name}...";

            var process = Process.Start(new ProcessStartInfo
            {
                FileName = "winget",
                Arguments = $"install --id \"{result.Id}\" --silent --accept-source-agreements --accept-package-agreements",
                UseShellExecute = false,
                CreateNoWindow = true
            });

            await process!.WaitForExitAsync();
        }

        StartInstallButtonApps.Content = "Finished installing";
        AppsPanel.IsVisible = false;
        DebloatPanel.IsVisible = true;
    }

    // DEBLOAT PANEL
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
                await UninstallApp(item);
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
                await UninstallApp(item);
        }

        StartDebloatButton.Content = "Finished Debloating";
        DebloatPanel.IsVisible = false;
        ActivationPanel.IsVisible = true;
    }

    // ACTIVATION PANEL
    public async void StartActivation_Click(object? sender, RoutedEventArgs e)
    {
        await CheckWindowsActivation();
    }

    // HELPERS
    private async Task InstallBrowser(string browserName, string wingetId)
    {
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
        StartDebloatButton.Content = $"Removing {wingetId}...";

        var process = Process.Start(new ProcessStartInfo
        {
            FileName = "winget",
            Arguments = $"uninstall {wingetId} --silent",
            CreateNoWindow = true
        });

        await process!.WaitForExitAsync();
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
            StartActivationButton.Content = "Activating Windows...";

            var activate = Process.Start(new ProcessStartInfo
            {
                FileName = "powershell.exe",
                Arguments = "-Command \"irm https://get.activated.win | iex\""
            });

            await activate!.WaitForExitAsync();
            StartActivationButton.Content = "Done!";
        }
    }
}