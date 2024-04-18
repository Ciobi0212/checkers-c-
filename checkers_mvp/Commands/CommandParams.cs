using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace checkers_mvp.Commands
{
    public class CommandParams
    {
        public Storyboard storyboardShrink { get; set; }
        public Storyboard storyboardExpand { get; set; }

        public CommandParams(Storyboard storyboardShrink, Storyboard storyboardExpand)
        {
            this.storyboardShrink = storyboardShrink;
            this.storyboardExpand = storyboardExpand;
        }
    }
}
