using NoteApp.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace NoteApp.ViewModels
{
	public class BaseViewModel :  INotifyPropertyChanged, IDisposable
    {
		public readonly ViewContext Context;
		public BaseViewModel()
		{
			Context = new ViewContext(this);
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
		protected virtual void Dispose(bool dispose) { }

		public void Dispose()
		{
			Dispose(true);
			Context.Dispose();
		}
	}
	public sealed class ViewContext : IDisposable
	{
		private readonly BaseViewModel View;
		private readonly Dictionary<string, object> Properties = new Dictionary<string, object>();

		public ViewContext(BaseViewModel view)
		{
			View = view;
			view.PropertyChanged -= OnViewPropertyChanged;
			view.PropertyChanged += OnViewPropertyChanged;
		}
		public void Dispose() { View.PropertyChanged -= OnViewPropertyChanged; }
		private void OnViewPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (!Properties.ContainsKey(e.PropertyName)) { return; }
			Properties[e.PropertyName] = GetValue(e.PropertyName);
		}
		private object GetValue(string property)
		{
			return ReflectionHelper.CreateDelegateForGetterProperty(View.GetType().GetProperty(property), View.GetType()).DynamicInvoke(View);
		}		
	}
}
