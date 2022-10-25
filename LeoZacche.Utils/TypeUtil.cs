using System;

namespace LeoZacche.Utils
{
    public static class TypeUtil
    {
        public static T? TryConvert<T>(object value) where T : struct
        {
            try
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }
            catch
            {
                return null;
            }
        }
        public static T ConvertTo<T>(object valor)
        {
            var theType = typeof(T);
            var underlyingType = Nullable.GetUnderlyingType(theType);
            if (underlyingType == null)
            {
                // o tipo para o qual se deseja converter não é nullable.

                if (valor == null)
                    throw new ArgumentNullException("valor", "null não pode ser convertido para um tipo não nullable.");

                if (theType.IsEnum)
                {
                    var strValue = Convert.ToString(valor);
                    var parsed = (T)Enum.Parse(theType, strValue);
                    return parsed;
                }
                else
                {
                    var changed = (T)Convert.ChangeType(valor, theType);
                    return changed;
                }
            }
            else
            {
                // o tipo é nullable de algo.

                if (valor == DBNull.Value)
                    return default(T);
                else
                {
                    if (valor == null)
                        return default(T);
                    else
                    {
                        if (underlyingType.IsEnum)
                        {
                            var strValue = Convert.ToString(valor);
                            var parsed = (T)Enum.Parse(underlyingType, strValue);
                            return parsed;
                        }
                        else
                        {
                            var parsed = (T)Convert.ChangeType(valor, underlyingType);
                            return parsed;
                        }
                    }
                }
            }
        }

        public static bool Is<T>(object value) where T : struct
        {
            return TryConvert<T>(value) != null;
        }

        public static bool IsSubclass(Type parent, Type sub)
        {
            if (!sub.IsSubclassOf(parent))
            {
                do
                {
                    if (sub.IsGenericType)
                        if (sub.GetGenericTypeDefinition() == parent)
                            return true;

                    sub = sub.BaseType;
                } while (sub != null);
            }
            else
                return true;

            return false;
        }

        public static dynamic GetDefaultValue(this Type t)
        {
            if (t.IsValueType)
                return Activator.CreateInstance(t);

            return null;
        }
    }
}
