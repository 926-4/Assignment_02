namespace Encryption
{
	public interface ICypher
	{
		string encrypt(string input);
		string decrypt(string input);
	}
}