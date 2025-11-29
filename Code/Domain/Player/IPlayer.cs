using Itmo.ObjectOrientedProgramming.Lab3.Domain.Board;
using Itmo.ObjectOrientedProgramming.Lab3.Domain.Collection;

namespace Itmo.ObjectOrientedProgramming.Lab3.Domain.Player;

public interface IPlayer
{
    string Name { get; }

    IBoard Board { get; }

    ICollection Collection { get; }
}