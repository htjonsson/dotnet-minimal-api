using System.Text;

public class Canvas
{
    private StringBuilder _stringBuilder;

    public Canvas()
    {
        _stringBuilder = new StringBuilder();
    }

    public byte[] GetBytes()
    {
        return Encoding.UTF8.GetBytes(_stringBuilder.ToString());
    }

    public void Begin(int width, int height, string style)
    {
        _stringBuilder.Clear();

        _stringBuilder.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"no\"?>");
        _stringBuilder.AppendLine("<!DOCTYPE svg PUBLIC \"-//W3C//DTD SVG 1.1//EN\" \"http://www.w3.org/Graphics/SVG/1.1/DTD/svg11.dtd\">");
        _stringBuilder.AppendLine($"<svg width=\"{width}\" height=\"{height}\" xmlns=\"http://www.w3.org/2000/svg\" xmlns:xlink=\"http://www.w3.org/1999/xlink\">");  
    
        _stringBuilder.AppendLine("<style>");
        _stringBuilder.AppendLine(style);
        _stringBuilder.AppendLine("</style>");
    }

    public void End()
    {
        _stringBuilder.AppendLine("</svg>");
    }

    public string DefaultStyle()
    {
        return """
        .backgroundColor {
            fill : white;
        }
        .rect {
            fill : white;
            stroke : #FFFFFF;
            stroke-width : 1;
        }
        .caption {
            font: italic 10px sans-serif;  		
        }
        .buttonHigh {
            stroke : lightgray;
        }
        .buttonLow {
            stroke : gray;
        }
        .textMiddle {
            text-anchor: middle;
        }
        .arcLine {
            stroke: black;
            stroke-width: 1;
        }
        """;        
    }

    public void Rectangle(int x, int y, int width, int height, string className)
    {
        _stringBuilder.AppendLine($"<rect x=\"{x}\" y=\"{y}\" width=\"{width}\" height=\"{height}\" class=\"{className}\"/>");
    }

    public void Line(int x1, int y1, int x2, int y2, string className)
    {
        _stringBuilder.AppendLine($"<line x1=\"{x1}\" y1=\"{y1}\" x2=\"{x2}\" y2=\"{y2}\" class=\"{className}\"/>");
    }

    public void Text(int x, int y, string text, string className)
    {
       _stringBuilder.AppendLine($"<text x=\"{x}\" y=\"{y}\" class=\"{className}\">{text}</text>");
    }

    public void Comment(string comment)
    {
        _stringBuilder.AppendLine($"<!-- {comment} -->");
    }

    public void Separator(int x, int y, int width, string topShadow, string bottomShadow)
    {
        Comment("Separator");
        Line(x, y, width + x, y, bottomShadow);
        Line(x, 1 + y, width + x, 1 + y, topShadow);    
    }

    public void Frame(int x, int y, int width, int height, string background, string topShadow, string bottomShadow)
    {
        Comment("Frame");
        // Background
        Rectangle(x, y, width, height, background);
        // Top
        Line(x, y, width + x, y, topShadow);
        // Left
        Line(x, y, x, height + y, topShadow);
        // Right
        Line(width + x, y, width + x, height + y, bottomShadow);
        // Bottom
        Line(x, height + y, width + x, height + y, bottomShadow);    
    }

    public void Box(int x, int y, int width, int height, string className1, string className2)
    {
        // Top
        Line(x, y, x+width, y, className1);
        Line(x, y+1, x+width-1, y+1, className1);

        // Left
        Line(x, y, x, y+height, className1);
        Line(x+1, y, x+1, y+height-1, className1);

        // Right
        Line(x+width, y, x+width, y+height, className2);
        Line(x+width-1, y, x+width-1, y+height-1, className2);

        // Bottom
        Line(x+1, y+height-1, x+width-1, y+height-1, className2);
        Line(x, y+height, x+width, y+height, className2);
    }
}

/*

Begin(1024, 768);

SolidRectangle(4, 117, 22, 22);
SolidRectangle(4, 117, 108, 24);
Text("ListProcessor");

SolidRectangle(158, 117, 22, 22);
SolidRectangle(181, 117, 36, 22);
Text("form");

240, 26
240, 130
scrolledWindow

385, 26
list

264, 26


264, 130

360, 52
selectAllButton

360, 78
unselectAllButton

360, 104
selectButton

360, 130
unselectButton

360, 156
applyButton

360, 182
dismissButton

360, 208
resetButton

*/

