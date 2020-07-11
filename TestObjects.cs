using System;

namespace hash_tree
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	class Parent : IHashable
	{
		public string StringProperty;
		public IList<Child> Children;
		public IList<OtherChild> OtherChildren;

		public byte[] ComputeHash()
		{
			var bytes = Encoding.UTF8.GetBytes(StringProperty.Normalize());
			var hash = MD5.ComputeHash(bytes);
			var hashes = Children.Select(x => x.ComputeHash()).
				Concat(OtherChildren.Select(x => x.ComputeHash())).
				Append(hash);
			return MD5.ComputeHash(hashes);
		}
	}

	class Child : IHashable
	{
		public DateTime DateTimeProperty;
		public IList<Grandchild> Children;

		public byte[] ComputeHash()
		{
			var bytes = BitConverter.GetBytes(DateTimeProperty.Ticks);
			var hash = MD5.ComputeHash(bytes);
			var hashes = Children.Select(x => x.ComputeHash()).Append(hash);
			return MD5.ComputeHash(hashes);
		}
	}

	class OtherChild : IHashable
	{
		public double DoubleProperty;
		public byte[] ComputeHash()
		{
			var bytes = BitConverter.GetBytes(DoubleProperty);
			return MD5.ComputeHash(bytes);
		}
	}

	class Grandchild : IHashable
	{
		public int IntProperty;
		public long LongProperty;

		public byte[] ComputeHash()
		{
			var bytes = BitConverter.GetBytes(IntProperty).
				Concat(BitConverter.GetBytes(LongProperty)).
				ToArray();

			return MD5.ComputeHash(bytes);
		}
	}
}
