using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SceneEditor.Core.Commands
{
    public class CommandManager
    {
        private Dictionary<Type, ICommandHandler> _commandHandlers;

        public CommandManager()
        {
            _commandHandlers = new Dictionary<Type, ICommandHandler>();
        }

        public void Execute(ICommand cmd)
        {
            if (cmd == null)
                throw new ArgumentNullException("cmd");

            ICommandHandler handler;
            if (!_commandHandlers.TryGetValue(cmd.GetType(), out handler))
                throw new InvalidOperationException("No command handler known to handle the type: " + cmd.GetType());
        }
    }
}
