using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SceneEditor.Core.Commands
{
    public class CommandManager
    {
        private readonly Dictionary<Type, ICommandHandler> _commandHandlers;

        public CommandManager()
        {
            _commandHandlers = new Dictionary<Type, ICommandHandler>();
            LoadAllCommandHandlers();
        }

        public void Execute(ICommand cmd)
        {
            if (cmd == null)
                throw new ArgumentNullException("cmd");

            ICommandHandler handler;
            if (!_commandHandlers.TryGetValue(cmd.GetType(), out handler))
                throw new InvalidOperationException("No command handler known to handle the type: " + cmd.GetType());

            handler.Execute(cmd);
        }

        private void LoadAllCommandHandlers()
        {
            var handlers = AppDomain.CurrentDomain
                                    .GetAssemblies()
                                    .SelectMany(x => x.GetTypes())
                                    .Where(x => typeof(ICommandHandler).IsAssignableFrom(x))
                                    .Where(x => !x.IsInterface)
                                    .Select(x => (ICommandHandler)Activator.CreateInstance(x))
                                    .ToArray();

            foreach (var handler in handlers)
                _commandHandlers.Add(handler.HandledCommandType, handler);
        }
    }
}
