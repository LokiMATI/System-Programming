using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace LabWork25;

class CustomButton : FrameworkElement
{
    public static readonly RoutedEvent ClickEvent =
    EventManager.RegisterRoutedEvent(
        "Click",
        RoutingStrategy.Bubble,
        typeof(RoutedEventHandler),
        typeof(CustomButton));

    public event RoutedEventHandler Click
    {
        add { AddHandler(ClickEvent, value); }
        remove { RemoveHandler(ClickEvent, value); }
    }

    public string Text { get; set; } = "Button";
    public double FontSize { get; set; } = 20;
    public Color FontColor { get; set; } = Colors.White;
    public Color ShadowColor { get; set; } = Color.FromArgb(50, 0, 0, 0);
    public Color PressedColor { get; set; } = Color.FromRgb(255, 152, 233);
    public Color HoveredColor { get; set; } = Color.FromRgb(152, 153, 255);
    public Color NonHorevedColor { get; set; } = Colors.DodgerBlue;

    private VisualCollection _children;
    private DrawingVisual _visual;
    private bool _isHovered;
    private bool _isPressed;

    private double _scale = 1.0;
    private double _targetScale = 1.0;

    protected override int VisualChildrenCount => _children.Count;
    protected override Visual GetVisualChild(int index) => _children[index];

    public CustomButton()
    {
        _children = new VisualCollection(this);
        _visual = new DrawingVisual();
        _children.Add(_visual);

        CompositionTarget.Rendering += OnRenderFrame;
        MouseEnter += OnMouseEnter;
        MouseLeave += OnMouseLeave;
        MouseDown += OnMouseDown;
        MouseUp += OnMouseUp;
    }

    private void OnMouseUp(object sender, MouseButtonEventArgs e)
    {
        _targetScale = _isHovered ? 1.1 : 1.0;
        _isPressed = false;
        RaiseEvent(new RoutedEventArgs(ClickEvent, this));
    }

    private void OnMouseDown(object sender, MouseButtonEventArgs e)
    {
        _targetScale = 0.95;
        _isPressed = true;
    }

    private void OnMouseLeave(object sender, MouseEventArgs e)
    {
        _targetScale = 1.0;
        _isHovered = false;
        _isPressed = false;
    }

    private void OnMouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
    {
        _targetScale = 1.1;
        _isHovered = true;
    }

    private void OnRenderFrame(object? sender, EventArgs e)
    {
        _scale += (_targetScale - _scale) * 0.15;
        Draw();
    }

    private void Draw()
    {
        using var dc = _visual.RenderOpen();

        dc.PushTransform(new ScaleTransform(_scale, _scale, Width / 2, Height / 2));

        dc.DrawRoundedRectangle(
            new SolidColorBrush(ShadowColor),
            null,
            new Rect(5, 5, Width, Height),
            10, 10);


        dc.DrawRoundedRectangle(
            new SolidColorBrush(_isPressed ? PressedColor : _isHovered ? HoveredColor : NonHorevedColor),
            null,
            new Rect(0, 0, Width, Height),
            10, 10);

        var text = new FormattedText(
            Text,
            System.Globalization.CultureInfo.CurrentCulture,
            FlowDirection.LeftToRight,
            new Typeface("Segoe UI"),
            FontSize,
            new SolidColorBrush(FontColor),
            1.25);

        dc.DrawText(text, new Point(Width / 2 - text.Width / 2, Height / 2 - text.Height / 2));

        dc.Pop();
    }
}
