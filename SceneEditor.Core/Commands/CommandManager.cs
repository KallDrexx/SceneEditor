using System;
using System.Collections.Generic;
using System.Linq;
using SceneEditor.Core.Assets;
using SceneEditor.Core.Exceptions;
using SceneEditor.Core.SceneManagement;

namespace SceneEditor.Core.Commands
{
    public class CommandManager
    {
        private readonly Dictionary<Type, ICommandHandler> _commandHandlers;
        private readonly ISceneManager _sceneManager;
        private readonly IAssetManager _assetmanager;

        public CommandManager(ISceneManager sceneManager, IAssetManager assetManager)
        {
            _commandHandlers = new Dictionary<Type, ICommandHandler>();
            _sceneManager = sceneManager;
            _assetmanager = assetManager;
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
                                    .Where(x => x.HandledCommandType != null)
                                    .ToArray();

            foreach (var handler in handlers)
            {
                _commandHandlers.Add(handler.HandledCommandType, handler);

                // Add required managers
                var requiresSceneManager = handler as IRequiresSceneManager;
                if (requiresSceneManager != null)
                    requiresSceneManager.SceneManager = _sceneManager;

                var requiredAssetManager = handler as IRequiresAssetManager;
                if (requiredAssetManager != null)
                    requiredAssetManager.AssetManager = _assetmanager;
            }
        }
    }
}
