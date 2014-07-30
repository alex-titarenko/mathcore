using System;
using System.Runtime.Serialization;
using System.Text;


namespace TAlex.MathCore.ExpressionEvaluation
{
    public class UnassignedVariableException : Exception
    {
        public string VariableName { get; private set; }


        public UnassignedVariableException(string variableName)
            : this(variableName, String.Format(Properties.Resources.EXC_UNASSIGNED_VARIABLE, variableName))
        {
        }

        public UnassignedVariableException(string variableName, string message)
            : base(message)
        {
            VariableName = variableName;
        }

        public UnassignedVariableException(string variableName, string message, Exception innerException)
            : base(message, innerException)
        {
            VariableName = variableName;
        }
    }
}
