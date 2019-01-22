using System;
using System.Threading.Tasks;
using CoreGraphics;
using CustomTabbedPage;
using CustomTabbedPage.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ExtendedTabbedPage), typeof(ExtendedTabbedPageRenderer))]
namespace CustomTabbedPage.iOS
{
    public class ExtendedTabbedPageRenderer : TabbedRenderer
    {
        UITabBarController tabbedController;

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                tabbedController = (UITabBarController)ViewController;
            }
        }

        public override void ViewWillAppear(bool animated)
        {
            if (TabBar?.Items == null)
                return;

            // Go through our elements and change the icons
            var tabs = Element as TabbedPage;
            if (tabs != null)
            {
                for (int i = 0; i < TabBar.Items.Length; i++)
                {
                    UpdateTabBarItem(TabBar.Items[i], tabs.Children[i].Icon);
                }
                AddFonts();
                AddSelectedTabIndicator();
            }

            base.ViewWillAppear(animated);
        }

        public override UIViewController SelectedViewController
        {
            get
            {
                if (base.SelectedViewController != null)
                {
                    AddFonts();

                }
                return base.SelectedViewController;
            }
            set
            {
                base.SelectedViewController = value;
                AddFonts();

            }
        }

        void AddFonts()
        {
            UITabBarItem.Appearance.SetTitleTextAttributes(new UITextAttributes
            {
              TextColor = Color.FromHex("#757575").ToUIColor(),
                Font = UIFont.FromName("GillSans-UltraBold", 12)
            }, UIControlState.Normal);

            UITabBarItem.Appearance.SetTitleTextAttributes(new UITextAttributes
            {
                 TextColor = Color.FromHex("#3C9BDF").ToUIColor(),
                 Font = UIFont.FromName("GillSans-UltraBold", 12)
            }, UIControlState.Selected);

        }


        private void UpdateTabBarItem(UITabBarItem item, string icon)
        {
            if (item == null || icon == null)
                return;

            // Set the font for the title.
           item.SetTitleTextAttributes(new UITextAttributes() { Font = UIFont.FromName("GillSans-UltraBold", 12), TextColor = Color.FromHex("#757575").ToUIColor() }, UIControlState.Normal);
           item.SetTitleTextAttributes(new UITextAttributes() { Font = UIFont.FromName("GillSans-UltraBold", 12), TextColor = Color.FromHex("#3C9BDF").ToUIColor() }, UIControlState.Selected);
           
        }


        protected override Task<Tuple<UIImage, UIImage>> GetIcon(Page page)
        {
            UIImage image;
            if (page.Title == "App")
            {
                image = UIImage.FromBundle(page.Icon.File).ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);
            }
            else
            {
                image = UIImage.FromBundle(page.Icon.File).ImageWithRenderingMode(UIImageRenderingMode.Automatic);
            }

            return Task.FromResult(new Tuple<UIImage, UIImage>(image, image));
        }

        void AddSelectedTabIndicator()
        {
            if (base.ViewControllers != null)
            {
                UITabBar.Appearance.SelectionIndicatorImage = GetImageWithColorPosition(Color.DarkGray.ToUIColor(), new CGSize(UIScreen.MainScreen.Bounds.Width / base.ViewControllers.Length, tabbedController.TabBar.Bounds.Size.Height + 4), new CGSize(UIScreen.MainScreen.Bounds.Width / base.ViewControllers.Length, 4));
            }

        }
        UIImage GetImageWithColorPosition(UIColor color, CGSize size, CGSize lineSize)
        {
            var rect = new CGRect(0, 0, size.Width, size.Height);
            var rectLine = new CGRect(0, size.Height - lineSize.Height, lineSize.Width, lineSize.Height);
            UIGraphics.BeginImageContextWithOptions(size, false, 0);
            UIColor.Clear.SetFill();
            UIGraphics.RectFill(rect);
            color.SetFill();
            UIGraphics.RectFill(rectLine);
            var img = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();
            return img;

        }
    }


}

