internal sealed class CreateImage
{
    public byte[] Handle()
    {
        string style = """
        .base {
            fill : rgb(213,199,133);
        }
        .background {
            fill : rgb(189,198,184);
        }
        .topShadow {
            stroke : rgb(227,231,224);
        }
        .bottomShadow {
            stroke : rgb(102,106,98);
        }
        .textBackground {
            fill : rgb(248,248,255);
        }
        .textTopShadow {
            stroke : rgb(149,149,153);
        }
        .textBottomShadow {
            stroke : rgb(223,223,229);
        }
        """;

        Canvas canvas = new Canvas();

        canvas.Begin(1024, 768, style);

        canvas.Rectangle(0, 0, 1024, 768, "base");

        // Frame
        canvas.Frame(6, 25, 547, 415, "background", "topShadow", "bottomShadow");

        // Menubar
        canvas.Frame(11, 30, 537, 29, "background", "topShadow", "bottomShadow");

        // Textfield
        canvas.Frame(129, 65, 418, 25, "textBackground", "textTopShadow", "textBottomShadow");

        // Textarea
        canvas.Frame(11, 96, 537, 304, "textBackground", "textTopShadow", "textBottomShadow");

        // Separator
        canvas.Separator(11, 405, 537, "topShadow", "bottomShadow");

        // Button - Ok
        canvas.Frame(68, 413, 59, 21, "background", "topShadow", "bottomShadow");

        // Button - Save
        canvas.Frame(189, 413, 59, 21, "background", "topShadow", "bottomShadow");

        // Button - Include
        canvas.Frame(311, 413, 59, 21, "background", "topShadow", "bottomShadow");

        // Button - Save
        canvas.Frame(432, 413, 59, 21, "background", "topShadow", "bottomShadow");        
    
        canvas.End();

        return canvas.GetBytes();
    }   
}