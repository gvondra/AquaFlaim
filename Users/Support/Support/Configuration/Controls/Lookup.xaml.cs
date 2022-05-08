using AquaFlaim.Interface.Configuration;
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

namespace AquaFlaim.User.Support.Configuration.Controls
{
    /// <summary>
    /// Interaction logic for Lookup.xaml
    /// </summary>
    public partial class Lookup : UserControl
    {
        public Lookup(LookupVM lookupVM)
        {            
            InitializeComponent();
            LookupVM = lookupVM;
            DataContext = lookupVM;
        }

        public LookupVM LookupVM { get; set; }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (LookupVM.ErrorCount == 0)
                {
                    Task.Run(async () => 
                    {
                        using (ILifetimeScope scope = DependencyInjection.ContainerFactory.Container.BeginLifetimeScope())
                        {
                            ISettingsFactory settingsFactory = scope.Resolve<ISettingsFactory>();
                            ILookupService lookupService = scope.Resolve<ILookupService>();
                            if (LookupVM.IsNew)
                            {
                                return await lookupService.Create(settingsFactory.CreateConfiguration(), LookupVM.InnerLookup);
                            }
                            else
                            {
                                return await lookupService.Update(settingsFactory.CreateConfiguration(), LookupVM.InnerLookup);
                            }
                        }
                    }).ContinueWith(SaveCallback, null, TaskScheduler.FromCurrentSynchronizationContext());
                    
                }
            }
            catch (Exception ex)
            {
                ErrorWindow.Open(ex, Window.GetWindow(this));
            }
        }

        private async Task SaveCallback(Task<ConfigModels.Lookup> save, object state)
        {
            try 
            {
                ConfigModels.Lookup lookup = await save;
                if (LookupVM.IsNew)
                {
                    LookupVM = new LookupVM(lookup);
                }
            }
            catch (Exception ex)
            {
                ErrorWindow.Open(ex, Window.GetWindow(this));
            }
        }
    }
}
