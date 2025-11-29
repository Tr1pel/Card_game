using Itmo.ObjectOrientedProgramming.Lab3.Context.Board;
using Itmo.ObjectOrientedProgramming.Lab3.Context.Collection;

namespace Itmo.ObjectOrientedProgramming.Lab3.Player;

public sealed class Player : IPlayer
{
    public string Name { get; }

    public IBoard Board { get; }

    public ICollection Collection { get; }

    public Player(string name, IBoard board, ICollection collection)
    {
        Name = name;
        Board = board;
        Collection = collection;
    }
}