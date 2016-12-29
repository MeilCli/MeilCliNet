---
Title: KLinq
Tags: [Kotlin]
Description: C#のLINQをKotlinで実装したライブラリ
IsProduct: true
IsLibrary: true
IsRecommended: true
LinkQiita: http://qiita.com/MeilCli/items/ee33c5c8065d79db6248
LinkGithub: https://github.com/MeilCli/KLinq
LinkBintray: https://bintray.com/meilcli/maven/KLinq/view#
---

C#のLINQをKotlinに移植したもの。KotlinでLINQ使いたい人向け(まだまだ改良の余地はあるけど

# 使用方法

## gradle
~~~
dependencies {
    compile 'net.meilcli:klinq:1.5'
}
~~~

## 使い方
KLinqを使用するには以下のインポート文が必要です

~~~ kotlin
import net.meilcli.klinq.*
~~~

またKLinq実装のIEnumerableを取り出すのにtoEnumerableなどの拡張関数を以下のように呼び出す必要があります

~~~ kotlin
arrayOf(1,2).toEnumerable().where{ x -> x%2==0 }.toList()
~~~

IEnumerableを取り出す拡張関数は以下のものが定義されてます

<div class="table-responsive">
  <table class="table table-striped">
    <thead>
      <tr>
        <th style="text-align: left">クラス</th>
        <th style="text-align: left">拡張関数</th>
        <th style="text-align: left">返り値</th>
      </tr>
    </thead>
    <tbody>
      <tr>
        <td style="text-align: left">Iterable<em>&lt;</em>TSource<em>&gt;</em></td>
        <td style="text-align: left">toEnumerable</td>
        <td style="text-align: left">IEnumerable<em>&lt;</em>TSource<em>&gt;</em></td>
      </tr>
      <tr>
        <td style="text-align: left">Sequence<em>&lt;</em>TSource<em>&gt;</em></td>
        <td style="text-align: left">toEnumerable</td>
        <td style="text-align: left">IEnumerable<em>&lt;</em>TSource<em>&gt;</em></td>
      </tr>
      <tr>
        <td style="text-align: left">IEnumerable<em>&lt;</em>TSource<em>&gt;</em></td>
        <td style="text-align: left">asEnumerable</td>
        <td style="text-align: left">IEnumerable<em>&lt;</em>TSource<em>&gt;</em></td>
      </tr>
      <tr>
        <td style="text-align: left">Array<em>&lt;</em>TSource<em>&gt;</em></td>
        <td style="text-align: left">toEnumerable</td>
        <td style="text-align: left">IEnumerable<em>&lt;</em>TSource<em>&gt;</em></td>
      </tr>
      <tr>
        <td style="text-align: left">BooleanArray</td>
        <td style="text-align: left">toEnumerable</td>
        <td style="text-align: left">IEnumerable<em>&lt;</em>Boolean<em>&gt;</em></td>
      </tr>
      <tr>
        <td style="text-align: left">CharArray</td>
        <td style="text-align: left">toEnumerable</td>
        <td style="text-align: left">IEnumerable<em>&lt;</em>Char<em>&gt;</em></td>
      </tr>
      <tr>
        <td style="text-align: left">ByteArray</td>
        <td style="text-align: left">toEnumerable</td>
        <td style="text-align: left">IEnumerable<em>&lt;</em>Byte<em>&gt;</em></td>
      </tr>
      <tr>
        <td style="text-align: left">ShortArray</td>
        <td style="text-align: left">toEnumerable</td>
        <td style="text-align: left">IEnumerable<em>&lt;</em>Short<em>&gt;</em></td>
      </tr>
      <tr>
        <td style="text-align: left">IntArray</td>
        <td style="text-align: left">toEnumerable</td>
        <td style="text-align: left">IEnumerable<em>&lt;</em>Int<em>&gt;</em></td>
      </tr>
      <tr>
        <td style="text-align: left">LongArray</td>
        <td style="text-align: left">toEnumerable</td>
        <td style="text-align: left">IEnumerable<em>&lt;</em>Long<em>&gt;</em></td>
      </tr>
      <tr>
        <td style="text-align: left">FloatArray</td>
        <td style="text-align: left">toEnumerable</td>
        <td style="text-align: left">IEnumerable<em>&lt;</em>Float<em>&gt;</em></td>
      </tr>
      <tr>
        <td style="text-align: left">DoubleArray</td>
        <td style="text-align: left">toEnumerable</td>
        <td style="text-align: left">IEnumerable<em>&lt;</em>Double<em>&gt;</em></td>
      </tr>
    </tbody>
  </table>
</div>

また特殊な用法として以下のような書き方も可能です

~~~ kotlin
(1..2).toEnumerable().where{ x -> x%2==0 }.toList()
~~~

<p class="alert alert-info">
その他の拡張関数においてはなるべくC#における実装に近づけたためC#の<a href="https://msdn.microsoft.com/ja-jp/library/9eekhta0(v=vs.110).aspx" class="alert-link">ドキュメント</a>が参考になります
</p>

## C#における実装との差異
- stdlibの拡張関数と関数名が被るためtoEnumeable拡張関数を呼び出す必要があります
- リフレクションを用いずにジェネリクスの規定値を得ることができないため以下の拡張関数においては規定値はnullになります
   - elementAtOrDefault
   - firstOrDefault
   - lastOrDefault
   - singleOrDefault
- ジェネリクスの型解決がうまくいかないため宣言が変更されているものがあります
   - fun <*TSource : Number*> IEnumerable<*TSource*>.average() : Double
   - fun <*TSource : Number*> IEnumerable<*TSource*>.sum() : Double
- 以下の拡張関数において型を明示した場合に差異が生じる可能性があります
   - inline fun <*TSource, reified TResult*> IEnumerable<*TSource*>.ofType(): IEnumerable<*TResult*>
   - inline fun <*TSource, reified TResult*> IEnumerable<*TSource*>.cast(): IEnumerable<*TResult*>

## Sequenceとの連携
Kotlin標準ライブラリにおける遅延実行リスト操作関数との連携のために以下のものが定義されています

<div class="table-responsive">
  <table class="table table-striped">
    <thead>
      <tr>
        <th style="text-align: left">クラス</th>
        <th style="text-align: left">拡張関数</th>
        <th style="text-align: left">返り値</th>
      </tr>
    </thead>
    <tbody>
      <tr>
        <td style="text-align: left">Sequence<em>&lt;</em>TSource<em>&gt;</em></td>
        <td style="text-align: left">toEnumerable</td>
        <td style="text-align: left">IEnumerable<em>&lt;</em>TSource<em>&gt;</em></td>
      </tr>
      <tr>
        <td style="text-align: left">IEnumerable<em>&lt;</em>TSource<em>&gt;</em></td>
        <td style="text-align: left">asSequence</td>
        <td style="text-align: left">Sequence<em>&lt;</em>TSource<em>&gt;</em></td>
      </tr>
    </tbody>
  </table>
</div>

## その他の拡張関数

利用率の高いforeach拡張関数が定義されてます

~~~ kotlin
fun <TSource> IEnumerable<TSource>.forEach(action: (TSource) -> Unit)
fun <TSource> IEnumerable<TSource>.forEach(action: (TSource, Int) -> Unit)
~~~

# ライセンス

このライブラリは[The MIT License](https://github.com/MeilCli/KLinq/blob/master/LICENSE)で公開しています

[Apache License, Version 2.0](http://www.apache.org/licenses/LICENSE-2.0)の[The Kotlin Standard Library](https://github.com/JetBrains/kotlin/tree/master/libraries/stdlib)を使用しています