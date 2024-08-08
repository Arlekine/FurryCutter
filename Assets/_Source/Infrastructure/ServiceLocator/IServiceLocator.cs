public interface IServiceLocator<T1>
{
    T2 GetService<T2>() where T2 : T1;
}