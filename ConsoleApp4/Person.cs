namespace ConsoleApp4
{
	public class Person : BaseObject
	{
		public virtual long Key { get; set; }
		public virtual string? FirstName { get; set; }
		public virtual Address? Address { get; set; }
	}
}
