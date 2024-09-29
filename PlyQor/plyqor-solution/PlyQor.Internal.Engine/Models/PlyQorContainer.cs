using System.Collections.Generic;
using System.Linq;

namespace PlyQor.Internal.Engine.Models
{
	public class PlyQorContainer
	{
		public string Name { get; private set; }
		public int Retention { get; private set; }
		public List<string> Tokens { get; private set; }

		public PlyQorContainer(string name, int retention, string tokens)
		{
			Name = name;
			Retention = retention;
			Tokens = tokens.Split(',').ToList();
		}

		public void AddToken(string token)
		{
			Tokens.Add(token);
		}

		public void RemoveToken(string token)
		{
			Tokens.Remove(token);
		}
	}
}
