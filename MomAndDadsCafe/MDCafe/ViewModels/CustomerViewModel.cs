using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDCafe.ViewModels
{
    class CustomerViewModel
    {
        public Model1Container _modelContext;

        public Model1Container ModelContext { get { return _modelContext; } }


        public CustomerViewModel()
        {
            _modelContext = new Model1Container();
        }
    }
}
