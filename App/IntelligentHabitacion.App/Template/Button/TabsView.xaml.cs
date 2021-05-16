using Neemacademy.CustomControls.Xam.Plugin.TabView;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Xam.Plugin.TabView;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IntelligentHabitacion.App.Template.Button
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabsView : ContentView
    {
        public ObservableCollection<TabItem> TabItems { get; } = new ObservableCollection<TabItem>();

        public static readonly BindableProperty TabsProperty = BindableProperty.Create(
            nameof(TabItems),
            typeof(IEnumerable),
            typeof(TabsView));

        public TabsView()
        {
            InitializeComponent();

            TabItems.CollectionChanged += OnTabsChanged;
        }

        private void OnTabsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var list = (ObservableCollection<TabItem>)sender;

            var listItems = list.Select(c => new TabModelBase
            {
                TabViewControlTabItemTitle = c.Title,
                TabContent = c.TabContent,
                TabViewControlTabItemIconSource = ImageSource.FromFile(c.Icon)
            });
            
            Tabs.TemplatedItemSource = new ObservableCollection<TabModelBase>(listItems);
        }
    }

    public class TabItem
    {
        public string Title { get; set; }
        public string Icon { get; set; }
        public Xamarin.Forms.View TabContent { get; set; }
    }

    public class TabModelBase : ObservableBase, ITabViewControlTabItem
    {
        public string TabViewControlTabItemTitle { get; set; }
        public ImageSource TabViewControlTabItemIconSource { get; set; }
        public Xamarin.Forms.View TabContent { get; set; }

        public void TabViewControlTabItemFocus() { }
    }

    public class AppColorTransformation : FFImageLoading.Transformations.TintTransformation
    {
        public AppColorTransformation()
        {
            HexColor = Application.Current.RequestedTheme == OSAppTheme.Light ? "#000000" : "#FFFFFF";
            EnableSolidColor = true;
        }
    }
}