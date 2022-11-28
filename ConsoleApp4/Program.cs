using NHibernate;

namespace ConsoleApp4
{
	public class Program
	{
		public static void Main(string[] args)
		{
			NHibernate.Cfg.Configuration cfg = new NHibernate.Cfg.Configuration();
			cfg.Configure();
			cfg.AddClass(typeof(Address));
			cfg.AddClass(typeof(Person));

			using var sessionFactory = cfg.BuildSessionFactory();
			
			long personID = CreateTestItem(sessionFactory);
			FetchTestItemFromDB(sessionFactory, personID);

			BaseObject.WasFinalized = false;
			Console.WriteLine("Attempting to finalize...");
			
			GC.Collect();
			GC.WaitForPendingFinalizers();

			if (BaseObject.WasFinalized)
				Console.WriteLine("Finalization was a success...");
			else
				Console.WriteLine("Finalization failed...");

			Console.WriteLine("Press any key to exit...");
			Console.ReadLine();
		}

		private static long CreateTestItem(ISessionFactory sessionFactory)
		{
			var session = sessionFactory.OpenSession();
			var transaction = session.BeginTransaction();

			var person = new Person
			{
				FirstName = "Test",
				Address = new Address
				{
					AddressLine = "test1, 123"
				}
			};

			session.Save(person);
			transaction.Commit();
			return person.Key;
		}

		private static void FetchTestItemFromDB(ISessionFactory sessionFactory, long personID)
		{
			using (var session = sessionFactory.OpenSession())
			using (var transaction = session.BeginTransaction())
			{
				var person = session.Get<Person>(personID); // <-- Get() creates unitialized AddressProxy
				person.Address = null; // <-- Making sure, that address gets garbadge collected ASAP
				person = null;
			}
		}

	}
}