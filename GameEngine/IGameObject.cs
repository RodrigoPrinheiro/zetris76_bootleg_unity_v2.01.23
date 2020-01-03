/// @file
/// @brief File contains the IGameObject interface to be used by the gameloop
/// to add new objects to the update and render methods.

using System.Collections.Generic;

namespace GameEngine
{
    /// <summary>
    /// Used to add the functionality to add an instance of a class to be updated
    /// in the game-loop.
    /// </summary>
    public interface IGameObject
    {
        /// <summary>
        /// </summary>
        void Update();
        /// <summary>
        /// Game objects must have a render method.
        /// </summary>
        /// <param name="buffer"> buffer to be rendered to</param>
        void Render(ScreenBuffer<char> buffer);
        /// <summary>
        /// Objects have a list with their children.
        /// </summary>
        List<IGameObject> Childs { get; }
    }
}
