using System.Security.Cryptography;

namespace Itmo.ObjectOrientedProgramming.Lab3.Context.Board;

public sealed class SystemRandom : IRng
{
    public int NextInt(int min, int max)
    {
        return RandomNumberGenerator.GetInt32(min, max);
    }
}
