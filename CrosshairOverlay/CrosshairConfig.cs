public class CrosshairConfig
{
    public double Radius { get; set; } = 25;
    public double OutlineRadius { get; set; } = 26;
    public double Thickness { get; set; } = 1;
    public double OutlineThickness { get; set; } = 1;
    public string StrokeColor { get; set; } = "#FF0000";
    public string OutlineColor { get; set; } = "#0000FF";
    public double StrokeOpacity { get; set; } = 1.0;
    public double OutlineOpacity { get; set; } = 1.0;
    public bool IsCounterStrafeEnabled { get; set; } = false;
    public int csPressureDuration { get; set; } = 100;
}