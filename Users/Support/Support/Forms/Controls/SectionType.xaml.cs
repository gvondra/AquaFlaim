using AquaFlaim.Interface.Forms.Models;
using AquaFlaim.User.Support.Forms.ViewModel;
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

        private void AddQuestion_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DataContext != null)
                {
                    SectionTypeVM sectionType = (SectionTypeVM)DataContext;
                    FormQuestionType questionType = new FormQuestionType()
                    {
                        Code = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString(),
                        Text = "New Question?"
                    };
                    sectionType.InnerSectionType.Questions.Add(questionType);
                    sectionType.Questions.Add(new QuestionTypeVM(questionType, sectionType));
                }

            }
            catch (Exception ex)
            {
                ErrorWindow.Open(ex, Window.GetWindow(this));
            }
        }

        private void QuestionType_MoveQuestionUp(object sender)
        {
            try
            {
                if (DataContext != null)
                {
                    SectionTypeVM sectionType = (SectionTypeVM)DataContext;
                    QuestionTypeVM questionType = (QuestionTypeVM)((QuestionType)sender).DataContext;
                    int currentIndex = sectionType.Questions.IndexOf(questionType);
                    if (currentIndex > 0)
                    {
                        sectionType.Questions.RemoveAt(currentIndex);
                        sectionType.Questions.Insert(currentIndex - 1, questionType);
                        sectionType.InnerSectionType.Questions.RemoveAt(currentIndex);
                        sectionType.InnerSectionType.Questions.Insert(currentIndex - 1, questionType.InnerQuestionType);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorWindow.Open(ex, Window.GetWindow(this));
            }
        }

        private void QuestionType_MoveQuestionDown(object sender)
        {
            try
            {
                if (DataContext != null)
                {
                    SectionTypeVM sectionType = (SectionTypeVM)DataContext;
                    QuestionTypeVM questionType = (QuestionTypeVM)((QuestionType)sender).DataContext;
                    int currentIndex = sectionType.Questions.IndexOf(questionType);
                    if (currentIndex < sectionType.Questions.Count - 1)
                    {
                        sectionType.Questions.RemoveAt(currentIndex);
                        sectionType.Questions.Insert(currentIndex + 1, questionType);
                        sectionType.InnerSectionType.Questions.RemoveAt(currentIndex);
                        sectionType.InnerSectionType.Questions.Insert(currentIndex + 1, questionType.InnerQuestionType);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorWindow.Open(ex, Window.GetWindow(this));
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                QuestionTypeVM questionType = (QuestionTypeVM)((MenuItem)sender).CommandParameter;
                SectionTypeVM targetSection = (SectionTypeVM)((MenuItem)sender).DataContext;
                questionType.SectionType = targetSection;
            }
            catch (Exception ex)
            {
                ErrorWindow.Open(ex, Window.GetWindow(this));
            }
        }
    }
}
