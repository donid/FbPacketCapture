using System;
using System.Collections.Generic;


namespace DnblCore
{
	public class DiffResults<T, TDIFFINFO>
	{
		public List<T> Added { get; } = new List<T>();
		public List<T> Removed { get; } = new List<T>();
		public List<DiffResultChangedItem<T, TDIFFINFO>> Changed { get; } = new List<DiffResultChangedItem<T, TDIFFINFO>>();

	}

	public class DiffResultChangedItem<T, TDIFFINFO>
	{
		public T Item1 { get; }
		public T Item2 { get; }
		public TDIFFINFO DiffInfo { get; }

		public DiffResultChangedItem(T item1, T item2, TDIFFINFO diffInfo)
		{
			Item1 = item1;
			Item2 = item2;
			DiffInfo = diffInfo;
		}
	}

	public class ListDiffer
	{
		public static DiffResults<T, TDIFFINFO> ListsDiff<T, TDIFFINFO, TKEY>(
				IEnumerable<T> rows1,
				IEnumerable<T> rows2,
				Func<T, TKEY> keySelector,
				Func<T, T, TDIFFINFO?> itemComparer)
		{
			var res = new DiffResults<T, TDIFFINFO>();

			var dict1 = new Dictionary<TKEY, T>();
			foreach (T current1 in rows1)
			{
				dict1.Add(keySelector(current1), current1);
			}

			foreach (T current2 in rows2)
			{
				TKEY key2 = keySelector(current2);
				if (dict1.TryGetValue(key2, out T foundValue1))
				{
					TDIFFINFO? itemCmpRes = itemComparer(foundValue1, current2);
					if (itemCmpRes != null)
					{
						res.Changed.Add(new DiffResultChangedItem<T, TDIFFINFO>(foundValue1, current2, itemCmpRes));
					}
					dict1.Remove(key2);
				}
				else
				{
					res.Added.Add(current2);
				}
			}
			foreach (KeyValuePair<TKEY, T> item in dict1)
			{
				res.Removed.Add(item.Value);
			}

			return res;
		}

		public static DiffResults<T, TDIFFINFO> SortedListsDiff<T, TDIFFINFO>(
						IEnumerable<T> rows1,
						IEnumerable<T> rows2,
						Func<T, T, int> keyComparer,
						Func<T, T, TDIFFINFO> itemComparer)
		{
			var res = new DiffResults<T, TDIFFINFO>();

			IEnumerator<T> enumerator1 = rows1.GetEnumerator();
			IEnumerator<T> enumerator2 = rows2.GetEnumerator();
			while (true)
			{
				bool moveSucces1 = enumerator1.MoveNext();
				bool moveSucces2 = enumerator2.MoveNext();
				if (!moveSucces1)
				{
					AddRemaining(enumerator2, res.Added);
				}
				if (!moveSucces2)
				{
					AddRemaining(enumerator1, res.Removed);
				}
				if (!moveSucces1 || !moveSucces2)
				{
					return res;
				}

				while (true)
				{
					T current1 = enumerator1.Current;
					T current2 = enumerator2.Current;
					int cmpres = keyComparer(current1, current2);

					if (cmpres > 0)
					{
						res.Added.Add(current2);
						if (!enumerator2.MoveNext())
						{
							AddRemaining(enumerator1, res.Removed);
							return res;
						}
					}
					else if (cmpres < 0)
					{
						res.Removed.Add(current1);
						if (!enumerator1.MoveNext())
						{
							AddRemaining(enumerator2, res.Added);
							return res;
						}
					}

					if (cmpres == 0)
					{
						var itemCmpRes = itemComparer(current1, current2);
						if (itemCmpRes != null)
						{
							res.Changed.Add(new DiffResultChangedItem<T, TDIFFINFO>(current1, current2, itemCmpRes));
						}
						break;
					}
				}
			}
		}

		static void AddRemaining<T>(IEnumerator<T> enumerator, List<T> list)
		{
			do
			{
				list.Add(enumerator.Current);
			} while (enumerator.MoveNext());
		}


	}
}
