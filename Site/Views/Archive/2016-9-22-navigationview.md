---
Title: NavigationViewが継承しているScrimInsetsFrameLayoutについて
Tags: [Android]
---

ツイクラ開発の進捗、まぁまぁです(夏休み終わってしまった顔)

今回はNavigationViewについてです

NavigationViewをDrawerLayoutに入れて使っていたのですが横画面にするとNavigationViewの右端にShadowがかかってしまうことに遭遇しました

調べたらNavigationViewは透過時のStatusBar,NavigationBarのとこに影をつけてくれるScrimInsetsFrameLayoutを継承してるようで、それが動作していたというわけでした

~~~ xml
    app:insetForeground="#00000000"
~~~

これでScrimInsetsFrameLayoutの影を透明にすれば解決します
