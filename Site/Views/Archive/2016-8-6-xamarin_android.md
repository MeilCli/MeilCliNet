---
Title: Xamarin.Androidでのアプリ開発の下準備
Tags: [C#, Xamarin, Android]
---

久しぶりの更新になりますがやっとツイクラ開発に取り組めるようになってきたのでノウハウ的なのをメモっておきます

Javaでもいえることもありますが主にC#環境向けになります

## AppCompatActivity使用時はAppCompatのThemeを利用する

これしないと落ちます()

~~~ xml
<?xml version="1.0" encoding="utf-8" ?>
<resources>
  <style name="AppTheme" parent="Theme.AppCompat.Light.DarkActionBar">
    <item name="colorPrimary">@color/ColorPrimary</item>
    <item name="colorPrimaryDark">@color/ColorPrimaryDark</item>
    <item name="colorAccent">@color/ColorAccent</item>
  </style>
  <style name="AppTheme.AppBarOverlay" parent="ThemeOverlay.AppCompat.Dark.ActionBar"/>
  <style name="AppTheme.PopupOverlay" parent="ThemeOverlay.AppCompat.Light"/>
</resources>
~~~

このような感じにしといてあげましょう

## Could not connect to the Debugger

このような感じのエラーが出てデバックできなければHyper-Vの設定を変えてやると直ることがあります

~~~ nohighlight
設定 => プロセッサ => 互換性 => プロセッサバージョンが異なる物理コンピューターへ移行する　をオンに
~~~

## 独自Applicationクラスを利用する

~~~ csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;


namespace Test {
    [Application(Icon = "@drawable/icon",Label ="Test",Theme = "@style/AppTheme")]
    public class TestApplication :Application{

        public TestApplication(IntPtr javaReference,JniHandleOwnership transfer) : base(javaReference,transfer) {
        }

        public override void OnCreate() {
            base.OnCreate();

        }
    }
}
~~~

重要なのはApplication属性をつけることとコンストラクタを明示的に書き足すことです

ついでのITestApplicationインタフェースを作って実装するとクロスプラットフォーム開発的にはいいかもしれないです

## ライフサイクルをEventHandlerで揃える

ActivityとFragmentでライフサイクルに微妙な差がありますが基底クラスで揃えるといいかもしれません(まだ自分でも試してる段階です)

~~~ csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Test {

    public delegate void LifeCycleEvent(LifeCycleEventArgs args);

    public class LifeCycleEventArgs {

        public string EventName { get; }

        public Bundle State { get; }

        public Intent Intent { get; }

        public LifeCycleEventArgs(string eventName,Bundle state = null,Intent intent = null) {
            EventName = eventName;
            State = state;
        }
    }

    public interface ILifeCycle {

        event LifeCycleEvent OnCreateEventHandler;

        event LifeCycleEvent OnDestoryEventHandler;

        event LifeCycleEvent OnResumeEventHandler;

        event LifeCycleEvent OnPauseEventHandler;

        event LifeCycleEvent OnSaveInstanceStateEventHandler;

        event LifeCycleEvent OnRestoreInstanceStateEventHandler;

        event LifeCycleEvent OnNewIntentEventHandler;
    }
}
~~~

~~~ csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V7.App;
using Twichirp.Core.App;
using SFragment = Android.Support.V4.App.Fragment;

namespace Test {

    public abstract class BaseActivity : AppCompatActivity ,ILifeCycle{ 

        public event LifeCycleEvent OnCreateEventHandler;
        public event LifeCycleEvent OnDestoryEventHandler;
        public event LifeCycleEvent OnPauseEventHandler;
        public event LifeCycleEvent OnRestoreInstanceStateEventHandler;
        public event LifeCycleEvent OnResumeEventHandler;
        public event LifeCycleEvent OnSaveInstanceStateEventHandler;
        public event LifeCycleEvent OnNewIntentEventHandler;

        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);
            OnViewCreate(savedInstanceState);
            OnCreateEventHandler?.Invoke(new LifeCycleEventArgs(nameof(OnCreate),savedInstanceState));
            if(savedInstanceState != null) {
                OnRestoreInstanceStateEventHandler?.Invoke(new LifeCycleEventArgs(nameof(OnCreate),savedInstanceState));
            }
        }

        protected abstract void OnViewCreate(Bundle savedInstanceState);

        protected override void OnDestroy() {
            base.OnDestroy();
            OnDestoryEventHandler?.Invoke(new LifeCycleEventArgs(nameof(OnDestroy)));
        }

        protected override void OnResume() {
            base.OnResume();
            OnResumeEventHandler?.Invoke(new LifeCycleEventArgs(nameof(OnResume)));
        }

        protected override void OnPause() {
            base.OnPause();
            OnPauseEventHandler?.Invoke(new LifeCycleEventArgs(nameof(OnPause)));
        }

        protected override void OnSaveInstanceState(Bundle outState) {
            base.OnSaveInstanceState(outState);
            OnSaveInstanceStateEventHandler?.Invoke(new LifeCycleEventArgs(nameof(OnSaveInstanceState),outState));
        }

        protected override void OnNewIntent(Intent intent) {
            base.OnNewIntent(intent);
            OnNewIntentEventHandler?.Invoke(new LifeCycleEventArgs(nameof(OnNewIntent),intent: intent));
            foreach(SFragment fragment in SupportFragmentManager.Fragments) {
                if(fragment is BaseFragment) {
                    (fragment as BaseFragment).OnNewIntent(intent);
                }
            }
        }
    }
}
~~~

~~~ csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using SFragment = Android.Support.V4.App.Fragment;

namespace Test{
    public abstract class BaseFragment : SFragment ,ILifeCycle{
        
        public event LifeCycleEvent OnCreateEventHandler;
        public event LifeCycleEvent OnDestoryEventHandler;
        public event LifeCycleEvent OnPauseEventHandler;
        public event LifeCycleEvent OnRestoreInstanceStateEventHandler;
        public event LifeCycleEvent OnResumeEventHandler;
        public event LifeCycleEvent OnSaveInstanceStateEventHandler;
        public event LifeCycleEvent OnNewIntentEventHandler;

        public override void OnActivityCreated(Bundle savedInstanceState) {
            base.OnActivityCreated(savedInstanceState);
            OnCreateEventHandler?.Invoke(new LifeCycleEventArgs(nameof(OnActivityCreated),savedInstanceState));
            if(savedInstanceState != null) {
                OnRestoreInstanceStateEventHandler?.Invoke(new LifeCycleEventArgs(nameof(OnActivityCreated),savedInstanceState));
            }
        }

        public override void OnDestroy() {
            base.OnDestroy();
            OnDestoryEventHandler?.Invoke(new LifeCycleEventArgs(nameof(OnDestroy)));
        }

        public override void OnResume() {
            base.OnResume();
            OnResumeEventHandler?.Invoke(new LifeCycleEventArgs(nameof(OnResume)));
        }

        public override void OnPause() {
            base.OnPause();
            OnPauseEventHandler?.Invoke(new LifeCycleEventArgs(nameof(OnPause)));
        }

        public override void OnSaveInstanceState(Bundle outState) {
            base.OnSaveInstanceState(outState);
            OnSaveInstanceStateEventHandler?.Invoke(new LifeCycleEventArgs(nameof(OnSaveInstanceState),outState));
        }

        public void OnNewIntent(Intent intent) {
            OnNewIntentEventHandler?.Invoke(new LifeCycleEventArgs(nameof(OnNewIntent),intent: intent));
        }

    }
}
~~~

このようにしてControllerクラス的なのからILifeCycleに対してイベント処理を登録すればActivity/Fragment両方で統一した動作をさせることが楽にできると思います(統一させる必要があることも少ないでしょうが)
