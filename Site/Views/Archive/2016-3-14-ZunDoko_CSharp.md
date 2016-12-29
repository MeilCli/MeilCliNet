---
Title: ズンドコキヨシ with C#
Tags: [C#,ズンドコキヨシ]
---

Kotlinで書いたらもちろんC#でもやらないとね！

## 元ネタ
<blockquote class="twitter-tweet" data-lang="ja"><p lang="ja" dir="ltr">Javaの講義、試験が「自作関数を作り記述しなさい」って問題だったから<br>「ズン」「ドコ」のいずれかをランダムで出力し続けて「ズン」「ズン」「ズン」「ズン」「ドコ」の配列が出たら「キ・ヨ・シ！」って出力した後終了って関数作ったら満点で単位貰ってた</p>&mdash; てくも (@kumiromilk) <a href="https://twitter.com/kumiromilk/status/707437861881180160">2016年3月9日</a></blockquote>
<script async src="//platform.twitter.com/widgets.js" charset="utf-8"></script>

## ソース
~~~ csharp
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;

namespace ZunDoko {
    class Program {
        static IEnumerable<string> ZunDoko() {
            var random = new Random();
            while(true)
                yield return random.Next(2) == 1 ? "ズン" : "ドコ";
        }

        static void Main(string[] args) {
            int count = 0;
            foreach(string s in ZunDoko().Select(x => { WriteLine(x); return x; })) {
                if(s == "ズン") {
                    count++;
                } else if(count >= 4 && s == "ドコ") {
                    WriteLine("キ・ヨ・シ！");
                    break;
                } else {
                    count = 0;
                }
            }
        }
    }
}
~~~

## 動作例

~~~
ズン
ズン
ドコ
ズン
ズン
ドコ
ズン
ズン
ドコ
ドコ
ズン
ズン
ドコ
ドコ
ズン
ズン
ズン
ドコ
ズン
ズン
ズン
ズン
ズン
ドコ
キ・ヨ・シ！
~~~

デバッグしてて気づいたんだけどズンズンズンズンズンドコを許すかどうかで処理変わるね(自分のやつ許すかどうかを深く考えてない)