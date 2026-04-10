namespace ScreenSaver;

public partial class TimerScreenSaver : Form
{
    int speedX = 3;
    int speedY = 3;
    private Point _startPosition = MousePosition;
    public TimerScreenSaver()
    {
        InitializeComponent();
        ScreenTimer.Start();
    }

    private void ScreenTimer_Tick(object sender, EventArgs e)
    {
        var loc = JumpingLabel.Location;

        if (JumpingLabel.Bounds.Right > Width
            || JumpingLabel.Bounds.Left < 0)
            speedX = -speedX;

        if (JumpingLabel.Bounds.Bottom > Height
            || JumpingLabel.Bounds.Top < 0)
            speedY = -speedY;

        loc = JumpingLabel.Location = new Point(loc.X + speedX, loc.Y + speedY);

        JumpingLabel.Location = loc;
        JumpingLabel.Text = DateTime.Now.ToString("HH:mm:ss");
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

    private void Form1_Load(object sender, EventArgs e)
    {
        JumpingLabel.Location = new Point(Width / 2 - JumpingLabel.Width / 2, Height / 2 + JumpingLabel.Height / 2);
    }
}
