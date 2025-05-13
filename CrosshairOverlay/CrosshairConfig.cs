public class CrosshairConfig
{
    public int Radius { get; set; } = 25;
    public int Thickness { get; set; } = 1;
    public string StrokeColor { get; set; } = "#FF0000";
    public double StrokeOpacity { get; set; } = 1.0;
    public int OutlineRadius { get; set; } = 26;
    public int OutlineThickness { get; set; } = 1;
    public string OutlineColor { get; set; } = "#0000FF";
    public double OutlineOpacity { get; set; } = 1.0;
    public int OutlineOffsetX { get; set; } = 0;
    public int OutlineOffsetY { get; set; } = 0;
    public int UnrestrictedWidth { get; set; } = 0;
    public int UnrestrictedHeight { get; set; } = 0;
    public int UnrestrictedTickness { get; set; } = 0;
    public string UnrestrictedColor { get; set; } = "#00FF00";
    public double UnrestrictedOpacity { get; set; } = 0.5;
    public int UnrestrictedOffsetX { get; set; } = 0;
    public int UnrestrictedOffsetY { get; set; } = 0;
    public bool IsCounterStrafeEnabled { get; set; } = false;
    public int csPressureDuration { get; set; } = 100;
}