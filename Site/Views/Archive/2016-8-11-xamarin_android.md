---
Title: Xamarin.Androidでのアプリ開発
Tags: [C#, Xamarin, Android]
---

ツイクラ開発の進捗、いいです

Javaでもいえることもありますが主にC#環境向けになります

今回は設定画面作成で出くわした問題への対処がメインになります

## PreferenceFragmentCompatを使う

###スタイルの指定

とりあえずNuGetでXamarin.Android.Support.v7.Preferenceをインストールしておく必要があります

そしてPreferenceFragmentCompatを使うにはスタイルの設定が必要なのですが調べて真っ先に出てくる

~~~ xml
<item name="preferenceTheme">@style/PreferenceThemeOverlay</item>
~~~

を指定するとデザインが崩れることがあります

これを回避する方法として調べると

~~~ xml
<item name="preferenceTheme">@style/PreferenceThemeOverlay.v14.Material</item>
~~~

が出てくるのですがこれはXamarin.Android.Support.v14.Preferenceが必要になります  
  
ということでスタイルのために追加インストールが必要になるという話でした。

### Preferenceの動的作成

つぎにコード上から設定画面を作るときの話をします

~~~ csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Support.V7.Preferences;
using Android.Support.V7.Widget;

namespace Setting {
    public class SettingFragment : PreferenceFragmentCompat {

        public override void OnCreatePreferences(Bundle savedInstanceState,string rootKey) {
            var screen = PreferenceManager.CreatePreferenceScreen(PreferenceManager.Context);
            
            screen.AddPreference(new PreferenceCategory(screen.Context));

            PreferenceScreen = screen;
        }

    }
}
~~~

ポイントとしては以下のようになります

- {:.list-group-item} `PreferenceManager.CreatePreferenceScreen(PreferenceManager.Context)`でPreferenceScreenを作成する(CreatePreferenceScreenでPreferenceManagerのContext設定できるなら引数なしのメソッドあってもいいんじゃ…)  
- {:.list-group-item} Preference作成にはPreferenceManager.Contextを指定する
- {:.list-group-item} PreferenceScreenには最後に設定する
{:.list-group}

### ListPreferenceにはKeyを必ず設定する
設定しないとダイアログで落ちます()

原因としてはPreferenceDialogFragmentCompat#onCreateで

~~~ java
mPreference = (DialogPreference) fragment.findPreference(key);
~~~

のようにKeyに対しての検索をしているからです

## 余談
DrawerLayoutってStatusBarColorを自前で設定してくれるんですね(これからはDrawerがないときにも重宝することになりそう)