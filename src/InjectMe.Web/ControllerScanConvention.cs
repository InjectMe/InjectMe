using System;
using System.Reflection;
using System.Web.Mvc;
using InjectMe.Registration;

namespace InjectMe.Web
{
    public class ControllerScanConvention : IScanConvention
    {
        private static readonly Type ControllerType;

        static ControllerScanConvention()
        {
            ControllerType = typeof(IController);
        }

        public void ProcessType(IContainerConfiguration container, TypeInfo type)
        {
            if (type.IsAbstract || type.IsInterface || type.IsGenericTypeDefinition || type.IsNested)
                return;

            if (ControllerType.IsAssignableFrom(type))
            {
                container.
                    Register(type).
                    AsTransient();
            }
        }
    }
}
