using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMerchandise.Data.Error
{

    //Code snippet for custom Exception inheritance: https://blogs.msdn.microsoft.com/agileer/2013/05/17/the-correct-way-to-code-a-custom-exception-class/

    [Serializable]
    public class InvalidValueDuplicationException : Exception
    {
        public string PropertyName { get; set; }

        public string PropertyValue { get; set; }

        public InvalidValueDuplicationException(string propertyName, string propertyValue)
            : base($"Value {propertyValue} for {propertyName} is invalid due to an existing constraint.") { }

        public InvalidValueDuplicationException(string propertyName, string propertyValue, Exception inner)
            : base($"Value {propertyValue} for {propertyName} is invalid due to an existing constraint.", inner) { }

        protected InvalidValueDuplicationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            PropertyName = info.GetString("PropertyName");
            PropertyValue = info.GetString("PropertyValue");
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException("info");
            info.AddValue("PropertyName", PropertyName);
            info.AddValue("PropertyValue", PropertyValue);
            base.GetObjectData(info, context);
        }
    }
}
