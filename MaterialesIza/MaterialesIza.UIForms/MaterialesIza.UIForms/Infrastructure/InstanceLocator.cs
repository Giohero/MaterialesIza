using MaterialesIza.UIForms.ViewModels;

namespace MaterialesIza.UIForms.Infrastructure
{
    public class InstanceLocator
    {
        public MainViewModel Main { get; set; }

        public InstanceLocator()
        {
            this.Main = new MainViewModel();
        }

    }
}
