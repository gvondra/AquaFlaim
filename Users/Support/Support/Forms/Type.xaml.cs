using AquaFlaim.Interface.Forms;
using AquaFlaim.Interface.Forms.Models;
using AquaFlaim.User.Support.Forms.Controls;
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

        private void AddSection_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FormSectionType sectionType = new FormSectionType() { Title = "New Section" };
                TypeVM.InnerFormType.Sections.Add(sectionType);
                TypeVM.Sections.Add(new SectionTypeVM(sectionType));
            }
            catch (Exception ex)
            {
                ErrorWindow.Open(ex, Window.GetWindow(this));
            }
        }

        private void SectionType_MoveSectionUp(object sender)
        {
            try
            {
                SectionTypeVM sectionTypeVM = (SectionTypeVM)((SectionType)sender).DataContext;
                int currentIndex = TypeVM.Sections.IndexOf(sectionTypeVM);
                if (currentIndex > 0)
                {
                    FormSectionType formSectionType = TypeVM.InnerFormType.Sections[currentIndex];
                    TypeVM.Sections.RemoveAt(currentIndex);
                    TypeVM.Sections.Insert(currentIndex - 1, sectionTypeVM);
                    TypeVM.InnerFormType.Sections.RemoveAt(currentIndex);
                    TypeVM.InnerFormType.Sections.Insert(currentIndex - 1, formSectionType);
                }
            }
            catch (Exception ex)
            {
                ErrorWindow.Open(ex, Window.GetWindow(this));
            }
        }

        private void SectionType_MoveSectionDown(object sender)
        {
            try
            {
                SectionTypeVM sectionTypeVM = (SectionTypeVM)((SectionType)sender).DataContext;
                int currentIndex = TypeVM.Sections.IndexOf(sectionTypeVM);
                if (currentIndex < TypeVM.Sections.Count - 1)
                {
                    FormSectionType formSectionType = TypeVM.InnerFormType.Sections[currentIndex];
                    TypeVM.Sections.RemoveAt(currentIndex);
                    TypeVM.Sections.Insert(currentIndex + 1, sectionTypeVM);
                    TypeVM.InnerFormType.Sections.RemoveAt(currentIndex);
                    TypeVM.InnerFormType.Sections.Insert(currentIndex + 1, formSectionType);
                }
            }
            catch (Exception ex)
            {
                ErrorWindow.Open(ex, Window.GetWindow(this));
            }
        }
    }
}
