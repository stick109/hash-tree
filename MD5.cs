#define DEBUG

namespace hash_tree
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Security.Cryptography;

	public static class MD5
	{
		static readonly IComparer<byte[]> _comparer = new Comparer();

		public static byte[] ComputeHash(byte[] data)
		{
			using var hasher = new MD5CryptoServiceProvider();
			var hash = hasher.ComputeHash(data);
#if DEBUG
			Console.WriteLine($"{BitConverter.ToString(data)} -> {BitConverter.ToString(hash)}");
#endif
			return hash;
		}

		public static byte[] ComputeHash(IEnumerable<byte[]> data)
		{
			var sorted = data.OrderBy(x => x, _comparer);
			return ComputeHash(sorted.SelectMany(x => x).ToArray());
		}

		class Comparer : IComparer<byte[]>
		{
			public int Compare(byte[] x, byte[] y)
			{
				if (x == null) throw new ArgumentNullException(nameof(x));
				if (y == null) throw new ArgumentNullException(nameof(y));
				for(int index = 0; index < Math.Min(x.Length, y.Length); index++)
				{
					var result = x[index].CompareTo(y[index]);
					if (result != 0) return result;
				}
				return x.Length.CompareTo(y.Length);
			}
		}
	}
}
