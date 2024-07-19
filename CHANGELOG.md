# CHANGELOG

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
