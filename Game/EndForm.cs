using System.Drawing;
using System.Windows.Forms;

namespace Game
{
    public partial class EndForm : Form
    {
        public EndForm()
        {
            InitializeComponent();
            var label = new Label
            {
                Location = new Point(ClientSize.Width / 3, 0),
                Size = new Size(ClientSize.Width / 2, 30),
                Text = "Поздравляю. Вы выйграли!!!"
            };
            var button = new Button
            {
                Location = new Point(15, 35),
                Size = new Size(ClientSize.Width - 30, 30),
                Text = "Начать заного"
                
            };
            var button2 = new Button
            {
                Location = new Point(15, 70),
                Size = new Size(ClientSize.Width - 30, 30),
                Text = "Выйти из игры"
                
            };
            Controls.Add(label);
            Controls.Add(button);
            Controls.Add(button2);
            button.Click += (sender, args) =>
            {
                var f = new Form1();
                f.Show();
                f.Activate();
                Hide();
            };
            button2.Click += (sender, args) => Application.Exit();
        }
    }
}