using Xamarin.Forms;

namespace CustomTabbedPage
{
    public partial class MainPage : ExtendedTabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
            this.SelectedItem = this.Children[2];
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();
            if (CurrentPage.Title == "App")
            {
                home.Icon = "app_logo.png";
            }
            else
            {
                home.Icon = "app_logo_unselected.png";
            }
            this.Title = CurrentPage.Title;
        }
    }
}
