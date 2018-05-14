using System;
using System.Runtime.Serialization;

namespace CodingExercise.Data
{
    [Serializable]
    public class CantAddExcption : Exception
    {
        public CantAddExcption() { }

        public CantAddExcption(string message) : base(message) { }

        public CantAddExcption(string message, Exception innerException) : base(message, innerException) { }

        protected CantAddExcption(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
