using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CS.Reflection
{
    /// <summary>
	/// <para>Implements a very fast object factory for dynamic object creation. Dynamically generates a factory class which will use the new() constructor of the requested type.</para>
	/// <para>Much faster than using Activator at the price of the first invocation being significantly slower than subsequent calls.</para>
	/// </summary>
	public static class FastActivator
    {
        private static readonly Dictionary<Type, Func<object>> FactoryCache = new Dictionary<Type, Func<object>>();

        /// <summary>
        /// Creates an instance of the specified type using a generated factory to avoid using Reflection.
        /// </summary>
        /// <typeparam name="T">The type to be created.</typeparam>
        /// <returns>The newly created instance.</returns>
        public static T Create<T>()
        {
            return TypeFactory<T>.Create();
        }

        /// <summary>
        /// Creates an instance of the specified type using a generated factory to avoid using Reflection.
        /// </summary>
        /// <param name="type">The type to be created.</param>
        /// <returns>The newly created instance.</returns>
        public static object Create(Type type)
        {
            Func<object> f;

            if (!FactoryCache.TryGetValue(type, out f))
                lock (FactoryCache)
                    if (!FactoryCache.TryGetValue(type, out f))
                    {
                        FactoryCache[type] = f = Expression.Lambda<Func<object>>(Expression.New(type)).Compile();
                    }

            return f();
        }

        private static class TypeFactory<T>
        {
            public static readonly Func<T> Create = Expression.Lambda<Func<T>>(Expression.New(typeof(T))).Compile();
        }
    }
}