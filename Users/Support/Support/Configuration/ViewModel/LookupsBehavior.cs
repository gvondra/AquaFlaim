using AquaFlaim.Interface.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.User.Support.Configuration.ViewModel
{
    public class LookupsBehavior
    {
        private readonly LookupsVM _lookups;
        private readonly ILookupService _lookupService;
        private readonly ISettingsFactory _settingsFactory;

        public LookupsBehavior(LookupsVM lookups,
            ISettingsFactory settingsFactory,
            ILookupService lookupService)
        {
            _lookups = lookups;
            _settingsFactory = settingsFactory;
            _lookupService = lookupService;
            LoadCodes();
        }

        private void LoadCodes()
        {
            try
            {
                Task.Run(async () =>
                {
                    return (await _lookupService.GetCodes(_settingsFactory.CreateConfiguration())).ToList();
                }).ContinueWith(LoadCodesCallback, _lookups, TaskScheduler.FromCurrentSynchronizationContext());
            }
            catch (Exception ex)
            {
                ErrorWindow.Open(ex, null);
            }
        }

        private async Task LoadCodesCallback(Task<List<string>> loadCodes, object state)
        {
            try
            {
                LookupsVM lookups = (LookupsVM)state;
                foreach (string code in await loadCodes)
                {
                    lookups.Codes.Add(code);
                }
            }
            catch (Exception ex)
            {
                ErrorWindow.Open(ex, null);
            }
        }
    }
}
