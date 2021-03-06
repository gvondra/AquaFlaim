using AuthModels = AquaFlaim.Interface.Authorization.Models;
using AquaFlaim.Interface.Authorization;
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
    /// Interaction logic for User.xaml
    /// </summary>
    public partial class User : Page
    {
        private readonly IContainer _container;

        public User()
        {
            _container = DependencyInjection.ContainerFactory.Container;
            NavigationCommands.BrowseBack.InputGestures.Clear();
            NavigationCommands.BrowseForward.InputGestures.Clear();
            InitializeComponent();
        }

        public FindUserVM UserVM { get; set; }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            UserVM = new FindUserVM();
            DataContext = UserVM;
            GoogleLogin.ShowLoginDialog();
            Task.Run(GetAllRoles)
                .ContinueWith(GetAllRolesCallback, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private async Task<IEnumerable<AuthModels.Role>> GetAllRoles()
        {
            using (ILifetimeScope scope = _container.BeginLifetimeScope())
            {
                ISettingsFactory settingsFactory = scope.Resolve<ISettingsFactory>();
                IRoleService roleService = scope.Resolve<IRoleService>();
                return await roleService.GetAll(settingsFactory.CreateAuthorization());
            }
        }

        private async Task GetAllRolesCallback(Task<IEnumerable<AuthModels.Role>> task, object state)
        {
            try
            {
                UserVM.AllRoles = (await task).ToList();
                LoadUserRoles();
            }
            catch (Exception ex)
            {
                ErrorWindow.Open(ex, Window.GetWindow(this));
            }
        }

        private void FindButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(UserVM.FindAddress))
                {
                    GoogleLogin.ShowLoginDialog();
                    Task.Run(() => GetUser(UserVM.FindAddress))
                        .ContinueWith(GetUserCallback, null, TaskScheduler.FromCurrentSynchronizationContext());
                }
            }
            catch (Exception ex)
            {
                ErrorWindow.Open(ex, Window.GetWindow(this));
            }
        }

        private async Task<AuthModels.User> GetUser(string emailAddress)
        {
            using (ILifetimeScope scope = _container.BeginLifetimeScope())
            {
                ISettingsFactory settingsFactory = scope.Resolve<ISettingsFactory>();
                IUserService userService = scope.Resolve<IUserService>();
                return await userService.GetByEmailAddress(settingsFactory.CreateAuthorization(), emailAddress);
            }
        }

        private async Task GetUserCallback(Task<AuthModels.User> task, object state)
        {
            try
            {
                UserVM.User = await task;
                LoadUserRoles();
            }
            catch (Exception ex)
            {
                ErrorWindow.Open(ex, Window.GetWindow(this));
            }
        }

        private void LoadUserRoles()
        {
            UserVM.Roles.Clear();
            if (UserVM.User != null && UserVM.AllRoles != null)
            {
                List<string> activeRoles = (UserVM.User.Roles ?? new Dictionary<string, string>()).Keys.ToList();
                foreach (AuthModels.Role role in UserVM.AllRoles)
                {
                    UserVM.Roles.Add(
                        new FindUserRoleVM(role) 
                        { 
                            IsActive = activeRoles.Any(r => string.Equals(r, role.PolicyName, StringComparison.OrdinalIgnoreCase)) 
                        });
                }
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (UserVM.User != null)
                {
                    UserVM.User.Roles = UserVM.Roles
                        .Where(r => r.IsActive)
                        .ToDictionary(r => r.PolicyName, r => r.Name);
                    Task.Run(() => SaveUser(UserVM.User))
                        .ContinueWith(SaverUserCallback, null, TaskScheduler.FromCurrentSynchronizationContext());
                }
            }
            catch (Exception ex)
            {
                ErrorWindow.Open(ex, Window.GetWindow(this));
            }
        }

        private async Task<AuthModels.User> SaveUser(AuthModels.User user)
        {
            using (ILifetimeScope scope = _container.BeginLifetimeScope())
            {
                ISettingsFactory settingsFactory = scope.Resolve<ISettingsFactory>();
                IUserService userService = scope.Resolve<IUserService>();
                return await userService.Update(settingsFactory.CreateAuthorization(), user);
            }
        }

        private async Task SaverUserCallback(Task<AuthModels.User> task, object state)
        {
            try
            {
                AuthModels.User user = await task;
                UserVM.User = user;
                MessageBox.Show(Window.GetWindow(this), "Save Complete", "Save", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                ErrorWindow.Open(ex, Window.GetWindow(this));
            }
        }
    }
}
