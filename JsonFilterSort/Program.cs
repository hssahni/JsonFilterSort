using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JsonFilterSort
{
	internal class Program
	{
		/// <summary>
		/// Mains the specified arguments.
		/// </summary>
		/// <param name="args">The arguments.</param>
		private static void Main(string[] args)
		{
			string url = @"http://agl-developer-test.azurewebsites.net/people.json";
			var client = new System.Net.WebClient();

			ParseJsonToObject(client.DownloadString(url));
		}

		/// <summary>
		/// Parses the json to object.
		/// </summary>
		/// <param name="json">The json string.</param>
		public static void ParseJsonToObject(string json)
		{
			JArray jsonArray = JArray.Parse(json);

			// Load Json to .Net object
			IList<Owner> owners = jsonArray.Select(x => new Owner
			{
				Name = (string)x["name"],
				Gender = (string)x["gender"],
				Age = (int)x["age"],
				Pets = (x["pets"]).Select(p => new Pet { PetName = (string)p["name"], PetType = (string)p["type"] }).ToList()
			}
			).ToList();

			Console.WriteLine("Male");

			// Print all cats owned by Male owners
			// Can be converted to single Linq statement
			foreach (var item in owners.Where(p => p.Gender == "Male"))
			{
				foreach (var pet in item.Pets.Where(p => p.PetType == "Cat").OrderBy(x => x.PetName))
				{
					Console.WriteLine("-" + pet.PetName);
				}
			}

			Console.WriteLine("Female");
			
			// Print all cats owned by Female owners
			foreach (var item in owners.Where(p => p.Gender == "Female"))
			{
				foreach (var pet in item.Pets.Where(p => p.PetType == "Cat").OrderBy(x => x.PetName))
				{
					Console.WriteLine("-" + pet.PetName);
				}
			}
			
			Console.WriteLine("Press any key to continue...");
			Console.ReadKey();
		}
	}

	/// <summary>
	/// Owner Class
	/// </summary>
	public class Owner
	{
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the gender.
		/// </summary>
		/// <value>
		/// The gender.
		/// </value>
		public string Gender { get; set; }

		/// <summary>
		/// Gets or sets the age.
		/// </summary>
		/// <value>
		/// The age.
		/// </value>
		public int Age { get; set; }

		/// <summary>
		/// Gets or sets the pets.
		/// </summary>
		/// <value>
		/// The pets.
		/// </value>
		public List<Pet> Pets { get; set; }
	}

	/// <summary>
	/// Pet Class
	/// </summary>
	public class Pet
	{
		/// <summary>
		/// Gets or sets the name of the pet.
		/// </summary>
		/// <value>
		/// The name of the pet.
		/// </value>
		public string PetName { get; set; }

		/// <summary>
		/// Gets or sets the type of the pet.
		/// </summary>
		/// <value>
		/// The type of the pet.
		/// </value>
		public string PetType { get; set; }
	}
}