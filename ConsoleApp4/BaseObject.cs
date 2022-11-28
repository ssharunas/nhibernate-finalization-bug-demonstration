namespace ConsoleApp4
{
	public class BaseObject
	{
		public static bool WasFinalized = false;

		~BaseObject()
		{
			WasFinalized= true;
		}
	}
}
