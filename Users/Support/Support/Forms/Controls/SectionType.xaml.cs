using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AquaFlaim.User.Support.Forms.Controls
{
    /// <summary>
    /// Interaction logic for SectionType.xaml
    /// </summary>
    public partial class SectionType : UserControl
    {
        public delegate void MoveSectionHandler(object sender);

        public event MoveSectionHandler MoveSectionUp;
        public event MoveSectionHandler MoveSectionDown;

        public SectionType()
        {
            InitializeComponent();
        }

        private void MoveUp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MoveSectionUp.Invoke(this);
            }
            catch (Exception ex)
            {
                ErrorWindow.Open(ex, Window.GetWindow(this));
            }
        }

        private void MoveDown_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MoveSectionDown.Invoke(this);
            }
            catch (Exception ex)
            {
                ErrorWindow.Open(ex, Window.GetWindow(this));
            }
        }
    }
}
