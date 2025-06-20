public class CrosshairConfig
{
    public int Radius { get; set; } = 49;
    public int Thickness { get; set; } = 1;
    public string StrokeColor { get; set; } = "#FF0000";
    public double StrokeOpacity { get; set; } = 1.0;
    public int OutlineRadius { get; set; } = 52;
    public int OutlineThickness { get; set; } = 1;
    public string OutlineColor { get; set; } = "#0000FF";
    public double OutlineOpacity { get; set; } = 1.0;
    public int OutlineOffsetX { get; set; } = 0;
    public int OutlineOffsetY { get; set; } = 0;
    public int UnrestrictedWidth { get; set; } = 0;
    public int UnrestrictedHeight { get; set; } = 0;
    public int UnrestrictedTickness { get; set; } = 48;
    public string UnrestrictedColor { get; set; } = "#FFFFFF";
    public double UnrestrictedOpacity { get; set; } = 0.1;
    public int UnrestrictedOffsetX { get; set; } = 0;
    public int UnrestrictedOffsetY { get; set; } = 0;
    public int FilterWidth { get; set; } = 0;
    public int FilterHeight { get; set; } = 0;
    public string FilterColor { get; set; } = "#FFFFFF";
    public double FilterOpacity { get; set; } = 0.1;
    public bool IsCounterStrafeEnabled { get; set; } = false;
    public int csPressureDuration { get; set; } = 100;
}