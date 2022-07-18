using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library
{
    public partial class MainForm : Form
    {
        private Classes.Library library = new Classes.Library(@"\excel.xlsx")
        {
            MaxDays = 7,
            MaxBooks = 3,
        };
        private Classes.User user;
        private List<Classes.Book> books;

        public MainForm()
        {
            InitializeComponent();

            this.user = null;
            this.books = new List<Classes.Book>();

            this.book_data1.Library = this.library;
            this.book_data2.Library = this.library;
            this.book_data3.Library = this.library;
            this.user_data.Library = this.library;
        }

        private void ChangeBookBarcode(object sender, EventArgs e)
        {
            switch (this.books.Count)
            {
                case 0: 
                    this.book_data1.FindBook((sender as RichTextBox).Text);
                    break;
                case 1: 
                    this.book_data2.FindBook((sender as RichTextBox).Text);
                    break;
                case 2:
                    this.book_data3.FindBook((sender as RichTextBox).Text);
                    break;
                default: 
                    break;
            }

            Classes.Book find = this.library.FindBook((sender as RichTextBox).Text);

            if (find != null)
            {
                (sender as RichTextBox).Text = "";

                this.books.Add(find);
                this.book_tab.SelectedTab = this.book_tab.TabPages[this.books.Count - 1];

                this.book_barcode.Focus();
            }
        }

        private void ChangeUserBarcode(object sender, EventArgs e)
        {
            this.user_data.FindUser((sender as RichTextBox).Text);

            Classes.User find = this.library.FindUser((sender as RichTextBox).Text);

            if (find != null)
            {
                (sender as RichTextBox).Text = "";

                this.user = find;

                this.user_barcode.Focus();
            }
        }

        private void ClickAddDay(object sender, EventArgs e)
        {
            this.library.AddDay();

            if (this.user != null)
            {
                this.user_data.FindUser(this.user.Barcode);
            }

            if (this.books.Count > 0)
            {
                this.book_data1.FindBook(this.books[0].Barcode);
            }
            if (this.books.Count > 1)
            {
                this.book_data2.FindBook(this.books[1].Barcode);
            }
            if (this.books.Count > 2)
            {
                this.book_data3.FindBook(this.books[2].Barcode);
            }
        }

        private void ClickBorrow(object sender, EventArgs e)
        {
            for (int i = 0; i < this.books.Count; i++)
            {
                if (!this.library.Borrow(this.user, this.books[i]))
                {
                    MessageBox.Show("대출에 실패했습니다.");

                    return;
                }
            }

            this.user_data.FindUser(this.user.Barcode); 

            if (this.books.Count > 0)
            {
                this.book_data1.FindBook(this.books[0].Barcode);
            }
            if (this.books.Count > 1)
            {
                this.book_data2.FindBook(this.books[1].Barcode);
            }
            if (this.books.Count > 2)
            {
                this.book_data3.FindBook(this.books[2].Barcode);
            }

            this.book_data1.Text = "";
            this.book_data2.Text = "";
            this.book_data3.Text = "";

            this.books = new List<Classes.Book>();
        }

        private void ClickGiveBack(object sender, EventArgs e)
        {
            for (int i = 0; i < this.books.Count; i++)
            {
                this.library.GiveBack(this.user, this.books[i]);
            }

            this.user_data.FindUser(this.user.Barcode);

            if (this.books.Count > 0)
            {
                this.book_data1.FindBook(this.books[0].Barcode);
            }
            if (this.books.Count > 1)
            {
                this.book_data2.FindBook(this.books[1].Barcode);
            }
            if (this.books.Count > 2)
            {
                this.book_data3.FindBook(this.books[2].Barcode);
            }

            this.book_data1.Text = "";
            this.book_data2.Text = "";
            this.book_data3.Text = "";

            this.books = new List<Classes.Book>();
        }

        private void ClickSave(object sender, EventArgs e)
        {
            this.library.SaveData();
        }

        private void ClickResetBook(object sender, EventArgs e)
        {
            this.book_data1.Text = "";
            this.book_data2.Text = "";
            this.book_data3.Text = "";

            this.books = new List<Classes.Book>();
        }
    }
}
