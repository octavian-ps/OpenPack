# OpenPack

**OpenPack** is the ultimate tool for setting up a fresh Windows installation. Install your favorite apps, remove bloatware, tweak your system, and activate Windows — all in just a few clicks.

> 🎉 **Version 1.0.0 is out!** → [Download the EXE](https://github.com/octavian-ps/OpenPack/releases/tag/Rel)

---

## 🚀 What is OpenPack?

OpenPack takes away the tedious work of setting up a new Windows installation. Select your browsers, search and install any app, debloat Windows, and activate it — all from a clean sidebar interface, fully powered by **winget** and **PowerShell** in the background.

---

## ✅ Currently Supported Features

### 🌐 Browsers
Pick one or more browsers to install. OpenPack checks if they're already installed before doing anything:

- **Firefox** — Mozilla.Firefox
- **Chrome** — Google.Chrome
- **Brave** — Brave.Brave
- **Opera** — Opera.Opera
- **Opera GX** — Opera.OperaGX
- **DuckDuckGo Browser** — DuckDuckGo.DesktopBrowser
- **Arc Browser** — TheBrowserCompany.Arc
- **LibreWolf** — LibreWolf.LibreWolf
- **Zen Browser** — Zen-Team.Zen-Browser

### 📦 Apps
Search the entire winget catalog directly from within the app. Type any app name, press Enter or click Search, and pick one or multiple results from the list. OpenPack checks if each app is already installed before installing, and shows the status for each one in real time.

### 🧹 Debloat
Automatically removes pre-installed Microsoft bloatware — tailored to your Windows version. Also disables Windows telemetry via registry:

- **Windows 11:** Removes Bing News, Weather, Get Started, Your Phone, Feedback Hub, Xbox apps, Zune, Office Hub, Skype, Solitaire, Todos, People, Maps, Alarms, Camera, Paint 3D, Mixed Reality Portal and more
- **Windows 10:** Removes 3D Builder, Bing Finance/Sports/Food/Health/Travel, Messaging, 3D Viewer, Power BI, Network Speed Test, Sway, OneConnect, Print 3D, Skype, Windows Phone, Zune, Solitaire, Feedback Hub and more

### 🔑 Windows Activation
Checks whether Windows is already activated. If not, it runs the activation process automatically — no manual steps needed.

---

## 🛠️ How Does It Work?

OpenPack has a **sidebar navigation** — switch between any section at any time:

1. **🌐 Browsers** — toggle the browsers you want and click "Install Selected"
2. **📦 Apps** — search for anything, select one or more results, click "Install Selected Apps"
3. **🧹 Debloat** — click "Debloat" to remove bloatware and disable telemetry (auto-detects Win10/11)
4. **🔑 Activation** — click "Check And Activate Windows 10/11" to check and activate if needed

All installations and removals run silently in the background with real-time status feedback.

---

## ⚙️ Technical Details

- **Framework:** [Avalonia UI](https://avaloniaui.net/) (Version 12.0.5)
- **Target Platform:** .NET 10.0 / Windows
- **Theme:** Dark UI (`#1e1e1e` / `#2b2b2b`)
- **Package Manager:** winget (Windows Package Manager)
- **Language:** C#
- **Requires Admin Rights:** Yes (for debloat & activation)

---

## 📋 Requirements

- Windows 10 / 11
- [winget](https://learn.microsoft.com/en-us/windows/package-manager/winget/) installed (included by default in modern Windows versions)
- .NET 10.0 Runtime
- Administrator privileges

---

## ⬇️ Download

### ✅ Version 1.0.0 — Available Now
Grab the EXE directly — no setup, no terminal, no dependencies. Just download and run.

**[→ Download OpenPack v1.0.0](https://github.com/octavian-ps/OpenPack/releases/tag/Rel)**

---

## 🔨 Build & Run

```bash
git clone https://github.com/octavian-ps/OpenPack.git
cd OpenPack
dotnet build
dotnet run --project OpenPack
```

---

## 🔮 What's Coming in v2.0.0

A major update is in the works. v2.0.0 will be a big step up in quality, stability, and features across the board:

### 🔧 Windows Tweaks
A brand new section dedicated to optimizing your fresh Windows install — performance tweaks, visual adjustments, privacy settings, and more. All in one place, all in one click.

### 🧹 Deeper Debloat & Selection
More control over what gets removed — choose exactly which bloatware to debloat instead of removing everything at once.

### 🔒 Security Improvements
More hardening options, better handling of admin rights, and safer execution of PowerShell commands throughout the app.

### 🌐 More Browsers
Even more browsers may be added to the selection.

### ⚙️ General Stability & Polish
Everything more robust — better error handling, smoother flows, and an overall more reliable experience from start to finish.

With v2.0.0, OpenPack aims to become the **ultimate tool for setting up a fresh Windows installation** — everything you need, nothing you don't.

> v2.0.0 is actively being worked on. Stay tuned on the [Releases](https://github.com/octavian-ps/OpenPack/releases) page.

---

## 🤝 Contributing

Ideas, suggestions, and pull requests are always welcome! Feel free to open an issue or submit a PR directly.

---

## 📄 License

This project is licensed under the [MIT License](LICENSE).
