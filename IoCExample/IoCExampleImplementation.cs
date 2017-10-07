namespace IoCExample
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public class IoCExampleImplementation
    {
        private Dictionary<Type, Type> mapping;

        public Dictionary<Type, Type> Mapping
        {
            get
            {
                this.mapping = this.mapping ?? new Dictionary<Type, Type>();
                return this.mapping;
            }
        }

        public T Create<T>()
        {
            return (T)this.Create(typeof(T));
        }

        private object Create(Type type)
        {
            Type instanceType;
            if (!Mapping.TryGetValue(type, out instanceType))
            {
                instanceType = type;
            }

            ConstructorInfo constructorInfo = instanceType.GetConstructors().Single();

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
                instance = Activator.CreateInstance(instanceType);
            }

            return instance;
        }
    }
}