using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Helpers
{
    public static class ReflectionHelper
    {
        public static Delegate CreateDelegateForMethod(MethodInfo method, object caller = null)
        {
            List<Type> args = method.GetParameters().Select(p => p.ParameterType).ToList();
            Type delegateType;

            if (method.ReturnType == typeof(void))
            {
                delegateType = Expression.GetActionType(args.ToArray());
            }
            else
            {
                args.Add(method.ReturnType);
                delegateType = Expression.GetDelegateType(args.ToArray());
            }

            return Delegate.CreateDelegate(delegateType, caller, method);
        }

        public static Delegate CreateDelegateForGetterProperty(PropertyInfo property, Type instanceType)
        {
            Type delegateType = Expression.GetFuncType(instanceType, property.PropertyType);
            return Delegate.CreateDelegate(delegateType, null, property.GetGetMethod());
        }

        public static Delegate CreateDelegateForGetterStaticProperty(PropertyInfo property)
        {
            Type delegateType = Expression.GetFuncType(property.PropertyType);
            return Delegate.CreateDelegate(delegateType, null, property.GetGetMethod());
        }

        public static Delegate CreateDelegateForSetterProperty(PropertyInfo property, Type instanceType)
        {
            Type delegateType = Expression.GetActionType(instanceType, property.PropertyType);
            return Delegate.CreateDelegate(delegateType, null, property.GetSetMethod());
        }
    }
}
