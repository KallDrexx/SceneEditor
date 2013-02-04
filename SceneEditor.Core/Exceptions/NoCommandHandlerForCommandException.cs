using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SceneEditor.Core.Exceptions
{
    public class NoCommandHandlerForCommandException : InvalidOperationException
    {
        public Type CommandType { get; private set; }

        public NoCommandHandlerForCommandException(Type commandType)
            : base(string.Format("No command handler found for the {0} command type", commandType))
        {
            CommandType = commandType;
        }
    }
}
