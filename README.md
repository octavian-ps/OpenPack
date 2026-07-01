# OpenPack

**OpenPack** is a lightweight Windows setup tool that lets you get your favorite applications up and running after a fresh install — in just a few clicks.

---

## 🚀 What is OpenPack?

OpenPack takes away the tedious work of downloading and installing every program one by one after a clean Windows installation. Simply select what you need, click "Install Selected" — and OpenPack handles the rest fully automatically in the background using **winget** (Windows Package Manager).

---

## ✅ Currently Supported Software

### 🌐 Browsers
Pick one or more browsers to install:

- **Firefox** — Mozilla.Firefox
- **Chrome** — Google.Chrome
- **Brave** — Brave.Brave
- **Opera** — Opera.Opera
- **Opera GX** — Opera.OperaGX
- **DuckDuckGo Browser** — DuckDuckGo.DesktopBrowser
- **Arc Browser** — TheBrowserCompany.Arc

### 📦 Apps
After the browser step, choose your desired applications:

- **Discord** — Discord.Discord
- **Proton Pass** — Proton.ProtonPass
- **Proton VPN** — Proton.ProtonVPN
- **Proton Drive** — Proton.ProtonDrive
- **Steam** — Valve.Steam
- **Malwarebytes** — Malwarebytes.Malwarebytes
- **Spotify** — Spotify.Spotify

---

## 🛠️ How Does It Work?

1. Launch OpenPack
2. Select your desired **browsers** by clicking the toggle buttons
3. Click **"Install Selected"** — browsers are installed silently and automatically
4. The **app selection** screen appears next — pick your apps
5. Click **"Install Selected Apps"** — done!

All installations run fully automatically in the background. No manual steps required.

---

## ⚙️ Technical Details

- **Framework:** [Avalonia UI](https://avaloniaui.net/) (Version 12.0.5)
- **Target Platform:** .NET 10.0 / Windows
- **Theme:** Fluent Design (follows system setting: Dark / Light)
- **Package Manager:** winget (Windows Package Manager)
- **Language:** C#

---

## 📋 Requirements

- Windows 10 / 11
- [winget](https://learn.microsoft.com/en-us/windows/package-manager/winget/) installed (included by default in modern Windows versions)
- .NET 10.0 Runtime

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
- **Uninstall mode** — easily remove apps you no longer need
- **Offline mode** — pre-download packages for devices without a permanent internet connection

> OpenPack grows continuously. New windows, new apps, and new categories will be added regularly over time. The goal is a complete setup tool for Windows — built by enthusiasts, for everyone.

---

## 🤝 Contributing

Ideas, suggestions, and pull requests are always welcome! Feel free to open an issue or submit a PR directly.

---

## 📄 License

This project is licensed under the [MIT License](LICENSE).
