using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labyrinth.Command.Base;

namespace Labyrinth.Command
{
    public class RelayCommand : BaseCommand
    {
        private readonly Action execute;
        public RelayCommand(Action execute)
        {
            this.execute = execute;
        }
        public override void Execute(object parameter)
        {
            execute();
        }
    }
}
