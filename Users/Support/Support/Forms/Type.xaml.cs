using AquaFlaim.Interface.Forms;
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
    /// Interaction logic for Type.xaml
    /// </summary>
    public partial class Type : Page
    {
        public Type(TypeVM typeVM)
        {
            NavigationCommands.BrowseBack.InputGestures.Clear();
            NavigationCommands.BrowseForward.InputGestures.Clear();
            InitializeComponent();
            TypeVM = typeVM;
            DataContext = typeVM;
        }

        public TypeVM TypeVM { get; set; }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (ILifetimeScope scope = DependencyInjection.ContainerFactory.Container.BeginLifetimeScope())
                {
                    IFormTypeService formTypeService = scope.Resolve<IFormTypeService>();
                    ISettingsFactory settingsFactory = scope.Resolve<ISettingsFactory>();
                    if (!TypeVM.HasErrors && TypeVM.InnerFormType.FormTypeId.HasValue)
                    {
                        Task.Run(() => formTypeService.Update(settingsFactory.CreateForms(), TypeVM.InnerFormType))
                            .ContinueWith(SaveUpdateCallback, null, TaskScheduler.FromCurrentSynchronizationContext());
                        
                    }
                    else if (!TypeVM.HasErrors && !TypeVM.InnerFormType.FormTypeId.HasValue)
                    {
                        Task.Run(() => formTypeService.Create(settingsFactory.CreateForms(), TypeVM.InnerFormType))
                            .ContinueWith(SaveCreateCallback, null, TaskScheduler.FromCurrentSynchronizationContext());
                        
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorWindow.Open(ex, Window.GetWindow(this));
            }
        }

        private async Task SaveCreateCallback(Task<FormType> create, object state)
        {
            try
            {
                FormType formType = await create;
                TypeVM = new TypeVM(formType);
                DataContext = TypeVM;
                MessageBox.Show(Window.GetWindow(this), $"Save {formType.Title} Complete", "Save", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                ErrorWindow.Open(ex, Window.GetWindow(this));
            }
        }

        private async Task SaveUpdateCallback(Task<FormType> update, object state)
        {
            try
            {
                await update;
                MessageBox.Show(Window.GetWindow(this), "Update Complete", "Save", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                ErrorWindow.Open(ex, Window.GetWindow(this));
            }
        }
    }
}
