using System.Security.Cryptography;
using SHA3.Net;

namespace task3_DiceGame;

public class FairRandomGenerator
{
    public byte[]? Key { get; private set; }
    public int Number { get; private set; }
    public string? Hmac { get; private set; }

    public void Generate(int range)
    {
        Key = RandomNumberGenerator.GetBytes(32);
        Number = RandomNumberGenerator.GetInt32(range);
        var message = System.Text.Encoding.UTF8.GetBytes(Number.ToString());
        var hmacBytes = ComputeHmac(Key, message);
        Hmac = BitConverter.ToString(hmacBytes).Replace("-", "").ToLower();
        Console.WriteLine($"HMAC: {Hmac}");
    }

    public void Reveal()
    {
        if (Key == null)
            throw new InvalidOperationException("Key has not been generated yet.");
        Console.WriteLine($"Secret key: {BitConverter.ToString(Key).Replace("-", "").ToLower()}");
        Console.WriteLine($"Computer number: {Number}");
    }
    
    private static byte[] ComputeHmac(byte[] key, byte[] message)
    {
        const int blockSize = 136;
        if (key.Length > blockSize)
            key = Sha3.Sha3256().ComputeHash(key);
        if (key.Length < blockSize)
        {
            var paddedKey = new byte[blockSize];
            Array.Copy(key, paddedKey, key.Length);
            key = paddedKey;
        }
        var ipad = new byte[blockSize];
        var opad = new byte[blockSize];
        for (var i = 0; i < blockSize; i++)
        {
            ipad[i] = 0x36;
            opad[i] = 0x5C;
        }
        var keyIpad = new byte[blockSize];
        var keyOpad = new byte[blockSize];
        for (var i = 0; i < blockSize; i++)
        {
            keyIpad[i] = (byte)(key[i] ^ ipad[i]);
            keyOpad[i] = (byte)(key[i] ^ opad[i]);
        }
        var innerInput = new byte[blockSize + message.Length];
        Array.Copy(keyIpad, 0, innerInput, 0, blockSize);
        Array.Copy(message, 0, innerInput, blockSize, message.Length);
        var innerHash = Sha3.Sha3256().ComputeHash(innerInput);

        var outerInput = new byte[blockSize + innerHash.Length];
        Array.Copy(keyOpad, 0, outerInput, 0, blockSize);
        Array.Copy(innerHash, 0, outerInput, blockSize, innerHash.Length);
        var hmac = Sha3.Sha3256().ComputeHash(outerInput);
        return hmac;
    }
}
