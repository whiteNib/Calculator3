using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public class RoundedPanel : Panel
{
    private int cornerRadius = 5;

    public int CornerRadius
    {
        get { return cornerRadius; }
        set
        {
            cornerRadius = value;
            UpdateRegion();
        }
    }

    public RoundedPanel()
    {
        UpdateRegion();
    }

    private void UpdateRegion()
    {
        using (GraphicsPath path = CreateRoundPath(ClientRectangle, cornerRadius))
        {
            Region = new Region(path);
        }
    }

    private static GraphicsPath CreateRoundPath(Rectangle rectangle, int radius)
    {
        GraphicsPath path = new GraphicsPath();

        int diameter = radius * 2;
        Rectangle arc = new Rectangle(rectangle.Location, new Size(diameter, diameter));

        // 왼쪽 위 모서리
        path.AddArc(arc, 180, 90);

        // 오른쪽 위 모서리
        arc.X = rectangle.Right - diameter;
        path.AddArc(arc, 270, 90);

        // 오른쪽 아래 모서리
        arc.Y = rectangle.Bottom - diameter;
        path.AddArc(arc, 0, 90);

        // 왼쪽 아래 모서리
        arc.X = rectangle.Left;
        path.AddArc(arc, 90, 90);

        path.CloseFigure();

        return path;
    }
}
