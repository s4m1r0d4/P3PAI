namespace tests;
using CryptographyLibrary;
using static System.Console;

public class CryptographyUnitTests
{
    [Fact]
    public void TestEncryptText()
    {
        string plainText = "Prueba1";
        string password = "contrasena";
        string result = String.Empty;

        result = Protector.Encrypt(plainText, password);

        WriteLine($"'{plainText}' encrypted is '{result}'");

        Assert.True(result.Length > 0, "Failed encrypting");
    }

    [Fact]
    public void TestDecryptText()
    {
        string encryptedText = "fdtjSsjGbPxf+KluxU1Q2A==";
        string password = "contrasena";
        string result = String.Empty;

        result = Protector.Decrypt(encryptedText, password);

        WriteLine($"'{encryptedText}' decrypted is '{result}'");

        Assert.True(result.Length > 0, "Failed decrypting");
    }

    [Fact]
    public void TestFailDecryptWithWrongPassword()
    {
        // TODO: implement this
    }
}