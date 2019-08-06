using Prism.Mvvm;
using System.Windows.Media;

namespace Models
{
    public class Element : BindableBase
    {
        public ImageSource Icon { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public Element() { }
    }
}
