using Itmo.ObjectOrientedProgramming.Lab3.Context.Board;
using Itmo.ObjectOrientedProgramming.Lab3.Context.Collection;

namespace Itmo.ObjectOrientedProgramming.Lab3.Player;

public interface IPlayer
{
    string Name { get; }

    IBoard Board { get; }

    ICollection Collection { get; }
}