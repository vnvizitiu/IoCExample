using System;

namespace IoCExample
{
    public class IoCExampleImplementation
    {
        public T Create<T>()
        {
            T instance = Activator.CreateInstance<T>();
            return instance;
        }
    }
}