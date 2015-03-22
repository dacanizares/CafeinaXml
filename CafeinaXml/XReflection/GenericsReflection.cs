using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Reflection;

namespace CafeinaXml.XReflection
{
    internal class GenericsReflection
    {
        public static object GenericCall(Type type, Type[] genericTypes, string method, object[] p)
        {
            // build the type at runtime 
            Type gchtype = type.MakeGenericType(genericTypes);

            // create an instance. Note, you'll have to know about your  
            // constructor args in advance. If the consturctor has no  
            // args, use Activator.CreateIntsance. 
            object ch = Activator.CreateInstance(gchtype);

            // now invoke SendSingleMessage ( assuming MessagingClient is a  
            // static class - hence first argument is null.  
            // now pass in a reference to our ch object. 
            MethodInfo sendsingle = gchtype.GetMethod(method);

            return sendsingle.Invoke(ch, p);
        }

        public static object GenericInstance(Type type, Type[] genericTypes)
        {
            // build the type at runtime 
            Type gchtype = type.MakeGenericType(genericTypes);

            return Activator.CreateInstance(gchtype);
        }

        private GenericsReflection() 
        { }
    }
}
