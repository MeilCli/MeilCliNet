---
Title: JekyllのコードハイライトをRougeからhighlight.jsに変更
Tags: [highlight.js, Rouge, Jekyll, kramdown, Kotlin, F#]
Image: kotlin-highlight.PNG
---

このサイトのコードハイライトにRougeを使っていたのですがKotlinとF#がコードハイライトに対応していない事実にぶち当たった(つらい)

<p class="thumbnail"><img src="/Asset/Image/kotlin-rouge.PNG" alt="Kotlin Rouge" /></p>

なので軽量と噂の[highlight.js](https://highlightjs.org/)を使うことにしました

## Rougeを止める

まずはRougeを止めないといけませんが、JekyllではRougeがデフォルト有効になっているらしいのでkramdownのオプションで止めます

_config.ymlに以下の記述を追加

~~~ yml
kramdown:
  syntax_highlighter_opts:
    disable: true
~~~

## highlight.jsに変更

[ここ](https://highlightjs.org/download/)からダウンロードして

~~~ html
<link rel="stylesheet" href="/css/vs.css">
<script src="/js/highlight.pack.js"></script>
<script>
  hljs.initHighlightingOnLoad();
</script>
~~~

こんな感じで有効化すると

<p class="thumbnail"><img src="/Asset/Image/kotlin-highlight.PNG" alt="Kotlin Rouge" /></p>

やったぜ(背景は変えました)