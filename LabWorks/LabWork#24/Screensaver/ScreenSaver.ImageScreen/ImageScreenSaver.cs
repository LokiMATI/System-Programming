namespace ScreenSaver.ImageScreen
{
    public partial class ImageScreenSaver : Form
    {
        private Point _startPosition = MousePosition;
        int currentImage = 0;
        FileInfo[] images;
        public ImageScreenSaver()
        {
            InitializeComponent();
            DirectoryInfo directory = new(@"C:\Users\221\Pictures");
            images = directory.GetFiles("*.jpg");

            LoadImage();
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            Close();
        }

        private void LoadImage()
        {
            if (images.Length > 0)
            {
                pictureBox1.Image = Image.FromFile(images[currentImage++].FullName);

                if (currentImage > images.Length - 1)
                    currentImage = 0;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            LoadImage();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            var radius = 10;

            if (e.X > _startPosition.X + radius || e.X < _startPosition.X - radius
                || e.Y > _startPosition.Y + radius || e.Y < _startPosition.Y - radius)
                Close();
        }
    }
}
