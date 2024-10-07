# CHANGELOG

## 1.1.8 -> 1.1.9 (...)

### Added
```csharp
class  PoCanvas.cs        ~ string cameraTag; ~ 加入 cameraTag 來針對需要找到指定的 Camera
                          ~ void OnEnable()   ~ 在開啟時找到需要的攝影機
class  WatcherViewUnit.cs ~ 現在不再用繼承的方式實作 UI 了，請改用 Unit 去組合
class  OnChangeFactory.cs ~ 這是透過訂閱 StateManager 的方式來達成 OnChangeFactory 的觸發                      
class  OnChange.cs        ~ 這是透過訂閱 StateManager 的方式來達成 OnChange 的觸發
class  PoStateManager.cs  ~ 管理 UI 的 Statable 與 StateWatcher                        
class  SingletonUnit.cs   ~ 現在不再用繼承的方式實作 Singleton 了，請改用 Unit 去組合    
class  SerializableSet.cs ~ 實作一個可以序列化的 Dictionary 簡稱 Set 啦    
class  PoAudioSettings.cs ~ 聲音、音檔、音效、音樂管理現在有呼叫時的設定了                          
struct AudioCueBinds.cs   ~ 聲音、音檔、音效、音樂管理現在有呼叫時的設定了
struct AudioCue.cs        ~ 聲音、音檔、音效、音樂管理現在有呼叫時的設定了
class  PoSettings.cs      ~ 預計加入一些 com.ponito.core 插件的設定
```

```csharp
class  AudioCueBindsDrawer.cs [CustomPropertyDrawer]
struct FloatRangeDrawer.cs    [CustomPropertyDrawer]
```

### Example
- 舉例來說怎麼樣使用 WatcherViewUnit
```csharp
public class MainView : MonoBehaviour
{
    [SerializeField] private PoButton start;
    [SerializeField] private PoButton settings;

    /// 用 unit 去組合，就可以讓一個功能不用被繼承的方式實作
    /// 如此以來也知道在幹嘛，不會被隱藏起來！
    private WatcherViewUnit unit;

    private void Awake()
    {
        unit = new WatcherViewUnit(OnChange, "MainView");

        var manager = PoStateManager.Singleton;
        manager.Add(new MainViewState());         // 先加入狀態
        manager.TryWatch(MainViewState.ID, unit); // 再用 unit 訂閱觀察狀態是否改變

        start.onClick.RemoveAllListeners(); // 避免一個按鈕被啟動很多次
        start.onClick.AddListener(() =>
        {
            // mutate 就是異變狀態的意思，狀態本身自己有資料可以檢查是否異變成功
            // 所以就算是按鈕可以無限按，但狀態本身還是有良好的檢查機制
            manager.TryMutate(MainViewState.ID, "-> start");
        });

        settings.onClick.RemoveAllListeners(); // 避免一個按鈕被啟動很多次
        settings.onClick.AddListener(() =>
        {
            // mutate 就是異變狀態的意思，狀態本身自己有資料可以檢查是否異變成功
            // 所以就算是按鈕可以無限按，但狀態本身還是有良好的檢查機制
            manager.TryMutate(MainViewState.ID, "-> settings");
        });
    }

    private void OnChange(Statable state, object args)
    {
        if (state is not MainViewState) return;

        switch ((string)args)
        {
            case "-> start": /* 這裡可以用來寫開始遊戲喔 */  return;
            case "-> settings": /* 這裡可以用來進入設定 */ return;
        }
    }

    private void OnDestroy()
    {
        var manager = PoStateManager.Singleton;
        manager.TryUnwatch(MainViewState.ID, unit);
        manager.Remove(MainViewState.ID);
    }
}
```

### Changed

```csharp
class PoTaskViewMono.cs [Obselete] ~ 淘汰了喔，盡量用 WatcherViewUnit 去組合功能，這樣繼承才不會亂掉
class PoAudioManager.Inits.cs      ~ private static readonly SingletonUnit<PoAudioManager> singleton;
                                   ~ 以 SingletonUnit 組合完成 Singleton 功能
struct FloatRange.cs               ~ [SerializedField] private float min
                                   ~ [SerializedField] private float max
                                   ~ 使 FloatRange 可以被序列化              
```


## 1.1.7 -> 1.1.8 (2024/09/10)

### Added

```csharp
class  PoTaskView.cs ~ PoTask InnerShow(object args, CancellationToken ct = default)
                     ~ PoTask InnerHide(object args, CancellationToken ct = default)
                     ~ 盡量不要再複寫 Show 或 Hide 了，容易漏掉重要的檢查
struct Punch.cs      ~ 特別的動畫，參照自 DOTween
```

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
README.md                 ~ 更新說明文件
class  AsyncsTests.cs     ~ 整理不會過的測試
class  AsyncsTestsMono.cs ~ 新增取消的測試
class  PoTask.Factory.cs  ~ Yield(CancellationToken ct = default)
                            Delay(int milliseconds, CancellationToken ct = default)
                            Delay(float seconds, CancellationToken ct = default)
                            WaitWhile(Func<bool> predicate, CancellationToken ct = default)
                            WaitUntil(Func<bool> predicate, CancellationToken ct = default)
                            Create(IEnumerator ie, CancellationToken ct = default)
struct PathNameInfo.cs    ~ 修正命名空間
```

## 1.1.4 -> 1.1.5 (2024/07/28)

### Added

```csharp
class CoroutineResult.cs ~ 給與 WaitAsCoroutine 擁有回傳值的方式
class AsyncsTestsMono.cs ~ 把測試加入 Monobehaviour 比單純在 Editor 中測試更有說服力
```

### Changed

```csharp
class AsyncsTests.cs ~ 把測試加入 Monobehaviour 比單純在 Editor 中測試更有說服力
```

### Removed

```csharp
class     Promise.cs           ~ 非常明確之後不再使用的類別
class     Promise<T>.cs        ~ 非常明確之後不再使用的類別
class     PromiseException.cs  ~ 非常明確之後不再使用的類別
interface PromiseState.cs      ~ 非常明確之後不再使用的類別
class     UnityWebResponse.cs  ~ 非常明確之後不再使用的類別
class     CoroutineRunner.cs   ~ 非常明確之後不再使用的類別
                               ~ 非常明確之後不再使用的類別
class     ReflectAwait.cs      ~ 非常明確之後不再使用的類別
class     TaskAwait.cs         ~ 非常明確之後不再使用的類別
class     ValueTaskAwait.cs    ~ 非常明確之後不再使用的類別
class     ValueTaskAwait<T>.cs ~ 非常明確之後不再使用的類別   
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
class Exts.cs ~ async Delay(this float seconds, CancellationToken ct)
              ~ Copilot 發現這樣用是個好點子
```

### Example
```csharp
{
    var cts = new CancellationTokenSource(); // 建立一個取消標旗
    await 1.5f.Delay(cts.Token);             // 1.5 秒後執行，或是取消後立即執行
}
```

### Changed

```csharp
class TimeState.cs      ~ async Play(float duration, Action<float> onAnimate, CancellationToken ct)
                        ~ async Play(float to, float duration, Action<float> onAnimate, CancellationToken ct)
                        ~ async Play(float from, float to, float duration, Action<float> onAnimate, CancellationToken ct)
class PoAudioManager.cs ~ async Play(AudioClip clip, AudioPlayType type, bool isOneShot, CancellationToken ct)
                        ~ async Stop(AudioPlayType type, float duration, CancellationToken ct)
                        ~       Play, Stop 加入 CancellationToken 以取消播放
```

## 1.1.1 -> 1.1.2 (2024/07/20)

### Added

```csharp
struct YieldAwait.cs     ~ CancellationTokenSource
struct DelayAwait.cs     ~ CancellationTokenSource
struct PredicateAwait.cs ~ CancellationTokenSource
class  Exts.cs           ~ Chain   (this PoTask t) 
                         ~ Chain<T>(this PoTask t)
                         ~ 比較直觀，比 ContinueWith 稍微好一點
```

### Changed

```csharp
class Exts.cs ~ async AsPoTask   (this ValueTask vt)    => PoTask 
              ~ async AsPoTask<T>(this ValueTask<T> vt) => PoTask<T> 
              ~ async AsPoTask   (this Task t)          => PoTask 
              ~ async AsPoTask<T>(this Task<T> t)       => PoTask<T> 
              ~       比較直覺。。。
```

## 1.1.0 -> 1.1.1 (2024/07/19)

### Added

```csharp
class PoTaskViewMono.cs ~ 為了序列化成為 Monobehaviour
```

### Changed

```csharp
interface AsyncView ~ 現在叫做 PoTaskView
```
