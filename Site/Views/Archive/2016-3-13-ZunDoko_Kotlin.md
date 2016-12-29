---
Title: ズンドコキヨシ with Kotlin
Tags: [Kotlin,ズンドコキヨシ]
LinkQiita: http://qiita.com/MeilCli/items/135e85b2de2146c64891
---

Qiitaでは遊びがてらなやつを上げときましたが小手調べに普通にやったのをこっちで出しときます

## 元ネタ
<blockquote class="twitter-tweet" data-lang="ja"><p lang="ja" dir="ltr">Javaの講義、試験が「自作関数を作り記述しなさい」って問題だったから<br>「ズン」「ドコ」のいずれかをランダムで出力し続けて「ズン」「ズン」「ズン」「ズン」「ドコ」の配列が出たら「キ・ヨ・シ！」って出力した後終了って関数作ったら満点で単位貰ってた</p>&mdash; てくも (@kumiromilk) <a href="https://twitter.com/kumiromilk/status/707437861881180160">2016年3月9日</a></blockquote>
<script async src="//platform.twitter.com/widgets.js" charset="utf-8"></script>

## ソース
~~~ kotlin
import java.util.*

fun main(args: Array<String>) {
    val zundoko = arrayOf("ズン", "ドコ")
    val random = Random()
    var count = 0
    while (true) {
        if (zundoko[random.nextInt(2)].let {
            println(it)
            when (count) {
                0, 1, 2, 3 -> {
                    if (it == "ズン") {
                        count++
                        false
                    } else {
                        count = 0
                        false
                    }
                }
                else -> {
                    if (it == "ドコ") {
                        println("キ・ヨ・シ！")
                        true
                    } else {
                        count = 0
                        false
                    }
                }
            }
        }) break
    }
}
~~~

## 動作例

~~~
ドコ
ドコ
ズン
ドコ
ドコ
ドコ
ドコ
ズン
ズン
ドコ
ズン
ドコ
ズン
ドコ
ズン
ズン
ズン
ズン
ドコ
キ・ヨ・シ！
~~~