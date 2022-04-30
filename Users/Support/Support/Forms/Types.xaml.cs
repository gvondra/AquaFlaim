using AquaFlaim.Interface.Forms.Models;
using AquaFlaim.User.Support.Forms.ViewModel;
using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AquaFlaim.User.Support.Forms
{
    /// <summary>
    /// Interaction logic for Types.xaml
    /// </summary>
    public partial class Types : Page
    {
        public Types()
        {
            NavigationCommands.BrowseBack.InputGestures.Clear();
            NavigationCommands.BrowseForward.InputGestures.Clear();
            InitializeComponent();
            this.Loaded += Types_Loaded;
        }

        TypesVM TypesVM { get; set; }

        private void Types_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                GoogleLogin.ShowLoginDialog();
                TypesVM = new TypesVM();
                DataContext = TypesVM;
                using (ILifetimeScope scope = DependencyInjection.ContainerFactory.Container.BeginLifetimeScope())
                {
                    TypeLoader typeLoader = scope.Resolve<TypeLoader>();
                    typeLoader.Load(TypesVM);
                }

            }
            catch (Exception ex)
            {
                ErrorWindow.Open(ex, Window.GetWindow(this));
            }
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FormType formType = new FormType()
                {
                    Title = "New Form"
                };
                Type typePage = new Type(new TypeVM(formType));
                NavigationService.Navigate(typePage);
            }
            catch (Exception ex)
            {
                ErrorWindow.Open(ex, Window.GetWindow(this));
            }
        }

        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                ListView listView = (ListView)sender;
                if (listView.SelectedItem != null)
                {
                    Type typePage = new Type((TypeVM)listView.SelectedValue);
                    NavigationService.Navigate(typePage);
                }
            }
            catch (Exception ex)
            {
                ErrorWindow.Open(ex, Window.GetWindow(this));
            }
        }
    }
}
