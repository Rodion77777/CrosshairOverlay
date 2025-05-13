public class CrosshairConfig
{
    public double Radius { get; set; } = 25;
    public double Thickness { get; set; } = 1;
    public string StrokeColor { get; set; } = "#FF0000";
    public double StrokeOpacity { get; set; } = 1.0;
    public double OutlineRadius { get; set; } = 26;
    public double OutlineThickness { get; set; } = 1;
    public string OutlineColor { get; set; } = "#0000FF";
    public double OutlineOpacity { get; set; } = 1.0;
    public double OutlineOffsetX { get; set; } = 0;
    public double OutlineOffsetY { get; set; } = 0;
    public double UnrestrictedWidth { get; set; } = 0;
    public double UnrestrictedHeight { get; set; } = 0;
    public double UnrestrictedTickness { get; set; } = 0;
    public string UnrestrictedColor { get; set; } = "#00FF00";
    public double UnrestrictedOpacity { get; set; } = 0.5;
    public double UnrestrictedOffsetX { get; set; } = 0;
    public double UnrestrictedOffsetY { get; set; } = 0;
    public bool IsCounterStrafeEnabled { get; set; } = false;
    public int csPressureDuration { get; set; } = 100;
}