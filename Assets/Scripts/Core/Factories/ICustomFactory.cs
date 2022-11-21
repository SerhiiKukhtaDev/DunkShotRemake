using Cysharp.Threading.Tasks;

namespace Core.Factories
{
    public interface IAsyncCustomFactory<TResult>
    {
        UniTask<TResult> Create();
    }
    
    public interface IAsyncCustomFactory<in TParam, TResult>
    {
        UniTask<TResult> Create(TParam param);
    }
    
    public interface ICustomFactory<out TResult>
    {
        TResult Create();
    }
    
    public interface ICustomFactory<in TParam, out TResult>
    {
        TResult Create(TParam param);
    }
}
