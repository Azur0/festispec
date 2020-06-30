using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.DesktopApplication.Views.Components.Dropdown
{
    public class DropDownItems
    {

        private ObservableCollection<Tuple<object, string>> _list = new ObservableCollection<Tuple<object, string>>();

        public void AddItem(object key, string value)
        {
            this._list.Add(new Tuple<object, string>(key,value));
        }

        public ObservableCollection<Tuple<object,string>> GetOptions { get { return this._list; } }
    
        public Tuple<object, string> GetOption(string key)
        {
            return _list.Where(item => item.Item1.ToString() == key).FirstOrDefault();
        }
    }
}