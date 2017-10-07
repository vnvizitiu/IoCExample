namespace IoCExample
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public class IoCExampleImplementation
    {
        public T Create<T>()
        {
            return (T)this.Create(typeof(T));
        }

        private object Create(Type type)
        {
            ConstructorInfo constructorInfo = type.GetConstructors().Single();

            List<object> parameters = null;
            ParameterInfo[] constructorParameters = constructorInfo.GetParameters();
            foreach (ParameterInfo parameterInfo in constructorParameters)
            {
                parameters = parameters ?? new List<object>();
                parameters.Add(this.Create(parameterInfo.ParameterType));
            }

            object instance;
            if (parameters != null)
            {
                instance = constructorInfo.Invoke(parameters.ToArray());
            }
            else
            {
                instance = Activator.CreateInstance(type);
            }

            return instance;
        }
    }
}