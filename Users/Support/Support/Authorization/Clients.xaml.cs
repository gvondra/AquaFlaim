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
    /// Interaction logic for Clients.xaml
    /// </summary>
    public partial class Clients : Page
    {
        private readonly IContainer _container;

        public Clients()
        {
            _container = DependencyInjection.ContainerFactory.Container;
            NavigationCommands.BrowseBack.InputGestures.Clear();
            NavigationCommands.BrowseForward.InputGestures.Clear();
            InitializeComponent();
        }
        
        ClientsVM ClientsVM { get; set; }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            ClientsVM = new ClientsVM();
            DataContext = ClientsVM;
            GoogleLogin.ShowLoginDialog();
            Task.Run(GetAllClients)
                .ContinueWith(GetAllClaimsCallback, null, TaskScheduler.FromCurrentSynchronizationContext());
            Task.Run(GetAllRoles)
                .ContinueWith(GetAllRolesCallback, null, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private async Task<List<Role>> GetAllRoles()
        {
            using (ILifetimeScope scope = _container.BeginLifetimeScope())
            {
                ISettingsFactory settingsFactory = scope.Resolve<ISettingsFactory>();
                IRoleService roleService = scope.Resolve<IRoleService>();
                return (await roleService.GetAll(settingsFactory.CreateAuthorization())).ToList();
            }
        }

        private async Task GetAllRolesCallback(Task<List<Role>> task, object state)
        {
            try
            {
                ClientsVM.AllRoles = await task;
            }
            catch (Exception ex)
            {
                ErrorWindow.Open(ex, Window.GetWindow(this));
            }
        }

        private async Task<List<Client>> GetAllClients()
        {
            using (ILifetimeScope scope = _container.BeginLifetimeScope())
            {
                ISettingsFactory settingsFactory = scope.Resolve<ISettingsFactory>();
                IClientService clientService = scope.Resolve<IClientService>();                
                return (await clientService.GetAll(settingsFactory.CreateAuthorization())).ToList();
            }
        }

        private async Task GetAllClaimsCallback(Task<List<Client>> task, object state)
        {
            try
            {
                ClientsVM.Clients.Clear();
                foreach (Client client in await task)
                {
                    ClientsVM.Clients.Add(new ClientVM(client));
                }
                if (ClientsVM.Clients.Count == 0)
                {
                    ClientVM clientVM = CreateClient();
                    ClientsVM.Clients.Add(clientVM);
                    ClientsVM.SelectedClient = clientVM;
                    await Task.Run(CreateSecret)
                        .ContinueWith(CreateSecretCallback, clientVM, TaskScheduler.FromCurrentSynchronizationContext());
                }
            }
            catch (Exception ex)
            {
                ErrorWindow.Open(ex, Window.GetWindow(this));
            }
        }

        private ClientVM CreateClient()
            => new ClientVM(new Client())
            {
                Name = "New Client",
                IsNew = true
            };

        private void AddHyperlink_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ClientVM clientVM = CreateClient();                
                ClientsVM.Clients.Add(clientVM);
                ClientsVM.SelectedClient = clientVM;
                LoadRoles();
                Task.Run(CreateSecret)
                    .ContinueWith(CreateSecretCallback, clientVM, TaskScheduler.FromCurrentSynchronizationContext());
            }
            catch (Exception ex)
            {
                ErrorWindow.Open(ex, Window.GetWindow(this));
            }
        }

        private async Task<string> CreateSecret()
        {
            using (ILifetimeScope scope = _container.BeginLifetimeScope())
            {
                ISettingsFactory settingsFactory = scope.Resolve<ISettingsFactory>();
                IClientService clientService = scope.Resolve<IClientService>();
                return await clientService.CreateSecret(settingsFactory.CreateAuthorization());
            }
        }

        private async Task CreateSecretCallback(Task<string> task, object state)
        {
            try
            {
                ((ClientVM)state).Secret = await task;
            }
            catch (Exception ex)
            {
                ErrorWindow.Open(ex, Window.GetWindow(this));
            }
        }

        private void ClientListBox_Selected(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (e.AddedItems != null && e.AddedItems.Count == 1)
                {
                    ClientsVM.SelectedClient = (ClientVM)e.AddedItems[0];
                    LoadRoles();
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
                if (ClientsVM.SelectedClient != null)
                {
                    if (!string.IsNullOrEmpty(ClientsVM.SelectedClient.Secret))
                    {
                        Clipboard.SetText(ClientsVM.SelectedClient.Secret);
                        MessageBox.Show(Window.GetWindow(this), "Secret Copied", "Secret", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    Task.Run(() => Save(ClientsVM.SelectedClient))
                        .ContinueWith(SaveCallback, ClientsVM.SelectedClient, TaskScheduler.FromCurrentSynchronizationContext());
                }
            }
            catch (Exception ex)
            {
                ErrorWindow.Open(ex, Window.GetWindow(this));
            }
        }

        private async Task<Client> Save(ClientVM clientVM)
        {
            using (ILifetimeScope scope = _container.BeginLifetimeScope())
            {
                clientVM.InnerClient.Roles = clientVM.Roles
                    .Where(r => r.IsActive)
                    .ToDictionary(r => r.PolicyName, r => r.Name);
                ISettingsFactory settingsFactory = scope.Resolve<ISettingsFactory>();
                IClientService clientService = scope.Resolve<IClientService>();
                if (clientVM.IsNew)
                    return await clientService.Create(settingsFactory.CreateAuthorization(), clientVM.InnerClient, clientVM.Secret);
                else
                    return await clientService.Update(settingsFactory.CreateAuthorization(), clientVM.InnerClient, clientVM.Secret);
            }
        }

        private async Task SaveCallback(Task<Client> task, object state)
        {
            try
            {
                Client client = await task;
                ((ClientVM)state).IsNew = false;
                ((ClientVM)state).ClientId = client.ClientId;
                ((ClientVM)state).Secret = null;
                MessageBox.Show(Window.GetWindow(this), "Save Complete", "Save", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                ErrorWindow.Open(ex, Window.GetWindow(this));
            }
        }

        private void NewSecretButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ClientsVM.SelectedClient != null)
                {
                    Task.Run(CreateSecret)
                        .ContinueWith(CreateSecretCallback, ClientsVM.SelectedClient, TaskScheduler.FromCurrentSynchronizationContext());
                }
            }
            catch (Exception ex)
            {
                ErrorWindow.Open(ex, Window.GetWindow(this));
            }
        }

        private void LoadRoles()
        {
            bool isActive;
            ClientVM clientVM = ClientsVM.SelectedClient;
            if (ClientsVM.AllRoles != null && clientVM != null && clientVM.Roles.Count == 0)
            {
                foreach (Role role in ClientsVM.AllRoles)
                {
                    isActive = (clientVM.InnerClient.Roles != null && clientVM.InnerClient.Roles.ContainsKey(role.PolicyName));
                    clientVM.Roles.Add(
                        new ClientRoleVM(role)
                        {
                            IsActive = isActive
                        });
                }
            }    
        }
    }
}
