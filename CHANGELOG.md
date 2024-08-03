# CHANGELOG

## 1.1.6 -> 1.1.7 (2024/08/03)

### Changed

```csharp
PoTask.Yield()     ~ Controls.Yield()
PoTask.Delay()     ~ Controls.Delay()
PoTask.WaitUntil() ~ Controls.WaitUntil()
PoTask.WaitWhile() ~ Controls.WaitWhile()
```

## 1.1.5 -> 1.1.6 (2024/07/29)

### Added

```csharp
//    提供預先取消的 Movable
class CancelAwait.cs
```

### Changed

```csharp
README.md                ~ // 更新說明文件
class AsyncsTests.cs     ~ // 整理不會過的測試
class AsyncsTestsMono.cs ~ // 新增取消的測試
class PoTask.Factory.cs  ~ Yield(CancellationToken ct = default)
                           Delay(int milliseconds, CancellationToken ct = default)
                           Delay(float seconds, CancellationToken ct = default)
                           WaitWhile(Func<bool> predicate, CancellationToken ct = default)
                           WaitUntil(Func<bool> predicate, CancellationToken ct = default)
                           Create(IEnumerator ie, CancellationToken ct = default)
struct PathNameInfo.cs   ~ // 修正命名空間
```

## 1.1.4 -> 1.1.5 (2024/07/28)

### Added

```csharp
//    給與 WaitAsCoroutine 擁有回傳值的方式
class CoroutineResult.cs
//    把測試加入 Monobehaviour 比單純在 Editor 中測試更有說服力
class AsyncsTestsMono.cs
```

### Changed

```csharp
//    把測試加入 Monobehaviour 比單純在 Editor 中測試更有說服力
class AsyncsTests.cs
```

### Removed

```csharp
//        非常明確不再使用的類別
class     Promise.cs
class     Promise<T>.cs
class     PromiseException.cs
interface PromiseState.cs
class     UnityWebResponse.cs
class     CoroutineRunner.cs

class     ReflectAwait.cs
class     TaskAwait.cs
class     ValueTaskAwait.cs
class     ValueTaskAwait<T>.cs    
```

## 1.1.3 -> 1.1.4 (2024/07/27)

### Added

```csharp
class MovableRunnerEditor.cs
interface ProgressMovalbe.cs
```

## 1.1.2 -> 1.1.3 (2024/07/23)

### Added

```csharp
///                   Copilot 發現這樣用是個好點子
class Exts.cs ~ async Delay(this float seconds, CancellationToken ct)
/// 例如
{
    var cts = new CancellationTokenSource(); // 建立一個取消標旗
    await 1.5f.Delay(cts.Token);             // 1.5 秒後執行，或是取消後立即執行
}
```

### Changed

```csharp
///                             Play, Stop 加入 CancellationToken 以取消播放
class TimeState.cs      ~ async Play(float duration, Action<float> onAnimate, CancellationToken ct)
                          async Play(float to, float duration, Action<float> onAnimate, CancellationToken ct)
                          async Play(float from, float to, float duration, Action<float> onAnimate, CancellationToken ct)
class PoAudioManager.cs ~ async Play(AudioClip clip, AudioPlayType type, bool isOneShot, CancellationToken ct)
                          async Stop(AudioPlayType type, float duration, CancellationToken ct)
```

## 1.1.1 -> 1.1.2 (2024/07/20)

### Added

```csharp
struct YieldAwait.cs     + CancellationTokenSource
struct DelayAwait.cs     + CancellationTokenSource
struct PredicateAwait.cs + CancellationTokenSource
///                        Chain   比較直觀，比 ContinueWith 稍微好一點
class  Exts.cs           + Chain   (this PoTask t) 
                         + Chain<T>(this PoTask t)
```

### Changed

```csharp
///                                                        比較直覺。。。
class Exts.cs ~ async AsPoTask   (this ValueTask vt)    => PoTask 
              ~ async AsPoTask<T>(this ValueTask<T> vt) => PoTask<T> 
              ~ async AsPoTask   (this Task t)          => PoTask 
              ~ async AsPoTask<T>(this Task<T> t)       => PoTask<T> 
```

## 1.1.0 -> 1.1.1 (2024/07/19)

### Added

```csharp
/// 為了序列化 <see cref="PoTaskView"/>
class PoTaskViewMono.cs
```

### Changed

```csharp
interface rename AsyncView => PoTaskView
```
