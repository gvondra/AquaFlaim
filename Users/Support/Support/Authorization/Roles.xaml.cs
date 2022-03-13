using AquaFlaim.Interface.Authorization;
using AquaFlaim.Interface.Authorization.Models;
using AquaFlaim.User.Support.Authorization.ViewModel;
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

namespace AquaFlaim.User.Support.Authorization
{
    /// <summary>
    /// Interaction logic for Roles.xaml
    /// </summary>
    public partial class Roles : Page
    {
        private readonly IContainer _container;

        public Roles()
        {
            _container = DependencyInjection.ContainerFactory.Container;
            NavigationCommands.BrowseBack.InputGestures.Clear();
            NavigationCommands.BrowseForward.InputGestures.Clear();
            InitializeComponent();
        }

        public RolesVM RolesVM { get; set; }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            RolesVM = new RolesVM();
            DataContext = RolesVM;
            GoogleLogin.ShowLoginDialog();
            Task.Run(() => GetRoles())
                .ContinueWith(GetRolesCallback, null, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private async Task<IEnumerable<Role>> GetRoles()
        {
            using (ILifetimeScope scope = _container.BeginLifetimeScope())
            {
                ISettingsFactory settingsFactory = scope.Resolve<ISettingsFactory>();
                IRoleService roleService = scope.Resolve<IRoleService>();
                return await roleService.GetAll(settingsFactory.CreateAuthorization());
            }
        }

        private async Task GetRolesCallback(Task<IEnumerable<Role>> task, object state)
        {
            try
            {
                foreach (Role role in await task)
                {
                    RoleVM roleVM = new RoleVM(role);
                    RolesVM.Roles.Add(roleVM);
                }
                if (RolesVM.Roles.Count == 0)
                    RolesVM.Roles.Add(CreateNewRoleVM());
                RolesVM.SelectedRole = RolesVM.Roles[0];
            }
            catch (Exception ex)
            {
                ErrorWindow.Open(ex, Window.GetWindow(this));
            }
        }

        private RoleVM CreateNewRoleVM()
            => new RoleVM
            {
                Name = "New Role",
                PolicyName = "Context:Access",
                IsNew = true
            };

        private void AddHyperlink_Click(object sender, RoutedEventArgs e)
        {
            RoleVM roleVM = CreateNewRoleVM();
            RolesVM.Roles.Add(roleVM);
            RolesVM.SelectedRole = roleVM;
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (e.AddedItems != null && e.AddedItems.Count == 1)
                {
                    RolesVM.SelectedRole = (RoleVM)e.AddedItems[0];
                }
            }
            catch (Exception ex)
            {
                ErrorWindow.Open(ex, Window.GetWindow(this));
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (RolesVM.SelectedRole != null)
                    Task.Run(() => Save(RolesVM.SelectedRole))
                        .ContinueWith(SaveCallback, RolesVM.SelectedRole, TaskScheduler.FromCurrentSynchronizationContext());
            }
            catch (Exception ex)
            {
                ErrorWindow.Open(ex, Window.GetWindow(this));
            }
        }

        private async Task<Role> Save(RoleVM role)
        {
            using (ILifetimeScope scope = _container.BeginLifetimeScope())
            {
                ISettingsFactory settingsFactory = scope.Resolve<ISettingsFactory>();
                IRoleService roleService = scope.Resolve<IRoleService>();
                if (role.IsNew)
                    return await roleService.Create(settingsFactory.CreateAuthorization(), role.InnerRole);
                else
                    return await roleService.Update(settingsFactory.CreateAuthorization(), role.InnerRole);
            }
        }

        private async Task SaveCallback(Task<Role> task, object state)
        {
            try
            {
                Role role = await task;
                ((RoleVM)state).RoleId = role.RoleId;
                ((RoleVM)state).IsNew = false;
                MessageBox.Show(Window.GetWindow(this), "Save complete", "Save", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                ErrorWindow.Open(ex, Window.GetWindow(this));
            }
        }
    }
}
