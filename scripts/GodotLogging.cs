using Godot;
interface GodotLogging 
{
    
    static void log(Node node, string message)
    {

        GD.Print(node.ToString() + ": " + node.Name + ":        " + message);
    }
}