using System;

public interface IPoolable<T> where T : IPoolable<T>
{
    public event Action<T> Destroyed;
}