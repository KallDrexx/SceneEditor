using System;
using System.Collections.Generic;
using System.Linq;
using SceneEditor.Core.Assets;
using SceneEditor.Core.Exceptions;
using SceneEditor.Core.SceneManagement;

namespace SceneEditor.Core.Commands
{
    public class CommandManager : ICommandManager
    {
        private readonly Dictionary<Type, ICommandHandler> _commandHandlers;
        private readonly ISceneManager _sceneManager;
        private readonly IAssetManager _assetmanager;
        private readonly Stack<UndoDetails> _undoableCommands;
        private readonly Stack<UndoDetails> _redoableCommands;
        private readonly object _padlock = new object();

        public IEnumerable<string> UndoableCommandNames
        {
            get
            {
                foreach (var details in _undoableCommands)
                    yield return details.CommandName;
            }
        }

        public bool CanRedo { get { return _redoableCommands.Count > 0; } }

        public CommandManager(ISceneManager sceneManager, IAssetManager assetManager)
        {
            _commandHandlers = new Dictionary<Type, ICommandHandler>();
            _undoableCommands = new Stack<UndoDetails>();
            _redoableCommands = new Stack<UndoDetails>();
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

            // Lock is to prevent 2 commands from executing at once
            //   causing possible undo issues
            lock(_padlock)
            {
                handler.Execute(cmd);

                var undoCmd = handler as IUndoableCommandHandler;
                if (undoCmd != null)
                {
                    var details = undoCmd.LastExecutionUndoDetails;
                    if (details != null)
                    {
                        _undoableCommands.Push(details);
                        _redoableCommands.Clear();
                    }
                }
            }
        }

        public void UndoLastCommand()
        {
            if (_undoableCommands.Count == 0)
                return;

            var undoAction = _undoableCommands.Pop();
            undoAction.PerformUndo(undoAction);

            _redoableCommands.Push(undoAction);
        }

        public void RedoLastUndoneCommand()
        {
            var redo = _redoableCommands.Pop();
            redo.PerformRedo(redo);

            _undoableCommands.Push(redo);
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
