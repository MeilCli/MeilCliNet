---
Title: XamarinのアップデートでResolveLibraryProjectImportsに失敗する
Tags: [Xamarin]
---

2016年9月中ごろのアップデートでいろいろ変わってしまったようだけどまぁビルドできてるしいいっかと思ってたら参照の追加やらで弄ってるとビルドできなくなったので調べてみた

## 症状

とりあえずビルドできないです

何回か試すと他のプロセスが動いてるからダメみたいに言われたり管理者でもアクセスできないフォルダをつくっちゃったりします()

## 解決法

[フォーラム](http://forums.xamarin.com/discussion/78725/resolvelibraryprojectimport-task-failed-unexpectedly)いずすごい

[こちら](https://releases.xamarin.com/stable-release-cycle-8-w-ios-10-and-xcode8-support/)のKnown Issuesの項目の[Xamarin.Android] – 44184のとこを参照してファイルを置き換えるだけです

これでだいたいは治ります


