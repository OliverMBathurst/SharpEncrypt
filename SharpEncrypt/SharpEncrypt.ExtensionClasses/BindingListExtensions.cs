using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace SharpEncrypt.ExtensionClasses
{
    public static class BindingListExtensions
    {
        public static void AddRange<T>(this BindingList<T> list, IEnumerable<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            foreach (var element in collection)
                list.Add(element);
        }

        public static void RemoveAll<T>(this BindingList<T> list, IEnumerable<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            foreach (var element in collection)
                list.Remove(element);
        }

        public static void RemoveAll<T>(this BindingList<T> list, IEnumerable collection)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            foreach (var element in collection)
            {
                if (element is T obj)
                {
                    list.Remove(obj);
                }
            }
        }

        public static void RemoveAll<T>(this BindingList<T> list, Func<T, bool> predicate)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            foreach (var element in list)
            {
                if (predicate(element))
                    list.Remove(element);
            }
        }
    }
}
