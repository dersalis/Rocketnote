using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Rocketnote.Resources;
using System.Windows.Media;

namespace Rocketnote.ViewModels
{
	public class MainViewModel : INotifyPropertyChanged
	{
		public MainViewModel()
		{
			this.Items = new ObservableCollection<ItemViewModel>();
		}

		/// <summary>
		/// A collection for ItemViewModel objects.
		/// </summary>
		public ObservableCollection<ItemViewModel> Items { get; private set; }

		private string _sampleProperty = "Sample Runtime Property Value";
		/// <summary>
		/// Sample ViewModel property; this property is used in the view to display its value using a Binding
		/// </summary>
		/// <returns></returns>
		public string SampleProperty
		{
			get
			{
				return _sampleProperty;
			}
			set
			{
				if (value != _sampleProperty)
				{
					_sampleProperty = value;
					NotifyPropertyChanged("SampleProperty");
				}
			}
		}

		/// <summary>
		/// Sample property that returns a localized string
		/// </summary>
		public string LocalizedSampleProperty
		{
			get
			{
				return AppResources.SampleProperty;
			}
		}

		public bool IsDataLoaded
		{
			get;
			private set;
		}

		/// <summary>
		/// Creates and adds a few ItemViewModel objects into the Items collection.
		/// </summary>
		public void LoadData()
		{
			// Sample data; replace with real data
			this.Items.Add(new ItemViewModel() { LineOne = "Runtime one", LineTwo = "Maecenas praesent accumsan bibendum", LineThree = "Facilisi faucibus habitant inceptos interdum lobortis nascetur pharetra placerat pulvinar sagittis senectus sociosqu", Box = new SolidColorBrush(Colors.Green) });
			this.Items.Add(new ItemViewModel() { LineOne = "Runtime two", LineTwo = "Dictumst eleifend facilisi faucibus", LineThree = "Suscipit torquent ultrices vehicula volutpat maecenas praesent accumsan bibendum dictumst eleifend facilisi faucibus", Box = new SolidColorBrush(Colors.Cyan) });
			this.Items.Add(new ItemViewModel() { LineOne = "Runtime three", LineTwo = "Habitant inceptos interdum lobortis", LineThree = "Habitant inceptos interdum lobortis nascetur pharetra placerat pulvinar sagittis senectus sociosqu suscipit torquent", Box = new SolidColorBrush(Colors.Cyan) });
			this.Items.Add(new ItemViewModel() { LineOne = "Runtime four", LineTwo = "Nascetur pharetra placerat pulvinar", LineThree = "Ultrices vehicula volutpat maecenas praesent accumsan bibendum dictumst eleifend facilisi faucibus habitant inceptos", Box = new SolidColorBrush(Colors.Red) });
			this.Items.Add(new ItemViewModel() { LineOne = "Runtime five", LineTwo = "Maecenas praesent accumsan bibendum", LineThree = "Maecenas praesent accumsan bibendum dictumst eleifend facilisi faucibus habitant inceptos interdum lobortis nascetur", Box = new SolidColorBrush(Colors.Yellow) });
			this.Items.Add(new ItemViewModel() { LineOne = "Runtime six", LineTwo = "Dictumst eleifend facilisi faucibus", LineThree = "Pharetra placerat pulvinar sagittis senectus sociosqu suscipit torquent ultrices vehicula volutpat maecenas praesent", Box = new SolidColorBrush(Colors.Transparent) });
			this.Items.Add(new ItemViewModel() { LineOne = "Runtime seven", LineTwo = "Habitant inceptos interdum lobortis", LineThree = "Accumsan bibendum dictumst eleifend facilisi faucibus habitant inceptos interdum lobortis nascetur pharetra placerat", Box = new SolidColorBrush(Colors.White) });
			this.Items.Add(new ItemViewModel() { LineOne = "Runtime eight", LineTwo = "Nascetur pharetra placerat pulvinar", LineThree = "Pulvinar sagittis senectus sociosqu suscipit torquent ultrices vehicula volutpat maecenas praesent accumsan bibendum", Box = new SolidColorBrush(Colors.Gray) });
			this.Items.Add(new ItemViewModel() { LineOne = "Runtime nine", LineTwo = "Maecenas praesent accumsan bibendum", LineThree = "Facilisi faucibus habitant inceptos interdum lobortis nascetur pharetra placerat pulvinar sagittis senectus sociosqu", Box = new SolidColorBrush(Colors.Purple) });
			this.Items.Add(new ItemViewModel() { LineOne = "Runtime ten", LineTwo = "Dictumst eleifend facilisi faucibus", LineThree = "Suscipit torquent ultrices vehicula volutpat maecenas praesent accumsan bibendum dictumst eleifend facilisi faucibus", Box = new SolidColorBrush(Colors.Orange) });
			this.Items.Add(new ItemViewModel() { LineOne = "Runtime eleven", LineTwo = "Habitant inceptos interdum lobortis", LineThree = "Habitant inceptos interdum lobortis nascetur pharetra placerat pulvinar sagittis senectus sociosqu suscipit torquent", Box = new SolidColorBrush(Colors.Magenta) });
			this.Items.Add(new ItemViewModel() { LineOne = "Runtime twelve", LineTwo = "Nascetur pharetra placerat pulvinar", LineThree = "Ultrices vehicula volutpat maecenas praesent accumsan bibendum dictumst eleifend facilisi faucibus habitant inceptos", Box = new SolidColorBrush(Colors.Brown) });
			this.Items.Add(new ItemViewModel() { LineOne = "Runtime thirteen", LineTwo = "Maecenas praesent accumsan bibendum", LineThree = "Maecenas praesent accumsan bibendum dictumst eleifend facilisi faucibus habitant inceptos interdum lobortis nascetur", Box = new SolidColorBrush(Colors.Blue) });
			this.Items.Add(new ItemViewModel() { LineOne = "Runtime fourteen", LineTwo = "Dictumst eleifend facilisi faucibus", LineThree = "Pharetra placerat pulvinar sagittis senectus sociosqu suscipit torquent ultrices vehicula volutpat maecenas praesent", Box = new SolidColorBrush(Colors.Green) });
			this.Items.Add(new ItemViewModel() { LineOne = "Runtime fifteen", LineTwo = "Habitant inceptos interdum lobortis", LineThree = "Accumsan bibendum dictumst eleifend facilisi faucibus habitant inceptos interdum lobortis nascetur pharetra placerat", Box = new SolidColorBrush(Colors.Green) });
			this.Items.Add(new ItemViewModel() { LineOne = "Runtime sixteen", LineTwo = "Nascetur pharetra placerat pulvinar", LineThree = "Pulvinar sagittis senectus sociosqu suscipit torquent ultrices vehicula volutpat maecenas praesent accumsan bibendum", Box = new SolidColorBrush(Colors.Green) });

			this.IsDataLoaded = true;
		}

		public event PropertyChangedEventHandler PropertyChanged;
		private void NotifyPropertyChanged(String propertyName)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (null != handler)
			{
				handler(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}