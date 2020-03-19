using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace NoteApp.Data
{
	public class BaseEntity : INotifyPropertyChanged, ICloneable
	{
		private readonly Type Type;

		public BaseEntity() { Type = GetType(); }


		[field: NonSerialized]
		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null, bool fireSync = false)
		{
			PropertyChanged?.Invoke(this, new ExtendentPropertyChangedEventArgs(propertyName, fireSync));
		}

		public virtual bool DeepEquals(object obj) { return false; }

		public virtual object Clone() { return new BaseEntity { }; }

		public virtual bool FastEquals(object obj) { return false; }

		public virtual string HandleChanges(BaseEntity obj) { return string.Empty; }
	}

	public class ExtendentPropertyChangedEventArgs : PropertyChangedEventArgs
	{
		public ExtendentPropertyChangedEventArgs(string propertyName, bool fireSync) : base(propertyName)
		{
			FireSync = fireSync;
		}

		public bool FireSync { get; }
	}
}
