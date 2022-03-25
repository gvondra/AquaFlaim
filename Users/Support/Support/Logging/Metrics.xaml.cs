using LogAPI = AquaFlaim.Interface.Log;
using Autofac;
using AquaFlaim.User.Support.Logging.ViewModel;
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

namespace AquaFlaim.User.Support.Logging
{
    /// <summary>
    /// Interaction logic for Metric.xaml
    /// </summary>
    public partial class Metric : Page
    {
        public Metric()
        {
            NavigationCommands.BrowseBack.InputGestures.Clear();
            NavigationCommands.BrowseForward.InputGestures.Clear();
            InitializeComponent();
            MetricVM = new MetricsVM();
            MetricVM.AddValidator();
            DataContext = MetricVM;
        }

        public MetricsVM MetricVM { get; set; }

        private void GetButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime dateTime;
            try
            {
                if (!string.IsNullOrEmpty(MetricVM.MaxTimestamp) && MetricVM[nameof(MetricVM.MaxTimestamp)] == null)
                {
                    GoogleLogin.ShowLoginDialog();
                    MetricVM.Metrics.Clear();
                    if (DateTime.TryParse(MetricVM.MaxTimestamp, out dateTime))
                    {
                        Task.Run(() => GetMetrics(dateTime))
                            .ContinueWith(GetMetricsCallback, null, TaskScheduler.FromCurrentSynchronizationContext());
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorWindow.Open(ex, Window.GetWindow(this));
            }
        }

        private Task<IEnumerable<LogAPI.Models.Metric>> GetMetrics(DateTime maxTimestamp)
        {
            using (ILifetimeScope scope = DependencyInjection.ContainerFactory.Container.BeginLifetimeScope())
            {
                ISettingsFactory settingsFactory = scope.Resolve<ISettingsFactory>();
                LogAPI.IMetricService metricService = scope.Resolve<LogAPI.IMetricService>();
                return metricService.GetTopByTimestamp(settingsFactory.CreateLog(), maxTimestamp.ToUniversalTime());
            }
        }

        private async Task GetMetricsCallback(Task<IEnumerable<LogAPI.Models.Metric>> getMetrics, object state)
        {
            try
            {
                MetricVM.Metrics.Clear();
                foreach (LogAPI.Models.Metric metric in await getMetrics)
                {
                    MetricVM.Metrics.Add(new MetricVM(metric));
                }
            }
            catch (Exception ex)
            {
                ErrorWindow.Open(ex, Window.GetWindow(this));
            }
        }
    }
}
