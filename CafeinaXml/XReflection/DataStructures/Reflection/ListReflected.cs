using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Globalization;

namespace CafeinaXml.XReflection.DataStructures.Reflection
{
    class ListReflected
    {
        public Structure Structure { get; set; }

        public Type GenericType { get; set; }

        public Structure GenericTypeStructure { get; set; }

        
        private MethodInfo _MethodCount;

        public int Count(object obj)
        {
            return int.Parse(_MethodCount.Invoke(obj, null).ToString(), CultureInfo.CurrentCulture);          
        }


        private MethodInfo _MethodElementAt;

        public object ElementAt(object obj, int index)
        {
            return _MethodElementAt.Invoke(obj, new Object[] { index });
        }

        private MethodInfo _MethodAdd;

        public void Add(object obj, object element)
        {
            _MethodAdd.Invoke(obj, new Object[] { element });
        }

        public object NewInstance()
        {
            return GenericsReflection.GenericInstance(typeof(List<>), new Type[]{this.GenericType});
        }

        public ListReflected(MethodInfo methodCount, MethodInfo methodElementAt, MethodInfo methodAdd)
        {
            _MethodCount = methodCount;
            _MethodElementAt = methodElementAt;
            _MethodAdd = methodAdd;
        }
    }
}
