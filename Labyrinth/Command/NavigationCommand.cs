using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;
using Labyrinth.Command.Base;
using Labyrinth.ViewModels.Base;

namespace Labyrinth.Command
{
    public class NavigationCommand : BaseCommand
    {
        private readonly Uri uri;
        public NavigationCommand(Uri uri)
        {
            this.uri = uri;
        }
        public override void Execute(object parameter)
        {
            Page currentPage = (Page) parameter;
            if (currentPage != null)
            {
                ViewModel.NavigateToPage(currentPage, uri);
            }
        }
    }
}
