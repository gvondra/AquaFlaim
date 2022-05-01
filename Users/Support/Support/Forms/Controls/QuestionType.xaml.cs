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
    /// Interaction logic for QuestionType.xaml
    /// </summary>
    public partial class QuestionType : UserControl
    {
        public delegate void MoveQuestionHandler(object sender);

        public event MoveQuestionHandler MoveQuestionUp;
        public event MoveQuestionHandler MoveQuestionDown;

        public QuestionType()
        {
            InitializeComponent();
        }

        private void MoveUp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MoveQuestionUp.Invoke(this);
            }
            catch (Exception ex)
            {
                ErrorWindow.Open(ex, Window.GetWindow(this));
            }
        }

        private void MoveQuestionDown_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MoveQuestionDown.Invoke(this);
            }
            catch (Exception ex)
            {
                ErrorWindow.Open(ex, Window.GetWindow(this));
            }
        }
    }
}
