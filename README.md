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

OpenPack is just getting started — and this is only the beginning! Here's what's actively being worked on and planned for upcoming releases:

### 🎨 Improved Design
A full UI overhaul is on the way — cleaner layout, better visual structure, and a more polished overall look and feel.

### 📦 100+ Apps with Search
The app library is getting a massive expansion to over 100 apps across all categories. A built-in **search bar** will let you find exactly what you need instantly — no more scrolling through endless lists.

### 🌐 More Browsers
Additional browsers will be added to the browser selection, covering even more options for every kind of user.

### 🛡️ Better Detection & Error Handling
Improved system detection (Windows version, already installed apps, activation status) and proper error handling throughout — so OpenPack handles edge cases gracefully instead of failing silently.

### And Much More...
New windows, new categories, and new features will keep arriving over time. The goal is a complete, all-in-one setup tool for Windows — built by enthusiasts, for everyone.

> OpenPack grows continuously. Stay tuned for updates on the [Releases](../../releases) page.

---

## 🤝 Contributing

Ideas, suggestions, and pull requests are always welcome! Feel free to open an issue or submit a PR directly.

---

## 📄 License

This project is licensed under the [MIT License](LICENSE).
