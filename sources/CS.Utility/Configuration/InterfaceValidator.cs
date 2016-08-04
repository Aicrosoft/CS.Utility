using System;
using System.Configuration;

namespace CS.Configuration
{
    public class InterfaceValidator : ConfigurationValidatorBase
    {
        private readonly Type interfaceType;

        public InterfaceValidator(Type type)
        {
            if (!type.IsInterface)
                throw new ArgumentException(type + " must be an interface");

            interfaceType = type;
        }

        public override bool CanValidate(Type type)
        {
            return (type == typeof (Type)) || base.CanValidate(type);
        }

        public override void Validate(object value)
        {
            if (value != null)
                ConfigurationHelper.CheckForInterface((Type) value, interfaceType);
        }
    }

    public sealed class InterfaceValidatorAttribute : ConfigurationValidatorAttribute
    {
        private readonly Type interfaceType;

        public InterfaceValidatorAttribute(Type type)
        {
            if (!type.IsInterface)
                throw new ArgumentException(type + " must be an interface");

            interfaceType = type;
        }

        public override ConfigurationValidatorBase ValidatorInstance
        {
            get { return new InterfaceValidator(interfaceType); }
        }
    }
}