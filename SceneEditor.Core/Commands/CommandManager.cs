using System;
using System.Collections.Generic;
using System.Linq;
using SceneEditor.Core.Exceptions;
using SceneEditor.Core.SceneManagement;

namespace SceneEditor.Core.Commands
{
    public class CommandManager
    {
        private readonly Dictionary<Type, ICommandHandler> _commandHandlers;
        private SceneManager _sceneManager;

        public CommandManager(SceneManager sceneManager)
        {
            _commandHandlers = new Dictionary<Type, ICommandHandler>();
            _sceneManager = sceneManager;
            LoadAllCommandHandlers();
        }

        public void Execute(ICommand cmd)
        {
            if (cmd == null)
                throw new ArgumentNullException("cmd");

            ICommandHandler handler;
            if (!_commandHandlers.TryGetValue(cmd.GetType(), out handler))
                throw new NoCommandHandlerForCommandException(cmd.GetType());

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
            {
                _commandHandlers.Add(handler.HandledCommandType, handler);

                // Add required managers
                var requiredScenemanager = handler as IRequiresSceneManager;
                if (requiredScenemanager != null)
                    requiredScenemanager.SceneManager = _sceneManager;
            }
        }
    }
}
