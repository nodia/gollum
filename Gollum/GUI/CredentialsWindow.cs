using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aidon.Tools.Gollum.GUI
{
    public sealed partial class CredentialsWindow : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CredentialsWindow" /> class.
        /// </summary>
        /// <param name="title">The title.</param>
        public CredentialsWindow(string title)
        {
            InitializeComponent();

            Text = title;
        }

        public Credentials GetCredentials()
        {
            return new Credentials { Password = textBoxPassword.Text, Username = textBoxUserName.Text };
        }
    }
}
