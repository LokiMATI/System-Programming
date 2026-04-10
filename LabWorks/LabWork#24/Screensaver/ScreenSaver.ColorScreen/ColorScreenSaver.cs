namespace ScreenSaver.ColorScreen
{
    public partial class ColorScreenSaver : Form
    {
        private Point _startPosition = MousePosition;
        public ColorScreenSaver()
        {
            InitializeComponent();
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            Close();
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            var radius = 10;

            if (e.X > _startPosition.X + radius || e.X < _startPosition.X - radius
                || e.Y > _startPosition.Y + radius || e.Y < _startPosition.Y - radius)
                Close();
        }
    }
}
