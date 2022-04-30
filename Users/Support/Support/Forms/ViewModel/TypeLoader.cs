using AquaFlaim.Interface.Forms;
using AquaFlaim.Interface.Forms.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.User.Support.Forms.ViewModel
{
    public class TypeLoader
    {
        private readonly ISettingsFactory _settingsFactory;
        private readonly IFormTypeService _fromTypeService;

        public TypeLoader (ISettingsFactory settingsFactory,
            IFormTypeService formTypeService)
        {
            _settingsFactory = settingsFactory;
            _fromTypeService = formTypeService;
        }

        public void Load(TypesVM vm)
        {
            try
            {
                Task.Run(async () =>
                {
                    List<TypeVM> types = new List<TypeVM>();
                    foreach (FormType formType in (await _fromTypeService.GetAll(_settingsFactory.CreateForms())))
                    {
                        types.Add(new TypeVM(formType));
                    }
                    return types;
                }).ContinueWith(LoadCallback, vm, TaskScheduler.FromCurrentSynchronizationContext());
            }
            catch (Exception ex)
            {
                ErrorWindow.Open(ex, null);
            }
        }

        public async Task LoadCallback(Task<List<TypeVM>> load, object state)
        {
            try
            {
                ObservableCollection<TypeVM> types = new ObservableCollection<TypeVM>();
                foreach (TypeVM type in await load)
                {
                    types.Add(type);
                }
                TypesVM vm = (TypesVM)state;
                vm.Types = types;
            }
            catch (Exception ex)
            {
                ErrorWindow.Open(ex, null);
            }
        }
    }
}
