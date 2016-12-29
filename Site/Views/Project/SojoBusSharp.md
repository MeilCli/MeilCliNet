---
Title: SojoBusSharp
Tags: [Android, Windows, Xamarin, C#]
Description: 高槻市営バスの時刻見るアプリ
IsProduct: true
IsApplication: true
LinkGithub: https://github.com/MeilCli/SojoBusSharp
LinkGooglePlay: https://play.google.com/store/apps/details?id=net.meilcli.sojobus&hl=ja
---

高槻市営バスの時刻見るC#製のアプリ

# Android

<div class="embed-responsive embed-responsive-16by9">
  <iframe class="embed-responsive-item" width="560" height="315" src="https://www.youtube.com/embed/ERYpreBmiPQ" frameborder="0" allowfullscreen></iframe>
</div>

[ダウンロード](https://play.google.com/store/apps/details?id=net.meilcli.sojobus&hl=ja)

## ソースコード

ソースは[Github](https://github.com/MeilCli/SojoBusSharp)にあります  
SojoBus.AndroidフォルダにAndroid用コード、SojoBus.Coreフォルダに共通コードがあります

# Windows(WPF)

<p class="thumbnail"><img src="/Asset/Image/sojobus_modernwpf.PNG" alt="image" /></p>

[ダウンロード](https://github.com/MeilCli/SojoBusSharp/releases)

## ソースコード

ソースは[Github](https://github.com/MeilCli/SojoBusSharp)にあります  
SojoBus.ModernWPFフォルダにWPF用コード、SojoBus.Coreフォルダに共通コードがあります

# 開発方法
以下のものを用意してください

- VisualStudio 2015 Community
- Xamarin

<p class="alert alert-warning">
iOS,Mac向けアプリを作成する場合はMacが必要になります
</p>

## ソースコードについて
このアプリのソースコードはMVVMパターンに基づいて作成されています、これはクロスプラットフォームな開発に適したデザインパターンとなっています。  
各ビルドターゲット向けのソースは基本的にはViewを用意するだけとなります、ViewModelについては[BusViewModel.cs](https://github.com/MeilCli/SojoBusSharp/blob/master/SojoBus.Core/ViewModel/BusViewModel.cs)を確認してください。


また、ライセンス表示については[LicenseViewModel.cs](https://github.com/MeilCli/SojoBusSharp/blob/master/SojoBus.Core/ViewModel/LicenseViewModel.cs)を使用するといいです、追加のライセンス表示がある場合はLicenseViewModelクラスを継承しInitLicenseメソッドをオーバーライドしてください。

# ライセンス

このライブラリは[The MIT License](https://github.com/MeilCli/SojoBusSharp/blob/master/LICENSE)で公開しています

その他使用しているライブラリのライセンスについてはアプリ内のライセンス表示を確認してください。