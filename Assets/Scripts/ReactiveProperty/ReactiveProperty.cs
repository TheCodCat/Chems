using System;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveProperty<T>
{
    public event Action<T> Changed;
    private T _value;
    private EqualityComparer<T> comparer;
    public T Value
    {
        get { return _value; }
        set
        {
            var old = _value;
            _value = value;
            if (!comparer.Equals(old, _value))
                Changed?.Invoke(value);
        }
    }

    public ReactiveProperty() : this (default(T), EqualityComparer<T>.Default)
    {

    }
    
    public ReactiveProperty(T value) : this(value, EqualityComparer<T>.Default)
    {
        _value = value;
    }
    public ReactiveProperty(T value, EqualityComparer<T> comparer)
    {
       _value = value;
        this.comparer = comparer;
    }
}
