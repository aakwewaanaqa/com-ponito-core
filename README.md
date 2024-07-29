# 讀我

## 第三方功能參考

請注意，這是一個收集資源的包，用於創建可能派上用場的庫。 <br>
由作者編寫的，但作者參考與蒐集原本的程式碼與資源。 <br>
如果您有任何更高級的功能，請訪問來源項目。 <br>

1. 漸變函式 : https://gizma.com/easing/
2. 解析設計模式 : https://youtu.be/klHyc9HQnNQ

## 銘謝清單

感謝您在網上開源提供的所有知識或腳本。

1. Uni 異步任務 : Yoshifumi Kawai, CEO/CTO of Cysharp, Inc.
2. 漸變函式 : Nic Mulvaney
3. 解析設計模式 in C# : Nicholas Blumhardt : NDC { Oslo }

## 問題與雷坑

### 1. 取消 PoTask.Yield()

```csharp
var before = Time.frameCount;
var cts    = new CancellationTokenSource();
// 這條程式碼是行不通的，還是會等待一幀，因為 yield return 的緣故
for (int i = 0; i < 5; i++) yield return PoTask.Yield(cts.Token).WaitAsCoroutine();
Assert.That(Time.frameCount, Is.EqualTo(before));
```