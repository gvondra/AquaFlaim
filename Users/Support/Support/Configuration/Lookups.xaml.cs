using AquaFlaim.Interface.Configuration;
using AquaFlaim.User.Support.Configuration.Controls;
using AquaFlaim.User.Support.Configuration.ViewModel;
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
using ConfigModels = AquaFlaim.Interface.Configuration.Models;

namespace AquaFlaim.User.Support.Configuration
{
    /// <summary>
    /// Interaction logic for Lookups.xaml
    /// </summary>
    public partial class Lookups : Page
    {
        public Lookups()
        {
            NavigationCommands.BrowseBack.InputGestures.Clear();
            NavigationCommands.BrowseForward.InputGestures.Clear();
            InitializeComponent();
            this.Loaded += Lookups_Loaded;
        }

        public LookupsVM LookupsVM { get; set; }

        private void Lookups_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                GoogleLogin.ShowLoginDialog();
                LookupsVM = new LookupsVM();
                DataContext = LookupsVM;
                using (ILifetimeScope scope = DependencyInjection.ContainerFactory.Container.BeginLifetimeScope())
                {
                    ISettingsFactory settingsFactory = scope.Resolve<ISettingsFactory>();
                    ILookupService lookupService = scope.Resolve<ILookupService>();
                    LookupsVM.AddBehavior(new LookupsBehavior(LookupsVM, settingsFactory, lookupService));
                }
            }
            catch (Exception ex)
            {
                ErrorWindow.Open(ex, Window.GetWindow(this));
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ConfigModels.Lookup newLookup = new ConfigModels.Lookup()
                {
                    Code = "new-lookup",
                    IsPublic = false                    
                };                 
                CodesList.SelectedItem = null;
                ShowSelectedLookup(new LookupVM(newLookup) { IsNew = true });
            }
            catch (Exception ex)
            {
                ErrorWindow.Open(ex, Window.GetWindow(this));
            }
        }

        private void ShowSelectedLookup(LookupVM lookupVM)
        {
            LookupPannel.Children.Clear();
            if (lookupVM != null)
                LookupPannel.Children.Add(new Lookup(lookupVM));
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string selectedItem = (string)((ListView)sender).SelectedItem;
                Task.Run(async () =>
                {
                    using (ILifetimeScope scope = DependencyInjection.ContainerFactory.Container.BeginLifetimeScope())
                    {
                        ISettingsFactory settingsFactory = scope.Resolve<ISettingsFactory>();
                        ILookupService lookupService = scope.Resolve<ILookupService>();
                        return await lookupService.GetByCode(settingsFactory.CreateConfiguration(), selectedItem);
                    }
                }).ContinueWith(GetLookupCallback, null, TaskScheduler.FromCurrentSynchronizationContext());
            }
            catch (Exception ex)
            {
                ErrorWindow.Open(ex, Window.GetWindow(this));
            }
        }

        private async Task GetLookupCallback(Task<ConfigModels.Lookup> getLookup, object state)
        {
            try
            {
                ShowSelectedLookup(new LookupVM(await getLookup));
            }
            catch (Exception ex)
            {
                ErrorWindow.Open(ex, Window.GetWindow(this));
            }
        }
    }
}
