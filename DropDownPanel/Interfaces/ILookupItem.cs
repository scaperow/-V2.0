using System;

namespace AT.STO.UI.Win
{
	public interface ILookupItem<T> where T: struct
	{
		T		Id		{ get; }
		string	Text	{ get; }
	}
}
