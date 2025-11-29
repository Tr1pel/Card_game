using Itmo.ObjectOrientedProgramming.Lab3.Domain.Board;
using Itmo.ObjectOrientedProgramming.Lab3.Domain.Collection;

namespace Itmo.ObjectOrientedProgramming.Lab3.Domain.Player;

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