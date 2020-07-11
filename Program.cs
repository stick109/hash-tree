using System;

namespace hash_tree
{
	class Program
	{
		static void Main(string[] args)
		{
			var parent = new Parent {
				StringProperty = "String",
				Children = new[] { 
					new Child {
						DateTimeProperty = DateTime.Now,
						Children = new[] {
							new Grandchild {
								IntProperty = 42,
								LongProperty = 42L << 42
							}
						}
					}
				},
				OtherChildren = new[] {
					new OtherChild {
						DoubleProperty = 42
					}
				}
			};

			Console.WriteLine("Started hashing");
			parent.ComputeHash();
		}
	}
}
