using Xamarin.Forms;

namespace ZombiepediaApp.XamlHelpers
{
	public class ListViewCommands
	{
		public static readonly BindableProperty ItemTappedProperty =
			BindableProperty.CreateAttached<ListViewCommands, Command>(
				bindable => GetItemTapped(bindable),
				null,
				BindingMode.OneWay,
				null,
				OnItemTappedChanged,
				null,
				null);

		public static Command GetItemTapped(BindableObject bo)
		{
			return (Command)bo.GetValue(ItemTappedProperty);
		}

		public static void SetItemTapped(BindableObject bo, Command value)
		{
			bo.SetValue(ItemTappedProperty, value);
		}

		public static void OnItemTappedChanged(BindableObject bo, Command oldValue, Command newValue)
		{
			var lv = bo as ListView;
			if (lv != null)
			{
				lv.ItemTapped += (sender, args) =>
				{
					newValue.Execute(args.Item);
				};
			}
		}
	}
}
