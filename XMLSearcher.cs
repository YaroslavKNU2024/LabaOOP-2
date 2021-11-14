using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Xsl;

namespace LabOOP2WinForms
{
    public partial class XMLSearcher : Form
    { 
        public List<Library> final = new List<Library>();
        private string _path = "Library.xml";
        private OutputTextBox output = null;
        XMLProcessor CurrentStrategy = null;

        public XMLSearcher()
        {
            InitializeComponent();
            output = new OutputTextBox(this);
            output.Show();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);
        }

        private void Search_Click(object sender, EventArgs e)
        {
            final = new List<Library>();
            Library _problem = OurLibrary();
            try
            {
                if (LINQButton.Checked || DOMButton.Checked || SAXButton.Checked)
                {
                    if (LINQButton.Checked)
                        CurrentStrategy = new LINQtoXML(_path);
                    else if (DOMButton.Checked)
                        CurrentStrategy = new DOMModel(_path);
                    else
                        CurrentStrategy = new SAXSearch(_path);
                    final = CurrentStrategy.Algorithm(_problem, _path);
                    PrintOutput(final);
                }
            }
            catch (System.IO.FileNotFoundException)
            {
                MessageBox.Show("НЕМАЄ ФАЙЛУ! ПЕРЕВІРТЕ ПРАВИЛЬНІСТЬ ВВОДУ!!!");
            }
        }

        private Library OurLibrary()
        {
            string[] information = new string[7];
            if (AuthorName.Checked) information[0] = Convert.ToString(AuthorNameBox.Text);
            if (BookName.Checked) information[1] = Convert.ToString(BookNameBox.Text);
            if (Anotation.Checked) information[2] = Convert.ToString(AnotationBox.Text);
            if (QualificationProperties.Checked) information[3] = Convert.ToString(QualificationBox.Text);
            if (Faculty.Checked) information[4] = Convert.ToString(FacultyBox.Text);
            if (Department.Checked) information[5] = Convert.ToString(DepartmentBox.Text);
            if (Position.Checked) information[6] = Convert.ToString(PositionBox.Text);
            return new Library(information);
        }

        private void PrintOutput(List<Library> final) => output.TESTFUNTION();

        private void Transform_Click(object sender, EventArgs e)
        {
            XslCompiledTransform xslt = new XslCompiledTransform();
            xslt.Load(@"C:\Users\yasma\source\repos\LabOOP2WinForms\bin\Debug\net5.0-windows\stylesheet.xsl");
            string input = _path;
            string result = @"C:\Users\yasma\source\repos\LabOOP2WinForms\bin\Debug\net5.0-windows\Root.html";
            xslt.Transform(input, result);
            MessageBox.Show("ПЕРЕТВОРЕНО ВДАЛО!");
        }

        private void FileLoader_Click(object sender, EventArgs e)
        {
            string Name = InputField.Text;
            Status.Text = Name;
            _path = Name;
            FileName.Text = "Current file " + Name;
        }
    }
}
