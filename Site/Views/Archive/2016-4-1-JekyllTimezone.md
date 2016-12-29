---
Title: GithubPagesのJekyllでPostが反映されない
Tags: [Jekyll, Github Pages]
---

記事を書いていて気付いたのですがJekyllは未来の日付の記事を処理しないようです

未来の日付の記事を書かなければいいのですが**当日の記事をGithub Pagesで上げる場合は注意が必要です**

Github Pagesのタイムゾーンが日本のタイムゾーンではないので時差の影響で未来の日付の記事として扱われることがあります

## 対策

`_config.yml`に以下の記述をするだけ

~~~ yml
timezone: Japan
~~~