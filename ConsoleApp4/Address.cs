namespace ConsoleApp4
{
	public class Address : BaseObject
	{
		public virtual long Key { get; set; }
		public virtual string? AddressLine { get; set; }
		~Address() { }
	}
}
