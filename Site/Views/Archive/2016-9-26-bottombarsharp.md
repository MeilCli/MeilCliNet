---
Title: BottomBarのC#翻訳版を作ってみた
Tags: [C#, Xamarin, Android]
---

## きっかけ

Material Desginに画面下部に表示するタブが追加されたのはもう有名ですかね？

まだSupportLibraryに追加されていないんですよね

そこでいろいろ調べると[BottomBar](https://github.com/roughike/BottomBar)が出てきたりNuGetにC#翻訳版があるわけですよ

それでですね、その翻訳版を使ってみるとナビゲーションバーの透過に対応してない古いバージョンのを翻訳してることが分かったので翻訳してみようかなと

## できたもの

[BottomBarSharp](https://github.com/MeilCli/BottomBarSharp)

これですライセンスはMITにしました

翻訳元との変更点はプロパティ化とかevent構文にも対応したとかそんな感じです

## 注意

NuGetには登録してないので使う場合は自分でビルドしてくださいね