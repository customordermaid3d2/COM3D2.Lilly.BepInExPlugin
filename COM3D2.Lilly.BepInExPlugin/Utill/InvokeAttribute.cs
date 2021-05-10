using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin.Utill
{
    class InvokeInit : Attribute
    {
        public static void Invoke()
        {
            InvokeClass.Invoke(typeof(InvokeInit));
        }
    }
    
    class InvokeAwake : Attribute
    {
        public static void Invoke()
        {
            InvokeClass.Invoke(typeof(InvokeAwake));
        }
    }

    class InvokeClass
    {
        /// <summary>
        /// 성능이 너무 나쁨. 하모니가 괜히 클래스 지정한게 아닌듯
        /// </summary>
        /// <param name="type"></param>
        public static void Invoke(Type type)
        {
            var methods = AppDomain.CurrentDomain.GetAssemblies() // Returns all currenlty loaded assemblies
            .SelectMany(x => x.GetTypes()) // returns all types defined in this assemblies
            .Where(x => x.IsClass) // only yields classes
            .SelectMany(x => x.GetMethods()) // returns all methods defined in those classes
            .Where(x => x.GetCustomAttributes(type, false).FirstOrDefault() != null); // returns only methods that have the InvokeAttribute

            foreach (var method in methods) // iterate through all found methods
            {
                var obj = Activator.CreateInstance(method.DeclaringType); // Instantiate the class
                MyLog.LogDebug("InvokeClass",method.DeclaringType.Name, method.Name);
                method.Invoke(obj, null); // invoke the method
            }

            /*
foreach (Type mytype in System.Reflection.Assembly.GetExecutingAssembly().GetTypes()
.Where(mytype => mytype.GetInterfaces().Contains(typeof(AwakeUtill))))
{
    mytype.GetMethod("invoke").Invoke(Activator.CreateInstance(mytype, null), null);
}
*/
        }
    }
}
