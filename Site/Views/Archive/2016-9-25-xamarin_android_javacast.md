---
Title: Xamarin.AndroidでWindowManagerをContextから取得する
Tags: [Xamarin, Android]
---

記事タイトルからしたら何それ簡単でしょ？って思ってしまいますがどうやらAndroid-Javaのようにいかないようです

~~~ csharp
(IWindowManager)Context.GetSystemService(Context.WindowService);
~~~

こんな感じでイケると思うじゃん？

~~~ csharp
Context.GetSystemService(Context.WindowService).JavaCast<IWindowManager>();
~~~

こうしろ()

っていう話です[フォーラム](https://forums.xamarin.com/discussion/7272/get-windowmanager-using-getsystemservice-where-is-the-class-windowmanager)すごい()
