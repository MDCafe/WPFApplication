using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDCafe.ViewModels
{
    class ItemsViewModel
    {
        public Model1Container _modelContext;

        public Model1Container ModelContext { get { return _modelContext; } }

        public ItemsViewModel()
        {
            _modelContext = new Model1Container();
        }
    }
}
