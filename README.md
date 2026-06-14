# Prime Video Speed Controller

**Prime Video Speed Controller** is a small Windows helper that opens Prime Video in a dedicated Microsoft Edge app window and adds a simple playback speed control to the video player.

It is designed for people who want a clean speed selector without modifying the official Prime Video app, bypassing DRM, or installing a browser extension.

Author: **𝓐.𝓒.𝓑**

## What It Does

- Opens Prime Video in an Edge app-style window.
- Adds a compact speed button only when a real video player is detected.
- Lets you choose common speeds: `0.5x`, `1x`, `1.25x`, `1.5x`, `1.75x`, `2x`.
- Supports fine adjustment with `+` and `-` in `0.1x` steps.
- Keeps applying the selected speed if Prime Video resets it.
- Allows dragging the speed button to a preferred position.
- Remembers the selected speed and button position locally.

## What It Does Not Do

- It does not modify the official Prime Video desktop app.
- It does not bypass DRM, download video, remove restrictions, or access private account data.
- It does not send telemetry or communicate with any server other than the local Edge debugging endpoint it starts.

## Requirements

- Windows 10 or Windows 11
- Microsoft Edge
- .NET 8 Runtime or .NET 8 SDK

## Quick Start

1. Download or clone the project.
2. Run `run.cmd`.
3. Sign in to Prime Video in the window that opens.
4. Start a video.
5. Use the speed button near the player controls.

If a published executable exists, `run.cmd` starts it. Otherwise, it runs the project with `dotnet run`.

## Controls

- Click the speed button to open the menu.
- Drag the speed button to move it.
- `+` increases speed by `0.1x`.
- `-` decreases speed by `0.1x`.
- `]` increases speed by `0.1x`.
- `[` decreases speed by `0.1x`.
- `\` resets speed to `1x`.

## Build From Source

```powershell
dotnet build
dotnet run
```

To create a local publish output:

```powershell
dotnet publish -c Release -o publish
```

## Privacy And Security

The app starts Edge with a dedicated local profile and a local remote debugging port. It only connects to `127.0.0.1` to inject the speed-control script into Prime Video pages opened by that Edge instance.

The injected script stores two values in the browser profile:

- selected playback speed
- speed button position

No passwords, tokens, cookies, watch history, or account details are read, stored, or transmitted by this project.

## Limitations

Prime Video can change its player implementation at any time. If the player DOM changes significantly, the speed button may need a small update.

Taskbar icon behavior is controlled by Windows and Edge when using Edge app mode. Playback reliability is prioritized over forcing a custom taskbar shell.

## License

Released under the MIT License. See [LICENSE](LICENSE).

---

## Türkçe

**Prime Video Speed Controller**, Prime Video'yu Microsoft Edge'in uygulama penceresinde açan ve video oynatıcıya sade bir hız kontrolü ekleyen küçük bir Windows yardımcı aracıdır.

### Özellikler

- Video oynatıcı açıldığında hız butonu görünür.
- `0.5x`, `1x`, `1.25x`, `1.5x`, `1.75x`, `2x` hızları seçilebilir.
- `+` ve `-` ile hız `0.1x` adımlarla ayarlanabilir.
- Hız butonu sürüklenebilir ve konumu hatırlanır.
- Seçilen hız yerel olarak saklanır.

### Kullanım

1. `run.cmd` dosyasını çalıştırın.
2. Açılan Prime Video penceresinde giriş yapın.
3. Bir video başlatın.
4. Hız butonuna tıklayarak istediğiniz hızı seçin.

Bu proje resmi Prime Video uygulamasını değiştirmez, DRM'i aşmaz ve video indirmez.

---

## Español

**Prime Video Speed Controller** es una pequeña herramienta para Windows que abre Prime Video en una ventana de aplicación de Microsoft Edge y agrega un control de velocidad al reproductor.

### Funciones

- Muestra el botón solo cuando hay un video real.
- Permite elegir `0.5x`, `1x`, `1.25x`, `1.5x`, `1.75x` y `2x`.
- Permite ajustar la velocidad en pasos de `0.1x`.
- El botón se puede arrastrar y recuerda su posición.
- La velocidad elegida se guarda localmente.

### Uso

1. Ejecuta `run.cmd`.
2. Inicia sesión en Prime Video.
3. Reproduce un video.
4. Haz clic en el botón de velocidad y elige una opción.

El proyecto no modifica la aplicación oficial, no evita DRM y no descarga contenido.

---

## Deutsch

**Prime Video Speed Controller** ist ein kleines Windows-Hilfsprogramm, das Prime Video in einem Microsoft-Edge-Appfenster öffnet und dem Videoplayer eine einfache Geschwindigkeitssteuerung hinzufügt.

### Funktionen

- Der Button erscheint nur, wenn ein echter Videoplayer erkannt wird.
- Unterstützt `0.5x`, `1x`, `1.25x`, `1.5x`, `1.75x` und `2x`.
- Feinanpassung in `0.1x`-Schritten.
- Der Button kann verschoben werden und merkt sich seine Position.
- Die gewählte Geschwindigkeit wird lokal gespeichert.

### Verwendung

1. `run.cmd` starten.
2. Bei Prime Video anmelden.
3. Ein Video abspielen.
4. Über den Geschwindigkeitsbutton die gewünschte Geschwindigkeit wählen.

Dieses Projekt verändert nicht die offizielle Prime-Video-App, umgeht kein DRM und lädt keine Videos herunter.

---

## Français

**Prime Video Speed Controller** est un petit utilitaire Windows qui ouvre Prime Video dans une fenêtre d'application Microsoft Edge et ajoute un contrôle de vitesse au lecteur vidéo.

### Fonctions

- Le bouton apparaît uniquement lorsqu'un lecteur vidéo est détecté.
- Vitesses disponibles : `0.5x`, `1x`, `1.25x`, `1.5x`, `1.75x`, `2x`.
- Réglage fin par pas de `0.1x`.
- Le bouton peut être déplacé et sa position est mémorisée.
- La vitesse choisie est enregistrée localement.

### Utilisation

1. Lancez `run.cmd`.
2. Connectez-vous à Prime Video.
3. Démarrez une vidéo.
4. Cliquez sur le bouton de vitesse et choisissez une valeur.

Ce projet ne modifie pas l'application officielle, ne contourne pas les DRM et ne télécharge pas de contenu.

---

## Português

**Prime Video Speed Controller** é uma pequena ferramenta para Windows que abre o Prime Video em uma janela de aplicativo do Microsoft Edge e adiciona um controle de velocidade ao player.

### Recursos

- O botão aparece somente quando um vídeo real é detectado.
- Velocidades disponíveis: `0.5x`, `1x`, `1.25x`, `1.5x`, `1.75x`, `2x`.
- Ajuste fino em passos de `0.1x`.
- O botão pode ser arrastado e lembra a posição.
- A velocidade escolhida é salva localmente.

### Uso

1. Execute `run.cmd`.
2. Entre na sua conta do Prime Video.
3. Reproduza um vídeo.
4. Clique no botão de velocidade e escolha uma opção.

Este projeto não modifica o aplicativo oficial, não contorna DRM e não baixa vídeos.
