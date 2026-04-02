namespace ScreenSaver;

public partial class Form1 : Form
{
    int speedX = 3;
    int speedY = 3;
    public Form1()
    {
        InitializeComponent();
        ScreenTimer.Start();
        WindowState = FormWindowState.Maximized;
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
    }

    private void Form1_KeyPress(object sender, KeyPressEventArgs e)
    {
        Close();
    }
}
