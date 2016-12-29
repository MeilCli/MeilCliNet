---
Title: 暗号化ライブラリConceal導入でハマったこと
Tags: [Android, Java]
---



## Concealまとめ

- Facebookが作成 [Conceal](https://facebook.github.io/conceal/)
- ライセンスは修正BSDライセンス
- OS4.3以前では標準ライブラリより高速
- 既知のOS ver間の差異にも対応
- 暗号化アルゴリズムはAES-GCMでOpenSSLの実装を利用
- JNI経由でネイティブ実行
- 秘密鍵は初回利用時にSharedPreferencesに保存
- 暗号復号化には同じEntityを渡さねばならない
 - Entityは秘密鍵ではない

だいたいこんなところかな

## ハマったこと1

~~~ nohighlight
JNI DETECTED ERROR IN APPLICATION: JNI FindClass called with pending exception 'java.lang.NoSuchFieldError' thrown in java.lang.String java.lang.Runtime.nativeLoad(java.lang.String, java.lang.ClassLoader, java.lang.String):
...
~~~

Githubのissueを探していると[ClassNotFoundException Native Crash On Lollipop using Gradle/AAR #47](https://github.com/facebook/conceal/issues/47)を発見

どうやら原因はProgurdの難読化らしい

~~~ nohighlight
-keep class com.facebook.** { *; }
-keep interface com.facebook.** { *; }
~~~
で対応

## ハマったこと2

~~~ java
public static String encrypt(Context context, String text, String entity){
   Crypto crypto = new Crypto(new SharedPrefsBackedKeyChain(context),new SystemNativeCryptoLibrary());
   if(!crypto.isAvailable()){
      return "";
   }
   try{
      byte[] cipherText = crypto.encrypt(text.getBytes("utf-8"), new Entity(entity+".conceal"));
      return new String(cipherText,"utf-8");
   }catch(Exception e){
      e.printStackTrace();
      return "";
   }
}

public static String decrypt(Context context, String text, String entity){
   Crypto crypto = new Crypto(new SharedPrefsBackedKeyChain(context),new SystemNativeCryptoLibrary());
   if(!crypto.isAvailable()){
      return "";
   }
   try{
      byte[] cipherText = crypto.decrypt(text.getBytes("utf-8"), new Entity(entity+".conceal"));
      return new String(cipherText,"utf-8");
   }catch(Exception e){
      e.printStackTrace();
      return "";
   }
}
~~~

こんなソースで実装したら

~~~ nohighlight
com.facebook.crypto.cipher.NativeGCMCipherException: The message could not be decrypted successfully.It has either been tampered with or the wrong resource is being decrypted.
~~~

という例外吐いて復号できない

~~~ java
public static String test(Context context, String text, String entity){
   Crypto crypto = new Crypto(new SharedPrefsBackedKeyChain(context),new SystemNativeCryptoLibrary());
   if(!crypto.isAvailable()){
      return "";
   }
   try{
      byte[] cipherText = crypto.encrypt(text.getBytes("utf-8"), new Entity(entity+".conceal"));
      return new String(crypto.decrypt(cipherText,new Entity(entity+".conceal")),"utf-8");
   }catch(Exception e){
      e.printStackTrace();
      return "";
   }
}
~~~

を試すとこれは正常に動く

どうやら暗号化後のbyte[]→String→byte[]のところが問題らしいので

~~~ java

public static String encrypt(Context context, String text, String entity){
   Crypto crypto = new Crypto(new SharedPrefsBackedKeyChain(context),new SystemNativeCryptoLibrary());
   if(!crypto.isAvailable()){
      return "";
   }
   try{
      byte[] cipherText = crypto.encrypt(text.getBytes("utf-8"), new Entity(entity+".conceal"));
      return Base64.encodeToString(cipherText,Base64.DEFAULT);
   }catch(Exception e){
      e.printStackTrace();
      return "";
   }
}

public static String decrypt(Context context, String text, String entity){
   Crypto crypto = new Crypto(new SharedPrefsBackedKeyChain(context),new SystemNativeCryptoLibrary());
   if(!crypto.isAvailable()){
      return "";
   }
   try{
      byte[] cipherText = crypto.decrypt(Base64.decode(text,Base64.DEFAULT), new Entity(entity+".conceal"));
      return new String(cipherText,"utf-8");
   }catch(Exception e){
      e.printStackTrace();
      return "";
   }
}
~~~

Base64で対応した