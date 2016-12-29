---
Title: AndroidStudioとGithub連携(CloneとPush)
Tags: [AndroidStudio, Github]
---

自作ライブラリHKJsonをいちいちGithub for WindowsでPushするのがめんどくさくなったからAndroidStudioとGithubを連携した話  
ちなみに日本語化環境です

## Cloneの仕方

~~~
バージョン管理->バージョン管理からチェックアウト->Github
~~~
でCloneしたいリポジトリを選べばGithubからCloneできます  
このとき開発していたプロジェクトのディレクトリがあると重複してCloneできないので消すか他の場所に退避させる

## Pushの仕方

~~~
バージョン管理->enable version control integration
~~~
を押し出てきたポップアップでgitを選択

あとは下のバージョン管理タブでCommitとPushをするだけ