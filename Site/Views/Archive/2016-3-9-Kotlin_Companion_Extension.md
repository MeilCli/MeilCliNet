---
Title: KotlinのCompanion Objectに拡張関数をつける
Tags: [Kotlin]
---

まず最初にKotlinで拡張関数を定義する方法

~~~ kotlin
fun Any.f(){
   // do something
}
~~~
Companion Objectの参照方法

~~~ kotlin
class A{
   companion object{
      fun a(){}
   }
}
~~~

~~~ kotlin
A.a()
A.Companion.a()
~~~
このどちらでもイケる  
これでJavaのstaticメソッドのようなことができる

そこで本題の拡張関数のつけかた

~~~ kotlin
fun A.Companion.f(){}
~~~

直接Companionを参照するところが肝心だが*定義したいクラスにCompanion Objectが定義されてる必要があることに注意*

~~~ kotlin
class B{}

fun B.Companion.f(){}//コンパイルエラー
~~~

それでわざわざこのような記事を書いたのはStringやLongなどのよく使うクラスにCompanion Objectの拡張関数をつけたかったからで…  
*結論：Companion Objectがなければできない*

そういうことなので自分で対応するクラスを定義しなければならない

~~~ kotlin
class StringC{
   companion object
}

class IntC{
   companion object
}

//こんな感じに定義する


//ちなみにinterfaceにもつけれるけど、さすがにこれは変態すぎでしょ
interface C{
   companion object
}
~~~