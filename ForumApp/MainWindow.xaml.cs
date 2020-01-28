using ForumApp.Services;
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

namespace ForumApp
{
    public partial class MainWindow : Window
    {
        List<Comment> comments = new List<Comment>();
        Comment selectedComment;

        public Comment SelectedComment { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            DisableEditDelete();
            UpdateList();
        }

        private void UpdateList()
        {
            DataAccess db = new DataAccess();
            comments = db.GetAllComments();

            if ((bool)profanityFilterCheckBox.IsChecked)
            {
                EnableProfanityFilter();
            }

            commentsListBox.ItemsSource = comments;
        }

        private void DisableEditDelete()
        {
            deletebutton.IsEnabled = false;
            editTextBox.IsEnabled = false;
            editButton.IsEnabled = false;
        }

        private void EnableEditDelete()
        {
            deletebutton.IsEnabled = true;
            editTextBox.IsEnabled = true;
            editButton.IsEnabled = true;
        }

        private void EnableProfanityFilter()
        {
            foreach (var c in comments)
            {
                c.ProfanityFilter = true;
            }
        }

        private void DisableProfanityFilter()
        {
            foreach (var c in comments)
            {
                c.ProfanityFilter = false;
            }
        }

        private void PostCommentButton_Click(object sender, RoutedEventArgs e)
        {
            DataAccess db = new DataAccess();

            if (!String.IsNullOrEmpty(commentTextBox.Text))
            {
                ProfanityFilterAPI filter = new ProfanityFilterAPI();

                string censored = filter.GetCensoredString(commentTextBox.Text);

                db.InsertComment(commentTextBox.Text, censored);
                commentTextBox.Text = "";
                UpdateList();
            }
        }

        private void CommentsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (commentsListBox.SelectedItem != null)
            {
                selectedComment = commentsListBox.SelectedItem as Comment;
                EnableEditDelete();
                editTextBox.Text = selectedComment.Text;
            }
        }

        private void Deletebutton_Click(object sender, RoutedEventArgs e)
        {
            DataAccess db = new DataAccess();

            MessageBoxResult result = MessageBox.Show("Delete comment?", "Delete", MessageBoxButton.OKCancel);
            
            if (result == MessageBoxResult.OK)
            {
                db.DeleteComment(selectedComment.Id);
                DisableEditDelete();
                editTextBox.Text = "";
                UpdateList(); 
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            DataAccess db = new DataAccess();
            MessageBoxResult result = MessageBox.Show("Edit comment?", "Edit", MessageBoxButton.OKCancel);

            if (result == MessageBoxResult.OK)
            {
                ProfanityFilterAPI filter = new ProfanityFilterAPI();

                string censored = filter.GetCensoredString(editTextBox.Text);

                db.UpdateComment(selectedComment.Id, editTextBox.Text, censored);
                DisableEditDelete();
                editTextBox.Text = "";
                UpdateList();
            }
        }

        private void ProfanityFilterCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            EnableProfanityFilter();
            commentsListBox.Items.Refresh();
        }

        private void ProfanityFilterCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            DisableProfanityFilter();
            commentsListBox.Items.Refresh();
        }

        private void CommentTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (commentTextBox.Text.Length > 140)
            {
                postCommentButton.IsEnabled = false;
                commentTextBox.Background = new SolidColorBrush(Color.FromRgb(255, 200, 200));
            }
            else
            {
                if (!postCommentButton.IsEnabled)
                {
                    postCommentButton.IsEnabled = true;
                }
                commentTextBox.Background = Brushes.White;
            }
        }

        private void EditTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (editTextBox.Text.Length > 140)
            {
                editButton.IsEnabled = false;
                editTextBox.Background = new SolidColorBrush(Color.FromRgb(255, 200, 200));
            }
            else
            {
                if (!editButton.IsEnabled)
                {
                    editButton.IsEnabled = true;
                }
                editTextBox.Background = Brushes.White;
            }
        }
    }
}
