# OpenPack

**OpenPack** is a lightweight Windows setup tool that lets you install your favorite apps, remove bloatware, and activate Windows after a fresh install — all in just a few clicks.

---

## 🚀 What is OpenPack?

OpenPack takes away the tedious work of setting up a new Windows installation. Select your browsers, pick your apps, debloat Windows, and activate it — OpenPack walks you through each step automatically using **winget** and **PowerShell** in the background.

---

## ✅ Currently Supported Features

### 🌐 Step 1 — Browsers
Pick one or more browsers to install:

- **Firefox** — Mozilla.Firefox
- **Chrome** — Google.Chrome
- **Brave** — Brave.Brave
- **Opera** — Opera.Opera
- **Opera GX** — Opera.OperaGX
- **DuckDuckGo Browser** — DuckDuckGo.DesktopBrowser
- **Arc Browser** — TheBrowserCompany.Arc

### 📦 Step 2 — Apps
Choose your desired applications:

- **Discord** — Discord.Discord
- **Proton Pass** — Proton.ProtonPass
- **Proton VPN** — Proton.ProtonVPN
- **Proton Drive** — Proton.ProtonDrive
- **Steam** — Valve.Steam
- **Malwarebytes** — Malwarebytes.Malwarebytes
- **Spotify** — Spotify.Spotify

### 🧹 Step 3 — Debloat
Automatically removes pre-installed Microsoft bloatware — tailored to your Windows version:

- **Windows 11:** Removes Bing News, Weather, Get Started, Your Phone, Feedback Hub, Xbox apps, Zune, Office Hub, Skype, Solitaire, Todos, People, Maps, Alarms, Camera, Paint 3D, Mixed Reality Portal and more
- **Windows 10:** Removes 3D Builder, Bing Finance/Sports/Food/Health/Travel, Messaging, 3D Viewer, Power BI, Network Speed Test, Sway, OneConnect, Print 3D, Skype, Windows Phone, Zune, Solitaire, Feedback Hub and more

### 🔑 Step 4 — Windows Activation
Checks whether Windows is already activated. If not, it runs the activation process automatically — no manual steps needed.

---

## 🛠️ How Does It Work?

1. Launch OpenPack
2. Select your **browsers** and click **"Install Selected"**
3. Select your **apps** and click **"Install Selected Apps"**
4. Click **"Debloat"** to remove unwanted Microsoft apps (auto-detected for Win10/11)
5. Click **"Check And Activate Windows 10/11"** — OpenPack checks your activation status and activates if needed

Every step flows into the next automatically. All installations and removals run silently in the background.

---

## ⚙️ Technical Details

- **Framework:** [Avalonia UI](https://avaloniaui.net/) (Version 12.0.5)
- **Target Platform:** .NET 10.0 / Windows
- **Theme:** Fluent Design (follows system setting: Dark / Light)
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

## ⬇️ Download (Coming Soon)

A pre-built **EXE installer** is coming soon — no setup, no terminal, no dependencies. Just download, double-click, and you're ready to go.

Stay tuned for the first release on the [Releases](../../releases) page.

---

## 🔨 Build & Run

```bash
git clone https://github.com/octavian-ps/OpenPack.git
cd OpenPack
dotnet build
dotnet run --project OpenPack
```

---

## 🔮 What's Coming Next?

OpenPack is just getting started — and this is only the beginning! Many more features and categories are planned. Here's a look at what's coming in the future:

### New Windows & Categories
The tool won't stop at browsers and apps. Dedicated sections are planned for:

- 🎮 **Gaming Tools** (e.g. Epic Games, GOG Galaxy, EA App, Ubisoft Connect, Playnite)
- 🎨 **Creative Software** (e.g. GIMP, Inkscape, DaVinci Resolve, OBS Studio, Blender)
- 💻 **Developer Tools** (e.g. VS Code, Git, Windows Terminal, Docker, Node.js, Python)
- 🔧 **System Tools** (e.g. 7-Zip, VLC, CPU-Z, HWMonitor, WinDirStat)
- 🔒 **Security & Privacy** (e.g. Bitwarden, KeePassXC, additional VPN solutions)
- 💬 **Communication** (e.g. Telegram, Signal, TeamSpeak, Zoom, Slack)

### More Planned Features
- **Custom package lists** — save and reuse your own selections
- **Profile system** — e.g. "Gaming PC Setup", "Office PC Setup", "Dev Workstation"
- **Progress indicator** — detailed installation status for each app
- **Update checker** — checks whether already installed apps can be updated
- **Offline mode** — pre-download packages for devices without a permanent internet connection

> OpenPack grows continuously. New windows, new apps, and new categories will be added regularly over time. The goal is a complete, all-in-one setup tool for Windows — built by enthusiasts, for everyone.

---

## 🤝 Contributing

Ideas, suggestions, and pull requests are always welcome! Feel free to open an issue or submit a PR directly.

---

## 📄 License

This project is licensed under the [MIT License](LICENSE).
