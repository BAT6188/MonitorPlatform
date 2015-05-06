using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;

namespace MonitorPlatform.Controls
{
    public class AutoResizeListBox : ListBox
    {
      
        protected override void OnItemsSourceChanged(System.Collections.IEnumerable oldValue, System.Collections.IEnumerable newValue)
        {
          
            base.OnItemsSourceChanged(oldValue, newValue);
            if (this.Items.Count > 0 && this.RenderSize.Width > 0)
            {
                double size = this.RenderSize.Width / this.Items.Count;
                for (int i = 0; i < size; i++)
                {
                    (this.ItemContainerGenerator.ContainerFromIndex(i) as FrameworkElement).Width = size;

                }
            }
        }


         


    
    }
}
