using System.Text;

public class ImageBuilder
{
    public ImageBuilder()
    {
    }

    public string buildSimpleDemo()
    {
        Node startNode = new ();
        startNode.Id = Guid.NewGuid();
        startNode.X = 200;
        startNode.Y = 200;
        startNode.Caption = "This is a long text so that we know what is happening";
        startNode.State = NodeState.Successful;
        startNode.Type = NodeType.Start;

        Node endNode = new ();
        endNode.Id = Guid.NewGuid();
        endNode.X = 600;
        endNode.Y = 200;
        endNode.Caption = "Execution";
        endNode.State = NodeState.Failure;
        endNode.Type = NodeType.Stop;

        List<Node> nodeList = new ();
        nodeList.AddRange(startNode, endNode);

        List<Arc> arcList = new ();
        arcList.Add(new Arc(startNode, endNode));

        return build(nodeList, arcList);
    }

    public string build(List<Node> nodeList, List<Arc> arcList)
    {
        Canvas canvas = new Canvas();

        string style = """
        .startCirle {
            fill: green;
        }
        .startText {
            font: bold 7px sans-serif;  
            fill: white;
            text-anchor: middle;
            cursor: default;
        }
        .stopCirle {
            fill: red;
        }
        .stopText {
            font: bold 7px sans-serif;  
            fill: white;
            text-anchor: middle;
            cursor: default;
        }
        .success {
            fill: green;
        }
        .failure {
            fill: red;
        }
        .information {
            stroke : rgb(213,199,129);
            stroke-width: 1px;
            fill: white;
        }
        .informationText {
            font: bold 11px sans-serif;  		
            fill: gray;
            text-anchor: middle; 
            cursor: pointer; 
        }
        .delete {
            stroke : red;
            stroke-width: 1px;
            fill: red;    	
        }
        .deleteText {
            font: bold 10px sans-serif;  		
            fill: white;
            text-anchor: middle;  
            cursor: pointer;
        }
        .node {
            stroke : rgb(213,199,129);
            stroke-width: 1px;
            fill: white;
        }
        .nodeText {
            font: 12px sans-serif;  		
            fill: gray;
            text-anchor: middle;  
            cursor: pointer;  	
        }
        .nodeSuccess {
            fill: green;
        }
        .nodeColor {
            fill: orange;
            cursor: pointer;
        }
        .arcLine {
            stroke: black;
            stroke-width: 1;
        }
        .arcDot {
        }        
        """;

        canvas.Begin(1024, 768, style);

        foreach(var node in nodeList)
        {
            node.Build(canvas);
        }

        foreach(var arc in arcList)
        {
            arc.Build(canvas);
        }

        canvas.End();

        return canvas.GetString();
    }
}

public enum NodeType
{
    None = 0, 
    Start = 1,
    Stop = 2
}

public enum NodeState
{
    None = 0,
    Successful = 1,
    Failure = 2
}

public class Node 
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public int X { get; set; } = 0;
    public int Y { get; set; } = 0;
    public int Height { get; set; } = 64;
    public int Width { get; set; } = 64;
    public string Caption { get; set; } = string.Empty;
    public NodeType Type { get; set; } = NodeType.None;
    public bool IsDeletable { get; set; } = false;
    public bool HasInformation { get; set; } = true;
    public NodeState State { get; set; } = NodeState.None;

    public void Build(Canvas canvas)
    {
        // Indicator
        if (Type != NodeType.None)
        {
            string className = "startCirle";
            string caption = "START";
            string textClassName = "startText"; 

            if (Type == NodeType.Stop)
            {
                className = "stopCirle";
                caption = "STOP";
                textClassName = "stopText";
            }

            canvas.Circle(X + 32, Y - 24, 18, className);
            canvas.Text(X + 32, Y - 21, caption, textClassName);
        } 

        // Node
        canvas.RoundRectangle(X, Y, Width, Height, 6, "node");
        canvas.RoundRectangle(X + 8, Y + 8, 48, 48, 6, "nodeColor");  
        canvas.Text(X + 32, Y + 78, Caption, "nodeText"); 

        // Information
        canvas.Circle(X + 72, Y, 6, "information");
        canvas.Text(X + 72, Y + 4, "i", "informationText");

        // Delete
        canvas.Circle(X - 8, Y + 64, 6, "delete");
        canvas.Text(X - 8, Y + 68, "X", "deleteText");   

        // State
        if (State != NodeState.None)
        {
            string className = "success";
            if (State == NodeState.Failure)
            {
                className = "failure";
            }
            canvas.Circle(X + 55, Y + 55, 6, className);
        }
    }
}

public class Arc
{
    public Node From { get; set; } = new ();
    public Node To { get; set; } = new ();

    public Arc(Node from, Node to)
    {
        this.From = from;
        this.To = to;
    }

    public void Build(Canvas canvas)
    {
        canvas.Line(From.X + 68, From.Y + 32, To.X - 6, To.Y + 32, "arcLine");
        canvas.Circle(To.X - 6, To.Y + 32, 2, "arcDot");
    }
}